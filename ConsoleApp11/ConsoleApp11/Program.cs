using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp11
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            double result = Calculator.EvaluateExpression(input);
            Console.WriteLine(result);
        }
    }

    public class Calculator
    {
        public static double EvaluateExpression(string input)
        {
            string pattern = @"(-?\d+\.?\d*)|(\+|\-|\*|\/)";
            string[] parts = Regex.Split(input, pattern);
            List<string> list = new List<string>(parts);
            list.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            // Словарь операций
            Dictionary<string, ISignature> dict = new Dictionary<string, ISignature>()
            {
                {"-", new Minus()},
                {"+", new Plus()},
                {"*", new Multiplication()},
                {"/", new Division()}
            };

            PerformOperations(list, dict, new string[] { "*", "/" });

            PerformOperations(list, dict, new string[] { "+", "-" });

            return double.Parse(list[0]);
        }

        private static void PerformOperations(List<string> list, Dictionary<string, ISignature> dict, string[] operations)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Array.Exists(operations, op => op == list[i]))
                {
                    double a = double.Parse(list[i - 1]);
                    double b = double.Parse(list[i + 1]);
                    Context context = new Context(dict[list[i]]);
                    double result = context.ExecuteOperation(a, b);
                    list[i - 1] = result.ToString();
                    list.RemoveRange(i, 2); 
                    i--; 
                }
            }
        }
    }

    public interface ISignature
    {
        double Sign(double a, double b);
    }

    public class Plus : ISignature
    {
        public double Sign(double a, double b) => a + b;
    }

    public class Minus : ISignature
    {
        public double Sign(double a, double b) => a - b;
    }

    public class Multiplication : ISignature
    {
        public double Sign(double a, double b) => a * b;
    }

    public class Division : ISignature
    {
        public double Sign(double a, double b) => a / b;
    }

    public class Context
    {
        private ISignature _oper;

        public Context(ISignature oper)
        {
            _oper = oper;
        }

        public ISignature Operation
        {
            set { _oper = value; }
        }

        public double ExecuteOperation(double a, double b) => _oper.Sign(a, b);
    }
}