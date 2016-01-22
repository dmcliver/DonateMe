using System.Collections.Generic;
using System.Web.Http;

namespace DonateMe.Web.Controllers
{
    public class BrandController : ApiController
    {
        public IEnumerable<string> Get([FromUri] string term)
        {
            return new[] { "val1" + term, "val2" + term };
        }
    }
}
