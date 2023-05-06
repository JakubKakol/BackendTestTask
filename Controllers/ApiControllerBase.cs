using BackendTestTask.Data.DbContexts;
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
        private readonly ApplicationDbContext _context;

        public ApiControllerBase(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
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
                CreatedAt = DateTimeOffset.UtcNow,
                RequestQueryString = queryString,
                RequestBody = requestBody,
                ExceptionType = exception is SecureException ? nameof(SecureException) : nameof(Exception),
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace
            };

            await _context.JournalItem.AddAsync(journalItem);
            await _context.SaveChangesAsync();

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
                    message = item.ExceptionType == nameof(SecureException) ? item.ExceptionMessage : $"Internal server error ID = {item.EventId}"
                }
            };
        }
    }
}
