using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName, DateTime appDate)
    {

      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, appDate, stylistId);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      return View("Show", model);
    }

    [HttpGet("stylists/new")]
    public ActionResult New()
    {
      return View();
    }


    [HttpGet("/stylists/{stylistId}")]
    public ActionResult Show(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(stylistId);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      return View(model);
    }

    [ActionName("Destroy"), HttpPost("/stylists/{stylistId}/delete")]
    public ActionResult Destroy(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      List<Client> stylistClients = stylist.GetClients();
      foreach(Client client in stylistClients)
      {
        client.Delete();
      }
      stylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{stylistId}/edit")]
     public ActionResult Edit(int stylistId)
     {
       Stylist stylist = Stylist.Find(stylistId);
       return View(stylist);
     }

    [HttpPost("/stylists/{stylistId}/update")]
    public ActionResult Update(int stylistId, string newName)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Edit(newName);
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpPost("/stylists/delete")]
     public ActionResult DeleteAll()
     {
       Stylist.ClearAll();
       return RedirectToAction("Index");
     }

     [HttpPost("/stylists/{stylistId}/specialties/add")]
      public ActionResult Update(int specialty, int stylistId)
      {
        Specialty addedSpecialty = Specialty.Find(specialty);
        Stylist stylist = Stylist.Find(stylistId);
        stylist.AddSpecialty(addedSpecialty);
        return RedirectToAction("Show");
      }

  }
}
