using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Common.WebApiCore.Controllers
{
    public class ValidateIdCompanyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == "GET" || context.HttpContext.Request.Method == "POST" || context.HttpContext.Request.Method == "PUT")
            {
                var model = context.ActionArguments.SingleOrDefault(p => p.Value.GetType().IsClass);
                if (model.Value != null)
                {

                    var idCompanyProperty = model.Value is CompanyDTO ? model.Value.GetType().GetProperty("Id") : model.Value.GetType().GetProperty("IdCompany");
                    idCompanyProperty = idCompanyProperty == null ? model.Value.GetType().GetProperty("CompanyId") : idCompanyProperty;
                    idCompanyProperty = idCompanyProperty == null ? model.Value.GetType().GetProperty("companyId") : idCompanyProperty;
                    if (idCompanyProperty != null)
                    {
                        var idCompanyValue = (int)idCompanyProperty.GetValue(model.Value);
                        if (idCompanyValue > 0)
                            ValidateIdCompany(context, idCompanyValue);
                    }
                }
                else if (context.HttpContext.Request.Method == "GET")
                {
                    var companyIdValue = context.HttpContext.Request.Query["CompanyId"];
                    if (int.TryParse(companyIdValue.ToString(), out var companyId))
                    //if (context.RouteData.Values.TryGetValue("CompanyId", out var companyIdValue) && int.TryParse(companyIdValue?.ToString(), out var companyId))
                    {
                        ValidateIdCompany(context, companyId);
                    }
                }
            }

            base.OnActionExecuting(context);
        }

        private static void ValidateIdCompany(ActionExecutingContext context, int idCompanyValue)
        {
            var claim = context.HttpContext.User.FindFirst("CompanyID");
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                var userCompanyId = Convert.ToInt32(claim.Value);

                if (idCompanyValue != userCompanyId)
                {
                    context.Result = new ForbidResult("No tienes permiso para acceder a este recurso.");
                }
            }
        }
    }
}
