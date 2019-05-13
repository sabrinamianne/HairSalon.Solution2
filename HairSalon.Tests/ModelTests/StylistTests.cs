using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using HairSalon;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
    }

    public StylistTest()
    {
    	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sabrina_mianne_test;";
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
    	string name = "Sabrina Mianne";
    	Stylist newStylist = new Stylist(name);
    	Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_StylistName()
    {
    	Stylist newStylist = new Stylist("Rita");
    	string result = "Rita";
    	Assert.AreEqual(result, newStylist.GetName());
    }

    [TestMethod]
    public void GetId_ReturnsId_Id()
    {
    	Stylist newStylist = new Stylist("Sabrina Mianne", 1);
    	int idStylist = 1;
    	Assert.AreEqual(idStylist, newStylist.GetId());
    }

    [TestMethod]
    public void GetAllStylists_ReturnAllStylists()
    {
    	Stylist firstStylist = new Stylist("Sabrina Mianne",3);
    	firstStylist.Save();
    	Stylist secondStylist = new Stylist("Sandra Kherrab",3);
    	secondStylist.Save();
    	List<Stylist> newList = new List<Stylist> {firstStylist,secondStylist};
    	List<Stylist> listStylists = Stylist.GetAll();
    	CollectionAssert.AreEqual (listStylists, newList);
    }
  }
}
