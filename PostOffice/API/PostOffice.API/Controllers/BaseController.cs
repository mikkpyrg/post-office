using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Model.Infrastructure;

namespace PostOffice.API.Controllers
{
	public class BaseController : Controller
	{
        protected ActionResult<APIResponse<T>> OkIfSuccess<T>(APIResponse<T> response)
        {
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        protected ActionResult<APIResponse> OkIfSuccess(APIResponse response)
        {
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
