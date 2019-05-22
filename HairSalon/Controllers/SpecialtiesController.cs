using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class SpecialtiesController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpPost("/specialties")]
    public ActionResult Create(string specialtyName)
    {
      Specialty newSpecialty = new Specialty(specialtyName);
      newSpecialty.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("specialties/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/specialties/{specialtyId}")]
    public ActionResult Show(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      return View(specialty);
    }

  

  }
}
