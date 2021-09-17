using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;
using TrueHome.Provider.Contracts;

namespace TrueHome.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public IConfiguration Configuration { get; private set; }

        private IActivityProvider _activityProvider;

        public ActivityController(IConfiguration configuration, IActivityProvider activityProvider)
        {
            this._activityProvider = activityProvider;
        }

		[Route("Activities")]
		[HttpGet]
		public async Task<IActionResult> GetActivities()
		{
			try
			{
				
				var addresses = await _activityProvider.GetAsync();

				return Ok(addresses);
			}
			catch (Exception baseEx)
			{
				
				return BadRequest(baseEx.Message);
			}
			
		}

		[Route("ActivitiesByProperty/{propertyId}")]
		[HttpGet]
		public async Task<IActionResult> GetActivitiesByProperty(int propertyId)
		{
			try
			{


				var addresses = await _activityProvider.GetAddressByPropertyIdAsync(propertyId);

				return Ok(addresses);
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}

		[Route("Activities/{id}")]
		[HttpGet]
		public async Task<IActionResult> GetActivitiesById(int id)
		{
			try
			{
				var activity = await _activityProvider.GetActivityByIdAsync(id);

				return Ok(activity);
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}

		[Route("AddActivities")]
		[HttpPost]
		public async Task<IActionResult> AddActivity([FromBody] Activity activity)
		{
			try
			{

				long id = await _activityProvider.CreateActivityAsync(activity);
				var json = CreateJson(id);
				return Ok(json);
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}

		[Route("UpdateScheduleActivitiy")]
		[HttpPost]
		public async Task<IActionResult> UpdateScheduleActivity([FromBody] Activity activity)
		{
			try
			{

				await _activityProvider.UpdateScheduleActivityAsync(activity);

				return Ok();
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}

		[Route("UpdateStatusActivity")]
		[HttpPost]
		public async Task<IActionResult> UpdateStatusActivity([FromBody] Activity activity)
		{
			try
			{
				await _activityProvider.UpdateStatusActivityAsync(activity);

				return Ok();
			}
			catch (Exception baseEx)
			{

				return BadRequest(baseEx.Message);
			}

		}

		private string CreateJson(long id)
		{
			dynamic obj = new JObject();
			obj.Id = id;

			var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
			return jsonString;
		}
	}
}
