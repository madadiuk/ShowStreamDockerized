using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;

public class UserManager
{
    private clsDataConnection db;

    public void AddUser(User user)
    {
        db = new clsDataConnection();
        // Check if user already exists
        db.AddParameter("@Username", user.Username);
        db.Execute("spCheckUserExists");

        if (db.Count == 0)
        {
            db = new clsDataConnection();
            db.AddParameter("@Username", user.Username);
            db.AddParameter("@Email", user.Email);
            db.AddParameter("@Password", PasswordHelper.HashPassword(user.Password));
            db.AddParameter("@Role", user.Role);
            db.Execute("spAddUser");
        }
        else
        {
            throw new Exception("User already exists.");
        }
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

    public User GetUserById(int userId)
    {
        db = new clsDataConnection();
        db.AddParameter("@UserID", userId);
        db.Execute("spGetUserById");

        if (db.Count == 1)
        {
            DataRow row = db.DataTable.Rows[0];
            User user = new User
            {
                UserID = Convert.ToInt32(row["UserID"]),
                Username = row["Username"].ToString(),
                Email = row["Email"].ToString(),
                Role = row["Role"].ToString()
            };
            return user;
        }
        else
        {
            throw new Exception("User not found.");
        }
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
        string token = Guid.NewGuid().ToString();
        db = new clsDataConnection();
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
}
