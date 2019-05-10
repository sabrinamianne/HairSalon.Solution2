using System.Collection.Generic;
using MySql.Data.MySqlCLient;

namespace HairSalon.Models
{
  public class Client
  {
    private string _name;
    private int _id;
    private int _stylistId;

    public client (string name, int id=0, int stylistId)
    {
      _name = name;
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

    public void SetCLienttName(string newName)
    {
      _name = newName;
    }




  }
}
