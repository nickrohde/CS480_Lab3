using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator_Rohde_Nick
{
    public partial class Calculator : Form
    {
        double previousAnswer;


        public Calculator()
        {
            InitializeComponent();
            previousAnswer = 0;

        } // end default constructor


        // Verifies the user input, and then sends it off to be parsed, and evaluated
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

                catch (ArgumentException exception) // invalid expression
                {
                    handleArgException(exception);
                    error.Text = "Invalid expression received, try again.";
                    error.Visible = true;
                } // end catch
                
                catch (DivideByZeroException)
                {
                    handleDivByZeroException();
                    error.Text = "Your expression contains a division by 0. This is not allowed.";
                    error.Visible = true;
                }
            } // end if 

            else // illegal characters detected
            {
                MessageBox.Show("The expression you entered contains illegal characters." +
                                "\n\nLegal characters:\n    Digits: 0-9\n    Operators: + - * / ^ " +
                                "\n\nPlease try again.", "Illegal Characters in Expression!");
                error.Text = "The expression you entered contains illegal characters, try again.";
                error.Visible = true;
            } // end else
        } // end method verifyInput


        // Parses input turning the infix into postfix, then sends the postfix to evaluateInput
        private void parseInput(string input)
        {
            // Objects
            Stack<string> stack = new Stack<string>();

            Regex operators   = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers     = new Regex("[0-9.]") ; // regex to detect numbers


            // Variables
            string expression = ""                  ;

            int    i          = 0                   ;
                

            // check if expression is valid
            try
            {
                input = isValidExpression(input);
                
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
                    while(stack.Count > 0 && stack.Peek() != "(")
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

            try
            {
                evaluateExpression(expression);
            }
            catch(Exception exception)
            {
                throw exception;
            }    
        } // end method parseInput


        // Verifies the expression entered is syntactically valid
        private string isValidExpression(string input)
        {
            // Objects
            Regex operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers   = new Regex("[0-9]")  ; // regex to detect numbers


            // Variables
            bool noOperator           = true      , // keeps track of whether an operator is allowed
                 noPeriod             = false     , // keeps track of whether a period is allowed
                 omittedMultiplyStart = false     , // handles the case a(b) = a*(b)
                 omittedMultiplyEnd   = false     ; // handles the case (a)b = (a)*b

            uint open                 = 0         ; // keeps track of open parentheses


            // leading negative is allowed
            if(input[0] == '-')
            {
                int i = 1;

                while(i < input.Length && numbers.IsMatch(input[i].ToString()))
                {
                    i++;
                }

                input = input.Insert(0, "(0");

                input = input.Insert(i+2, ")");
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
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end if
                    noPeriod = true;
                } // end elif
                else if (operators.IsMatch(input[i].ToString()))
                {
                    if(noOperator && input[i] == '-')
                    {
                        int k = i+1;

                        while (k < input.Length && numbers.IsMatch(input[k].ToString()))
                        {
                            k++;
                        }

                        input = input.Insert(i, "(0");

                        input = input.Insert(k + 2, ")");
                        i = k+2;
                    }
                    else if (noOperator)
                    {
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end if

                    omittedMultiplyEnd = false;
                    omittedMultiplyStart = false;
                    noOperator = true; // next character can't be an operator
                    noPeriod = false;
                } // end elif
                else if (input[i] == '(')
                {
                    if(omittedMultiplyStart)
                    {
                        input = input.Insert(i, "*");
                        i++;
                        omittedMultiplyStart = false;
                        omittedMultiplyEnd = false;
                    }

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
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end else

                    omittedMultiplyEnd = true;
                    noOperator = false; // next character can be an operator
                    noPeriod = false;
                } // end elif
                else if (numbers.IsMatch(input[i].ToString()))// parse number into string
                {
                    if (omittedMultiplyEnd)
                    {
                        input = input.Insert(i, "*");
                        i++;
                        omittedMultiplyStart = false;
                        omittedMultiplyEnd = false;
                    }
                    omittedMultiplyStart = true;
                    noOperator = false; // next character can be an operator
                } // end else
                // Illegal character in string
                else
                {
                    throw new ArgumentException(i.ToString() + " " + input);
                } // end else
            }// end for

            if(noOperator)
            {
                throw new ArgumentException("-1 " + input);
            } // end if

            return input;

        } // end method isValidExpression


        // Evaluated the postfix expression from parseInput
        private void evaluateExpression(string input)
        {
            if(input.Length == 0)
            {
                throw new ArgumentException("-1");
            }

            // Objects
            Stack<double> stack = new Stack<double>();


            // Variables
            double result = 0;

            string[] expression = input.Split();

            for(int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == " " || expression[i] == "")
                {
                    continue;
                }

                double temp = 0;

                if (double.TryParse(expression[i], out temp))
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
                            if (a == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            else
                            {
                                stack.Push(b / a);
                            }
                            break;
                        case ("^"):
                            stack.Push(Math.Pow(b,a));
                            break;
                        default:
                            throw new ArgumentException("-1");
                    } // end switch
                } // end if
            } // end for

            result = stack.Pop();
            previousAnswer = result;

            if (display.Text.Length <= 25)
            {
                resultLabel.Text = display.Text;
            }
            else
            {
                resultLabel.Text = "result";
            }
      
            resultLabel.Text += " = ";
            resultLabel.Text += result.ToString();
            resultLabel.Visible = true;
        } // end method evaluateExpression


        // Exception Handlers:
        // ArgumentException handler
        private void handleArgException(ArgumentException exception)
        {
            int index = 0;

            string errorMessage = "There was an issue with the expression you entered.";

            string[] message    = exception.Message.Split();


            if (!(int.TryParse(message[0], out index)))
            {
                MessageBox.Show(errorMessage + " Please try again.", "Invalid expression!");
                return;
            }

            if (index == -1 || message.Length != 2)
            {
                MessageBox.Show(errorMessage + " Please try again.", "Invalid Expression!");
                return;
            }

            errorMessage += " The error occured at the ";

            index++;

            errorMessage += index.ToString();

            if(index == 1)
            {
                errorMessage += "st";
            }
            else if (index == 2)
            {
                errorMessage += "nd";
            }
            else if(index == 3)
            {
                errorMessage += "rd";
            }
            else
            {
                errorMessage += "th";
            }

            errorMessage += " postion in the expression.";
            errorMessage += "\n\nThe expression was interpreted as: " + message[1];
            errorMessage += "\n\nPlease check the expression, and try again.";

            MessageBox.Show(errorMessage, "Invalid Expression!");

        } // end method handleArgException


        // Division by zero handler
        private void handleDivByZeroException()
        {
            MessageBox.Show("The expression you entered contains a division by 0. This is not a legal operation. Please try again.", "Divison by 0 in Expression!");
        } // end method handleDivByZeroException


        // Button click event handlers:
        private void digit0Press    (object sender, EventArgs e)
        {
            display.Text += "0";
            enter.Focus();
        }
        private void digit1Press    (object sender, EventArgs e)
        {
            display.Text += "1";
            enter.Focus();
        }
        private void digit2Press    (object sender, EventArgs e)
        {
            display.Text += "2";
            enter.Focus();
        }
        private void digit3Press    (object sender, EventArgs e)
        {
            display.Text += "3";
            enter.Focus();
        }
        private void digit4Press    (object sender, EventArgs e)
        {
            display.Text += "4";
            enter.Focus();
        }
        private void digit5Press    (object sender, EventArgs e)
        {
            display.Text += "5";
            enter.Focus();
        }
        private void digit6Press    (object sender, EventArgs e)
        {
            display.Text += "6";
            enter.Focus();
        }
        private void digit7Press    (object sender, EventArgs e)
        {
            display.Text += "7";
            enter.Focus();
        }
        private void digit8Press    (object sender, EventArgs e)
        {
            display.Text += "8";
            enter.Focus();
        }
        private void digit9Press    (object sender, EventArgs e)
        {
            display.Text += "9";
            enter.Focus();
        }
        private void opDivPress     (object sender, EventArgs e)
        {
            display.Text += "/";
            enter.Focus();
        }
        private void opMulPress     (object sender, EventArgs e)
        {
            display.Text += "*";
            enter.Focus();
        }
        private void opSubPress     (object sender, EventArgs e)
        {
            display.Text += "-";
            enter.Focus();
        }
        private void opAddPress     (object sender, EventArgs e)
        {
            display.Text += "+";
            enter.Focus();
        }
        private void opExpPress     (object sender, EventArgs e)
        {
            display.Text += "^";
            enter.Focus();
        }
        private void openParenPress (object sender, EventArgs e)
        {
            display.Text += "(";
            enter.Focus();
        }
        private void closeParenPress(object sender, EventArgs e)
        {
            display.Text += ")";
            enter.Focus();
        }
        private void clearPress     (object sender, EventArgs e)
        {
            display.Text = "";
            enter.Focus();
        }
        private void periodPress    (object sender, EventArgs e)
        {
            display.Text += ".";
            enter.Focus();
        }
        private void insertAnswer   (object sender, EventArgs e)
        {
            display.Text += previousAnswer.ToString();
            enter.Focus();
        }
        private void insertPi       (object sender, EventArgs e)
        {
            display.Text += Math.PI.ToString().Remove(8);
            enter.Focus();
        }
        private void insertE        (object sender, EventArgs e)
        {
            display.Text += Math.E.ToString().Remove(8);
            enter.Focus();
        }


        // Key press event handler
        private void keyboardPress  (object sender, KeyPressEventArgs e)
        {
            char temp = e.KeyChar;

            if(sender.Equals(display))
            {
                if(temp == '\u001b')
                {
                    display.Text = "";
                }
                return;
            }
            
            switch(temp)
            {
                case ('0'):
                    display.Text += "0";
                    break;
                case ('1'):
                    display.Text += "1";
                    break;
                case ('2'):
                    display.Text += "2";
                    break;
                case ('3'):
                    display.Text += "3";
                    break;
                case ('4'):
                    display.Text += "4";
                    break;
                case ('5'):
                    display.Text += "5";
                    break;
                case ('6'):
                    display.Text += "6";
                    break;
                case ('7'):
                    display.Text += "7";
                    break;
                case ('8'):
                    display.Text += "8";
                    break;
                case ('9'):
                    display.Text += "9";
                    break;
                case ('.'):
                    display.Text += ".";
                    break;
                case ('*'):
                    display.Text += "*";
                    break;
                case ('/'):
                    display.Text += "/";
                    break;
                case ('-'):
                    display.Text += "-";
                    break;
                case ('+'):
                    display.Text += "+";
                    break;
                case ('\u001b'):
                    display.Text = "";
                    break;
                default:
                    break;
            }
        }

        
    } // end class Calculator
} // end namespace Calculator