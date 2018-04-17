using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var __calculator = new Calculator();
            while (true)
            {
                System.Console.WriteLine("Введите выражение или exit");
                var __input = System.Console.ReadLine();
                if (__input == "exit")
                    break;

                try
                {
                    var __result = __calculator.Calculate(__input);
                    System.Console.WriteLine(__result.ToString(CultureInfo.InvariantCulture));
                }
                catch (Exception __ex)
                {
                    var __color = System.Console.ForegroundColor;
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(__ex.Message);
                    System.Console.ForegroundColor = __color;
                }
            }
        }
    }
}
