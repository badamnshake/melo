using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _ctx;

        public BuggyController(DataContext ctx)
        {
            _ctx = ctx;
        }


        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _ctx.Users.Find(-1);

            if (thing == null)
            {
                return NotFound();
            }

            return Ok(thing);
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            try
            {
                var thing = _ctx.Users.Find(-1);

                var thingToReturn = thing.ToString();

                return thingToReturn;

            }
            catch (Exception ex)
            {

                return StatusCode(500, "computer says no");
            }
        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This wasn't a good request");
        }
    }
}