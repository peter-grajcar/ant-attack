using System;
using System.IO;
using System.Text;

namespace AntAttack
{
    public class ConsoleUtil
    {
        public static void init()
        {
            var stdout = Console.OpenStandardOutput();
            var con = new StreamWriter(stdout, Encoding.ASCII);
            con.AutoFlush = true;
            Console.SetOut(con);
        }
        public static void Clear()
        {
            Console.WriteLine("\x1B[2J");
        }
    }
}