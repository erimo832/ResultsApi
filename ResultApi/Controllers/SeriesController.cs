using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResultManager.Respository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private IRoundRespository roundRespository = new RoundRespository();

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return roundRespository.GetSeries();
        }
    }
}
