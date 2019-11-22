using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GB_Algoritmen_Lesson_5
{
    public class PostfixNotationExpression
    {
        public PostfixNotationExpression()
        {
            operators = new List<string>(operatorsList);
        }

        private List<string> operators;
        private List<string> operatorsList = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^" });
        private Dictionary<string, byte> oper = new Dictionary<string, byte>() { { "(", 0 }, { ")", 1 }, { "+", 2 }, { "-", 3 }, { "*", 4 }, { "/", 5 }, { "^", 6 }, };

        private IEnumerable<string> Separate(string input)
        {
            List<string> inputSeparated = new List<string>();
            int pos = 0;
            while (pos < input.Length)
            {
                string s = string.Empty + input[pos];
                if (!operatorsList.Contains(input[pos].ToString()))
                {
                    if (Char.IsDigit(input[pos]))
                        for (int i = pos + 1; i < input.Length && (Char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++) s += input[i];
                    else if (Char.IsLetter(input[pos]))
                        for (int i = pos + 1; i < input.Length && (Char.IsLetter(input[i]) || Char.IsDigit(input[i])); i++) s += input[i];
                }
                yield return s;
                pos += s.Length;
            }
        }

        private byte GetPriority(string s)
        {
            byte t; 
            if (oper.TryGetValue(s, out t)) return t;
            else return 6;
        }

        public string ConvertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Separate(input))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) >= GetPriority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) < GetPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                    outputSeparated.Add(c);
            }

            while(stack.Count !=0)            
                outputSeparated.Add(stack.Pop());            

            return outputSeparated.Aggregate((x, y) => $"{x} {y}");
        }
    }
}
