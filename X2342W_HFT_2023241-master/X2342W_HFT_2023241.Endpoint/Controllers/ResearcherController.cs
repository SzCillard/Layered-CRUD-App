using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using X2342W_HFT_2023241.Logic;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Endpoint.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ResearcherController : ControllerBase
	{
		IResearcherLogic logic;

		public ResearcherController(IResearcherLogic logic)
		{
			this.logic = logic;
		}

		[HttpGet]
		public IEnumerable<Researcher> ReadAll()
		{
			return this.logic.ReadAll();
		}

		[HttpGet("{id}")]
		public Researcher Read(int id)
		{
			return this.logic.Read(id);
		}

		[HttpPost]
		public void Create([FromBody] Researcher value)
		{
			this.logic.Create(value);
		}

		[HttpPut]
		public void Update([FromBody] Researcher value)
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
