using Azure.Core;
using BackendTestTask.Data.Models;
using BackendTestTask.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BackendTestTask.Controllers
{
    [Route("api.user.[controller].[action]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiControllerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [NonAction]
        public async Task<IActionResult> TryCatchResultAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action.Invoke();
                return Ok(result);
            }
            //TODO - Add custom exception and handling
            //catch (SecureException ex)
            //{

            //}
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //TODO - Customize message
            }
        }

        [NonAction]
        public async Task<IActionResult> TryCatchOperationAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            //TODO - Add custom exception and handling
            //catch (SecureException ex)
            //{

            //}
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //TODO - Customize message
            }

            return Ok();
        }
    }
}
