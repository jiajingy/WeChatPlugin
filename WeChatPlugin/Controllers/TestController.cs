using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.MyLogger;

namespace WeChatPlugin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IMyLogger _logger;

        public TestController(IMyLogger logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("HttpGetTest")]
        public IActionResult HttpGetTest()
        {
            _logger.Info("testing this");
            return Ok();
        }
    }
}