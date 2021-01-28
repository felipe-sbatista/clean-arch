using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace clean.api.Controllers.Base
{
    public class BaseHandlerController: ControllerBase
    {
        protected void HandleModelStateErrors()
        {
            var result = from ms in ModelState
                         where ms.Value.Errors.Any()
                         let key = ms.Key
                         let erros = ms.Value.Errors
                         from error in erros
                         select new
                         {

                         };

            result.ToList().ForEach(er => Console.WriteLine(er));
        }

        private async Task<T> ExecuteAsync<T> (Func <Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                return ResponseApi();
            }
        }

        private T Execute <T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"do whatever here {ex.Message}");
                return ResponseApi();
            }
        }

        protected Task<dynamic> ResponseApiAsync(Func<Task<dynamic>> action) => ExecuteAsync( async () => ResponseApi(await action()));

        protected ActionResult ResponseApi(Func<dynamic> action) => Execute(() => ResponseApi(action()));

        protected Task<dynamic> ResponseApiAsync(Func<Task> action)
               => ExecuteAsync(async () =>
               {
                   await action();
                   return ResponseApi();
               });

        protected dynamic ResponseApi(dynamic result = null, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            // execute any rule to http responses
            if (result is null) return NoContent();

            if (result is IList && Enumerable.Count(result) <= 0) return NotFound();

            return Ok(result);
        }

    }
}
