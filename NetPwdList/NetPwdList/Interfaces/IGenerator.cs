using System.Collections.Generic;

namespace NetPwdList.Interfaces
{
  public interface IGenerator
  {
    char[] AlphabetSpace { get; }
    int[] Registers { get; }
    string GetNextPwd();
    string GetPwd();
    void IncrementRegisters(out bool isOverflow);
  }
}
