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
            { "1", new DecimaalToBinair() },
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

                n = q.Question<int>("Введите ", new HashSet<char>() { '0', '1', }, true);
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

    class DecimaalToBinair : Act
    {
        Stack<char> stack = new Stack<char>();

        public override void Work() => 
            WriteLine($"Число в двоичной системе исчисления: { ToBinary(int.Parse((new Questions()).Question<int>("Введите число:", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }, true))) }");

        public string ToBinary(int num)
        {
            Convert(num);
            return new string(stack.Select(x => x).Reverse().ToArray());
        }

        private void Convert(int number)
        {
            stack = new Stack<char>();
            var step = (int)Math.Sqrt(number);
            var t = 0;
            char answer = '0';
            var sum = 0;
            while (true)
            {
                answer = '0';
                t = (int)Math.Pow(2, step);
                WriteLine($"{t.ToString()}");
                if (t <= number) { answer = '1'; number -= t; sum += t; }
                step--;
                if(sum > 0) stack.Push(answer);
                if (number <= 0 && step < 0) break;                
            }
        }
    }


    class MyStack
    {
        MyStack head;
        MyStack teal;


    }
}
