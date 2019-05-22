using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Specialty
  {

    private string _name;
    private int _id;


    public Specialty (string name, int id=0)
    {
    _name= name;
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

    public void SetName(string newSpecialty)
    {
      _name = newSpecialty;
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties= new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        string specialty_name = rdr.GetString(1);
        int specialty_id = rdr.GetInt32(0);
        Specialty newSpecialty = new Specialty (specialty_name, specialty_id);
        allSpecialties.Add(newSpecialty);
      }
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void Save()
    {
      MySqlConnection conn =DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@name);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName= "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Specialty Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int specialty_id = 0;
        string specialty_name = "";
        while(rdr.Read())
        {
          specialty_id = rdr.GetInt32(0);
          specialty_name = rdr.GetString(1);
        }
        Specialty newSpecialty = new Specialty(specialty_name, specialty_id);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newSpecialty;
      }

    public override int GetHashCode()
      {
          return this.GetId().GetHashCode();
      }

    public override bool Equals (System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool idEquality = this.GetId().Equals(newSpecialty.GetId());
        bool nameEquality = this.GetName().Equals(newSpecialty.GetName());
        return (idEquality && nameEquality);
      }
    }



    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
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
        cmd.CommandText = @"DELETE FROM specialties WHERE id = @SpecialtyId;";
        MySqlParameter specialtyIdParameter = new MySqlParameter();
        specialtyIdParameter.ParameterName = "@SpecialtyId";
        specialtyIdParameter.Value = _id;
        cmd.Parameters.Add(specialtyIdParameter);
        cmd.ExecuteNonQuery();
        if (conn != null)
        {
          conn.Close();
        }
      }

      public List<Stylist> GetStylists()
         {
          List<Stylist> allStylists = new List<Stylist> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM stylist_specialties WHERE specialty_id=@SpecialtyId;";
          MySqlParameter SpecialtyId = new MySqlParameter();
          SpecialtyId.ParameterName = "@StylistId";
          SpecialtyId.Value = this._id;
          cmd.Parameters.Add(SpecialtyId);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int stylist_id = rdr.GetInt32(1);
            allStylists.Add(Stylist.Find(stylist_id));
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return allStylists;
        }


      public void AddStylist(Stylist stylist)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();

          MySqlCommand cmd = conn.CreateCommand();
          cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

          MySqlParameter StylistId = new MySqlParameter();
          StylistId.ParameterName = "@StylistId";
          StylistId.Value = this._id;
          cmd.Parameters.Add(StylistId);
          MySqlParameter SpecialtyId = new MySqlParameter();
          SpecialtyId.ParameterName = "@SpecialtyId";
          SpecialtyId.Value = this._id;
          cmd.Parameters.Add(SpecialtyId);

          cmd.ExecuteNonQuery();

          conn.Close();
          if(conn != null) conn.Dispose();
      }



  }
}
