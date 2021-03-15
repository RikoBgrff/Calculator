using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double firstNumber;
        double secondNumber;
        static bool isPressedOperator = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void zeroBtn_Click(object sender, EventArgs e)
        {
            if (isPressedOperator == false)
            {
                firstNumber = double.Parse(zeroBtn.Text);
            }
            else
            {
                secondNumber = double.Parse(zeroBtn.Text);
            }
        }

        private void plusBtn_Click(object sender, EventArgs e)
        {
            isPressedOperator = true;
            resultLbl.Text = Convert.ToString(firstNumber + secondNumber);
        }
        private void minusBtn_Click(object sender, EventArgs e)
        {
            isPressedOperator = true;
        }

        private void divideBtn_Click(object sender, EventArgs e)
        {
            isPressedOperator = true;
        }

        private void multipleBtn_Click(object sender, EventArgs e)
        {
            isPressedOperator = true;
        }

        private void equalBtn_Click(object sender, EventArgs e)
        {
            isPressedOperator = true;
        }
    }
}
