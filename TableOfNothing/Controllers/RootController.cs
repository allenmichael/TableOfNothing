using Microsoft.AspNetCore.Mvc;

namespace TableOfNothing.Controllers
{
    [ApiController]
    [Route("")]
    public class RootController : ControllerBase
    {
        public RootController()
        {
        }

        public ActionResult Get()
        {
            return new OkResult();
        }
    }
}