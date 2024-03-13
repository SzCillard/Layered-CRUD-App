using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using X2342W_HFT_2023241.Models;
using X2342W_HFT_2023241.Repository;
using X2342W_HFT_2023241.Logic;

namespace X2342W_HFT_2023241.Test
{
	[TestFixture]
	public class LogicTester
	{
		ResearcherLogic researcherLogic;
		Mock<IRepository<Researcher>> mockResearcherRepo;
		ExcavationLogic excavationLogic;
		Mock<IRepository<Excavation>> mockExcavationRepo;
		ExcavationSiteLogic excavationSiteLogic;
		Mock<IRepository<ExcavationSite>> mockExcavationSiteRepo;
		SettlementLogic settlementLogic;
		Mock<IRepository<Settlement>> mockSettlementRepo;


		[SetUp]
		public void Init()
		{
			IEnumerable<Researcher> researchers = new List<Researcher>()
			{
					new Researcher("1#Nagy Gábor#Archeologist"),
					new Researcher("2#Kovács Eszter#Historian"),
					new Researcher("3#Tóth Péter#Archeologist"),
					new Researcher("4#Szabó Katalin#Archeologist")
			};
			IEnumerable<ExcavationSite> sites = new List<ExcavationSite>()
			{
					new ExcavationSite("1#1#Shrine#Ancient"),
					new ExcavationSite("2#1#Tower#Early Medieval"),
					new ExcavationSite("3#3#Aqueduct#Post Medieval"),
					new ExcavationSite("4#2#Tunnel#Ancient"),
			};
			IEnumerable<Excavation> excavations = new List<Excavation>()
			{
				new Excavation("1#1#1#1966.03.17#1966.09.17"),
				new Excavation("2#2#2#1997.04.11#1997.10.11"),
				new Excavation("3#1#2#1989.09.23#1989.03.23"),
				new Excavation("4#1#3#1985.06.01#1985.12.01"),
				new Excavation("5#3#3#2000.11.23#2001.05.23"),
				new Excavation("6#4#4#2003.06.16#2003.12.16"),
				new Excavation("7#4#1#1993.12.21#1994.06.21"),
				new Excavation("8#4#2#2014.09.17#2015.03.17"),
				new Excavation("9#4#3#1978.05.24#1978.11.24"),
				new Excavation("10#2#3#1975.11.25#1976.05.25"),
			};
			IEnumerable<Settlement> settlements = new List<Settlement>() {
				new Settlement("1#Abaújszántó#Borsod-Abaúj-Zemplén"),
				new Settlement("2#Alsózsolca#Borsod-Abaúj-Zemplén"),
				new Settlement("3#Adács#Heves"),
			};
			foreach (var ex in excavations)
			{
				ex.ExcavationSite = sites.First(s => s.SiteId == ex.SiteId);
				ex.Researcher = researchers.First(r => r.ResearcherId == ex.ResearcherId);
			}
			foreach (var site in sites)
			{
				site.Excavations = excavations.Where(e => e.SiteId == site.SiteId).ToList();
				site.Researchers = excavations.Where(ex => ex.SiteId == site.SiteId)
					.Select(ex => ex.Researcher).ToList();
			}
			foreach (var researcher in researchers)
			{
				researcher.Excavations = excavations.Where(e => e.ResearcherId == researcher.ResearcherId).ToList();
				researcher.Sites = excavations.Where(ex => ex.ResearcherId == researcher.ResearcherId)
					.Select(ex => ex.ExcavationSite).ToList();
			}
			foreach(var settlement in settlements)
			{
				settlement.Sites=sites.Where(s=>s.SettlementId==settlement.SettlementId).ToList();
			}
			mockExcavationRepo = new Mock<IRepository<Excavation>>();
			mockExcavationRepo.Setup(m => m.ReadAll()).Returns(excavations.AsQueryable());
			excavationLogic = new ExcavationLogic(mockExcavationRepo.Object);

			mockExcavationSiteRepo = new Mock<IRepository<ExcavationSite>>();
			mockExcavationSiteRepo.Setup(m => m.ReadAll()).Returns(sites.AsQueryable());
			excavationSiteLogic = new ExcavationSiteLogic(mockExcavationSiteRepo.Object);

			mockResearcherRepo = new Mock<IRepository<Researcher>>();
			mockResearcherRepo.Setup(m => m.ReadAll()).Returns(researchers.AsQueryable());
			researcherLogic = new ResearcherLogic(mockResearcherRepo.Object);

			mockSettlementRepo = new Mock<IRepository<Settlement>>();
			mockSettlementRepo.Setup(m => m.ReadAll()).Returns(settlements.AsQueryable());
			settlementLogic = new SettlementLogic(mockSettlementRepo.Object);
		}
		//ResearcherLogic
		[Test]
		public void ExcavationSitesOfTest_IncorrectData()
		{
			IEnumerable<ExcavationSite> excavationSites = researcherLogic.ExcavationSitesOf(1);
			var expected = new List<ExcavationSite>()
			{
							   new ExcavationSite(){
									SiteId=5,
									SettlementId=1,
									SiteType="Shrine",
									AgeOfArtifact="Modern"
							   },
							   new ExcavationSite(){
									SiteId=4,
									SettlementId=4,
									SiteType="Tunnel",
									AgeOfArtifact="Ancient"
							   },
			};
			Assert.AreNotEqual(expected, excavationSites);
		}
		[Test]
		public void ExcavationSitesOfTest_CorrectData()
		{
			var excavationSites = researcherLogic.ExcavationSitesOf(1).ToList();
			var expected = new List<ExcavationSite>()
			{
							   new ExcavationSite(){
									SiteId=1,
									SettlementId=1,
									SiteType="Shrine",
									AgeOfArtifact="Ancient"
							   },
							   new ExcavationSite(){
									SiteId=4,
									SettlementId=2,
									SiteType="Tunnel",
									AgeOfArtifact="Ancient"
							   },
			};
			CollectionAssert.AreEquivalent(expected, excavationSites);
		}
		[Test]
		public void WithTheMostExcavationTest()
		{
			Researcher researcher = researcherLogic.WithTheMostExcavation();
			Researcher expected = new Researcher()
			{
				ResearcherId = 3,
				ResearcherName = "Tóth Péter",
				Profession = "Archeologist"
			};
			Assert.AreEqual(expected, researcher);
		}
		[Test]
		public void CreateResearcher_Correct()
		{
			var researcher = new Researcher() { ResearcherId = 71, ResearcherName = "Török Attila", Profession = "Historian" };
			researcherLogic.Create(researcher);
			mockResearcherRepo.Verify(r => r.Create(researcher), Times.Once());
		}
		[Test]
		public void CreateResearcher_InCorrect()
		{
			var researcher = new Researcher() { ResearcherId=10,ResearcherName = "Kata Kata"};
			Assert.Throws<ArgumentNullException>(() => researcherLogic.Create(researcher));
			mockResearcherRepo.Verify(r => r.Create(researcher), Times.Never);
		}

