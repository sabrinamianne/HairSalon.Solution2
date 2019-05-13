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

    public void SetClientName(string newName)
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
        string client_name = rdr.GetString(0);
        DateTime client_app = rdr.GetDateTime(1);
        int stylist_id = rdr.GetInt32(2);
        int client_id = rdr.GetInt32(3);
        Client newClient = new Client (client_name, client_app, stylist_id, client_id);
        allClients.Add(newClient);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static Client Find (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int client_id= 0;
      string client_name= "";
      int stylist_id = 0;
      DateTime client_app = new DateTime();
      while(rdr.Read())
      {
        client_name = rdr.GetString(0);
        client_app =rdr.GetDateTime(1);
        stylist_id = rdr.GetInt32(2);
        client_id = rdr.GetInt32(3);
      }
      Client foundClient= new Client (client_name, client_app, stylist_id, client_id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }


    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id=@id;";
      MySqlParameter client_id= new MySqlParameter();
      client_id.ParameterName = "@id";
      client_id.Value = this._id;
      cmd.Parameters.Add(client_id);
      cmd.ExecuteNonQuery();
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
      cmd.Parameters.Add(stylistParameter);

      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newName , DateTime newAppointment )
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"UPDATE clients SET name = @newName, appointment = @newAppointment WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = _id;
     cmd.Parameters.Add(searchId);

     MySqlParameter name = new MySqlParameter();
     name.ParameterName = "@newname";
     name.Value = newName;
     cmd.Parameters.Add(name);

     MySqlParameter appointment = new MySqlParameter();
     appointment.ParameterName = "@newAppointment ";
     appointment.Value = newAppointment ;
     cmd.Parameters.Add(appointment);
     cmd.ExecuteNonQuery();

     _name = newName;
     _appointmentdate = newAppointment ;

     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
    }



  }
}
