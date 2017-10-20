using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator_Rohde_Nick
{
    public partial class Calculator : Form
    {
        Regex regex; // used to initially verify input

        public Calculator()
        {
            InitializeComponent();
            regex = new Regex("[0-9-+/*()^.]"); // initialise regex for input strings
        } // end default constructor

        private void verifyInput(object sender, EventArgs e)
        {
            // filter out illegal characters with regex
            if (regex.IsMatch(display.Text))
            {
                for(;;)
                {
                    try
                    {
                        parseInput(display.Text); // parse input
                        break; // break loop if parse is successful
                    }
                    catch (ArgumentException) // invalid expression
                    {
                        error.Text = "Invalid expression received, try again.";
                        return;
                    }
                }
            } // end if 
            else // illegal characters detected
            {
                error.Text = "Invalid input received, try again.";
                error.Visible = true;
            } // end else
        } // end method verifyInput


        private void parseInput(string input)
        {
            Stack<string> stack = new Stack<string>();

            Regex operators = new Regex("[-+/*()^]");

            string temp = "";

            bool noOperator = true;

            for(int i = 0; i < input.Length; i++)
            {
                // if the current is an operator, push the number, and the operator on the stack
                if(operators.IsMatch(input[i].ToString()))
                {
                    if(noOperator)
                    {
                        throw new ArgumentException();
                    }

                    noOperator = true; // next character can't be an operator
                    stack.Push(temp);
                    stack.Push(input[i].ToString());
                } // end if
                else // parse number into string
                {
                    noOperator = false; // next character can be an operator
                    temp += input[i];
                }

            }// end for  

        } // end method parseInput


        // Button click handlers
        private void digit0Press    (object sender, EventArgs e)
        {
            display.Text += "0";
        }
        private void digit1Press    (object sender, EventArgs e)
        {
            display.Text += "1";
        }
        private void digit2Press    (object sender, EventArgs e)
        {
            display.Text += "2";
        }
        private void digit3Press    (object sender, EventArgs e)
        {
            display.Text += "3";
        }
        private void digit4Press    (object sender, EventArgs e)
        {
            display.Text += "4";
        }
        private void digit5Press    (object sender, EventArgs e)
        {
            display.Text += "5";
        }
        private void digit6Press    (object sender, EventArgs e)
        {
            display.Text += "6";
        }
        private void digit7Press    (object sender, EventArgs e)
        {
            display.Text += "7";
        }
        private void digit8Press    (object sender, EventArgs e)
        {
            display.Text += "8";
        }
        private void digit9Press    (object sender, EventArgs e)
        {
            display.Text += "9";
        }
        private void opDivPress     (object sender, EventArgs e)
        {
            display.Text += "/";
        }
        private void opMulPress     (object sender, EventArgs e)
        {
            display.Text += "*";
        }
        private void opSubPress     (object sender, EventArgs e)
        {
            display.Text += "-";
        }
        private void opAddPress     (object sender, EventArgs e)
        {
            display.Text += "+";
        }
        private void opExpPress     (object sender, EventArgs e)
        {
            display.Text += "^";
        }
        private void openParenPress (object sender, EventArgs e)
        {
            display.Text += "(";
        }
        private void closeParenPress(object sender, EventArgs e)
        {
            display.Text += ")";
        }
    } // end class Calculator
} // end namespace Calculator