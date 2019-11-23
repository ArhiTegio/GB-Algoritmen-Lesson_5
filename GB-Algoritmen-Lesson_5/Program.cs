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
            { "2", new TestNewStack() },
            { "3", new Brackets() },
            { "4", new CloneStack() },
            { "5", new FromInfixToPostfix() },
            { "6", new TestNewQueue() },
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
                    "2. Добавить в программу «Реализация стека на основе односвязного списка» проверку на выделение памяти. Если память не выделяется, то должно выводиться соответствующее сообщение. Постарайтесь создать ситуацию, когда память не будет выделяться(добавлением большого количества данных)." + Environment.NewLine +
                    "3. Написать программу, которая определяет, является ли введённая скобочная последовательность правильной. Примеры правильных скобочных выражений – (), ([])(), { } (), ([{ }]), неправильных – )(, ())({), (, ])}), ([(]), для скобок – [, (, {." + Environment.NewLine +
                    "        Например: (2 + (2 * 2)) или[2 /{5 * (4 + 7)}]." + Environment.NewLine +
                    "4.	* Создать функцию, копирующую односвязный список(то есть создающую в памяти копию односвязного списка без удаления первого списка)." + Environment.NewLine +
                    "5.	** Реализовать алгоритм перевода из инфиксной записи арифметического выражения в постфиксную." + Environment.NewLine +
                    "6. * Реализовать очередь" + Environment.NewLine +
                    "0. Нажмите для выхода из программы.");

                n = q.Question<int>("Введите ", new HashSet<char>() { '0', '1', '2', '3', '4', '5', '6' }, true);
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
                if (t <= number) { answer = '1'; number -= t; sum += t; }
                step--;
                if(sum > 0) stack.Push(answer);
                if (number <= 0 && step < 0) break;                
            }
        }
    }

    class Brackets : Act
    {
        List<string> list = new List<string>()
        {
            "()", "([])()", "{ } ()", "([{ }])",
            ")(", "()) ({)", "(", "])})", "([(])",
            "(2 + (2 * 2))", "[2 /{5 * (4 + 7)}]", 
        };

        public override void Work()
        {
            var c = new CheckBrackets();

            foreach (var e in list)            
                WriteLine($"Cкобочное последовательность {e}: {c.Check(e)}");
            
            WriteLine($"Ответ: { (new CheckBrackets()).Check((new Questions()).Question<string>("Введите выражение:", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '+', '-','*', '/', '{', '}', '[', ']', '(', ')' }, true)) }");
          
        }
    }

    class CloneStack : Act
    {
        MyStack<int> stack1 = new MyStack<int>();
        Random r = new Random();
        public override void Work()
        {
            var n = r.Next(5, 10);
            for (int i = 0; i < n; ++i)            
                stack1.Push(r.Next(0,255));
            WriteLine("");
            foreach (var e in stack1) Write($"{e}, ");      
            
            var stack2 = stack1.Clone();
            stack2.Push(0);
            WriteLine("");
            foreach (var e in stack2) Write($"{e}, ");
            WriteLine("");
        }
    }

    class TestQueue : Act
    {
        
        MyStack<int> stack1 = new MyStack<int>();
        Random r = new Random();
        public override void Work()
        {
            var n = r.Next(5, 10);
            for (int i = 0; i < n; ++i)
                stack1.Push(r.Next(0, 255));
            WriteLine("");
            foreach (var e in stack1) Write($"{e}, ");

            var stack2 = stack1.Clone();
            stack2.Push(0);
            WriteLine("");
            foreach (var e in stack2) Write($"{e}, ");
            WriteLine("");
        }
    }

    class FromInfixToPostfix : Act
    {
        
        public override void Work()
        {
            var fix = new PostfixNotationExpression();
            WriteLine( $"{ fix.ConvertToPostfixNotation((new Questions()).Question<string>("Введите выражение:", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '+', '-', '*', '/', '{', '}', '[', ']', '(', ')' }, true)) }");            
        }
    }



    class CheckBrackets 
    {
        Dictionary<char, char> dict = new Dictionary<char, char>() { { ']', '[' }, { ')', '(' }, { '}', '{' }, };
        Stack<char> stack = new Stack<char>();
        public bool Check(string text)
        {
            foreach (var e in text)
            {
                if (e == '[' || e == '(' || e == '{') stack.Push(e);
                if (e == ']' || e == ')' || e == '}') if (stack.Count == 0 || stack.Pop() != dict[e]) return false;                
            }
            return true;
        }
    }

    class TestNewStack : Act
    {
        public override void Work()
        {
            var count = 0;
            var stack = new MyStack<int>();
            var r = new Random();
            try
            {
                while (true)
                {
                    stack.Push(r.Next());
                    count++;
                }
            }
            catch
            {
            }
            WriteLine($"Память закончилась! Записано {count} значений.");
        }
    }

    class TestNewQueue : Act
    {
        MyQueue<int> queue1 = new MyQueue<int>();
        Random r = new Random();
        public override void Work()
        {
            var n = r.Next(5, 10);
            WriteLine("");
            for (int i = 0; i < n; ++i)
            {
                var t = r.Next(0, 255);
                Write($"{t}, ");
                queue1.Enqueue(t);
            }
            WriteLine("");
            while (queue1.Count != 0)        
                Write($"{queue1.Dequeue()}, ");
            WriteLine("");
        }
    }

    // Очередь
    public class MyQueue<T>
    {
        private Node head;
        private Node tail;
        private int count;

        public int Count { get => count; }

        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            //Конструктор
            public Node(T value)
            {
                Value = value;
                Next = null;
            }

        }

        public MyQueue<T> Clone()
        {
            var ms = new MyQueue<T>();
            var s = new Stack<T>();
            foreach (var e in this) s.Push(e);
            foreach (var e in s) ms.Enqueue(e);
            return ms;
        }

        public void Enqueue(T data)
        {
            Node node = new Node(data);
            if (Count == 0)
            {
                head = node;
                tail = new Node(data);
                tail.Previous = head;
                head.Next = tail;
                count++;
            }
            else
            {
                node.Next = head;
                head.Previous = node;
                head = node;
                count++;
            }
        }

        public T Dequeue()
        {
            var node = tail.Previous;
            tail = tail.Previous;
            count--;
            if (Count == 0)
            {
                tail = null;
                head = null;
            }

            return node.Value;
        }

        //Перечеслитель
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }


    // Односвязный список
    public class MyStack<T>
    {
        private Node head;
        private Node tail;
        private int count;

        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }

            //Конструктор
            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        public MyStack<T> Clone()
        {
            var ms = new MyStack<T>();
            var s = new Stack<T>();
            foreach(var e in this)
                s.Push(e);
            foreach (var e in s)
                ms.Push(e);
            return ms;
        }

        public void Push(T data)
        {
            try
            {
                Node node = new Node(data);
                node.Next = head;
                head = node;
                if (count == 0) tail = head;
                count++;
            }
            catch
            {
                WriteLine("Память закончилась!");
                throw new Exception();
            }
        }

        public T Pop()
        {
            var node = head;
            head = head.Next;
            count--;
            if (count == 0) tail = head;
            return node.Value;
        }

        //Перечеслитель
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}