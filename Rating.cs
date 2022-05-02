using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Extension;

namespace ral.rab._3.LINQ;

public class Rating
{
    private Table TablePrinter = new(new[] { 5, 25, 10, 18 });

    public List<Teacher> teachers { get; set; } = new();
    Dictionary<string, ConsoleColor> services = new();

    private delegate bool IsCanAddTeacher(string name);
    private IsCanAddTeacher isCanAddTeacher;

    public Rating()
    {
        teachers.Add(new Teacher() { FIO = "Демченко Ольга Сергеевна", Institute = "ИУБП", VideoService = "Zoom" });
        teachers.Add(new Teacher() { FIO = "Ликсонова Дарья Игоревна", Institute = "ИКИТ", VideoService = "Discord" });
        teachers.Add(new Teacher() { FIO = "Бакшеев Андрей Иванович", Institute = "ГИ", VideoService = "Zoom" });
        teachers.Add(new Teacher() { FIO = "Чубарова Олеся Викторовна", Institute = "ИКИТ", VideoService = "WebinarSFU" });

        services.Add("Discord", ConsoleColor.DarkMagenta);
        services.Add("Zoom", ConsoleColor.Cyan);
        services.Add("WebinarSFU", ConsoleColor.DarkYellow);

        isCanAddTeacher = (name) =>
        {
            foreach (Teacher teacher in teachers)
            {
                if (teacher.FIO == name) return false;
            }
            return true;
        };
    }
    public void Start()
    {
        bool stop = false;
        while (!stop)
        {
            PrintTeachers();
            PrintTop();

            ConsoleColor backcolor = Console.BackgroundColor;
            ConsoleColor forecolor = Console.ForegroundColor;   
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            
            while (true)
            {
                Console.WriteLine("\nНажмите А что-бы добавить преподователя");
                Console.WriteLine("Нажмите Enter что-бы завершить работу программы");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.A)
                {
                    AddTeacher();
                    break;
                }
                if (key == ConsoleKey.Enter)
                {
                    stop = true;
                    break;
                }
            }
            Console.BackgroundColor = backcolor;
            Console.ForegroundColor = forecolor;
        }
    }

    public void PrintTeachers()
    {
        TablePrinter.PrintHeader("Преподаватели");
        for (int i = 0; i < teachers.Count; i++)
        {
            TablePrinter.PrintRow(new string[]
            {
                    (i + 1).ToString(),
                    teachers[i].FIO.ConvertToShortForm(),
                    teachers[i].Institute,
                    teachers[i].VideoService
            }, services[teachers[i].VideoService]);
        }
        TablePrinter.PrintFooter();
    }

    public void PrintTop()
    {
        Console.WriteLine();
        TablePrinter.PrintHeader("Топ 3");
        var top = teachers.GroupBy(teacher => teacher.VideoService,
            (name, enume) => new
            {
                Key = name,                 //последовательность имён
                Count = enume.Count()       //последовательность кол-ва использований
            }).OrderBy(arg => arg.Count).Reverse().Take(3).ToList();

        for (var i = 0; i < top.Count; i++)
        {
            Console.Write($"{i + 1} - ");
            Console.ForegroundColor = services[top[i].Key];
            Console.Write("{0,-13}", "[" + top[i].Key + "]"); //форматирование ширины столбца
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Количество использований: {top[i].Count}");
        }
        TablePrinter.PrintFooter();
    }

    public void AddTeacher()
    {
        Console.WriteLine("\nВведите ФИО:");
        string fio;
        while (true)
        {
            fio = Console.ReadLine() ?? String.Empty;
            if (!Regex.IsMatch(fio, "[А-Я][а-я]{1,20}\\s[А-Я][а-я]{1,20}\\s[А-Я][а-я]{1,20}"))
            {
                Console.WriteLine("Введите ФИО, согласно стандарту: \"Иванов Иван Иванович\"");
                continue;
            }
            if (!isCanAddTeacher(fio))
            {
                Console.WriteLine("Преподаватель с данным ФИО уже есть в списке, введите другое ФИО");
            }
            else
                break;
        }

        Console.WriteLine("Введите название института (аббревиатуру):");
        string inst;
        while (true)
        {
            inst = Console.ReadLine() ?? "";
            if (!Regex.IsMatch(inst, "[А-Я][а-я]{0,1}[А-Я]{1,10}[а-я]{0,1}[А-Я]{0,10}"))
            {
                Console.WriteLine("Неправильно ввели аббревиатуру, исправьтесь:");
            }
            else
                break;
        }  

        Console.WriteLine("Выберете сервис, которым пользуется преподаватель:");
        for (var i = 0; i < services.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {services.ElementAt(i).Key}");
        }

        string service = services.ElementAt(pr.ReadLineOfRange(1, services.Count) - 1).Key;

        teachers.Add(new Teacher() { FIO = fio, Institute = inst, VideoService = service });
            Console.WriteLine("Данные успешно добавлены!");
    }

}
