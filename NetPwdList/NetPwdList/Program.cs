using System.Collections.Generic;
using System.IO;

namespace NetPwdList
{
  class Program
  {
    static void Main(string[] args)
    {
      int pwdLength = 8;
      int maxFileLength = 1000000;
      var generator = new Generator(pwdLength);

      var pwd = generator.GetNextPwd();
      while (!string.IsNullOrWhiteSpace(pwd))
      {
        var filename = string.Format("pwd_{0}.lst", pwd);
        var lineBuffer = new List<string>();
        for (int i = 0; i < maxFileLength && !string.IsNullOrWhiteSpace(pwd); i++)
        {
          lineBuffer.Add(pwd);
          pwd = generator.GetNextPwd();
        }

        File.WriteAllLines(filename, lineBuffer);
      }
    }

  }
}
