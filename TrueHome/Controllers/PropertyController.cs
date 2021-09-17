using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHome.Provider.Contracts;

namespace TrueHome.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
		public IConfiguration Configuration { get; private set; }

		private IPropertyProvider _propertyProvider;

		public PropertyController(IConfiguration configuration, IPropertyProvider propertyProvider)
		{
			this._propertyProvider = propertyProvider;
		}

		[Route("Properties")]
		[HttpGet]
		public async Task<IActionResult> GetProperties()
		{
			try
			{

				var addresses = await _propertyProvider.GetAsync();

				return Ok(addresses);
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}
	}
}
