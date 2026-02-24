using M006.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M006.Controllers;

/// <summary>
/// EF Core Power Tools
/// 
/// VS Extension installieren
/// Rechtsklick auf das Projekt -> EF Core Power Tools -> Reverse Engineer
/// </summary>
public class HomeController(ILogger<HomeController> logger, KursDBContext kursDB) : Controller
{
	public IActionResult Index()
	{
		IQueryable<Kurse> kurs = kursDB.Kurse; //Lädt selbst noch keine Daten

		//ToList()
		//Lädt die Daten von der Datenbank (zur ASP-Anwendung)
		//WICHTIG: Mit Bedacht benutzen
		//Effekt: ToList() lädt die Daten (eine Iteration), Frontend zeigt die Daten an (zweite Iteration)
		//kurs.ToList();

		///////////////////////////////////////////////////////////

		//Beispiel: IEnumerable

		IEnumerable<int> zahlen = Enumerable.Range(0, 100);
		List<int> z = zahlen.ToList(); //Anleitung ausführen

		List<int> neueZahlen = [];
		neueZahlen.AddRange(z); //Doppelte Iterierung

		//ToList() immer sparsam benutzen

		///////////////////////////////////////////////////////////

		return View(kurs);
	}

	public IActionResult Privacy()
	{
		//kursDB.Kurse.Join(kursDB.KursInhalte,
		//	left => left.ID,
		//	right => right.KursId,
		//	(left, right) => new object[]{ left.ID, left.DauerInTagen, left.KursName, right.InhaltTitel });

		IQueryable<KurseMitInhalte> kurse = kursDB.Kurse.Join(
			kursDB.KursInhalte,
			left => left.ID,
			right => right.KursId,
			(left, right) => new KurseMitInhalte(left.ID, left.KursName, left.DauerInTagen, right.InhaltTitel));

		kurse.ToList();

		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}

public record KurseMitInhalte(int ID, string KursName, int? DauerInTagen, string InhaltTitel);