		//ExcavationSiteLogic
		[Test]
		public void GetSiteExcavationDates()
		{
			var dates = excavationSiteLogic.GetExcavationStartDates(2).ToList();
			List<DateTime> expected =new List<DateTime>() 
			{
			 new DateTime(1997,04,11),
			 new DateTime(1975,11,25)
			};
			Assert.That(expected, Is.EqualTo(dates));
		}
		[Test]
		public void GetSiteExcavationDates_InCorrectInput()
		{
			Assert.Throws<ArgumentException>(() => excavationSiteLogic.GetExcavationStartDates(-2).ToList());
		}
		[Test]
		public void GetSiteExcavationDates_InCorrect()
		{
			var dates = excavationSiteLogic.GetExcavationStartDates(2).ToList();
			List<DateTime> expected = new List<DateTime>()
			{
			 new DateTime(3000,04,11),
			 new DateTime(3000,11,25)
			};
			Assert.That(expected, !Is.EqualTo(dates));
		}
		[Test]
		public void CreateExcavationSite()
		{
			var site = new ExcavationSite() { SiteId= 5, SettlementId = 1, SiteType= "Fortress", AgeOfArtifact="Mid Medieval" };
			excavationSiteLogic.Create(site);
			mockExcavationSiteRepo.Verify(r => r.Create(site), Times.Once());
		}
		[Test]
		public void CreateExcavationSite_InCorrect()
		{
			var site = new ExcavationSite() { SiteId = 5, SettlementId = 1, AgeOfArtifact = "Mid Medieval" };
			Assert.Throws<ArgumentNullException>(()=>excavationSiteLogic.Create(site));
			mockExcavationSiteRepo.Verify(r => r.Create(site), Times.Never());
		}
		[Test]
		public void CreateExcavationSite_InCorrectID()
		{
			var site = new ExcavationSite() { SiteId = 5, SettlementId = -1, SiteType="Fortress",AgeOfArtifact = "Mid Medieval" };
			Assert.Throws<ArgumentException>(() => excavationSiteLogic.Create(site));
			mockExcavationSiteRepo.Verify(r => r.Create(site), Times.Never());
		}
		
