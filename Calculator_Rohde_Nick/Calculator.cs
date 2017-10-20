using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator_Rohde_Nick
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();

        } // end default constructor

        private void verifyInput(object sender, EventArgs e)
        {
            // Objects
            Regex regex = new Regex("[0-9-+/*()^.]"); // used to initially verify input


            // filter out illegal characters with regex
            if (regex.IsMatch(display.Text))
            {
                try
                {
                    parseInput(display.Text); // parse input
                    error.Visible = false;
                } // end try

                catch (ArgumentException) // invalid expression
                {
                    error.Text = "Invalid expression received, try again.";
                    error.Visible = true;
                } // end catch
            } // end if 

            else // illegal characters detected
            {
                error.Text = "Invalid input received, try again.";
                error.Visible = true;
            } // end else
        } // end method verifyInput


        private void parseInput(string input)
        {
            // Objects
            Stack<string> stack = new Stack<string>();

            Regex operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers   = new Regex("[0-9.]")  ; // regex to detect numbers


            // Variables
            string expression = "";


            try
            {
                isValidExpression(input);
                
            } // end try
            catch (ArgumentException e)
            {
                throw e; // handled by verifyInput
            } // end catch
            
            // turn infix expression into postfix expression
            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] == ' ')
                {
                    continue; // ignore spaces
                }
                else if (operators.IsMatch(input[i].ToString()))
                {
                    expression += input[i].ToString() + " ";
                } // end if
                else if (input[i] == '(')
                {
                    stack.Push(input[i].ToString());
                } // end elif
                else if (input[i] == ')')
                {
                    while(stack.Count > 0 && stack.Peek() != ")")
                    {
                        string temp = "";
                        if (numbers.IsMatch(stack.Peek()))
                        {
                            while (numbers.IsMatch(stack.Peek()))
                            {
                                temp += stack.Pop();
                                
                            }
                        }
                        else
                        {
                            temp = stack.Pop();
                        }
                        display.Text += temp;
                        expression += temp + " ";
                    } // end while
                    
                    stack.Pop(); // pop left parenthesis off the stack
                } // end elif
                else if (numbers.IsMatch(input[i].ToString()))// parse number into string
                {
                    
                } // end else
            } // end for

            evaluateExpression(expression);
                
        } // end method parseInput

        private void isValidExpression(string input)
        {
            // Objects
            Regex operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers   = new Regex("[0-9]")  ; // regex to detect numbers


            // Variables
            bool noOperator = true                , // keeps track of whether an operator is allowed
                 noPeriod   = true                ; // keeps track of whether a period is allowed

            uint open       = 0                   ; // keeps track of open parentheses


            // leading negative is allowed
            if(input[0] == '-')
            {
                noOperator = false;
            }

            // verify this is a valid expression
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == ' ')
                {
                    continue; // ignore spaces
                }
                else if (operators.IsMatch(input[i].ToString()))
                {
                    if (noOperator)
                    {
                        throw new ArgumentException();
                    } // end if

                    noOperator = true; // next character can't be an operator
                } // end if
                else if (input[i] == '(')
                {
                    open++;
                    noOperator = true; // open parenthesis can't be followed by operator
                } // end elif
                else if (input[i] == ')')
                {
                    if(open > 0)
                    {
                        open--;
                    } // end if
                    else
                    {
                        throw new ArgumentException();
                    } // end else
                    noOperator = false; // next character can be an operator
                } // end elif
                else if (numbers.IsMatch(input[i].ToString()))// parse number into string
                {
                    noOperator = false; // next character can be an operator
                } // end else
                // Illegal character in string
                else
                {
                    throw new ArgumentException();
                } // end else
            }// end for  
        } // end method isValidExpression

        private void evaluateExpression(string expression)
        {
            double result = 0;



            resultLabel.Text = result.ToString();
        }

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
        private void clearPress     (object sender, EventArgs e)
        {
            display.Text = "";
        }
    } // end class Calculator
} // end namespace Calculator