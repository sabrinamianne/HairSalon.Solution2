using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private DateTime _appointmentdate;
    private int _id;
    private int _stylistId;


    public Client (string name, DateTime appointmentdate,  int stylistId, int id=0)
    {
      _name = name;
      _appointmentdate = appointmentdate;
      _id= id;
      _stylistId = stylistId;
    }

    public string GetClientName()
    {
      return _name;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    public int GetId()
    {
      return _id;
    }

    public void SetCLienttName(string newName)
    {
      _name = newName;
    }

    public DateTime GetAppDate()
    {
      return _appointmentdate;
    }

    public void SetAppDate(DateTime newdate)
    {
      _appointmentdate = newdate;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients= new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        string clientName = rdr.GetString(0);
        DateTime clientAppointment = rdr.GetDateTime(1);
        int stylistId = rdr.GetInt32(2);
        int clientId = rdr.GetInt32(3);
        Client newClient = new Client (clientName, clientAppointment, stylistId, clientId)
      }
    }





  }
}
