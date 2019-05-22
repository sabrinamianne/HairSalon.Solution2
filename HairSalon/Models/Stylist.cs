using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {

    private string _name;
    private int _id;


    public Stylist (string stylistName, int id=0)
    {
    _name = stylistName;
    _id = id;

    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCountClients()
      {
        Stylist selectedStylist = Stylist.Find(_id);
        List<Client> stylistClients = selectedStylist.GetClients();
        int count = stylistClients.Count ;
        return count;
      }

    public override int GetHashCode()
      {
          return this.GetId().GetHashCode();
      }

    public override bool Equals (System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId().Equals(newStylist.GetId());
        bool nameEquality = this.GetName().Equals(newStylist.GetName());
        return (idEquality && nameEquality);
      }
    }



    public static List<Stylist> GetAll()
      {
        List<Stylist> allStylists = new List<Stylist> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          string StylistName = rdr.GetString(0);
          int StylistId = rdr.GetInt32(1);
          Stylist newStylist = new Stylist(StylistName, StylistId);
          allStylists.Add(newStylist);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylists;
      }

    public List<Client> GetClients()
       {
        List<Client> allStylistItems = new List<Client> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM clients WHERE stylistId = @stylistId;";
        MySqlParameter StylistId = new MySqlParameter();
        StylistId.ParameterName = "@stylistId";
        StylistId.Value = this._id;
        cmd.Parameters.Add(StylistId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          string client_name = rdr.GetString(0);
          DateTime client_app =rdr.GetDateTime(1);
          int stylist_id = rdr.GetInt32(2);
          int client_id = rdr.GetInt32(3);
          Client newClient = new Client (client_name, client_app, stylist_id, client_id);
          allStylistItems.Add(newClient);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allStylistItems;
      }

      public void Save()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";
        MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@name";
        name.Value = this._name;
        cmd.Parameters.Add(name);
        cmd.ExecuteNonQuery();
        _id = (int) cmd.LastInsertedId;
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public static Stylist Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int StylistId = 0;
        string StylistName = "";
        while(rdr.Read())
        {
          StylistName = rdr.GetString(0);
          StylistId = rdr.GetInt32(1);
        }
        Stylist newStylist = new Stylist(StylistName, StylistId);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newStylist;
      }

      public void Edit(string newName)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";
          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = _id;
          cmd.Parameters.Add(searchId);
          MySqlParameter name = new MySqlParameter();
          name.ParameterName = "@newName";
          name.Value = newName;
          cmd.Parameters.Add(name);
          cmd.ExecuteNonQuery();
          _name = newName;
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
        cmd.CommandText = @"DELETE FROM stylists;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
         conn.Dispose();
        }
      }

      public void Delete()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM stylists WHERE id=@stylistId;";
        MySqlParameter StylistId= new MySqlParameter();
        StylistId.ParameterName = "@stylistId";
        StylistId.Value = this._id;
        cmd.Parameters.Add(StylistId);
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
      }

      public void AddSpecialty (Specialty specialty)
      {
      	MySqlConnection conn = DB.Connection();
      	conn.Open();
      	MySqlCommand cmd = conn.CreateCommand();
      	cmd.CommandText = @"INSERT INTO stylist_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";
      	MySqlParameter specialtyId = new MySqlParameter("@SpecialtyId", specialty.GetId());
      	MySqlParameter stylistId = new MySqlParameter("@StylistId",this._id);
      	cmd.Parameters.Add(specialtyId);
      	cmd.Parameters.Add(stylistId);

      	cmd.ExecuteNonQuery();

      	conn.Close();
      	if (conn != null) conn.Dispose();
      }


    public List<Specialty> GetSpecialties()
       {
        List<Specialty> allSpecialties = new List<Specialty> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM stylist_specialties WHERE stylist_id=@StylistId";
        MySqlParameter StylistId = new MySqlParameter();
        StylistId.ParameterName = "@StylistId";
        StylistId.Value = this._id;
        cmd.Parameters.Add(StylistId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {

          int specialty_id = rdr.GetInt32(2);
          allSpecialties.Add(Specialty.Find(specialty_id));
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allSpecialties;
      }


  }
}
