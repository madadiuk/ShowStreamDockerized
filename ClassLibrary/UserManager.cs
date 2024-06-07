using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public class UserManager
{
    private static Dictionary<int, string> passwordResetTokens = new Dictionary<int, string>();
    private static Dictionary<int, List<string>> userActivities = new Dictionary<int, List<string>>();

    public void AddUser(User user)
    {
        ValidateUser(user);

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Username", user.Username);
            db.AddParameter("@Email", user.Email);
            db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
            db.AddParameter("@Role", user.Role);
            db.Execute("spAddUser");

            if (db.Count == 0)
            {
                throw new Exception("No rows affected.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding user to the database: " + ex.Message);
        }
    }

    private void ValidateUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new Exception("Username cannot be empty.");
        }

        if (user.Username.Length < 2)
        {
            throw new Exception("Username must be at least 2 characters long.");
        }

        if (user.Username.Length > 255)
        {
            throw new Exception("Username cannot be more than 255 characters long.");
        }

        if (!IsValidEmail(user.Email, out string emailError))
        {
            throw new Exception(emailError);
        }
    }

    private bool IsValidEmail(string email, out string error)
    {
        error = null;

        if (email.Length > 255)
        {
            error = "Email cannot be more than 255 characters long.";
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            if (addr.Address != email)
            {
                error = "Invalid email format.";
                return false;
            }
        }
        catch
        {
            error = "Invalid email format.";
            return false;
        }

        return true;
    }

    public void DeleteUser(int userId)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.Execute("spDeleteUser");
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting user from the database: " + ex.Message);
        }
    }


    public void UpdateUser(User user)
    {
        ValidateUser(user);

        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", user.UserID);
            db.AddParameter("@Username", user.Username);
            db.AddParameter("@Email", user.Email);
            db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
            db.AddParameter("@Role", user.Role);
            db.Execute("spUpdateUser");
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating user in the database: " + ex.Message);
        }
    }

    public List<User> GetAllUsers()
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.Execute("spGetAllUsers");
            List<User> users = new List<User>();

            foreach (DataRow row in db.DataTable.Rows)
            {
                users.Add(new User
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Role = row["Role"].ToString()
                });
            }
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving users from the database: " + ex.Message);
        }
    }

    public User GetUserById(int userId)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.Execute("spGetUserById");
            if (db.Count == 1)
            {
                DataRow row = db.DataTable.Rows[0];
                return new User
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Role = row["Role"].ToString()
                };
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving user by ID from the database: " + ex.Message);
        }
    }

    public User GetUserByUsername(string username)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Username", username);
            db.Execute("spGetUserByUsername");
            if (db.Count == 1)
            {
                DataRow row = db.DataTable.Rows[0];
                return new User
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString(),
                    Role = row["Role"].ToString()
                };
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving user by username from the database: " + ex.Message);
        }
    }

    public List<User> SearchUsers(string username = null, string email = null, string role = null)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            if (!string.IsNullOrEmpty(username))
                db.AddParameter("@Username", username);
            if (!string.IsNullOrEmpty(email))
                db.AddParameter("@Email", email);
            if (!string.IsNullOrEmpty(role))
                db.AddParameter("@Role", role);

            db.Execute("spSearchUsers");

            List<User> users = new List<User>();
            foreach (DataRow row in db.DataTable.Rows)
            {
                users.Add(new User
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    Role = row["Role"].ToString()
                });
            }
            return users;
        }
        catch (Exception ex)
        {
            throw new Exception("Error searching users from the database: " + ex.Message);
        }
    }

    public string GeneratePasswordResetToken(int userId)
    {
        try
        {
            string token = Guid.NewGuid().ToString();
            passwordResetTokens[userId] = token;
            return token;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating password reset token: " + ex.Message);
        }
    }

    public bool ResetPassword(string token, string newPassword)
    {
        try
        {
            int userId = passwordResetTokens.FirstOrDefault(kvp => kvp.Value == token).Key;
            if (userId == 0)
            {
                return false;
            }

            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.AddParameter("@Password", PasswordHelper.HashPassword(newPassword));
            db.Execute("spResetPassword");
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error resetting password: " + ex.Message);
        }
    }

    public void LogUserActivity(int userId, string activity)
    {
        try
        {
            if (!userActivities.ContainsKey(userId))
            {
                userActivities[userId] = new List<string>();
            }

            userActivities[userId].Add($"{DateTime.Now}: {activity}");
        }
        catch (Exception ex)
        {
            throw new Exception("Error logging user activity: " + ex.Message);
        }
    }

    public User AuthenticateUser(string username, string password)
    {
        try
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Username", username);
            db.Execute("spGetUserByUsername");

            if (db.Count == 1)
            {
                DataRow row = db.DataTable.Rows[0];
                string storedPassword = row["Password"].ToString();

                if (PasswordHelper.VerifyPassword(password, storedPassword))
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"].ToString(),
                        Role = row["Role"].ToString()
                    };
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error authenticating user: " + ex.Message);
        }
    }
}
