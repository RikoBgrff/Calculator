﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void CalculateEquation()
        {
            this.ResultLbl.Text = ParseOperation();

            FocusInputText();
        }
        private string ParseOperation()
        {
            try
            {
                var input = this.ResultTxtbx.Text;

                input = input.Replace(" ", "");

                var operation = new Operation();
                var leftSide = true;

                for (int i = 0; i < input.Length; i++)
                {
                    if ("0123456789.".Any(c => input[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, input[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, input[i]);
                    }
                    else if ("+-*/".Any(c => input[i] == c))
                    {
                        if (!leftSide)
                        {
                            var operatorType = GetOperationType(input[i]);

                            if (operation.RightSide.Length == 0)
                            {
                                if (operatorType != OperationType.Sub)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number");

                                operation.RightSide += input[i];
                            }
                            else
                            {
                                operation.LeftSide = CalculateOperation(operation);

                                operation.OperationType = operatorType;

                                operation.RightSide = string.Empty;
                            }
                        }
                        else
                        {
                            var operatorType = GetOperationType(input[i]);

                            if (operation.LeftSide.Length == 0)
                            {
                                if (operatorType != OperationType.Sub)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an left side number");

                                operation.LeftSide += input[i];
                            }
                            else
                            {
                                operation.OperationType = operatorType;

                                leftSide = false;
                            }
                        }
                    }
                }

                return CalculateOperation(operation);
            }
            catch (Exception ex)
            {
                return $"Invalid equation. {ex.Message}";
            }
        }
        private string CalculateOperation(Operation operation)
        {
            decimal left = 0;
            decimal right = 0;

            if (string.IsNullOrEmpty(operation.LeftSide) || !decimal.TryParse(operation.LeftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");

            if (string.IsNullOrEmpty(operation.RightSide) || !decimal.TryParse(operation.RightSide, out right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");

            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();
                    case OperationType.Sub:
                        return (left - right).ToString();
                    case OperationType.Div:
                        return (left / right).ToString();
                    case OperationType.Mul:
                        return (left * right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown operator type when calculating operation. { operation.OperationType }");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {ex.Message}");
            }
        }
        private OperationType GetOperationType(char character)
        {
            switch (character)
            {
                case '+':
                    return OperationType.Add;
                case '-':
                    return OperationType.Sub;
                case '/':
                    return OperationType.Div;
                case '*':
                    return OperationType.Mul;
                default:
                    throw new InvalidOperationException($"Unknown operator type { character }");
            }
        }
        private string AddNumberPart(string currentNumber, char newCharacter)
        {
            if (newCharacter == '.' && currentNumber.Contains('.'))
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be added");

            return currentNumber + newCharacter;
        }
        private void InsertTextValue(string value)
        {
            var selectionStart = this.ResultTxtbx.SelectionStart;

            this.ResultTxtbx.Text = this.ResultTxtbx.Text.Insert(this.ResultTxtbx.SelectionStart, value);

            this.ResultTxtbx.SelectionStart = selectionStart + value.Length;

        }
        
        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Guna2CircleButton btn)
            {
                InsertTextValue(btn.Text);

                FocusInputText();
            }
        }
        private void FocusInputText()
        {
            this.ResultTxtbx.Focus();
        }
        private void DelBtn_Click(object sender, EventArgs e)
        {
            DeleteTextValue();

            FocusInputText();
        }
        private void DeleteTextValue()
        {
            if (this.ResultTxtbx.SelectionStart == 0)
                return;


            var selectionStart = this.ResultTxtbx.SelectionStart;

            this.ResultTxtbx.Text = this.ResultTxtbx.Text.Remove(this.ResultTxtbx.SelectionStart - 1, 1);

            this.ResultTxtbx.SelectionStart = selectionStart - 1;
        }
        private void EqualBtn_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }

        private void CeBtn_Click(object sender, EventArgs e)
        {
            ClearUserInput();
            ClearResult();
        }

        private void CBtn_Click(object sender, EventArgs e)
        {
            ClearUserInput();
        }

        private void ClearUserInput()
        {
            this.ResultTxtbx.Text = string.Empty;
        }

        private void ClearResult()
        {
            this.ResultLbl.Text = string.Empty;
        }

      
        private void plusBtn_Click(object sender, EventArgs e)
        {
            //double num1 = Convert.ToDouble(ResultTxtbx.Text);
            //ResultTxtbx.Text = "";
            //double num2 = Convert.ToDouble(ResultTxtbx.Text);
            //double sum = num1 + num2;
            if (!ResultTxtbx.Text.Contains("+"))
            {
            string s = ResultTxtbx.Text;

            string[] subs = s.Split('+');

            MessageBox.Show(subs[0]);
            }
            ResultTxtbx.Text += "+";
        }
    }
}
