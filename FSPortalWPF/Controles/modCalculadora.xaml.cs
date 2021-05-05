using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FSPortalWPF.Controles
{
    public partial class modCalculadora : System.Windows.Controls.UserControl
    {
        public modCalculadora()
        {
            InitializeComponent();
        }


        private enum CalcState
        {
            CannotDivideByZero,
            FirstOperand,
            SecondOperand
        }

        private enum CalcOperation
        {
            Equal,
            Multiply,
            Divide,
            Add,
            Substract
        }

        private double operand1 = 0;
        private double operand2 = 0;
        private CalcState State = CalcState.FirstOperand;
        private CalcOperation LastOperation = CalcOperation.Equal;
        private bool coma = false;

        void AddDigit(byte digit)
        {
            double displayValue = 0;
            string c = "";
            switch(State)
            {
                case CalcState.FirstOperand:
                    if (coma) { c = ","; coma = false; }
                    try
                    {
                    operand1 = Convert.ToDouble(Convert.ToString(operand1) + c + Convert.ToString(digit));
                    }
                    catch
                    {
                    }
                    displayValue = operand1;
                    break;
                case CalcState.SecondOperand:
                    if (coma) { c = ","; coma = false; }
                    try
                    {
                        operand2 = Convert.ToDouble(Convert.ToString(operand2) + c + Convert.ToString(digit));
                    }
                    catch
                    {
                    }
                    displayValue = operand2;
                    break;
                case CalcState.CannotDivideByZero:
                    return;
            }

            textBox1.Text = displayValue.ToString();
        }

        void DoOperation(CalcOperation op)
        {
            double displayValue = 0;
            State = CalcState.SecondOperand;

            switch (LastOperation)
            {
                case CalcOperation.Equal:
                    displayValue = operand1;
                    break;
                case CalcOperation.Multiply:
                    operand1 *= operand2;
                    operand2 = 1;
                    displayValue = operand1;
                    break;
                case CalcOperation.Divide:
                    if(operand2 == 0)
                    {
                        State = CalcState.CannotDivideByZero;
                        textBox1.Text = "No se puede dividir por cero";
                        return;
                    }
                    operand1 /= operand2;
                    operand2 = 1;
                    displayValue = operand1;
                    break;
                case CalcOperation.Add:
                    operand1 += operand2;
                    operand2 = 0;
                    displayValue = operand1;
                    break;
                case CalcOperation.Substract:
                    operand1 -= operand2;
                    operand2 = 0;
                    displayValue = operand1;
                    break;
            }
            LastOperation = op;
            textBox1.Text = operand1.ToString();
            operand2 = 0;
        }

        void OnButton0Click(object sender, RoutedEventArgs args)
        {
            AddDigit(0);
        }

        void OnButton1Click(object sender, RoutedEventArgs args)
        {
            AddDigit(1);
        }

        void OnButton2Click(object sender, RoutedEventArgs args)
        {
            AddDigit(2);
        }

        void OnButton3Click(object sender, RoutedEventArgs args)
        {
            AddDigit(3);
        }

        void OnButton4Click(object sender, RoutedEventArgs args)
        {
            AddDigit(4);
        }

        void OnButton5Click(object sender, RoutedEventArgs args)
        {
            AddDigit(5);
        }

        void OnButton6Click(object sender, RoutedEventArgs args)
        {
            AddDigit(6);
        }

        void OnButton7Click(object sender, RoutedEventArgs args)
        {
            AddDigit(7);
        }

        void OnButton8Click(object sender, RoutedEventArgs args)
        {
            AddDigit(8);
        }

        void OnButton9Click(object sender, RoutedEventArgs args)
        {
            AddDigit(9);
        }

        void OnButtonComaClick(object sender, RoutedEventArgs args)
        {
            coma = true;
        }

        void OnButtonSignClick(object sender, RoutedEventArgs args)
        {
            double displayValue = 0;
            switch(State)
            {
                case CalcState.CannotDivideByZero:
                    return;
                case CalcState.FirstOperand:
                    operand1 = -operand1;
                    displayValue = operand1;
                    break;
                case CalcState.SecondOperand:
                    operand2 = -operand2;
                    displayValue = operand2;
                    break;
            }
            textBox1.Text = displayValue.ToString();
        }

        void OnButtonEqualClick(object sender, RoutedEventArgs args)
        {
            DoOperation(CalcOperation.Equal);
        }

        void OnButtonDivClick(object sender, RoutedEventArgs args)
        {
            DoOperation(CalcOperation.Divide);
        }

        void OnButtonMulClick(object sender, RoutedEventArgs args)
        {
            DoOperation(CalcOperation.Multiply);
        }

        void OnButtonSubClick(object sender, RoutedEventArgs args)
        {
            DoOperation(CalcOperation.Substract);
        }

        void OnButtonAddClick(object sender, RoutedEventArgs args)
        {
            DoOperation(CalcOperation.Add);
        }

        void OnButtonBackClick(object sender, RoutedEventArgs args)
        {
            double displayValue = 0;
            switch (State)
            {
                case CalcState.CannotDivideByZero:
                    return;
                case CalcState.FirstOperand:
                    operand1 /= 10;
                    displayValue = operand1;
                    break;
                case CalcState.SecondOperand:
                    operand2 /= 10;
                    displayValue = operand2;
                    break;
            }
            textBox1.Text = displayValue.ToString();
        }

        void OnButtonClearClick(object sender, RoutedEventArgs args)
        {
            State = CalcState.FirstOperand;
            LastOperation = CalcOperation.Equal;
            operand1 = 0;
            operand2 = 0;
            textBox1.Text = operand1.ToString();
        }

    }
}