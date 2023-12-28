using template.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
namespace template.Utility
{
   
    public class AuthAttribute : ActionFilterAttribute
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        string role;
        public AuthAttribute(string _role)
        {
            role = _role;
            //_httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            if (httpContext!=null)
            {
               IJwtUser User=(IJwtUser)httpContext.Items["User"];
                if(User!=null)
                {
                    //if (User.Id == 1)
                    //{
                    //    throw new HttpRequestException("Asdasd");

                    //}
                    context.ActionArguments["AuthUser"] = User;
                }
                //else
                //{
                //    throw new HttpRequestException("sadasdas");
                //}

            }

            Console.WriteLine(role);
            // Do something with HttpContext...
        }
    }
}
