using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    [Route("api.user.[controller].[action]")]
    public abstract class ApiControllerBase : Controller
    {
        [NonAction]
        public IActionResult TryCatchResult<T>(Func<T> action)
        {
            try
            {
                var result = action.Invoke();
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
        public IActionResult TryCatchOperation(Action action)
        {
            try
            {
                action.Invoke();
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
