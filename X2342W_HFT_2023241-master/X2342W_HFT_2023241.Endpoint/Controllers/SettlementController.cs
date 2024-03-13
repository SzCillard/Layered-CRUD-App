using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X2342W_HFT_2023241.Logic;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class SettlementController : ControllerBase
	{
		ISettlementLogic logic;

		public SettlementController(ISettlementLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IEnumerable<Settlement> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Settlement Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Settlement value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Settlement value)
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
