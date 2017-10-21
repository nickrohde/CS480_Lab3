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
                  numbers   = new Regex("[0-9.]") ; // regex to detect numbers


            // Variables
            string expression = "";

            int    i          = 0 ;


            // check if expression is valid
            try
            {
                isValidExpression(input);
                
            } // end try
            catch (ArgumentException e)
            {
                throw e; // handled by verifyInput
            } // end catch
            
            // turn infix expression into postfix expression
            while(i < input.Length)
            {
                if(input[i] == ' ')
                {
                    i++;
                    continue; // ignore spaces
                } // end if
                else if (numbers.IsMatch(input[i].ToString()) || input[i] == '.')// parse number into string
                {
                    string temp = "";
                    while (numbers.IsMatch(input[i].ToString()) || input[i] == '.')
                    {
                        temp += input[i].ToString();
                        if (i < input.Length-1)
                        {
                            i++;
                        } // end if
                        else
                        {
                            i++;
                            break; 
                        }
                    } // end while
                    expression += temp;
                    expression += " ";
                    continue;
                } // end else
                else if (input[i] == '(')
                {
                    stack.Push(input[i].ToString());
                } // end elif
                else if (input[i] == ')')
                {
                    while(stack.Count > 0 && stack.Peek() != ")")
                    {
                        string temp = "";
                        if (numbers.IsMatch(stack.Peek()) || stack.Peek() == ".")
                        {
                            while (stack.Count > 0 && (numbers.IsMatch(stack.Peek()) || stack.Peek() == "."))
                            {
                                temp += stack.Pop();
                            } // end while
                        } // end if
                        else
                        {
                            temp = stack.Pop();
                        } // end else

                        temp += " ";
                        expression += temp;
                        Console.Out.WriteLine(expression);
                    } // end while
                    
                    stack.Pop(); // pop left parenthesis off the stack
                } // end elif
                else if (operators.IsMatch(input[i].ToString()))
                {
                    if(stack.Count == 0 || stack.Peek() == "(")
                    {
                        stack.Push(input[i].ToString());
                    } // end if
                    else
                    {
                        while(stack.Count != 0 && stack.Peek() != "(")
                        {
                            if (((input[i] == '*' || input[i] == '/') && (stack.Peek() == "+" || stack.Peek() == "-")))
                            {
                                break;
                            } // end if
                            if ((input[i] == '^' && (stack.Peek() == "+" || stack.Peek() == "-" || stack.Peek() == "*" || stack.Peek() == "/")))
                            {
                                break;
                            } // end if

                            string temp = "";

                            if (numbers.IsMatch(stack.Peek()))
                            {
                                while (numbers.IsMatch(stack.Peek()))
                                {
                                    temp += stack.Pop();
                                } // end while
                            } // end if
                            else
                            {
                                temp = stack.Pop();
                            } // end else

                            expression += temp;
                            expression += " ";
                        } // end while

                        stack.Push(input[i].ToString());

                    } // end else
                } // end elif
                i++;
            } // end while

            while(stack.Count > 0)
            {
                string temp = "";

                if (numbers.IsMatch(stack.Peek()))
                {
                    while (stack.Count > 0 && numbers.IsMatch(stack.Peek()))
                    {
                        temp += stack.Pop();
                    } // end while
                } // end if
                else
                {
                    temp = stack.Pop();
                } // end else

                expression += temp;
                if (stack.Count != 0)
                {
                    expression += " ";
                } // end if
            } // end while

            evaluateExpression(expression);
                
        } // end method parseInput

        private void isValidExpression(string input)
        {
            // Objects
            Regex operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers   = new Regex("[0-9]")  ; // regex to detect numbers


            // Variables
            bool noOperator = true                , // keeps track of whether an operator is allowed
                 noPeriod   = false               ; // keeps track of whether a period is allowed

            uint open       = 0                   ; // keeps track of open parentheses


            // leading negative is allowed
            if(input[0] == '-')
            {
                noOperator = false;
            } // end if

            // verify this is a valid expression
            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == ' ')
                {
                    continue; // ignore spaces
                } // end if
                else if(input[i] == '.')
                {
                    if(noPeriod)
                    {
                        throw new ArgumentException();
                    } // end if
                    noPeriod = true;
                } // end elif
                else if (operators.IsMatch(input[i].ToString()))
                {
                    if (noOperator)
                    {
                        throw new ArgumentException();
                    } // end if

                    noOperator = true; // next character can't be an operator
                    noPeriod = false;
                } // end elif
                else if (input[i] == '(')
                {
                    open++;
                    noOperator = true; // open parenthesis can't be followed by operator
                    noPeriod = false;
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
                    noPeriod = false;
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

            if(noOperator)
            {
                throw new ArgumentException();
            } // end if

        } // end method isValidExpression

        private void evaluateExpression(string input)
        {
            if(input.Length == 0)
            {
                throw new ArgumentException();
            }

            // Objects
            Stack<double> stack = new Stack<double>();


            // Variables
            double result = 0;

            string[] expression = input.Split();

            for(int i = 0; i < expression.Length; i++)
            {
                double temp = 0;
                if(double.TryParse(expression[i], out temp))
                {
                    stack.Push(temp);
                } // end if

                else
                {
                    double a = stack.Pop(),
                           b = stack.Pop();

                    switch(expression[i])
                    {
                        case ("+"):
                            stack.Push(b + a);
                            break;
                        case ("-"):
                            stack.Push(b - a);
                            break;
                        case ("*"):
                            stack.Push(b * a);
                            break;
                        case ("/"):
                            stack.Push(b / a);
                            break;
                        case ("^"):
                            stack.Push(Math.Pow(b,a));
                            break;
                        default:
                            throw new ArgumentException();
                    } // end switch
                } // end if
            } // end for

            result = stack.Pop();
            resultLabel.Text = display.Text;
            resultLabel.Text += " = ";
            resultLabel.Text += result.ToString();
            resultLabel.Visible = true;
        } // end method evaluateExpression

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