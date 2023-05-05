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
            catch (Exception ex)
            {
                var journalItem = await createJournalItemAsync(ex);
                return StatusCode(500, getErrorResult(journalItem));
            }
        }

        [NonAction]
        public async Task<IActionResult> TryCatchOperationAsync(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                var journalItem = await createJournalItemAsync(ex);
                return StatusCode(500, getErrorResult(journalItem));
            }

            return Ok();
        }

        private async Task<JournalItem> createJournalItemAsync(Exception exception)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var queryString = request.QueryString.ToString();
            var requestBody = await getRequestBodyAsync(request);

            var journalItem = new JournalItem
            {
                EventId = Guid.NewGuid().ToString(),
                Timestamp = DateTimeOffset.UtcNow, //TODO - Date format
                RequestQueryString = queryString,
                RequestBody = requestBody,
                ExceptionType = exception.GetType().Name, //TODO - Exception type name adjustment
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace
            };

            //TODO - Add DbSet for JournalItem

            return journalItem;
        }

        private async Task<string> getRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();
            var encoding = Encoding.UTF8;
            using var reader = new StreamReader(request.Body, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true);
            var requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return requestBody;
        }

        private object getErrorResult(JournalItem item)
        {
            return new
            {
                type = item.ExceptionType,
                id = item.EventId,
                data = new
                {
                    message = item.ExceptionType == "Secure" ? item.ExceptionMessage : $"Internal server error ID = {item.EventId}" //TODO - Remove workaround
                }
            };
        }
    }
}
