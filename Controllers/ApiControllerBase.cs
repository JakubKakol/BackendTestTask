using Microsoft.AspNetCore.Mvc;

namespace BackendTestTask.Controllers
{
    [Route("api.user.[controller].[action]")]
    public abstract class ApiControllerBase : Controller
    { }
}
