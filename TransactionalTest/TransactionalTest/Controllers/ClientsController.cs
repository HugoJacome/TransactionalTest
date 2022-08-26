using Microsoft.AspNetCore.Mvc;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        [HttpGet]
        public IActionResult Get() => throw new NotImplementedException();
        [HttpPost]
        public IActionResult Post() => throw new NotImplementedException();
        [HttpPut]
        public IActionResult Put() => throw new NotImplementedException();
        [HttpPatch]
        public IActionResult Push() => throw new NotImplementedException();
        [HttpDelete]
        public IActionResult Delete() => throw new NotImplementedException();
    }
}
