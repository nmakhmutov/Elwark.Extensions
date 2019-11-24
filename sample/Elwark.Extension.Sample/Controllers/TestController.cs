using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Elwark.Extension.Sample.Controllers
{
    [ApiController, Route("[controller]")]
    public class TestController : ControllerBase
    {
        public ActionResult<IEnumerable<int>> Get() =>
            Ok(Enumerable.Range(0, 10));
    }
}