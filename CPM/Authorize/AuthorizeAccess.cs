using System.Net;
using System.Text;
using CPM_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CPM_Project.Authorize;

public class AuthorizeAccess : ActionFilterAttribute
{
    private LoginData loginData = new LoginData();
    public string Level { get; set; }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var db = new AppDBContext();

        var levelTMP = Level.Split(',');
        var paramCount = levelTMP.Length;
        var unlock = false;
        
        try
        {
            loginData = JsonConvert.DeserializeObject<LoginData>(context.HttpContext.Session.GetString("LoginData"));
        }
        catch
        {
            loginData = null;
        }
        
        var request = context.HttpContext.Request;
        var response = context.HttpContext.Response;

        if (loginData == null)
        {
            response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new RedirectResult("~/user/login"); //new RedirectToRouteResult("Login", "User"); // new UnauthorizedResult(); //EmptyResult();
        }
        else
        {
            // if (request.Method.ToUpper() == "GET")
            // {
            //     var param = request.QueryString.Value;
            //     //request.QueryString = new QueryString("");
            // }
            // else
            // {
            //     var body = new StreamReader(request.Body).ReadToEndAsync().Result; 
            //     //request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
            // }
            
            if (paramCount == 1)
            {
                if (!string.Equals(loginData.JENIS_USER, Level, StringComparison.CurrentCultureIgnoreCase))
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new RedirectResult("~/user/login"); //new RedirectToRouteResult("Login", "User");
                }
            }
            else
            {
                foreach (var xLevel in levelTMP)
                {
                    if (string.Equals(xLevel, loginData.JENIS_USER, StringComparison.CurrentCultureIgnoreCase))
                    {
                        unlock = true;
                    }
                }

                if (!unlock)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new RedirectResult("~/user/login"); //new RedirectToRouteResult("Login", "User");
                }
            }
        }

        //Add Validation code here
        base.OnActionExecuting(context);
    }
}