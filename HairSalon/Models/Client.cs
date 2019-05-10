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
        Client newClient = new Client (clientName, clientAppointment, stylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Delete()
    {
      MySqlConnection con = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id=@client_id;";
      MySqlParameter clientId= new MySqlParameter();
      clientId.ParameterName = "@client_id";
      clientId.Value = this._id;
      cmd.Parameters.Add(clientId);
      cmd.ExcecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetClientName() == newClient.GetClientName();
        bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
        return(idEquality && nameEquality && stylistEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = @"INSERT INTO clients (name, appointment, stylistId) VALUES (@ClientName, @ClientAppointmentDate, @ClientStylisId);";

      MySqlParameter nameParameter = new MySqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this._name;
      cmd.Parameters.Add(nameParameter);

      MySqlParameter appointmentDateParameter = new MySqlParameter();
      appointmentDateParameter.ParameterName = "@ClientAppointmentDate";
      appointmentDateParameter.Value = this._appointmentdate;
      cmd.Parameters.Add(appointmentDateParameter);

      MySqlParameter stylistParameter = new MySqlParameter();
      stylistParameter.ParameterName = "@ClientStylisId";
      stylistParameter.Value = this._stylistId;
      cmd.Parameters.Add(stylystParameter);

      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newName , DateTime newdappointmentDate)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";
     cmd.CommandText = @"UPDATE clients SET appointment = @newdappointmentDate WHERE id = @searchId;";
     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = _id;
     cmd.Parameters.Add(searchId);

     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@newname";
     .Value = newName;
     cmd.Parameters.Add(name);
     MySqlParameter appointment = new MySqlParameter();
     appointment.ParameterName = "@newappointmentDate";
     appointment.Value = newappointmentDate;
     cmd.Parameters.Add(appointment);
     cmd.ExecuteNonQuery();

     _name = newName;
     _appointment = newappointmentDate;

     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
    }

    public static Client Find (int id)
    {

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
      MySqlParameter idParameter = new MySqlParameter();
      idParameter.ParameterName = "@searchId";
      idParameter.Value = id;
      cmd.Parameters.Add(idParameter);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId=0;
      string clientName ="";
      DateTime clientAppointmentDate = new DateTime();
      int clientStylistId = 0;

      while(rdr.Read())
      {
      string clientName = rdr.GetString(0);
      DateTime clientAppointment = rdr.GetDateTime(1);
      int stylistId = rdr.GetInt32(2);
      int clientId = rdr.GetInt32(3);
      }
      Client foundClient= new Client (clientName, clientAppointment, stylistId, clientId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }





  }
}
