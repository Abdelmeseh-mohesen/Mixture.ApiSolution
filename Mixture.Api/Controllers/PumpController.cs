using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mixture.Core.Dto;
using Mixture.Core.Entity;
using Mixture.Core.Repositery;
using Mixture.Servise;
using Mixture.Servise.Abstract;

namespace Mixture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumpController : ControllerBase
    {
        private readonly IPumpReadingService pumpReadingService;

        public PumpController(IPumpReadingService pumpReadingService)
        {
            this.pumpReadingService = pumpReadingService;
        }

        [HttpPost("AddPumpReading")]
        public async Task<IActionResult> AddPumpReading([FromQuery] PumpReadingDto PumpDto)
        {
          var pump = await pumpReadingService.AddPump(PumpDto);  
            return Ok(pump);

        }

        [HttpGet("GetPumpReading")]
        public async Task<IActionResult> GetPumpReading( )
        {
            var pump = await pumpReadingService.GetPump();
            return Ok(pump);

        }


    }
}
