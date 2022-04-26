using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ral.rab._3.LINQ
{
    public class pr
    {
        /// <summary>
        /// Определяет ввели ли с клавиатуры число, принадлежащее промежутку [f,s]
        /// </summary>
        /// <param name="f">от</param>
        /// <param name="s">до</param>
        /// <returns>введенное число</returns>
        public static int ReadLineOfRange(int f, int s)
        {
            while (true)
            {
                string? line = Console.ReadLine();
                if (line != null && Regex.IsMatch(line, "[0-9]"))
                {
                    int i = Convert.ToInt32(line);
                    if (f <= i && i <= s)
                    {
                        return i;
                    }
                }
                Console.WriteLine($"Введите челое число от {f} до {s}");
            }
        }
    }
}
namespace Extension
{
    static class MyExtensions
    {
        /// <summary>
        /// Имя и отчество превращает в инициалы
        /// </summary>
        /// <param name="fio">ФИО</param>
        public static string ConvertToShortForm(this string fio)
        {
            string[] fioSplit = fio.Split(" ");
            return fio = fioSplit[0] + new string(Convert.ToChar(" "), 14 - fioSplit[0].Length) + fioSplit[1].Substring(0, 1) + "." + fioSplit[2].Substring(0, 1) + ".";
        }
    }
}
