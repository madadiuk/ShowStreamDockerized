using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

public class Global : HttpApplication
{
    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/scripts/jquery-1.12.4.min.js",
            DebugPath = "~/scripts/jquery-1.12.4.js",
            CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js",
            CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.js"
        });
    }
}
