using System;
using System.Collections.Generic;
using System.Data;

namespace ClassLibrary
{
    public class UserManager
    {
        public void AddUser(User user)
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@Username", user.Username);
            db.AddParameter("@Email", user.Email);
            db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
            db.AddParameter("@Role", user.Role);
            db.Execute("spAddUser");
        }

        public void DeleteUser(int userId)
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.Execute("spDeleteUser");
        }

        public void UpdateUser(User user)
        {
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", user.UserID);
            db.AddParameter("@Username", user.Username);
            db.AddParameter("@Email", user.Email);
            db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
            db.AddParameter("@Role", user.Role);
            db.Execute("spUpdateUser");
        }

        public List<User> GetAllUsers()
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

        public User GetUserById(int userId)
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
                    Role = row["Role"].ToString()
                };
            }
            return null;
        }

        public User GetUserByUsername(string username)
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

        public List<User> FilterUsersByRole(string role)
        {
            clsDataConnection db = new clsDataConnection();
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

        public List<User> SearchUsers(string username = null, string email = null, string role = null)
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

        public string GeneratePasswordResetToken(int userId)
        {
            clsDataConnection db = new clsDataConnection();
            string token = Guid.NewGuid().ToString();
            db.AddParameter("@UserID", userId);
            db.AddParameter("@ResetToken", token);
            db.Execute("spAddPasswordResetToken");
            return token;
        }

        public bool ResetPassword(string token, string newPassword)
        {
            clsDataConnection db = new clsDataConnection();
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
            clsDataConnection db = new clsDataConnection();
            db.AddParameter("@UserID", userId);
            db.AddParameter("@Activity", activity);
            db.AddParameter("@ActivityDate", DateTime.Now);
            db.Execute("spLogUserActivity");
        }

        public User AuthenticateUser(string username, string password)
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
    }
}
