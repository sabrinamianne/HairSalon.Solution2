using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest : IDisposable
  {
    public void Dispose()
    {
      Specialty.ClearAll();
    }

  public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sabrina_mianne_test;";
    }
    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("Color", 1);
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetAllSpecialty_ReturnAllSpecialties()
    {
      Specialty firstSpecialty = new Specialty("Color",3);
      firstSpecialty.Save();
      Specialty secondSpecialty = new Specialty("Hair cut",2);
      secondSpecialty.Save();
      List<Specialty> newList = new List<Specialty> {firstSpecialty,secondSpecialty};
      List<Specialty> listSpecialties = Specialty.GetAll();
      CollectionAssert.AreEqual (listSpecialties, newList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Specialty()
    {
      Specialty firstSpecialty = new Specialty("Color");
      Specialty secondSpecialty = new Specialty("Color");
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

  }
}
