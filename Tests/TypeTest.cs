using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class TypeTest : IDisposable
  {
    public TypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_TypeEmptyAtFirst()
    {
      //Arrange, Act
      int result = Type.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Type firstType = new Type("Household chores", 1);
      Type secondType = new Type("Household chores", 1);

      //Assert
      Assert.Equal(firstType, secondType);
    }

    [Fact]
    public void Test_Save_SavesTypeToDatabase()
    {
      //Arrange
      Type testType = new Type("test", 1);
      testType.Save();

      //Act
      List<Type> result = Type.GetAll();
      List<Type> testList = new List<Type>{testType};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToTypeObject()
    {
      //Arrange
      Type testType = new Type("Household chores", 1);
      testType.Save();

      //Act
      Type savedType = Type.GetAll()[0];

      int result = savedType.GetId();
      int testId = testType.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsTypeInDatabase()
    {
      //Arrange
      Type testType = new Type("Household chores", 1);
      testType.Save();

      //Act
      Type foundType = Type.Find(testType.GetId());

      //Assert
      Assert.Equal(testType, foundType);
    }


    public void Dispose()
    {
      Animal.DeleteAll();
      Type.DeleteAll();
    }
    
  }
}
