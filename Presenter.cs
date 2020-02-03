using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    class Presenter
    {
        int i = 0, t = 0, k = 1;
        double s = 0, p = 0, mPlusSum, mMinusSum;
        readonly Model model;
        readonly Form1 mainWindow;
        bool plusPlus = true;
        bool Operation = true;
        bool Signs = false;
        bool keyDelete = false;
        char signTwo, signOperation, literal;
        string number1, number12, numberOne, numberTwo, sign, alllabeltext, labelText, numberMore, numberNextPlus, sortText, beginWord, numberThree, mNumber;
        public Presenter(Form1 mainWindow)
        {
            this.mainWindow = mainWindow;
            this.model = new Model();
            this.mainWindow.EventOperation += MainWindow_Operation;
            this.mainWindow.EventMouseCancel += MainWindow_MouseCancel;
            this.mainWindow.EventFocusText += MainWindow_ReplaceBeginnerTextNull;
            this.mainWindow.EventKeyPress += MainWindow_KeyPress;
            this.mainWindow.EventKeyPressKey += MainWindow_KeyPressKey;
            this.mainWindow.EventRemoveText += MainWindow_RemoveText;
            this.mainWindow.EventRemoveTextTwo += MainWindow_RemoveTextTwo;
            this.mainWindow.EventAllNumbers += MainWindow_AllNumbers;
            this.mainWindow.MoreNumbersInTextBox += MainWindow_MoreNumbersInTextBox;
            this.mainWindow.EventSigns += MainWindow_Signs;
            this.mainWindow.EventWithOutSign += WithoutText;
            this.mainWindow.EventDelete += MainWindow_eventDelete;
            this.mainWindow.EventBackspace += Backspace;
            this.mainWindow.EventOnetoX += MainWindow_OnetoX;
            this.mainWindow.EventShareOf += MainWindow_ShareOf;
            this.mainWindow.EventSQRT += MainWindow_SQRT;
            this.mainWindow.EventDeleteCE += MainWindow_DeleteCE;
            this.mainWindow.EventMPlus += MainWindow_MPlus;
            this.mainWindow.EventMMinus += MainWindow_MMinus;
            this.mainWindow.EventMRead += MainWindow_MRead;
            this.mainWindow.EventMSave += MainWindow_MSave;
            this.mainWindow.EventMClear += MainWindow_MClear;
            this.mainWindow.EventOperationMC += MainWindow_OperationMC;
            this.mainWindow.EventInsertText += MainWindow_InsertText;
            this.mainWindow.EventPressingM += MainWindow_PressingM;
            this.mainWindow.EventBoolM += eventBoolM;
        }


        /* Меетод-обработчик события EventBoolM для textBoxKeyPress и TextBox1.
           Метод позволяет после нажатия на кнопки (M+, M-, MS, MR) заново вводить ряд чисел в TextBox1 (часть 1).
        */
        private void eventBoolM(object sender, EventArgs e)
        {
            if (this.mainWindow.label3.Text == "M" && t++ == 0)
            {
                ((TextBox)sender).Text = "\0\0\0";
                numberOne = null;
            }
            else if (k == 0)
            {
                k++;
                numberOne = null;
            }

        }


        /* Меетод-обработчик события EventPressingM для botton.
           Метод позволяет после нажатия на кнопки (M+, M-, MS, MR) заново вводить ряд чисел в TextBox1 (часть 2).
        */
        private void MainWindow_PressingM(object sender, EventArgs e)
        {
            beginWord = "0";
            ((TextBox)sender).Text = beginWord;
            i = 0;
            alllabeltext = null;
            this.mainWindow.label3.Text = "M";
            k = 0;
        }


        /* Меетод-обработчик события EventInsertText для InsertMenu.
           Метод позволяет вставить скопированный ранее ряд чисел из буфера обмена в TextBox1.
        */
        private void MainWindow_InsertText(object sender, EventArgs e)
        {
            beginWord = ((TextBox)sender).Text;
            this.mainWindow.textBox1.Text = beginWord;
            this.mainWindow.label1.Text = beginWord;
        }


        /* Меетод-обработчик события EventOperationMC для botton.
           Метод реализует возможность удаления  всех сохраненных данных из памяти калькулятора при нажатии на кнопки (M+, M-, MR, MS) в TextBox1 (часть1).
        */
        private void MainWindow_OperationMC(object sender, EventArgs e)
        {
            Operation = false;
        }


        /* Меетод-обработчик события EventMClearC для botton.
           Метод реализует возможность удаления всех сохраненных данных из памяти калькулятора при нажатии на кнопки (M+, M-, MR, MS) в TextBox1 (часть2).
        */
        private void MainWindow_MClear(object sender, EventArgs e)
        {
            beginWord = mNumber;
            ((TextBox)sender).Text = beginWord;
            this.mainWindow.textBox1.Text = beginWord;
            alllabeltext = beginWord;
            mNumber = "0";
            mMinusSum = Convert.ToDouble(mNumber);
            mPlusSum = mMinusSum;
            this.mainWindow.label1.Text = null;
            this.mainWindow.label3.Text = null;
            t = 0;
        }


        /* Меетод-обработчик события EventMSave для botton.
           Метод реализует возможность сохранения в памяти калькулятора всех ранее введенных пользователем данных при нажатии на кнопки (M+, M-, MR) в TextBox1.
        */
        private void MainWindow_MSave(object sender, EventArgs e)
        {
            mNumber = ((TextBox)sender).Text;
            alllabeltext = null;
            beginWord = mNumber;
            mMinusSum = Convert.ToDouble(mNumber);
            mPlusSum = mMinusSum;
            this.mainWindow.textBox1.Text = beginWord;
            ((TextBox)sender).Text = beginWord;
            this.mainWindow.label1.Text = null;
            t = 0;
        }


        /* Меетод-обработчик события EventMRead для botton.
           Метод реализует возможность вывода в TextBox1 из памяти калькулятора всех ранее введенных пользователем данных при нажатии на кнопки (M+, M-, MS).
        */
        private void MainWindow_MRead(object sender, EventArgs e)
        {
            alllabeltext = null;
            beginWord = mNumber;
            //mMinusSum = mNumber;
            //mPlusSum = mMinusSum;
            this.mainWindow.textBox1.Text = beginWord;
            ((TextBox)sender).Text = beginWord;
            this.mainWindow.label1.Text = alllabeltext;
            k = 0;
        }


        /* Меетод-обработчик события EventMMinus для botton.
           Метод реализует возможность вычитания введенных пользователем чисел в TextBox1 при нажатии на кнопку (M-).
        */
        private void MainWindow_MMinus(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(((TextBox)sender).Text)))
            {
                mNumber = ((TextBox)sender).Text;
                mMinusSum -= Convert.ToDouble(mNumber);
                mNumber = mMinusSum.ToString();
                if (mNumber.Length > 12)
                {
                    mMinusSum = Math.Round(mMinusSum, 6);
                    mNumber = Convert.ToString(mMinusSum);
                    if (!(mNumber.Length >= 12))
                    {
                        mNumber = mNumber.Substring(0, mNumber.Length);
                    }
                    else
                    {
                        mNumber = mNumber.Substring(0, 12);
                    }
                }
                mPlusSum = mMinusSum;
                this.mainWindow.label3.Text = "M";
            }
            else
            {
                alllabeltext = null;
                beginWord = "0";
                this.mainWindow.textBox1.Text = beginWord;
                ((TextBox)sender).Text = beginWord;
                ((KeyPressEventArgs)e).KeyChar = '0';
            }

        }

        /* Меетод-обработчик события EventMPlus для botton.
           Метод реализует возможность сложения введенных пользователем чисел в TextBox1 при нажатии на кнопку (M+).
        */
        private void MainWindow_MPlus(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(((TextBox)sender).Text)))
            {
                mNumber = ((TextBox)sender).Text;
                mPlusSum += Convert.ToDouble(mNumber);
                mNumber = mPlusSum.ToString();
                if (mNumber.Length > 12)
                {
                    mPlusSum = Math.Round(mPlusSum, 6);
                    mNumber = Convert.ToString(mPlusSum);
                    if (!(mNumber.Length >= 12))
                    {
                        mNumber = mNumber.Substring(0, mNumber.Length);
                    }
                    else
                    {
                        mNumber = mNumber.Substring(0, 12);
                    }
                }
                mMinusSum = mPlusSum;
                this.mainWindow.label3.Text = "M";
            }
            else
            {
                alllabeltext = null;
                beginWord = "0";
                this.mainWindow.textBox1.Text = beginWord;
                ((TextBox)sender).Text = beginWord;
                ((KeyPressEventArgs)e).KeyChar = '0';
            }

        }


        /* Меетод-обработчик события EventDeleteCE для botton.
           Метод реализует возможность частичного удаления введенных пользователем чисел в TextBox1 при нажатии на кнопку "CE".
        */
        private void MainWindow_DeleteCE(object sender, EventArgs e)
        {
            if (!(alllabeltext.Contains("+") || (alllabeltext.Contains("-") && !alllabeltext.StartsWith("-")) || alllabeltext.Contains("*") || alllabeltext.Contains("/")) && !(CountLiteralsFour('-') >= 2))
            {
                this.mainWindow.textBox1.Text = "0";
                this.mainWindow.label1.Text = "0";
                numberOne = "0";
                alllabeltext = "";
                signOperation = '\0';
                i = 0;
                keyDelete = false;
            }
            else
            {
                beginWord = "0";
                this.mainWindow.textBox1.Text = beginWord;
                this.mainWindow.label1.Text = alllabeltext.Substring(0, alllabeltext.LastIndexOf(signOperation) + 1);
                keyDelete = false;
            }
            this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
        }


        /* Меетод-обработчик события EventSQRT для botton.
           Метод реализует возможность взять корень квадратный от введенных пользователем чисел в TextBox1 при нажатии на кнопку "√".
        */
        private void MainWindow_SQRT(object sender, EventArgs e)
        {
            if (!(alllabeltext.EndsWith("+") || alllabeltext.EndsWith("-") || alllabeltext.EndsWith("*") || alllabeltext.EndsWith("/")) && !(alllabeltext.StartsWith("-")))
            {
                double numberOneS = Convert.ToDouble(alllabeltext);
                numberOneS = Math.Sqrt(numberOneS);
                numberOne = numberOneS.ToString();
                if (numberOne.Length > 12)
                {
                    numberOneS = Math.Round(numberOneS, 6);
                    numberOne = Convert.ToString(numberOneS);
                    if (!(numberOne.Length >= 12))
                    {
                        numberOne = numberOne.Substring(0, numberOne.Length);
                    }
                    else
                    {
                        numberOne = numberOne.Substring(0, 12);
                    }
                    alllabeltext = numberOne;
                    signOperation = '\0';
                }
                else if (numberOne == "не число")
                {
                    numberOne = "0";
                    i = 0;
                    alllabeltext = null;
                    signOperation = '\0';
                }
                else
                {
                    alllabeltext = numberOne;
                    signOperation = '\0';
                }
                beginWord = numberOne;
                this.mainWindow.textBox1.Text = numberOne;
                this.mainWindow.label1.Text = numberOne;
            }
            else
            {
                signOperation = '\0';
            }
        }


        /* Меетод-обработчик события EventShareOf для botton.
           Метод реализует возможность получить процентное соотношение от введенного пользователем числа в TextBox1 при нажатии на кнопку "%".
        */
        private void MainWindow_ShareOf(object sender, EventArgs e)
        {
            switch (signOperation)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    {
                        numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf(signOperation));
                        numberTwo = alllabeltext.Substring(alllabeltext.LastIndexOf(signOperation) + 1);
                        double numberOneN = Convert.ToDouble(numberOne);
                        double numberTwoN = Convert.ToDouble(numberTwo);
                        numberTwoN = (numberTwoN * 0.01) * numberOneN;
                        numberTwo = numberTwoN.ToString();
                        break;
                    }
            }
            beginWord = numberTwo;
            signTwo = signOperation;
            alllabeltext = numberOne + signOperation + numberTwo;
            this.mainWindow.textBox1.Text = numberTwo;
            this.mainWindow.label1.Text = alllabeltext;
        }

        /* Меетод-обработчик события EventOnetoX для botton.
           Метод реализует возможность взять "1/x", где x - введенное пользователем число в TextBox1 при нажатии на кнопку "1/x".
        */
        private void MainWindow_OnetoX(object sender, EventArgs e)
        {
            if (!(((KeyPressEventArgs)e).KeyChar == '\0'))
            {
                alllabeltext = ((TextBox)sender).Text;
                numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('/'));
                numberTwo = alllabeltext.Substring(alllabeltext.LastIndexOf('/') + 1);
                beginWord = numberTwo;
                signOperation = '/';
                signTwo = signOperation;
                this.mainWindow.textBox1.Text = numberTwo;
                this.mainWindow.label1.Text = alllabeltext;
            }
            else
            {
                signOperation = '\r';
                alllabeltext = ((TextBox)sender).Text;
                this.mainWindow.label1.Text = alllabeltext;
            }

        }


        /* Меетод-обработчик события EventBackspace для textBoxKeyPress и TextBox1.
           Метод реализует последовательное удаление литералов числовой строки в TextBox1 при нажатии на клавишу BackSpace на клавиатуре.
        */
        private void Backspace(object sender, KeyPressEventArgs e)
        {
            if (!(alllabeltext.Length == 1) && !(alllabeltext == "") && !(string.IsNullOrEmpty(this.mainWindow.label1.Text)))
            {
                if (!(signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/'))
                {
                    alllabeltext = alllabeltext.Remove(alllabeltext.Length - 1, 1);
                    beginWord = alllabeltext;
                    this.mainWindow.textBox1.Text = alllabeltext;
                    this.mainWindow.label1.Text = alllabeltext;
                }
                else if (numberOne == "-")
                {
                    alllabeltext = alllabeltext.Remove(alllabeltext.Length - 1, 1);
                    beginWord = alllabeltext;
                    this.mainWindow.textBox1.Text = alllabeltext;
                    this.mainWindow.label1.Text = alllabeltext;
                }
                else
                {
                    if (!(beginWord == signOperation.ToString()) && !(alllabeltext[alllabeltext.Length - 1] == signOperation))
                    {
                        int z;
                        beginWord = null;
                        char[] array = new char[alllabeltext.Length];
                        alllabeltext = alllabeltext.Remove(alllabeltext.Length - 1, 1);
                        for (z = 1; alllabeltext[alllabeltext.Length - z] != signOperation; z++)
                        {
                            beginWord += alllabeltext[alllabeltext.Length - z];
                            array[z - 1] = alllabeltext[alllabeltext.Length - z];
                        }
                        z--;
                        char[] arrayTwo = new char[z];
                        beginWord = null;
                        for (; arrayTwo.Length - z < arrayTwo.Length; z--)
                        {
                            arrayTwo[arrayTwo.Length - z] = array[z - 1];
                            beginWord += arrayTwo[arrayTwo.Length - z];
                        }

                        this.mainWindow.textBox1.Text = beginWord;
                        this.mainWindow.label1.Text = alllabeltext;
                        if (alllabeltext[alllabeltext.Length - 1] == signOperation)
                        {
                            beginWord = "0";
                            this.mainWindow.textBox1.Text = "0";
                        }
                    }
                    else
                    {
                        beginWord = "0";
                        this.mainWindow.textBox1.Text = beginWord;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                        return;
                    }
                }
            }
            else
            {
                if (!(string.IsNullOrEmpty(this.mainWindow.label1.Text)))
                {
                    beginWord = "0";
                    this.mainWindow.textBox1.Text = beginWord;
                    this.mainWindow.label1.Text = beginWord;
                    alllabeltext = "";
                    signOperation = '\0';
                    i = -1;
                }
                else
                {
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    return;
                }
            }
            this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
        }


        /* Меетод-обработчик события EventDelete для textBoxKeyPress и TextBox1.
           Метод реализует возможность полного удаления числовой строки в TextBox1 при нажатии на клавишу Delete на клавиатуре.
        */
        private void MainWindow_eventDelete(object sender, KeyPressEventArgs e)
        {
            keyDelete = true;
        }


        /* 
           Метод реализует возможность (часть 1):
             - запрещает ввод всех остальных значений клавиш клавиатуры, кроме: арифметических операторов (+, -, *, "/"), запятой, точки, цифр от (0..9);
             - запрещает ввод двух одинаковых последовательных литералов в числовой строке (,, или ..);
             - запрещает ввод двух разных последователльных литералов в числовой строке (,+ или *-);
             - запрещает ввод двух одинаковых, разных непоследовательных литералов в числовой строке ("0,123.178" или "352,+789");
        */
        public bool SignsOperation()
        {
            if (Signs == false)
            {
                return false;
            }
            else { return true; }
        }


        /* Меетод-обработчик события EventWithoutText для textBox1_TextChanged и TextBox1.
           Метод реализует возможность обнуления числовой строки в TextBox1.
        */
        public void WithoutText(object sender, EventArgs e)
        {
            if (keyDelete != true)
            {
                this.mainWindow.textBox1.Text = beginWord;
            }
            else
            {
                this.mainWindow.textBox1.Text = "0";
                this.mainWindow.label1.Text = "0";
                numberOne = "0";
                alllabeltext = "";
                signOperation = '\0';
                i = 0;
                keyDelete = false;
            }
            this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
        }


        /* 
           Метод-обработчик события EventSigns для textBoxKeyPress и TextBox1.
           Метод реализует возможность (часть 2):
             - запрещает ввод всех остальных значений клавиш клавиатуры, кроме: арифметических операторов (+, -, *, "/"), запятой, точки, цифр от (0..9);
             - запрещает ввод двух одинаковых последовательных литералов в числовой строке (,, или ..);
             - запрещает ввод двух разных последователльных литералов в числовой строке (,+ или *-);
             - запрещает ввод двух одинаковых, разных непоследовательных литералов в числовой строке ("0,123.178" или "352,+789");
        */
        private void MainWindow_Signs(object sender, KeyPressEventArgs e)
        {
            if (keyDelete != true)
            {
                if (alllabeltext != null)
                {
                    if (alllabeltext.Length != 0)
                    {
                        literal = (alllabeltext.Substring(alllabeltext.Length - 1))[0];
                    }
                }
                switch (e.KeyChar)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                    case ',':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        {
                            e.KeyChar = (e.KeyChar == '.') ? ',' : e.KeyChar;
                            beginWord = ((TextBox)sender).Text;
                            signTwo = e.KeyChar;
                            switch (literal.ToString() + e.KeyChar.ToString())
                            {
                                case "++":
                                case "+-":
                                case "+*":
                                case "+/":
                                case "+,":
                                case "-+":
                                case "*+":
                                case "/+":
                                case ",+":
                                //
                                case "--":
                                case "-*":
                                case "-/":
                                case "-,":
                                case "*-":
                                case "/-":
                                case ",-":
                                //
                                case "**":
                                case "*/":
                                case "*,":
                                case "/*":
                                case ",*":
                                //
                                case "//":
                                case "/,":
                                case ",/":
                                //
                                case ",,":
                                    {
                                        if (beginWord.Contains(","))
                                        {
                                            if ((beginWord.Length - (beginWord.IndexOf(',') + 1)) == 1)
                                            {
                                                Signs = true;
                                                break;
                                            }
                                            else
                                            {
                                                Signs = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (beginWord != "/" || beginWord != "+" || beginWord != "*" || beginWord != "-" || beginWord != ",")
                                            {
                                                Signs = true;
                                                break;
                                            }
                                            else
                                            {
                                                Signs = false;
                                                break;
                                            }
                                        }
                                    }
                                default:
                                    {
                                        if (!(beginWord.Contains(",")))
                                        {
                                            if (this.mainWindow.label1.Text == "0" && (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/'))
                                            {
                                                if (e.KeyChar == '-')
                                                {
                                                    Signs = true;
                                                }
                                                else
                                                {
                                                    Signs = false;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                Signs = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if ((beginWord.Length - (beginWord.IndexOf(',') + 1)) == 1)
                                            {
                                                Signs = true;
                                                break;
                                            }
                                            else
                                            {
                                                if (beginWord != "/" || beginWord != "+" || beginWord != "*" || beginWord != "-" || beginWord != "," || beginWord.Contains(','))
                                                {
                                                    if (beginWord.Contains(',') && e.KeyChar == ',')
                                                    {
                                                        Signs = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Signs = true;
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    Signs = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            if ((signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/') && e.KeyChar == '\r')
                            {
                                Signs = true;
                                beginWord = ((TextBox)sender).Text;
                                break;
                            }
                            else
                            {
                                Signs = false;
                                beginWord = ((TextBox)sender).Text;
                                break;
                            }
                        }
                }

            }
            else
            {
                if ((signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/') && (this.mainWindow.label1.Text.EndsWith("+") || this.mainWindow.label1.Text.EndsWith("-") || this.mainWindow.label1.Text.EndsWith("*") || this.mainWindow.label1.Text.EndsWith("/")))
                {
                    Signs = false;
                    Operation = false;
                }
                else if (!(String.IsNullOrEmpty(sender.ToString())) && this.mainWindow.label1.Text.StartsWith("0"))
                {
                    Signs = false;
                    Operation = false;
                }
                else if (!(String.IsNullOrEmpty(sender.ToString())) && (this.mainWindow.label1.Text.StartsWith("-") || !this.mainWindow.label1.Text.EndsWith("-") || !this.mainWindow.label1.Text.EndsWith("+") || !this.mainWindow.label1.Text.EndsWith("*") || !this.mainWindow.label1.Text.EndsWith("/")))
                {
                    Signs = false;
                    Operation = false;
                }
                else
                {
                    Signs = true;
                    keyDelete = false;
                }
            }
        }


        /* Меетод-обработчик события EventOperation для textBoxKeyPress и TextBox1.
           Метод реализует:
            - выполнение основных арифметических операций над числами;
            - присвоение переменным numberOne и numberTwo значений литералов числового ряда введенного пользователем чисел;
            и т.д.
            .
        */
        private void MainWindow_Operation(object sender, KeyPressEventArgs e)
        {
            if (keyDelete != true)
            {
                if (this.mainWindow.label1.Text.Length >= 0 && (this.mainWindow.label1.Text.Contains('+') || (this.mainWindow.label1.Text.Contains('-') && signOperation == '-') || this.mainWindow.label1.Text.Contains('*') || this.mainWindow.label1.Text.Contains('/')) && (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/' || e.KeyChar == '\r') && Signs == true)
                {
                    if (numberOne == "-")
                    {
                        if (e.KeyChar == '\r')
                        {
                            numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('-'));
                        }
                        if (e.KeyChar == '-')
                        {
                            if (this.mainWindow.label1.Text.StartsWith("-") == true && signOperation == '-')
                            {
                                numberOne = this.mainWindow.textBox1.Text;
                                signOperation = e.KeyChar;
                                return;
                            }
                            else
                            {
                                numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('-'));
                            }
                        }
                        if (e.KeyChar == '+')
                        {
                            if (this.mainWindow.label1.Text.StartsWith("-") == true && signOperation == '-')
                            {
                                numberOne = this.mainWindow.textBox1.Text;
                                signOperation = e.KeyChar;
                                return;
                            }
                            else
                            {
                                numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('-'));
                            }
                        }
                        if (e.KeyChar == '*')
                        {
                            if (this.mainWindow.label1.Text.StartsWith("-") == true && signOperation == '-')
                            {
                                numberOne = this.mainWindow.textBox1.Text;
                                signOperation = e.KeyChar;
                                return;
                            }
                            else
                            {
                                numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('-'));
                            }
                        }
                        if (e.KeyChar == '/')
                        {
                            if (this.mainWindow.label1.Text.StartsWith("-") == true && signOperation == '-')
                            {
                                numberOne = this.mainWindow.textBox1.Text;
                                signOperation = e.KeyChar;
                                return;
                            }
                            else
                            {
                                numberOne = alllabeltext.Remove(alllabeltext.LastIndexOf('-'));
                            }
                        }
                    }
                    double a = Math.Round(Convert.ToDouble(numberOne), 2);
                    double b;
                    if (!(signOperation == '/'))
                    {
                        b = Math.Round((Convert.ToDouble(numberTwo) * s), 2);
                    }
                    else
                    {
                        b = Math.Round((Convert.ToDouble(numberTwo) * p), 2);
                    }
                    if (!(a == b))
                    {
                        if (!(numberOne.Contains("-") && signTwo == '-'))
                        {

                            numberTwo = this.mainWindow.textBox1.Text;
                            numberMore = numberTwo;
                            s++;

                        }
                        else
                        {
                            if (!(signOperation == '+' || signOperation == '*' || signOperation == '/'))
                            {
                                return;
                            }
                            else
                            {
                                numberTwo = this.mainWindow.textBox1.Text;
                            }
                        }
                    }
                    else if ((e.KeyChar == '\r') && (signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/') && (this.mainWindow.label1.Text.Contains("+") || this.mainWindow.label1.Text.Contains("-") || this.mainWindow.label1.Text.Contains("*") || this.mainWindow.label1.Text.Contains("/")))
                    {
                        numberTwo = ((TextBox)sender).Text;
                        s = 0;
                    }
                    else
                    {
                        if (!(numberOne.Contains("-") && signTwo == '-'))
                        {
                            numberOne = beginWord;
                            if (numberOne.Contains("-") && signTwo == '-')
                            {
                                return;
                            }
                            else
                            {
                                numberTwo = numberMore;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                    switch (signOperation)
                    {
                        case '+':
                            {
                                if (!(e.KeyChar == '\r'))
                                {
                                    signOperation = e.KeyChar;
                                }
                                numberThree = this.model.Addition(numberOne, numberTwo);
                                this.mainWindow.textBox1.Text = numberThree;
                                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                numberOne = this.mainWindow.textBox1.Text;
                                s++;
                                Operation = false;
                                break;
                            }
                        case '-':
                            {
                                if (!(e.KeyChar == '\r'))
                                {
                                    signOperation = e.KeyChar;
                                }

                                numberThree = this.model.Subtraction(numberOne, numberTwo);
                                this.mainWindow.textBox1.Text = numberThree;
                                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                numberOne = this.mainWindow.textBox1.Text;

                                s--;
                                Operation = false;
                                break;
                            }
                        case '*':
                            {
                                if (!(e.KeyChar == '\r'))
                                {
                                    signOperation = e.KeyChar;
                                }

                                numberThree = this.model.Multiplication(numberOne, numberTwo);
                                this.mainWindow.textBox1.Text = numberThree;
                                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                numberOne = this.mainWindow.textBox1.Text;
                                s = s++ * 2;
                                Operation = false;
                                break;
                            }
                        case '/':
                            {
                                if (!(e.KeyChar == '\r'))
                                {
                                    signOperation = e.KeyChar;
                                }

                                numberThree = this.model.Division(numberOne, numberTwo);
                                this.mainWindow.textBox1.Text = numberThree;
                                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                numberOne = this.mainWindow.textBox1.Text;

                                p = Math.Pow(10, -s);
                                Operation = false;
                                break;
                            }
                        case '\r':
                            {
                                signOperation = e.KeyChar;
                                numberThree = this.model.Division(numberOne, numberTwo);
                                this.mainWindow.textBox1.Text = numberThree;
                                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                numberOne = this.mainWindow.textBox1.Text;
                                Operation = false;
                                break;
                            }
                    }
                }


                else if (((TextBox)sender).Text.Length >= 1 && (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/') && Signs == true)
                {
                    if (((TextBox)sender).Text.Length >= 13)
                    {
                        signOperation = e.KeyChar;
                        return;
                    }
                    else
                    {
                        if (this.mainWindow.label1.Text == "")
                        {
                            numberOne = ((TextBox)sender).Text;
                        }

                        else
                        {
                            if (!(e.KeyChar == '-'))
                            {
                                numberOne = ((TextBox)sender).Text;
                            }
                            else
                            {
                                numberOne = "-";
                            }
                        }
                        signOperation = e.KeyChar;
                    }
                }
                else
                {
                    if (this.mainWindow.label1.Text.Length >= 1 && ((this.mainWindow.label1.Text.Contains('-') && signOperation == '-')) && (e.KeyChar == '-' || e.KeyChar == '\r') && Signs == true)
                    {
                        if (!(CountLiteralsThree(signTwo) == 2))
                        {
                            numberTwo = this.mainWindow.textBox1.Text;
                            signOperation = e.KeyChar;
                            if (numberOne == "-")
                            {
                                numberOne = this.mainWindow.textBox1.Text;
                            }
                        }
                        else
                        {
                            numberTwo = number1;
                            numberOne = numberTwo;
                        }
                        numberThree = this.model.Subtraction(numberOne, numberTwo);
                        this.mainWindow.textBox1.Text = numberThree;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                        numberOne = this.mainWindow.textBox1.Text;
                        Operation = false;
                        s--;
                    }
                    else
                    {
                        if (e.KeyChar == '\r' && (signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/' || signOperation == '\r'))
                        {
                            // signOperation = sign;
                            double a = Math.Round(Convert.ToDouble(numberOne), 2);
                            double b = 0;
                            if (!(signOperation == '/'))
                            {
                                if (signOperation == '-')
                                {
                                    b = Math.Round((Convert.ToDouble(numberTwo) + (Convert.ToDouble(numberTwo) * s)), 2);
                                }
                                else if (signOperation == '+')
                                {
                                    b = Math.Round((Convert.ToDouble(numberTwo) * s), 2);
                                }
                                else if (signOperation == '*')
                                {
                                    b = Math.Pow(Convert.ToDouble(numberTwo), s);
                                    b = Math.Round(b, 2);
                                }

                            }
                            else
                            {
                                b = Convert.ToDouble(numberTwo);
                                b /= Math.Pow(Convert.ToDouble(numberTwo), s);
                                b = Math.Round(b, 2);
                            }
                            if (!(a == b))
                            {
                                numberMore = numberTwo;
                                s++;
                            }
                            else if ((e.KeyChar == '\r') && !(this.mainWindow.label1.Text.Contains("+") || this.mainWindow.label1.Text.Contains("-") || this.mainWindow.label1.Text.Contains("*") || this.mainWindow.label1.Text.Contains("/")))
                            {
                                if (!(numberOne.Length >= 12))
                                {
                                    if (!(number1.Contains("\r\n")))
                                    {
                                        numberTwo = signTwo.ToString();
                                    }
                                    else
                                    {
                                        number1 = sortText;
                                        numberTwo = number1;
                                    }
                                }
                                else
                                {
                                    numberTwo = sortText;
                                    number1 = numberTwo;
                                }
                            }
                            else
                            {
                                numberTwo = numberMore;
                            }
                            switch (signOperation)
                            {
                                case '+':
                                    {
                                        if (!(e.KeyChar == '\r'))
                                        {
                                            signOperation = e.KeyChar;
                                        }
                                        numberThree = this.model.Addition(numberOne, numberTwo);
                                        this.mainWindow.textBox1.Text = numberThree;
                                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                        numberOne = this.mainWindow.textBox1.Text;
                                        s++;
                                        Operation = false;
                                        break;
                                    }
                                case '-':
                                    {
                                        if (!(e.KeyChar == '\r'))
                                        {
                                            signOperation = e.KeyChar;
                                        }

                                        numberThree = this.model.Subtraction(numberOne, numberTwo);
                                        beginWord = numberThree;
                                        this.mainWindow.textBox1.Text = numberThree;
                                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                        numberOne = this.mainWindow.textBox1.Text;

                                        s--;
                                        Operation = false;
                                        break;
                                    }
                                case '*':
                                    {
                                        if (!(e.KeyChar == '\r'))
                                        {
                                            signOperation = e.KeyChar;
                                        }

                                        numberThree = this.model.Multiplication(numberOne, numberTwo);
                                        this.mainWindow.textBox1.Text = numberThree;
                                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                        numberOne = this.mainWindow.textBox1.Text;

                                        s++;
                                        Operation = false;
                                        break;
                                    }
                                case '/':
                                    {
                                        if (!(e.KeyChar == '\r'))
                                        {
                                            signOperation = e.KeyChar;
                                        }

                                        numberThree = this.model.Division(numberOne, numberTwo);
                                        this.mainWindow.textBox1.Text = numberThree;
                                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                                        numberOne = this.mainWindow.textBox1.Text;

                                        s++;
                                        Operation = false;
                                        break;
                                    }
                            }

                        }
                        else
                        {
                            Operation = true;
                        }
                    }
                }
            }
        }


        // Метод для подсчета количества последовательных одинаковых литералов (точки или запятой) в строке
        private int CountLiterals(char signTwo)
        {
            int count = 0;
            string word = sortText + signTwo;
            switch (signTwo)
            {
                case ',':
                case '.':
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (i + 1 < word.Length && word[i] == word[i + 1])
                            {
                                if (word[i] == signTwo && word[i + 1] == signTwo)
                                {
                                    ++count;
                                }
                            }
                        }
                        break;
                    }
            }
            return count;
        }



        // Метод для подсчета количества непоследовательных одинаковых литералов (точки или запятой) в строке
        private int CountLiteralsTwo(char signTwo)
        {
            int countTwo = 0;
            string word = sortText + signTwo;
            switch (signTwo)
            {
                case ',':
                case '.':
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == signTwo)
                            {
                                ++countTwo;
                            }
                        }
                        break;
                    }
            }
            return countTwo;
        }


        // Метод для подсчета количества непоследовательных одинаковых литералов (минуса) в строке
        private int CountLiteralsThree(char signTwo)
        {
            int countTwo = 0;
            string word = sortText + signTwo;
            switch (signTwo)
            {
                case '-':
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == signTwo)
                            {
                                ++countTwo;
                            }
                        }
                        break;
                    }
            }
            return countTwo;
        }



        // Метод для подсчета количества непоследовательных одинаковых литералов (+, -, /, *) в строке
        private int CountLiteralsFour(char sign)
        {
            int countTwo = 0;
            for (int i = 0; i < alllabeltext.Length; i++)
            {
                if (alllabeltext[i] == sign)
                {
                    ++countTwo;
                }
            }
            return countTwo;
        }


        /* Меетод-обработчик события EventRemoveTextTwo для textBox1_TextChanged и TextBox1.
           Метод реализует (часть1):
            - последовательное удаление чисел, которые выходят за границы окна TextBox1;
            - присвоение переменной textBox1.Text определенного значения литералов числового ряда введенного пользователем чисел;
            и т.д.
            .
        */
        private void MainWindow_RemoveTextTwo(object sender, EventArgs e)
        {
            if (this.mainWindow.label1.Text.Length > 19 && !((CountLiterals(signTwo) == 1 || CountLiteralsTwo(signTwo) == 2)) && Signs == true)
            {
                if (this.mainWindow.label1.Text.LastIndexOf('+') == this.mainWindow.label1.Text.Length - 1)
                {
                    if (numberNextPlus != ((TextBox)sender).Text)
                    {
                        this.mainWindow.textBox1.Text = numberOne;
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = numberNextPlus;
                    }

                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                else if (this.mainWindow.label1.Text.LastIndexOf('-') == this.mainWindow.label1.Text.Length - 1)
                {
                    if (numberNextPlus != ((TextBox)sender).Text)
                    {
                        this.mainWindow.textBox1.Text = numberOne;
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = numberNextPlus;
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                else if (this.mainWindow.label1.Text.LastIndexOf('*') == this.mainWindow.label1.Text.Length - 1)
                {
                    if (numberNextPlus != ((TextBox)sender).Text)
                    {
                        this.mainWindow.textBox1.Text = numberOne;
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = numberNextPlus;
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                else if (this.mainWindow.label1.Text.LastIndexOf('/') == this.mainWindow.label1.Text.Length - 1)
                {
                    if (numberNextPlus != ((TextBox)sender).Text)
                    {
                        this.mainWindow.textBox1.Text = numberOne;
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = numberNextPlus;
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                else
                {
                    if (signOperation == '+')
                    {
                        if (this.mainWindow.label1.Text.EndsWith("+"))
                        {
                            this.mainWindow.textBox1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('+') + 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (signOperation == '-')
                    {
                        if (this.mainWindow.label1.Text.EndsWith("-"))
                        {
                            this.mainWindow.textBox1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('-') + 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (signOperation == '*')
                    {
                        if (this.mainWindow.label1.Text.EndsWith("*"))
                        {
                            this.mainWindow.textBox1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('*') + 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (signOperation == '/')
                    {
                        if (this.mainWindow.label1.Text.EndsWith("/"))
                        {
                            this.mainWindow.textBox1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('/') + 1);
                        }
                        else
                        {
                            return;
                        }
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
            }
            else if (this.mainWindow.label1.Text.Length > 1 && (CountLiterals(signTwo) == 1 || CountLiteralsTwo(signTwo) == 2) && Signs == true)
            {
                if (!(sortText[0] == sortText[1]) || CountLiteralsTwo(signTwo) == 2)
                {
                    double oneText;
                    if (CountLiteralsTwo(signTwo) == 2 && (signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/'))
                    {
                        oneText = Convert.ToDouble(numberThree);
                    }
                    else
                    {
                        oneText = Convert.ToDouble(sortText);
                    }
                    double twoText = Convert.ToDouble(sortText);
                    if (oneText <= Math.Abs(twoText))
                    {
                        this.mainWindow.textBox1.Text = sortText;
                    }
                    else
                    { return; }
                }
                else
                {
                    this.mainWindow.textBox1.Text = sortText + signTwo;
                }
                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
            }
            if (((TextBox)sender).Text == "--")
            {
                this.mainWindow.textBox1.Text = "-";
                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
            }
        }



        /* Меетод-обработчик события MoreNumbersInTextBox для textBox1_TextChanged и TextBox1.
           Метод реализует (часть 2):
            - последовательное удаление чисел, которые выходят за границы окна TextBox1;
            - присвоение переменной textBox1.Text определенного значения литералов числового ряда введенного пользователем чисел;
            и т.д.
            .
        */
        private void MainWindow_MoreNumbersInTextBox(object sender, EventArgs e)
        {
            if (plusPlus == true && Signs == true)
            {
                if (this.mainWindow.label1.Text.Length > 1 && (CountLiterals(signTwo) == 1 || CountLiteralsTwo(signTwo) == 2))
                {
                    number1 = sortText;
                }
                else
                {
                    number1 = ((TextBox)sender).Text;
                    numberMore = number1.Substring(number1.Length - 1);
                }
                number1 = number1.Substring(number1.Length - 13);
            }
            else if (Signs == true)
            {
                number1 = number1.Substring(number1.Length - 13);
                this.mainWindow.textBox1.Text = number1;
            }
        }

        // Метод-обработчик события EventAllNumbers для Labe1 выводит в окне операций информацию по всем арифметическим действиям с числами
        private void MainWindow_AllNumbers(object sender, EventArgs e)
        {
            if (this.mainWindow.label1.Text.Length > 1 && CountLiteralsTwo(signTwo) == 2 && Signs == true)
            {
                alllabeltext = this.mainWindow.label1.Text;
            }
            else if (Signs == true)
            {
                if (!(this.mainWindow.textBox1.Text == "-"))
                {
                    this.mainWindow.label1.Text += sign;
                }
                else
                {
                    this.mainWindow.label1.Text = "-";
                }
                alllabeltext = this.mainWindow.label1.Text;
            }
        }


        // Метод-обработчик события EventRemoveText для TextBox1 выводит в окне TextBox1 информацию по всем арифметическим действиям с числами
        private void MainWindow_RemoveText(object sender, EventArgs e)
        {
            if (Operation == true && Signs == true)
            {
                if (plusPlus == true)
                {
                    if ((this.mainWindow.label1.Text.EndsWith("+")) && this.mainWindow.label1.Text.Length > 19 && plusPlus == true)
                    {
                        if (numberNextPlus == numberThree)
                        {
                            numberNextPlus = null;
                            number1 = numberNextPlus;
                        }
                        else
                        {
                            number1 = numberNextPlus;
                        }
                        this.mainWindow.textBox1.Text = number1;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                    if ((this.mainWindow.label1.Text.EndsWith("-")) && this.mainWindow.label1.Text.Length > 19 && plusPlus == true)
                    {
                        if (numberNextPlus == numberThree)
                        {
                            numberNextPlus = null;
                            number1 = numberNextPlus;
                        }
                        else
                        {
                            number1 = numberNextPlus;
                        }
                        this.mainWindow.textBox1.Text = number1;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                    if ((this.mainWindow.label1.Text.EndsWith("*")) && this.mainWindow.label1.Text.Length > 19 && plusPlus == true)
                    {
                        if (numberNextPlus == numberThree)
                        {
                            numberNextPlus = null;
                            number1 = numberNextPlus;
                        }
                        else
                        {
                            number1 = numberNextPlus;
                        }
                        this.mainWindow.textBox1.Text = number1;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                    if ((this.mainWindow.label1.Text.EndsWith("/")) && this.mainWindow.label1.Text.Length > 19 && plusPlus == true)
                    {
                        if (numberNextPlus == numberThree)
                        {
                            numberNextPlus = null;
                            number1 = numberNextPlus;
                        }
                        else
                        {
                            number1 = numberNextPlus;
                        }
                        this.mainWindow.textBox1.Text = number1;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                    else if (this.mainWindow.label1.Text.Length > 1 && (CountLiterals(signTwo) == 1 || CountLiteralsTwo(signTwo) == 2 || CountLiteralsThree(signTwo) == 2))
                    {
                        this.mainWindow.textBox1.Text = sortText;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = number1;
                        this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                    }
                }
                else
                {
                    this.mainWindow.textBox1.Text = number1;
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
            }
            else if (Signs == true)
            {
                this.mainWindow.textBox1.Text = numberOne;
                this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
            }
        }


        // Метод-обработчик события EventKeyPressKey для textBoxKeyPress, Labe1 выводит в окне Labe1 информацию по всем арифметическим действиям с числами
        private void MainWindow_KeyPressKey(object sender, KeyPressEventArgs e)
        {
            if (keyDelete != true)
            {
                if ((e.KeyChar == '+' || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/') && plusPlus == true && Operation == true && Signs == true)
                {
                    if (!(this.mainWindow.label1.Text.Length > 19))
                    {
                        number1 = this.mainWindow.textBox1.Text;
                        if (string.IsNullOrEmpty(number1))
                        {
                            number1 = number12;
                        }
                        sign = e.KeyChar.ToString();
                        MainWindow_AllNumbers(sender, e);
                    }
                    else
                    {
                        sign = e.KeyChar.ToString();
                        MainWindow_AllNumbers(sender, e);
                        labelText = this.mainWindow.label1.Text;
                        alllabeltext = this.mainWindow.label1.Text;
                        this.mainWindow.label1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.Length - 20);
                    }
                }
                else if (plusPlus == true && Signs == true)
                {
                    if (!(this.mainWindow.label1.Text.Length > 19))
                    {
                        if (!(e.KeyChar == '\r'))
                        {
                            if (this.mainWindow.label1.Text == "")
                            {
                                this.mainWindow.label1.Text = numberOne + e.KeyChar.ToString();
                                alllabeltext = this.mainWindow.label1.Text;
                            }
                            else
                            {
                                this.mainWindow.label1.Text = alllabeltext + e.KeyChar.ToString();
                                alllabeltext = this.mainWindow.label1.Text;
                            }
                        }
                        else
                        {
                            this.mainWindow.label1.Text = "";
                            alllabeltext = numberOne;
                        }
                    }
                    else
                    {
                        labelText = this.mainWindow.label1.Text;
                        alllabeltext = labelText;
                        this.mainWindow.label1.Text = alllabeltext + e.KeyChar.ToString();
                        if (signOperation == '+' || signOperation == '-' || signOperation == '*' || signOperation == '/')
                        {
                            if (!(this.mainWindow.label1.Text.EndsWith("\r")))
                            {
                                numberNextPlus = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('+') + 1);
                            }
                            else
                            {
                                numberNextPlus = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('\r') + 1);
                                alllabeltext = "";
                            }
                        }
                        if (signOperation == '-')
                        {
                            numberNextPlus = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('-') + 1);
                        }
                        if (signOperation == '*')
                        {
                            numberNextPlus = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('*') + 1);
                        }
                        if (signOperation == '/')
                        {
                            numberNextPlus = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.LastIndexOf('/') + 1);
                        }
                        if (!(this.mainWindow.label1.Text.EndsWith("\r")))
                        {
                            this.mainWindow.label1.Text = this.mainWindow.label1.Text.Substring(this.mainWindow.label1.Text.Length - 20);
                        }
                        else
                        {
                            this.mainWindow.label1.Text = "";
                        }
                    }
                }
            }
        }


        // Метод-обработчик события EventMouseCancel для botton, TextBox1. После нажатия на кнопки программы автоматически перемещает фокус курсорав окно TextBox1
        private void MainWindow_MouseCancel(object sender, EventArgs e)
        {
            if (((KeyPressEventArgs)e).KeyChar == ',')
            {
                this.mainWindow.textBox1.Text += ",";
            }
            this.mainWindow.textBox1.Focus();
            this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
            if (this.mainWindow.label1.Text.StartsWith("0") && this.mainWindow.label1.Text.Length == 1)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }

        // Метод обработчик события EventKeyPress для textBoxKeyPress. Определяет ввод данных через нажатия клавиши на клавиатуре.
        private void MainWindow_KeyPress(object sender, EventArgs e)
        {
            if (keyDelete != true)
            {
                if (Signs == true)
                {
                    if (this.mainWindow.label1.Text.EndsWith("+") && ((KeyPressEventArgs)e).KeyChar == '+')
                    {
                        plusPlus = false;
                    }
                    else if (this.mainWindow.label1.Text.EndsWith(".") && ((KeyPressEventArgs)e).KeyChar == '.')
                    {
                        plusPlus = false;
                    }
                    else if (this.mainWindow.label1.Text.EndsWith(",") && ((KeyPressEventArgs)e).KeyChar == ',')
                    {
                        plusPlus = false;
                    }
                    if (this.mainWindow.label1.Text.EndsWith("-") && ((KeyPressEventArgs)e).KeyChar == '-')
                    {
                        plusPlus = false;
                    }
                    if (this.mainWindow.label1.Text.EndsWith("*") && ((KeyPressEventArgs)e).KeyChar == '*')
                    {
                        plusPlus = false;
                    }
                    if (this.mainWindow.label1.Text.EndsWith("/") && ((KeyPressEventArgs)e).KeyChar == '/')
                    {
                        plusPlus = false;
                    }
                    else if (Signs == true)
                    {
                        plusPlus = true;
                    }
                }
                else if (Signs == true)
                {
                    plusPlus = true;
                }
                // Начальное условие: замена нуля в TextBox и перемещение фокуса курсора в положение 0
                if ((i++ == 0) && plusPlus == true && Operation == true && Signs == true)
                {
                    // Удаление нуля при вводе цифры
                    if (this.mainWindow.label1.Text.StartsWith("0") && this.mainWindow.label1.Text.Length < 13 && (signOperation == '-' || ((KeyPressEventArgs)e).KeyChar == ','))
                    {
                        if (((KeyPressEventArgs)e).KeyChar != ',')
                        {
                            this.mainWindow.textBox1.Text = "-";
                        }
                        else
                        {
                            this.mainWindow.textBox1.Text = "0";
                        }
                    }
                    else
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    // Перемещение фокуса курсора в нулевое положение
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                if (this.mainWindow.label1.Text.EndsWith("+") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf('+') + 1);
                    number12 = number1;

                    if (number12.Length >= 0)
                    {
                        this.mainWindow.textBox1.Text = number12;
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains('+')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;

                }
                if (this.mainWindow.label1.Text.EndsWith(".") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf('.') + 1);
                    number12 = number1;
                    if (number12.Length >= 0 && !(this.mainWindow.textBox1.Text.Contains('.')))
                    {
                        this.mainWindow.textBox1.Text = number12;
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains('.')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                if (this.mainWindow.label1.Text.EndsWith(",") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf(',') + 1);
                    number12 = number1;
                    if (number12.Length >= 0 && !(this.mainWindow.textBox1.Text.Contains(',')))
                    {
                        this.mainWindow.textBox1.Text = number12;
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains(',')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
                }
                if (this.mainWindow.label1.Text.EndsWith("-") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf('-') + 1);
                    number12 = number1;
                    if (number12.Length >= 0)
                    {
                        if (!(this.mainWindow.label1.Text.StartsWith("-")))
                        {
                            this.mainWindow.textBox1.Text = number12;
                        }
                        else
                        {
                            if (!(this.mainWindow.label1.Text.EndsWith("-")))
                            {
                                this.mainWindow.textBox1.Text = "-";
                            }
                            else
                            {
                                if (!(this.mainWindow.label1.Text.Length == 1))
                                {
                                    this.mainWindow.textBox1.Text = number12;
                                }
                                else
                                {
                                    this.mainWindow.textBox1.Text = "-";
                                }
                            }
                        }
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains('-')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;

                }
                if (this.mainWindow.label1.Text.EndsWith("*") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf('*') + 1);
                    number12 = number1;
                    if (number12.Length >= 0)
                    {
                        this.mainWindow.textBox1.Text = number12;
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains('*')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;

                }
                if (this.mainWindow.label1.Text.EndsWith("/") && plusPlus == true && Operation == true && Signs == true)
                {
                    alllabeltext = this.mainWindow.label1.Text;
                    number1 = alllabeltext.Substring(alllabeltext.LastIndexOf('/') + 1);
                    number12 = number1;
                    if (number12.Length >= 0)
                    {
                        this.mainWindow.textBox1.Text = number12;
                    }
                    else if (!(this.mainWindow.textBox1.Text.Contains('/')))
                    {
                        this.mainWindow.textBox1.Text = this.mainWindow.textBox1.Text.Remove(0);
                    }
                    this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;

                }
                if (Operation == true && Signs == true)
                {
                    signTwo = ((KeyPressEventArgs)e).KeyChar;
                    if (this.mainWindow.label1.Text.StartsWith("0") && this.mainWindow.label1.Text.Length < 13 && signTwo == '-')
                    {
                        sortText = ((TextBox)sender).Text;
                    }
                    else
                    {
                        sortText = ((TextBox)sender).Text;
                    }
                }
            }
        }


        // Метод-обработчик события EventFocusText для Form1_Load_1. После запуска программы автоматически перемещает фокус курсора в окно TextBox1
        private void MainWindow_ReplaceBeginnerTextNull(object sender, EventArgs e)
        {
            this.mainWindow.textBox1.SelectionStart = this.mainWindow.textBox1.Text.Length;
        }

    }
}
