using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
  public class Animal
  {
    private int _id;
    private string _name;
    private int _categoryId;
    //private bool _gender;
    //private int _date;
    //private string _breed;

    public Animal(string name, int categoryId, int id = 0)//, bool gender, int date, string breed)
    {
      _id = id;
      _name = name;
      _categoryId = categoryId;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetTypeId()
    {
      return _categoryId;
    }
    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int AnimalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        Console.WriteLine("TEST");
        int speciesId = rdr.GetInt32(2);
        Animal newAnimal = new Animal(animalName, speciesId, AnimalId);
        allAnimals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAnimals;
    }
    public override bool Equals(System.Object otherAnimal)
    {
      if (!(otherAnimal is Animal))
      {
        return false;
      }
      else
      {
        Animal newAnimal = (Animal) otherAnimal;
        bool idEquality = (this.GetId() == newAnimal.GetId());
        bool animalEquality = (this.GetName() == newAnimal.GetName());
        bool typeEquality = this.GetTypeId() == newAnimal.GetTypeId();
        return (idEquality && animalEquality && typeEquality);
      }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO animal (name, categoryID) OUTPUT INSERTED.id VALUES (@AnimalName, @TypeID)", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@AnimalName";
      nameParameter.Value = this.GetName();


      SqlParameter TypeIdParameter = new SqlParameter();
      TypeIdParameter.ParameterName = "@TypeID";
      TypeIdParameter.Value = this.GetTypeId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(TypeIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animal;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
    public static Animal Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE id = @AnimalId;", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@AnimalId";
      taskIdParameter.Value = id.ToString();
      cmd.Parameters.Add(taskIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundAnimalId = 0;
      string foundAnimalDescription = null;
      int foundTypeCategoryId = 0;
      while(rdr.Read())
      {
        foundAnimalId = rdr.GetInt32(0);
        foundAnimalDescription = rdr.GetString(1);
        foundTypeCategoryId = rdr.GetInt32(2);
      }
      Animal foundAnimal = new Animal(foundAnimalDescription, foundAnimalId, foundTypeCategoryId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundAnimal;
    }
  }
}
