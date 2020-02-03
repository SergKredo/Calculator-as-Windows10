using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Calculator
{
    // Шаблон для построения десктопной формы калькулятора
    public partial class Form1 : Form
    {
        Keys keys;   // Перечисление в котором задаются коды и модификаторы клавиш клавиатуры.
        Message message;
        readonly Presenter presenter;
        object senderDelete;
        readonly object senderDeleteX;
        bool keyDelete = false;      // Булевая переменная, которая отвечает за доступ к функционалу для Delete клавиши с клавиатуры
        bool keyBackspace = false;   // Булевая переменная, которая отвечает за доступ к функционалу для BackSpace клавиши с клавиатуры
        readonly char keysDelete = '0';
        KeyPressEventArgs keyPressEventArgs;
        EventArgs eventArgs;
        public Form1()  // Конструктор класса Form1
        {
            InitializeComponent();  // Метод, который задает весь внешний  userinterface функционал (окно программы, кнопки, текстбокс и т.д.)
            presenter = new Presenter(this); // Экземпляр класса Presenter для объединения userinterface (Form1) и модели (Model), по которой выполняются основные арифметические операции с переменными
            keyPressEventArgs = new KeyPressEventArgs(keysDelete); // Объявление экземпляра класса, который предоставляет данные для события System.Windows.Forms.Control.KeyPress.
            senderDelete = new TextBox();   // Приведение к базовому типу (UpCast)
            senderDeleteX = new TextBox();   // Приведение к базовому типу (UpCast)
        }

        // События, реализуемые методами-обработчиками в классе Presenter
        public event EventHandler EventFocusText = null;  // 
        public event EventHandler EventKeyPress = null;
        public event EventHandler EventMouseCancel = null;
        public event KeyPressEventHandler EventKeyPressKey = null;
        public event EventHandler EventRemoveText = null;
        public event EventHandler EventAllNumbers = null;
        public event EventHandler MoreNumbersInTextBox = null;
        public event EventHandler EventRemoveTextTwo = null;
        public event KeyPressEventHandler EventOperation = null;
        public event KeyPressEventHandler EventSigns = null;
        public event EventHandler EventWithOutSign = null;
        public event KeyPressEventHandler EventDelete = null;
        public event KeyPressEventHandler EventBackspace = null;
        public event EventHandler EventOnetoX = null;
        public event EventHandler EventShareOf = null;
        public event EventHandler EventSQRT = null;
        public event EventHandler EventDeleteCE = null;
        public event EventHandler EventMPlus = null;
        public event EventHandler EventMMinus = null;
        public event EventHandler EventMRead = null;
        public event EventHandler EventMSave = null;
        public event EventHandler EventMClear = null;
        public event EventHandler EventOperationMC = null;
        public event EventHandler EventInsertText = null;
        public event EventHandler EventPressingM = null;
        public event EventHandler EventBoolM = null;

        // Метод, который позволяет перехватывать параметры ключей клавиш клавиатуры
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                keys = keyData;
                keyDelete = true;
                textBoxKeyPress(senderDelete, keyPressEventArgs);
                textBox1_TextChanged(senderDelete, (EventArgs)keyPressEventArgs);
            }
            if (keyData == Keys.Back)
            {
                keys = keyData;
                keyBackspace = true;
                textBoxKeyPress(senderDelete, keyPressEventArgs);
                return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        // Метод-обработчик события Load для Form1, возникающее при запуске программы
        private void Form1_Load_1(object sender, EventArgs e)
        {
            EventFocusText.Invoke(sender, e);
        }

        // Метод-обработчик события Click для label1, возникающее при щелчке єлемента управления
        private void label1_Click(object sender, EventArgs e)
        {
            EventAllNumbers.Invoke(sender, e);
        }

        // Метод-обработчик события TextChanged для textBox1, возникающее когда в Control изменяется значение свойства Text
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (presenter.SignsOperation() != false)
            {
                string param = ((TextBox)sender).Text;

                if (!(param.Length > 13))
                {
                    if (param.EndsWith("\r\n"))
                    {
                        int p = param.IndexOf('\r');
                        if (param.IndexOf('\r') != 0 && param.IndexOf('\r') > 0)
                        {
                            param = param.Substring(param.IndexOf('\r'), 1);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("+"))
                    {
                        int p = param.IndexOf('+');
                        if (param.IndexOf('+') != 0 && param.IndexOf('+') > 0)
                        {
                            param = param.Substring(param.IndexOf('+'), 1);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("-"))
                    {
                        if (!(param.StartsWith("-")))
                        {
                            int p = param.IndexOf('-');
                            if (param.IndexOf('-') != 0 && param.IndexOf('-') > 0)
                            {
                                param = param.Substring(param.IndexOf('-'), 1);
                                EventRemoveText.Invoke(sender, e);
                            }
                        }
                        if (param.EndsWith("-") && param.Length > 2)
                        {
                            EventRemoveText.Invoke(sender, e);
                        }

                    }
                    if (param.EndsWith("*"))
                    {
                        int p = param.IndexOf('*');
                        if (param.IndexOf('*') != 0 && param.IndexOf('*') > 0)
                        {
                            param = param.Substring(param.IndexOf('*'), 1);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("/"))
                    {
                        int p = param.IndexOf('/');
                        if (param.IndexOf('/') != 0 && param.IndexOf('/') > 0)
                        {
                            param = param.Substring(param.IndexOf('/'), 1);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    else if (param.Length <= 12)
                    {
                        EventRemoveTextTwo.Invoke(sender, e);
                    }
                }
                else
                {
                    if (param.EndsWith("+"))
                    {
                        int p = param.IndexOf('+');
                        if (param.IndexOf('+') != 0 && param.IndexOf('+') > 0)
                        {
                            param = param.Substring(param.IndexOf('+'), 1);
                            MoreNumbersInTextBox.Invoke(sender, e);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("-"))
                    {
                        int p = param.IndexOf('-');
                        if (param.IndexOf('-') != 0 && param.IndexOf('-') > 0)
                        {
                            param = param.Substring(param.IndexOf('-'), 1);
                            MoreNumbersInTextBox.Invoke(sender, e);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("*"))
                    {
                        int p = param.IndexOf('*');
                        if (param.IndexOf('*') != 0 && param.IndexOf('*') > 0)
                        {
                            param = param.Substring(param.IndexOf('*'), 1);
                            MoreNumbersInTextBox.Invoke(sender, e);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    if (param.EndsWith("/"))
                    {
                        int p = param.IndexOf('/');
                        if (param.IndexOf('/') != 0 && param.IndexOf('/') > 0)
                        {
                            param = param.Substring(param.IndexOf('/'), 1);
                            MoreNumbersInTextBox.Invoke(sender, e);
                            EventRemoveText.Invoke(sender, e);
                        }
                    }
                    else
                    {
                        MoreNumbersInTextBox.Invoke(sender, e);
                        EventRemoveText.Invoke(sender, e);
                    }
                }
            }
            else
            {
                EventWithOutSign(sender, e);
            }
            if (keyBackspace != true)
            {
                senderDelete = sender;
            }
            else
            {
                senderDelete = sender;
            }
        }

        // Метод-обработчик события KeyPress для textBox1, возникающее когда элемент управления находится в фокусе и пользователь нажимает и отпускает клавишу
        private void textBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            keyPressEventArgs.KeyChar = e.KeyChar;
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            ((TextBox)senderDeleteX).Text = ((TextBox)senderDelete).Text;
            EventBoolM.Invoke(senderDeleteX, eventArgs);
            if (keyDelete == true)
            {
                EventDelete.Invoke(senderDelete, keyPressEventArgs);
                keyDelete = false;
                e.KeyChar = '\0';
            }
            if (keyBackspace == true)
            {
                EventBackspace.Invoke(senderDelete, keyPressEventArgs);
                keyBackspace = false;
                return;
            }
            if (((TextBox)senderDeleteX).Text == "\0\0\0")
            {
                ((TextBox)senderDelete).Text = "0";
            }
            EventSigns.Invoke(sender, e);
            EventOperation.Invoke(sender, e);
            EventKeyPress.Invoke(sender, e);
            EventKeyPressKey.Invoke(sender, e);
            keyPressEventArgs = e;
        }

        // Метод-обработчик события MouseUp для botton, возникающее в момент отпускания кнопки мыши
        private void MouseUPCancel(object sender, EventArgs e)
        {
            EventMouseCancel.Invoke(sender, e);
        }

        // Метод-обработчик события Click для botton29, возникающее при щелчке кнопки "0" в калькуляторе
        private void button29_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton25, возникающее при щелчке кнопки "1" в калькуляторе
        private void button25_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '1';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton24, возникающее при щелчке кнопки "2" в калькуляторе
        private void button24_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '2';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton23, возникающее при щелчке кнопки "3" в калькуляторе
        private void button23_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '3';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton20, возникающее при щелчке кнопки "4" в калькуляторе
        private void button20_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '4';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton19, возникающее при щелчке кнопки "5" в калькуляторе
        private void button19_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '5';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton18, возникающее при щелчке кнопки "6" в калькуляторе
        private void button18_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '6';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton15, возникающее при щелчке кнопки "7" в калькуляторе
        private void button15_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '7';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton14, возникающее при щелчке кнопки "8" в калькуляторе
        private void button14_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '8';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton13, возникающее при щелчке кнопки "9" в калькуляторе
        private void button13_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '9';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            ((TextBox)senderDelete).Text += keyPressEventArgs.KeyChar;
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton27, возникающее при щелчке кнопки "," в калькуляторе
        private void button27_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = ',';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton26, возникающее при щелчке кнопки "+" в калькуляторе
        private void button26_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '+';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton22, возникающее при щелчке кнопки "-" в калькуляторе
        private void button22_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '-';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton17, возникающее при щелчке кнопки "*" в калькуляторе
        private void button17_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '*';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton12, возникающее при щелчке кнопки "/" в калькуляторе
        private void button12_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '/';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton21, возникающее при щелчке кнопки "=" в калькуляторе
        private void button21_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\r';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton8, возникающее при щелчке кнопки "C" в калькуляторе
        private void button8_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            keys = Keys.Delete;
            ProcessCmdKey(ref message, keys);
            EventMouseCancel.Invoke(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton10, возникающее при щелчке кнопки "⟵" в калькуляторе 
        private void button10_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            keys = Keys.Back;
            ProcessCmdKey(ref message, keys);
            EventMouseCancel.Invoke(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton7, возникающее при щелчке кнопки "±" в калькуляторе 
        private void button7_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '-';
            if (!(((TextBox)senderDelete).Text.Length > 1 && ((TextBox)senderDelete).Text != "0"))
            {
                ((TextBox)senderDelete).Text = "0";
                eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
                textBoxKeyPress(senderDelete, keyPressEventArgs);
                ((TextBox)senderDelete).Text = (keyPressEventArgs.KeyChar).ToString();
                textBox1_TextChanged(senderDelete, eventArgs);
            }
            else
            {
                eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
                EventMouseCancel(senderDelete, eventArgs);
                return;
            }
            EventMouseCancel(senderDelete, eventArgs);
        }


        // Метод-обработчик события Click для botton16, возникающее при щелчке кнопки "1/x" в калькуляторе 
        private void button16_Click(object sender, EventArgs e)
        {
            ((TextBox)senderDeleteX).Text = ((TextBox)senderDelete).Text;

            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            keys = Keys.Delete;
            ProcessCmdKey(ref message, keys);
            EventMouseCancel.Invoke(senderDelete, eventArgs);

            senderDelete = new TextBox();
            ((TextBox)senderDelete).Text += "1/";
            ((TextBox)senderDelete).Text += ((TextBox)senderDeleteX).Text;
            keyPressEventArgs.KeyChar = '\r';
            EventOnetoX.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);

        }

        // Метод-обработчик события Click для botton11, возникающее при щелчке кнопки "%" в калькуляторе 
        private void button11_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventShareOf.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton6, возникающее при щелчке кнопки "√" в калькуляторе 
        private void button6_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventSQRT.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton9, возникающее при щелчке кнопки "CE" в калькуляторе  
        private void button9_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventDeleteCE.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }

        // Метод-обработчик события Click для botton3, возникающее при щелчке кнопки "M+" в калькуляторе  
        private void button3_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventMPlus.Invoke(senderDelete, keyPressEventArgs);
            EventMouseCancel(senderDelete, eventArgs);
            EventPressingM.Invoke(senderDeleteX, eventArgs);
        }

        // Метод-обработчик события Click для botton5, возникающее при щелчке кнопки "M-" в калькуляторе  
        private void button5_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventMMinus.Invoke(senderDelete, keyPressEventArgs);
            EventMouseCancel(senderDelete, eventArgs);
            EventPressingM.Invoke(senderDeleteX, eventArgs);
        }

        // Метод-обработчик события Click для botton2, возникающее при щелчке кнопки "MR" в калькуляторе  
        private void button2_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            ((TextBox)senderDeleteX).Text = ((TextBox)senderDelete).Text;
            EventMRead.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
            EventPressingM.Invoke(senderDeleteX, eventArgs);
        }

        // Метод-обработчик события Click для botton4, возникающее при щелчке кнопки "MS" в калькуляторе  
        private void button4_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            ((TextBox)senderDeleteX).Text = ((TextBox)senderDelete).Text;
            EventMSave.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
            EventPressingM.Invoke(senderDeleteX, eventArgs);
        }

        // Метод-обработчик события Click для botton1, возникающее при щелчке кнопки "MC" в калькуляторе  
        private void button1_Click(object sender, EventArgs e)
        {
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventMClear.Invoke(senderDelete, keyPressEventArgs);
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
            EventOperationMC(senderDelete, keyPressEventArgs);
        }

        // Метод-обработчик события Click для about, возникающее при щелчке подменю About в калькуляторе 
        private void AboutProgram(object sender, EventArgs e)
        {
            MessageBox.Show("An analogue of a conventional calculator as in Windows 10. The program is written in C # in WF (Windows Forms) using the MVP (Model View Presenter) design pattern.", "Calculator: information (2020) @Sergey Kredentser".ToUpper(), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        // Метод-обработчик события Click для GitHub, возникающее при щелчке подменю GitHub code в калькуляторе 
        private void GithubCode(object sender, EventArgs e)
        {
            Process.Start("https://github.com/SergKredo/Calculator-as-Windows10");
        }

        // Метод-обработчик события Click для LinkedinMenu, возникающее при щелчке подменю Linkedin в калькуляторе 
        private void LinkedinPage(object sender, EventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/sergey-kredentser-05369811b/");
        }

        // Метод-обработчик события Click для CopyMenu, возникающее при щелчке подменю Copy в калькуляторе 
        private void CopyText(object sender, EventArgs e)
        {
            Clipboard.SetText(((TextBox)senderDelete).Text);
        }

        // Метод-обработчик события Click для InsertMenu, возникающее при щелчке подменю Insert в калькуляторе 
        private void InsertText(object sender, EventArgs e)
        {
            string text = Clipboard.GetText();
            ((TextBox)senderDeleteX).Text = text;
            keyPressEventArgs.KeyChar = '\0';
            eventArgs = new KeyPressEventArgs(keyPressEventArgs.KeyChar);
            EventInsertText.Invoke(senderDeleteX, keyPressEventArgs);
            ((TextBox)senderDelete).Text = ((TextBox)senderDeleteX).Text;
            textBoxKeyPress(senderDelete, keyPressEventArgs);
            textBox1_TextChanged(senderDelete, eventArgs);
            EventMouseCancel(senderDelete, eventArgs);
        }
    }
}
