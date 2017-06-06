using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalTest : IDisposable
  {
    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, ACT
      int result = Animal.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_IfNameSame_True()
    {
      Animal firstAnimal = new Animal("sparky", 1);
      Animal secondAnimal = new Animal("sparky", 1);

      Assert.Equal(firstAnimal, secondAnimal);
    }
    [Fact]
    public void Test_Save_SavesToDataBase()
    {
      Animal testAnimal = new Animal("sparky", 1);
      testAnimal.Save();

      List<Animal> result = Animal.GetAll();
      List<Animal> testList = new List<Animal>{testAnimal};

      Assert.Equal(testList, result);
    }
    // [Fact]
    // public void Test_Find_FindsAnimalInDatabase()
    // {
    //   //Arrange
    //   Animal testAnimal = new Animal("sparky", 1);
    //   testAnimal.Save();
    //
    //   //Act
    //   Animal foundAnimal = Animal.Find(testAnimal.GetId());
    //
    //   //Assert
    //   Assert.Equal(testAnimal, foundAnimal);
    // }
    public void Dispose()
    {
      Animal.DeleteAll();
    }
  }
}
