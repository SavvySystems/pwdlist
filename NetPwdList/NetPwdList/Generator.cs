using NetPwdList.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPwdList
{
  public class Generator : IGenerator
  {
    private int _pwdLength = 8;

    private char[] _alphabetSpace = null;
    public char[] AlphabetSpace
    {
      get
      {
        if (null == _alphabetSpace)
        {
          //48-57 = 0..9
          //65-90? = A..Z
          var list = new List<char>();
          for (int i = 48; i <= 57; i++)
            list.Add((char)i);
          for (int i = 65; i <= 90; i++)
            list.Add((char)i);
          _alphabetSpace = list.ToArray();
        }
        return _alphabetSpace;
      }
    }

    public int[] Registers { get; protected set; }

    public Generator(int pwdLength)
    {
      _pwdLength = pwdLength;
      Registers = new int[_pwdLength];
    }
    

    public string GetNextPwd()
    {
      bool isOverflow = false;
      IncrementRegisters(out isOverflow);
      if (isOverflow)
        return string.Empty;
      //Else
      return GetPwd();
    }

    public string GetPwd()
    {
      StringBuilder sb = new StringBuilder();
      foreach (var reg in Registers)
      {
        sb.Append(AlphabetSpace[reg]);
      }
      return sb.ToString();
    }

    public void IncrementRegisters(out bool isOverflow)
    {
      IncrementRegister(Registers, _pwdLength - 1, AlphabetSpace.Count() - 1, out isOverflow);
    }

    public static int[] IncrementRegister(int[] registers, int pos, int maxRegisterValue, out bool isOverflow)
    {
      isOverflow = false;
      var newValue = registers[pos] + 1;
      if (newValue <= maxRegisterValue)
      {
        registers[pos] = newValue;

        //Check for double chars
        var posOfDoubleChar = FindPositionOfDoubleChar(registers);
        if (posOfDoubleChar > -1)
        {
          //Double char found so reset less significant registers to 0 and increment the double char
          for (int i = posOfDoubleChar + 1; i < registers.Length; i++)
            registers[i] = 0;
          return IncrementRegister(registers, posOfDoubleChar, maxRegisterValue, out isOverflow);
        }
      }
      else//"clocked" the register so set 0 and carry 1 to next register
      {
        registers[pos] = 0;
        if (pos > 0)
        {
          return IncrementRegister(registers, pos - 1, maxRegisterValue, out isOverflow);
        }
        else
        {
          isOverflow = true;
        }
      }
      return registers;
    }

    public static int FindPositionOfDoubleChar(int[] registers)
    {
      int lastRegister = registers[0];
      for (int i = 1; i < registers.Length; i++)
      {
        if (registers[i] == lastRegister)
          return i;
        lastRegister = registers[i];
      }
      return -1;//No double chars found
    }

  }
}
