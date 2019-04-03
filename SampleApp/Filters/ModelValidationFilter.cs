using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
    public class ModelValidationFilterAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            //model valid not pass  
            if (!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                var message = entry.Errors.FirstOrDefault().ErrorMessage;

                //modify the result  
                context.Result = new OkObjectResult(new
                {
                    code = -1,
                    data = new JObject(),
                    msg = message,
                });
            }
        }
    }

    //public class ModelValidationFilter : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        if (!context.ModelState.IsValid)
    //        {
    //            context.Result = new BadRequestObjectResult(context.ModelState);
    //        }
    //    }

    //}

}
