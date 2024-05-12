<%@ WebHandler Language="C#" Class="UserSearch" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq; // For using .Select() with List<>
using ClassLibrary; // This will reference the namespace where User class is defined

public class UserSearch : IHttpHandler {
   
        public void ProcessRequest(HttpContext context)
        {
            string searchTerm = context.Request.QueryString["term"];
            List<User> users = SearchUsers(searchTerm); // Implement this method to search users based on input

            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(users.Select(u => new { id = u.UserID, text = u.Username }));
            context.Response.ContentType = "application/json";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        // Example method to search users (you would replace this with actual database logic)
        private List<User> SearchUsers(string searchText)
        {
            // Assuming you have a method to search users in your TransactionManager or similar
            TransactionManager tm = new TransactionManager();
            return tm.SearchUsers(searchText);
        }

}