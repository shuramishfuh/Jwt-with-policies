using FoodAppAPI.AppPolicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
          
    
        [HttpPost]
       [Authorize(Policy = Policies.Admin)]    
        public ActionResult GetAdminData()    
        {    
            return Ok("This is an Admin user");    
        }    
    }
}
