using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtiesControllerTest
    {
      [TestMethod]
      public void New_ReturnsCorrectView_True()
      {
        //Arrange
        SpecialtiesController  controller = new SpecialtiesController();
        // Act
        ActionResult newView = controller.New();
        // Assert
        Assert.IsInstanceOfType (newView, typeof(ViewResult));
      }

    }
}
