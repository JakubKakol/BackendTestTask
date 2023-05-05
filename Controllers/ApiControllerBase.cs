using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendTestTask.Controllers
{
    [Route("api.user.[controller].[action]")]
    public abstract class ApiControllerBase : Controller
    {
        public override JsonResult Json(object data)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return base.Json(data, serializeOptions);
        }
    }
}
