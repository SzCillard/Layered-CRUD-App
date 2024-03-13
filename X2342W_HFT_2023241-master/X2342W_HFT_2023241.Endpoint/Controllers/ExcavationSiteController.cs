using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X2342W_HFT_2023241.Logic;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ExcavationSiteController : ControllerBase
	{
		IExcavationSiteLogic logic;

		public ExcavationSiteController(IExcavationSiteLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IEnumerable<ExcavationSite> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public ExcavationSite Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] ExcavationSite value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] ExcavationSite value)
		{
			this.logic.Update(value);
		}

		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			this.logic.Delete(id);
		}
	}
}
