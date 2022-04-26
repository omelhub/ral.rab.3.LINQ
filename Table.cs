using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ral.rab._3.LINQ;

public class Table
{
    private int[] collums;
    private int width;

    /// <summary>
    /// Задаем размеры таблицы
    /// </summary>
    /// <param name="collums">кол-во элементов массива = кол-ву столбцов; значение элемента = ширине столбца</param>
    public Table(int[] collums)
    {
        this.collums = collums;
        foreach (int collum in collums)
        {
            width += collum;
        }
    }

    /// <summary>
    /// Печатает строку таблицы()
    /// </summary>
    /// <param name="strs">Массив не более, чем из 4 строк</param>
    public void PrintRow(string[] strs, ConsoleColor color)
    {
        for (int i = 0; i < strs.Length; i++)
        {
            if (i == 4) continue; //ограничение по кол-ву выводимых столбцов с данными
            if (i == 3)
            {
                Console.ForegroundColor = color;
                Console.Write("[" + strs[i] + "]" + new string(Convert.ToChar(" "), collums[i] - strs[i].Length));
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                Console.Write(strs[i] + new string(Convert.ToChar(" "), collums[i] - strs[i].Length));
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Печатает заголовок()
    /// </summary>
    /// <param name="head">Заголовок</param>
    public void PrintHeader(string head)
    {
        if (width <= head.Length)
        {
            Console.WriteLine(head);
            width = head.Length;
            return;
        }
        if ((width - head.Length) % 2 != 0)
            width += 1;
        int space = (width - head.Length) / 2;
        Console.WriteLine(new string(Convert.ToChar("="), space) + head + new string(Convert.ToChar("="), space));
    }

    /// <summary>
    /// Печатает нижний контитул таблицы (пунктирную линию)
    /// </summary>
    public void PrintFooter()
    {
        Console.WriteLine(new string(Convert.ToChar("-"), width));
    }

}
