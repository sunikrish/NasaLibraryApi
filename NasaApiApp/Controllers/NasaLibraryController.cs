using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NasaApi.Application.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NasaApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaLibraryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NasaLibraryController(IMediator mediator)
        {
            _mediator = mediator;
        }
       

        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByQueryKeys=new string[] { "FreeText", "StartYear", "EndYear", "MediaType" })]
        [Route("data")]
        public async Task<NasaResponse> GetData([FromQuery]GetNasaSearchQuery test)
        {
            return await _mediator.Send(test);
        }




    }
}
