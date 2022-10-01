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
        // GET: api/<NasaLibraryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("data")]
        public async Task<NasaResponse> GetData([FromQuery]GetNasaSearchQuery test)
        {
            return await _mediator.Send(test);
            //return await Task.FromResult(new NasaResponse());
           // return new string[] { "value1", "value2" };
        }




    }
}