		//Excavation
		[Test]
		public void GetExcavationDatesInPeriod()
		{
			var actual = excavationLogic.GetExcavationsInPeriod(DateTime.Parse("1990.01.01"), DateTime.Parse("2000.12.12"));
			var expected = new List<Excavation>()
			{
						new Excavation("2#2#2#1997.04.11#1997.10.11"),
						new Excavation("7#4#1#1993.12.21#1994.06.21")
			};
			Assert.AreEqual(expected, actual);
		}
		[Test]
		public void GetExcavationDatesInPeriod_InCorrect()
		{
			var actual = excavationLogic.GetExcavationsInPeriod(DateTime.Parse("1990.01.01"), DateTime.Parse("2000.12.12"));
			var expected = new List<Excavation>()
			{
						new Excavation("900#2#2#1997.04.11#1997.10.11"),
						new Excavation("7#100#1#1993.12.21#1994.06.21")
			};
			Assert.AreNotEqual(expected, actual);
		}
		[Test]
		public void CreateExcavation()
		{
			Excavation excavation = new Excavation("1#1#1#1966.03.17#1966.09.17");
			excavationLogic.Create(excavation);
			mockExcavationRepo.Verify(r => r.Create(excavation), Times.Once());
		}
		[Test]
		public void CreateExcavation_InCorrect()
		{
			Excavation excavation = new Excavation("-1#-1#0#1966.03.17#1966.09.17");
			Assert.Throws<ArgumentException>(() => excavationLogic.Create(excavation));
			mockExcavationRepo.Verify(r => r.Create(excavation), Times.Never());
		}

		//Settlement
		[Test]
		public void Researchers()
		{
			var actual = settlementLogic.Researchers("Adács");
			var expected = new List<Researcher>()
			{
									new Researcher("3#Tóth Péter#Archeologist"),
			};
			Assert.That(expected, Is.EquivalentTo(actual));
		}
		[Test]
		public void Researchers_InCorrect()
		{
			var actual = settlementLogic.Researchers("Adács");
			var expected = new List<Researcher>()
			{
									new Researcher("10#Tóth Péter#Archeologist"),
			};
			Assert.That(expected, !Is.EquivalentTo(actual));
		}
		[Test]
		public void AgeOfArtifacts()
		{
			var actual = settlementLogic.AgeOfArtifacts("Adács");
			var expected = new List<string>()
			{
				new string("Post Medieval")
			};
			Assert.That(expected, Is.EqualTo(actual));
		}
		[Test]
		public void NumberOfExcavations()
		{
			var actual = settlementLogic.NumberOfExcavations(2);
			Assert.That(4==actual);
		}
		[Test]
		public void LatestExcavation()
		{
			var actual = settlementLogic.LatestExcavation(2);
			Excavation expected = new Excavation("8#4#2#2014.09.17#2015.03.17");
			Assert.That(expected, Is.EqualTo(actual));
		}
		[Test]
		public void CreateSettlement()
		{
			var settlement = new Settlement("4#Csákvár#Fejér");
			settlementLogic.Create(settlement);
			mockSettlementRepo.Verify(s=>s.Create(settlement), Times.Once());
		}
		[Test]
		public void CreateSettlement_InCorrect()
		{
			var settlement = new Settlement()
			{
				SettlementId=4,
				SettlementName="Csákvár"
			};
			Assert.Throws<ArgumentNullException>(() => settlementLogic.Create(settlement));
			mockSettlementRepo.Verify(s => s.Create(settlement), Times.Never());
		}
	}
}
