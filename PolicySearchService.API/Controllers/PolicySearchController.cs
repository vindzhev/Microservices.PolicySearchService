namespace PolicySearchService.API.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    
    using MicroservicesPOC.Shared.Controllers;
    
    using PolicySearchService.Application.Policy.Queries;

    [ApiController]
    [Route("api/[controller]")]
    public class PolicySearchController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult> SearchAsync([FromQuery] string query) =>
            this.Ok(await this.Mediator.Send(new FindPolicyQuery(query)));
    }
}
