using NetPwdList;
using NetPwdList.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;

namespace NewPwdListTests
{
  [TestFixture]
  public class GeneratorTests
  {
    private IGenerator _generator;

    [SetUp]
    public void Setup()
    {
      var passwordLength = 8;
      _generator = new Generator(passwordLength);
    }

    public class AlphabetSpace : GeneratorTests
    {
      [Test]
      public void Given_Generator__Then_the_AlphabetSpaceSize_is_36()
      {
        //Arrange
        int expectedAlphabetSpaceSize = 36;

        //Act
        var alphabestSpace = _generator.AlphabetSpace;

        //Assert
        Assert.IsNotNull(alphabestSpace);
        Assert.AreEqual(expectedAlphabetSpaceSize, alphabestSpace.Count());
      }

      [Test]
      public void Given_Generator__Then_the_AlphabetSpaceSize_is_0_to_Z()
      {
        //Arrange
        char expectedFirstChar = '0';
        char expectedLastChar = 'Z';

        //Act
        var alphabestSpace = _generator.AlphabetSpace;

        //Assert
        Assert.IsNotNull(alphabestSpace);
        Assert.AreEqual(expectedFirstChar, alphabestSpace.First());
        Assert.AreEqual(expectedLastChar, alphabestSpace.Last());
      }
    }

    public class Register : GeneratorTests
    {
      [Test]
      public void Given_a_pwd_length__Then_a_CharRegisterList_of_that_size_is_returned()
      {
        //Arrange
        int expectedCharRegisterSize = 8;
        _generator = new Generator(expectedCharRegisterSize);

        //Act
        var charRegistersList = _generator.Registers;

        //Assert
        Assert.IsNotNull(charRegistersList);
        Assert.AreEqual(expectedCharRegisterSize, charRegistersList.Count());
      }

      [Test]
      public void Given_a_new_CharRegisterList__Then_it_is_all_0s()
      {
        //Arrange
        int expectedCharRegisterSize = 8;
        _generator = new Generator(expectedCharRegisterSize);

        //Act
        var charRegistersList = _generator.Registers;

        //Assert
        Assert.IsNotNull(charRegistersList);
        Assert.AreEqual(expectedCharRegisterSize, charRegistersList.Count(x => x == 0));
      }
    }

    public class GetPwd : GeneratorTests
    {
      [Test]
      public void Given_new_generator__Then_GetPwd_creates_000()
      {
        //Arrange
        string expectedPwd = "000";
        int pwdLength = 3;
        _generator = new Generator(pwdLength);

        //Act
        var pwd = _generator.GetPwd();

        //Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(pwd));
        Assert.AreEqual(expectedPwd, pwd);
      }
    }

    public class GetNextPwd : GeneratorTests
    {
      [Test]
      public void Given_new_generator__Then_GetNextPwd_creates_010()
      {
        //Arrange
        string expectedPwd = "010";
        int pwdLength = 3;
        _generator = new Generator(pwdLength);

        //Act
        var pwd = _generator.GetNextPwd();

        //Assert
        Assert.IsFalse(string.IsNullOrWhiteSpace(pwd));
        Assert.AreEqual(expectedPwd, pwd);
      }
    }
  }
}
