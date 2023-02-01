using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Entities;
using Repository;

namespace Project315.ActionFilters
{
     public class ValidateEntityExistsAttribute<T> : IActionFilter where T : class, IEntity
     {
         private readonly RepositoryBase<T> _context;

         public ValidateEntityExistsAttribute(RepositoryBase<T> context)
         {
             _context = context;
         }

         public void OnActionExecuting(ActionExecutingContext context)
         {
             Guid id = Guid.Empty;

             if (context.ActionArguments.ContainsKey("id"))
             {
                 id = (Guid)context.ActionArguments["id"];
             }
             else
             {
                 context.Result = new BadRequestObjectResult("Bad id parameter");
                 return;
             }

             var entity = _context.FindByCondition(x => x.Id.Equals(id));
             if (entity == null)
             {
                 context.Result = new NotFoundResult();
             }
             else
             {
                 context.HttpContext.Items.Add("entity", entity);
             }
         }

         public void OnActionExecuted(ActionExecutedContext context)
         {
         }
     }
    }

