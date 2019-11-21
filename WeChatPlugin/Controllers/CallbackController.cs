using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.MyLogger;

namespace WeChatPlugin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private IMyLogger _logger;

        public CallbackController(IMyLogger logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public IActionResult PublicAccountCallback()
        {
            _logger.Info("testing this");
            return Ok();
        }




    }
}