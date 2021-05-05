<%@ WebHandler Language="C#" Class="SessionHandler" %>

using System.Web;
using FSLibrary.Funciones;

public class SessionHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string key = Functions.ValorRequest(context.Request.QueryString["key"]);
        string val = Functions.ValorRequest(context.Request.QueryString["val"]);
        string hash = Functions.ValorRequest(context.Request.QueryString["hash"]);

        context.Response.ContentType = "text/plain";

        if (hash != Functions.Crypt(key + val))
        {
            context.Response.Write("Bad request");
        }
        else
        {
            if (val != "")
            {
                context.Session[key] = val;
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(context.Session[key]);
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}