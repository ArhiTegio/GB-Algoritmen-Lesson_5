using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;
using System.Reflection;

namespace GB_Algoritmen_Lesson_5
{
    class Program
    {
        static Dictionary<string, Act> dict = new Dictionary<string, Act>
        {
            { "1", new Act },
        };

        static void Main(string[] args)
        {
            var ex = new Extension();
            var q = new Questions();
            var n = "";
            WriteLine("С# - Алгоритмы и структуры данных. Задание 5.");
            WriteLine("Кузнецов");
            while (n != "0")
            {
                WriteLine("Введите номер интересующей вас задачи:" + Environment.NewLine +
                    "1.	Реализовать перевод из десятичной в двоичную систему счисления с использованием стека." + Environment.NewLine +
                    "2. Добавить в программу «Реализация стека на основе односвязного списка» проверку на выделение памяти.Если память не выделяется, то должно выводиться соответствующее сообщение.Постарайтесь создать ситуацию, когда память не будет выделяться(добавлением большого количества данных)." + Environment.NewLine +
                    "3. Написать программу, которая определяет, является ли введённая скобочная последовательность правильной.Примеры правильных скобочных выражений – (), ([])(), { } (), ([{ }]), неправильных – )(, ())({), (, ])}), ([(]), для скобок – [, (, {." + Environment.NewLine +
                    "        Например: (2 + (2 * 2)) или[2 /{5 * (4 + 7)}]." + Environment.NewLine +
                    "4.	* Создать функцию, копирующую односвязный список(то есть создающую в памяти копию односвязного списка без удаления первого списка)." + Environment.NewLine +
                    "5.	* Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную." + Environment.NewLine +
                    "6.	Реализовать очередь:" + Environment.NewLine +
                    "1. С использованием массива." + Environment.NewLine +
                    "2. * С использованием односвязного списка." + Environment.NewLine +
                    "7.	* Реализовать двустороннюю очередь." + Environment.NewLine +
                    "0. Нажмите для выхода из программы.");

                n = q.Question<int>("Введите ", new HashSet<char>() { '0', '1', '2', '3', '4' }, true);
                if (n == "0") break;
                dict[n].Work();
            }

            Console.ReadKey();
        }
    }

    abstract class Act
    {
        public abstract void Work();
    }
}
