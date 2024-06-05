using ClassLibrary;
using System.Collections.Generic;
using System.Data;
using System;

public class UserManager
{
    private clsDataConnection db;

    public void AddUser(User user)
    {
        db = new clsDataConnection();
        db.AddParameter("@Username", user.Username);
        db.AddParameter("@Email", user.Email);
        db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
        db.AddParameter("@Role", user.Role);
        db.Execute("spAddUser");
    }

    public void DeleteUser(int userId)
    {
        db = new clsDataConnection();
        db.AddParameter("@UserID", userId);
        db.Execute("spDeleteUser");
    }

    public void UpdateUser(User user)
    {
        db = new clsDataConnection();
        db.AddParameter("@UserID", user.UserID);
        db.AddParameter("@Username", user.Username);
        db.AddParameter("@Email", user.Email);
        db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
        db.AddParameter("@Role", user.Role);
        db.Execute("spUpdateUser");
    }

    public List<User> GetAllUsers()
    {
        db = new clsDataConnection();
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

    public User GetUserById(int userId)
    {
        db = new clsDataConnection();
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
                Role = row["Role"].ToString()
            };
        }
        return null;
    }

    public List<User> FilterUsersByRole(string role)
    {
        db = new clsDataConnection();
        db.AddParameter("@Role", role);
        db.Execute("spFilterUsersByRole");
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

    public string GeneratePasswordResetToken(int userId)
    {
        db = new clsDataConnection();
        string token = Guid.NewGuid().ToString();
        db.AddParameter("@UserID", userId);
        db.AddParameter("@ResetToken", token);
        db.Execute("spAddPasswordResetToken");
        return token;
    }

    public bool ResetPassword(string token, string newPassword)
    {
        db = new clsDataConnection();
        db.AddParameter("@ResetToken", token);
        db.Execute("spGetUserIDByResetToken");
        if (db.Count == 1)
        {
            int userId = Convert.ToInt32(db.DataTable.Rows[0]["UserID"]);
            db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.AddParameter("@Password", PasswordHelper.HashPassword(newPassword));
            db.Execute("spResetPassword");
            return true;
        }
        return false;
    }

    public void LogUserActivity(int userId, string activity)
    {
        db = new clsDataConnection();
        db.AddParameter("@UserID", userId);
        db.AddParameter("@Activity", activity);
        db.AddParameter("@ActivityDate", DateTime.Now);
        db.Execute("spLogUserActivity");
    }

    // Add the AuthenticateUser method
    public bool AuthenticateUser(string username, string password, out User authenticatedUser)
    {
        db = new clsDataConnection();
        db.AddParameter("@Username", username);
        db.AddParameter("@Password", PasswordHelper.HashPassword(password));
        db.Execute("spAuthenticateUser");

        if (db.Count == 1)
        {
            DataRow row = db.DataTable.Rows[0];
            authenticatedUser = new User
            {
                UserID = Convert.ToInt32(row["UserID"]),
                Username = row["Username"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString()
            };
            return true;
        }
        authenticatedUser = null;
        return false;
    }
}
