using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X2342W_HFT_2023241.Logic;
using X2342W_HFT_2023241.Models;


namespace X2342W_HFT_2023241.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ExcavationController : ControllerBase
	{
		IExcavationLogic logic;

		public ExcavationController(IExcavationLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IEnumerable<Excavation> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Excavation Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Excavation value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Excavation value)
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
