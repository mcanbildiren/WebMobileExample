using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminTemplate.Controllers.Apis
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}