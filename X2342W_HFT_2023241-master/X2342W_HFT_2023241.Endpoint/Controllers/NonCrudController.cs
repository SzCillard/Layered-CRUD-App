using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using X2342W_HFT_2023241.Logic;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Endpoint.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class NonCrudController : ControllerBase
	{
		IResearcherLogic researcherLogic;
		IExcavationLogic excavationLogic;
		IExcavationSiteLogic excavationSiteLogic;
		ISettlementLogic settlementLogic;

		public NonCrudController(IResearcherLogic researcherLogic, IExcavationLogic excavationLogic, IExcavationSiteLogic excavationSiteLogic, ISettlementLogic settlementLogic)
		{
			this.researcherLogic = researcherLogic;
			this.excavationLogic = excavationLogic;
			this.excavationSiteLogic = excavationSiteLogic;
			this.settlementLogic = settlementLogic;
		}

		[HttpGet]
		public Researcher WithTheMostExcavation()
		{
			return this.researcherLogic.WithTheMostExcavation();
		}

		[HttpGet("{id}")]
		public IEnumerable<ExcavationSite> ExcavationSitesOf(int id)
		{
			return this.researcherLogic.ExcavationSitesOf(id);
		}
		[HttpGet("{start},{end}")]
		public IEnumerable<Excavation> GetExcavationsInPeriod(DateTime start, DateTime end)
		{
			return this.excavationLogic.GetExcavationsInPeriod(start,end);
		}

		[HttpGet("{id}")]
		public IEnumerable<DateTime> GetExcavationStartDates(int id)
		{
			return this.excavationSiteLogic.GetExcavationStartDates(id);
		}
		[HttpGet("{settlementName}")]
		public IEnumerable<Researcher> Researchers(string settlementName)
		{
			return this.settlementLogic.Researchers(settlementName);
		}
		[HttpGet("{settlementName}")]
		public IEnumerable<string> AgeOfArtifacts(string settlementName)
		{
			return this.settlementLogic.AgeOfArtifacts(settlementName);
		}
		[HttpGet("{id}")]
		public int NumberOfExcavations(int id)
		{
			return this.settlementLogic.NumberOfExcavations(id);
		}
		[HttpGet("{id}")]
		public Excavation LatestExcavation(int id)
		{
			return this.settlementLogic.LatestExcavation(id);
		}
	}
}
