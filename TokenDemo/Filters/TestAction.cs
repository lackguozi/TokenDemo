using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TokenDemo.Filters
{
    public class TestActionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("动作完成之后2");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("动作完成之前1");
            Console.WriteLine(context.HttpContext.Request.Headers["Connection"].ToString());
            Console.WriteLine("动作完成之前");
        }
    }
}
