using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Client
{
    internal class Program
	{
		static RestService rest;
		
		static void List(string entity)
		{
			if (entity == "Researcher")
			{
				List<Researcher> items = rest.Get<Researcher>("researcher");
				Console.WriteLine("Id" + "\t" + "Name"+"\t"+"Profession");
				foreach (var item in items)
				{
					Console.WriteLine(item.ResearcherId + "\t" + item.ResearcherName+"\t"+item.Profession);
				}
			}
			else if (entity=="Settlement")
			{
				List<Settlement> items = rest.Get<Settlement>("settlement");
				Console.WriteLine("Id" + "\t" + "Name"+"\t"+"County");
				foreach (var item in items)
				{
					Console.WriteLine(item.SettlementId + "\t" + item.SettlementName+"\t"+item.CountyName);
				}
			}
			else if (entity == "Excavation")
			{
				List<Excavation> items =rest.Get<Excavation>("excavation");
				Console.WriteLine("Id" + "\t" + "Excavation Site Id"+"\t"+"Researcher Id"+"\t"+"Start Of Excavation"+"\t"+"End Of Excavation");
				foreach (var item in items)
				{
					Console.WriteLine(item.ExcavationId + "\t"+item.SiteId+"\t"+item.ResearcherId+"\t"+item.StartOfExcavation+"\t"+item.EndOfExcavation);
				}
			}
			else 
			{
				List<ExcavationSite> items =rest.Get<ExcavationSite>("excavationsite");
				Console.WriteLine("Id" + "\t" + "Settlement Id" + "\t" + "Site Type"+"\t"+ "AgeOfArtifact");
				foreach (var item in items)
				{
					Console.WriteLine(item.SiteId+"\t"+item.SettlementId + "\t" + item.SiteType + "\t" + item.AgeOfArtifact);
				}
			}
			Console.ReadLine();
		}
		static void Create(string entity)
		{
			int Id;
			if (entity == "Researcher")
			{
                Console.WriteLine("Add values to the followings: ");
				Console.Write("Name: "); var name = Console.ReadLine();
				Console.Write("Profession: "); var prof = Console.ReadLine();
				Researcher r = new Researcher()
				{
					ResearcherName = name,
					Profession = prof
				};
				rest.Post(r,"researcher");
			}
			else if (entity == "Excavation")
			{
				Console.WriteLine("Add values to the followings: ");
				Console.Write("Excavation Site Id: "); int siteId = int.Parse(Console.ReadLine());
				Console.Write("Researcher Id: "); int rId = int.Parse(Console.ReadLine());
				Console.Write("Start of the excavation(YYYY.MM.DD): "); var start = Console.ReadLine();
				Console.Write("End of the excavation(YYYY.MM.DD): "); var end = Console.ReadLine();

				Excavation e = new Excavation()
				{
					SiteId = siteId,
					ResearcherId = rId,
					StartOfExcavation = DateTime.Parse(start),
					EndOfExcavation = DateTime.Parse(end)
				};
				rest.Post(e,"excavation");
			}
			else if (entity == "ExcavationSite")
			{
				Console.WriteLine("Add values to the followings: ");
				Console.Write("SettlementId: "); int settlementId = int.Parse(Console.ReadLine());
				Console.Write("Site type: "); var type = Console.ReadLine();
				Console.Write("Age of the artifacts: "); var age = Console.ReadLine();

				ExcavationSite es = new ExcavationSite()
				{
					SettlementId = settlementId,
					SiteType = type,
					AgeOfArtifact = age
				};
				rest.Post(es,"excavationsite");
			}
			else
			{
				Console.WriteLine("Add values to the followings: ");
				Console.Write("Settlement: "); var name = Console.ReadLine();
				Console.Write("County: "); var county = Console.ReadLine();

				Settlement s = new Settlement()
				{
					SettlementName = name,
					CountyName = county
				};
				rest.Post(s,"settlement");
			}
		}
		static void Update(string entity)
		{
			int Id;
			if (entity=="Researcher")
			{
				Console.Write("Enter researcher's ID to update: "); Id = int.Parse(Console.ReadLine());
				Console.WriteLine("Values you want to set:");
				Console.Write("Name: "); var name = Console.ReadLine();
				Console.Write("Profession: "); var prof = Console.ReadLine();
				Researcher r = new Researcher()
				{
					ResearcherId = Id,
					ResearcherName = name,
					Profession = prof
				};
				rest.Put(r,"researcher");
			}
			else if (entity=="Excavation")
			{
				Console.Write("Enter excavation's ID to update: "); Id = int.Parse(Console.ReadLine());
				Console.WriteLine("Values you must set:");
				Console.Write("Excavation Site Id: "); int siteId = int.Parse(Console.ReadLine());
				Console.Write("Researcher Id: "); int rId = int.Parse(Console.ReadLine());
				Console.Write("Start of the excavation(YYYY.MM.DD): "); var start = Console.ReadLine();
				Console.Write("End of the excavation(YYYY.MM.DD): "); var end = Console.ReadLine();

				Excavation e = new Excavation()
				{
					ExcavationId = Id,
					SiteId = siteId,
					ResearcherId = rId,
					StartOfExcavation = DateTime.Parse(start),
					EndOfExcavation = DateTime.Parse(end)
				};
				rest.Put(e, "excavation");
			}
			else if (entity=="ExcavationSite")
			{
				Console.Write("Enter excavation site's ID to update: "); Id = int.Parse(Console.ReadLine());
				Console.WriteLine("Values you must set:");
				Console.Write("SettlementId: "); int settlementId = int.Parse(Console.ReadLine());
				Console.Write("Site type: "); var type = Console.ReadLine();
				Console.Write("Age of the artifacts: "); var age = Console.ReadLine();

				ExcavationSite es = new ExcavationSite()
				{
					SiteId = Id,
					SettlementId = settlementId,
					SiteType = type,
					AgeOfArtifact = age
				};
				rest.Put(es, "excavationsite");
			}
			else
			{
				Console.Write("Enter settlement's ID to update: "); Id = int.Parse(Console.ReadLine());
				Console.WriteLine("Values you must set:");
				Console.Write("Name: "); var name = Console.ReadLine();
				Console.Write("County: "); var county = Console.ReadLine();

				Settlement s = new Settlement()
				{
					SettlementId = Id,
					SettlementName = name,
					CountyName=county
				};
				rest.Put(s, "settlement");
			}
		}
		static void Delete(string entity)
		{
			int Id;
			if (entity == "Researcher")
			{
				Console.Write("Enter researcher's ID to delete: "); Id = int.Parse(Console.ReadLine());
				
				rest.Delete(Id,"researcher");
			}
			else if (entity == "Excavation")
			{
				Console.Write("Enter excavation's ID to delete: "); Id = int.Parse(Console.ReadLine());
				
				rest.Delete(Id, "excavation");
			}
			else if (entity == "ExcavationSite")
			{
				Console.Write("Enter excavation site's ID to delete: "); Id = int.Parse(Console.ReadLine());
				
				rest.Delete(Id,"excavationsite");
			}
			else
			{
				Console.Write("Enter settlement's ID to delete: "); Id = int.Parse(Console.ReadLine());
				
				rest.Delete(Id,"settlement");
			}
		}
		static void WithTheMostExcavation()
		{
			Researcher result=rest.GetSingle<Researcher>("noncrud/withthemostexcavation");
			Console.WriteLine("Id: " + result.ResearcherId + "\n" + "Name: " + result.ResearcherName + "\n" + "Profession: "+ result.Profession);
			Console.ReadLine();
		}
		static void ExcavationSitesOf()
		{
            Console.Write("Enter researcher's ID: "); int id = int.Parse(Console.ReadLine()); 
            var result = rest.Get<IEnumerable<ExcavationSite>>(id,"noncrud/excavationsitesof");
			Console.WriteLine("Id" + "\t" + "Settlement Id" + "\t" + "Site Type" + "\t" + "AgeOfArtifact");
			foreach (var item in result)
			{
				Console.WriteLine(item.SiteId + "\t" + item.SettlementId + "\t" + item.SiteType + "\t" + item.AgeOfArtifact);
			}
			Console.ReadLine();
		}
		static void GetExcavationStartDates()
		{
			Console.Write("Enter excavation site's ID: "); int id = int.Parse(Console.ReadLine());
			var result = rest.Get<IEnumerable<DateTime>>(id, "noncrud/getexcavationstartdates");
			Console.WriteLine("Dates: ");
			foreach (var item in result)
			{
				Console.WriteLine(item.ToString());
			}
			Console.ReadLine();

		}
		static void NumberOfExcavations()
		{
			Console.Write("Enter settlement's ID: "); int id = int.Parse(Console.ReadLine());
			var result = rest.Get<int>(id, "noncrud/numberofexcavations");
			Console.WriteLine("Excavations: "+result);
			
			Console.ReadLine();
		}
		static void LatestExcavation()
		{
			Console.Write("Enter settlement's ID: "); int id = int.Parse(Console.ReadLine());
			var result = rest.Get<Excavation>(id, "noncrud/latestexcavation");
            Console.WriteLine($"Excavation ID: {result.ExcavationId}\tExcavation site ID: {result.SiteId}\tStart Date: {result.StartOfExcavation}");
            Console.ReadLine();
		}

		static void Main(string[] args)
        {
			rest = new RestService("http://localhost:47263/","researcher");


			var researcherSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Researcher"))
				.Add("Create", () => Create("Researcher"))
				.Add("Delete", () => Delete("Researcher"))
				.Add("Update", () => Update("Researcher"))
				.Add("With the most excavation", () => WithTheMostExcavation())
				.Add("Check reseacher sites", () => ExcavationSitesOf())
				.Add("Exit", ConsoleMenu.Close);

			var excavationSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Excavation"))
				.Add("Create", () => Create("Excavation"))
				.Add("Delete", () => Delete("Excavation"))
				.Add("Update", () => Update("Excavation"))
				.Add("Exit", ConsoleMenu.Close);

			var excavationSiteSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("ExcavationSite"))
				.Add("Create", () => Create("ExcavationSite"))
				.Add("Delete", () => Delete("ExcavationSite"))
				.Add("Update", () => Update("ExcavationSite"))
				.Add("Check start dates of excavations", () => GetExcavationStartDates())
				.Add("Exit", ConsoleMenu.Close);

			var settlementSubMenu = new ConsoleMenu(args, level: 1)
				.Add("List", () => List("Settlement"))
				.Add("Create", () => Create("Settlement"))
				.Add("Delete", () => Delete("Settlement"))
				.Add("Update", () => Update("Settlement"))
				.Add("Number of excavations", () => NumberOfExcavations())
				.Add("Latest excavation", () => LatestExcavation())
				.Add("Exit", ConsoleMenu.Close);
			
			var menu = new ConsoleMenu(args, level: 0)
				.Add("Researchers", () => researcherSubMenu.Show())
				.Add("Excavations", () => excavationSubMenu.Show())
				.Add("ExcavationSites", () => excavationSiteSubMenu.Show())
				.Add("Settlements", () => settlementSubMenu.Show())
				.Add("Exit", ConsoleMenu.Close);

			menu.Show();
		}
    }
}
