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

    [TestMethod]
    public void GetId_ReturnsId_Id()
    {
    	Specialty newSpecialty = new Specialty("Color",1);
    	int result = 1;
    	Assert.AreEqual(result, newSpecialty.GetId());
    }

    [TestMethod]
    public void GetAllSpecialties_ReturnAllSpecialties()
    {
    	Specialty newSpecialty = new Specialty("Hairmaster");
    	newSpecialty.Save();
    	Specialty newSpecialty1 = new Specialty("Nailmaster");
    	newSpecialty1.Save();
    	List<Specialty> allSpecialties = new List<Specialty> {
    		newSpecialty,newSpecialty1
    	};
    	List<Specialty> result = Specialty.GetAll();
    	CollectionAssert.AreEqual (result, allSpecialties);
    }

    [TestMethod]
    public void GetStylists_ReturnSpecialtyWithAllStylists()
    {
    	Specialty newSpecialty = new Specialty("Color");
    	newSpecialty.Save();
    	Stylist newStylist = new Stylist("Jocelyn Navy",2);
    	newStylist.Save();
    	Stylist newStylist1 = new Stylist("Laura Gonthier",2);
    	newStylist1.Save();
    	newSpecialty.AddStylist(newStylist);
    	newSpecialty.AddStylist(newStylist1);

    	List<Stylist> allStylists = new List<Stylist> {
    		newStylist,newStylist1
    	};
    	List<Stylist> result = newSpecialty.GetStylists();
    	CollectionAssert.AreEqual(allStylists,result);
    }

  }
}
