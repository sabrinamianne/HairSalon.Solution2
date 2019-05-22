using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      Client.ClearAll();
    }

  public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sabrina_mianne_test;";
    }
    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("Test", new DateTime(), 1);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetClientName_GetNameOfClient_String()
    {
      string name = "Sabrina";
      Client newClient = new Client(name, new DateTime(), 1);
      string result = newClient.GetClientName();
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetClientName_SetNameOfClient_String()
    {
      string name = "Jimmynou";
      Client newClient = new Client(name, new DateTime(), 1);

      string updatedName = "Jimmy";
      newClient.SetClientName(updatedName);
      string result = newClient.GetClientName();

      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAllClients_ReturnsAnEmptyListFromDatabase_ClientsList()
    {
      List<Client> newList = new List<Client> { };
      List<Client> result = Client.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }
    
  }
}
