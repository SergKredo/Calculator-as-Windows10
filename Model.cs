using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Model
    {
        // Метод, производит операцию сложения двух числовых переменных
        public string Addition(string s1, string s2)
        {
            double number1 = Convert.ToDouble(s1);
            double number2 = Convert.ToDouble(s2);
            double result = number1 + number2;
            string r = Convert.ToString(result);
            if (r.Length > 12)
            {
                result = Math.Round(result, 6);
                r = Convert.ToString(result);
                if (!(r.Length >= 12))
                {
                    return r.Substring(0, r.Length);
                }
                else
                {
                    return r.Substring(0, 12);
                }
            }
            else
            {
                return r;
            }
        }

        // Метод, производит операцию вычитания двух числовых переменных
        public string Subtraction(string s1, string s2)
        {
            double number1 = Convert.ToDouble(s1);
            double number2 = Convert.ToDouble(s2);
            double result = number1 - number2;
            string r = Convert.ToString(result);
            if (r.Length > 12)
            {
                result = Math.Round(result, 6);
                r = Convert.ToString(result);
                if (!(r.Length >= 12))
                {
                    return r.Substring(0, r.Length);
                }
                else
                {
                    return r.Substring(0, 12);
                }
            }
            else
            {
                return r;
            }
        }

        // Метод, производит операцию умножения двух числовых переменных
        public string Multiplication(string s1, string s2)
        {
            double number1 = Convert.ToDouble(s1);
            double number2 = Convert.ToDouble(s2);
            double result = number1 * number2;
            string r = Convert.ToString(result);
            if (r.Length > 12)
            {
                result = Math.Round(result, 6);
                r = Convert.ToString(result);
                if (!(r.Length >= 12))
                {
                    return r.Substring(0, r.Length);
                }
                else
                {
                    return r.Substring(0, 12);
                }
            }
            else
            {
                return r;
            }

        }

        // Метод, производит операцию деления двух числовых переменных
        public string Division(string s1, string s2)
        {
            double number1 = Convert.ToDouble(s1);
            double number2 = Convert.ToDouble(s2);
            if (number2 == 0)
            {
                return null;
            }
            else
            {
                double result = number1 / number2;
                string r = Convert.ToString(result);
                if (r.Length > 12)
                {
                    result = Math.Round(result, 6);
                    r = Convert.ToString(result);
                    if (!(r.Length >= 12))
                    {
                        return r.Substring(0, r.Length);
                    }
                    else
                    {
                        return r.Substring(0, 12);
                    }
                }
                else
                {
                    return r;
                }
            }
        }
    }
}
