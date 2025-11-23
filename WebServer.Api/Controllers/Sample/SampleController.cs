using Microsoft.AspNetCore.Mvc;

namespace WebServer.Api.Controllers.Sample
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SampleResponse))]
        public IActionResult Get()
        {
            var response = new SampleResponse
            {
                Message = "Hello from WebServer API!",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0"
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SampleResponse))]
        public IActionResult GetById(int id)
        {
            var response = new SampleResponse
            {
                Message = $"Hello from WebServer API! ID: {id}",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0"
            };
            return Ok(response);
        }
    }

    public class SampleResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Version { get; set; } = string.Empty;
    }
}

