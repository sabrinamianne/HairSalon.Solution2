using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controllers
  {
    [HttpGet]
    public ActionResult New()
    {
      Client client = new Client;
      return View(client);
    }
  }
}
