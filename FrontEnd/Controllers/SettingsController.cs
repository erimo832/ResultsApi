using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private IConfiguration Configuration { get; }

        public SettingsController(IConfiguration settings)
        {
            Configuration = settings;
        }


        [HttpGet]
        public ClientSettings Get()
        {
            var conf = new ClientSettings();
            Configuration.Bind(typeof(ClientSettings).Name, conf);
            return conf;
        }        
    }
}
