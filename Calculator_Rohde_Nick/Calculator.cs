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
            enter.Focus();
        } // end default constructor


        // Verifies the user input, and then sends it off to be parsed, and evaluated
        private void verifyInput(object sender, EventArgs e)
        {
            // Variables
            Regex regex = new Regex("[0-9-+/*()^.]"); // used to initially verify input
            

            if (display.Text.Length == 0)
            {
                resultLabel.Text = "= 0";
                return;
            } // end if

            // filter out illegal characters with regex
            if (regex.IsMatch(display.Text))
            {
                try
                {
                    parseInput(display.Text); // parse input
                } // end try
                // handle invalid expression errors
                catch (ArgumentException exception) // invalid expression
                {
                    handleArgException(exception);
                } // end catch
                // handle division by 0 errors
                catch (DivideByZeroException)
                {
                    handleDivByZeroException();
                } // end catch
                // handle invalid operation errors
                catch (ArithmeticException exception)
                {
                    handleArithmeticException(exception);
                } // end catch
                // handle all other exception that might occur
                catch (Exception exception)
                {
                    handleUnknownException(exception);
                } // end catch
            } // end if 

            else // illegal characters detected by regex
            {
                MessageBox.Show("The expression you entered contains illegal characters." +
                                "\n\nLegal characters:\n    Digits: 0-9\n    Operators: + - * / ^ " +
                                "\n\nPlease try again.", "Illegal Characters in Expression!");
            } // end else
        } // end method verifyInput


        // Parses input turning the infix into postfix, then sends the postfix to evaluateInput
        private void parseInput(string input)
        {
            // Variables
            Stack<string> stack = new Stack<string>();

            Regex operators   = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers     = new Regex("[0-9.]") ; // regex to detect numbers

            string expression = ""                  ;

            int    i          = 0                   ;
                

            // check if expression is valid
            try
            {
                input = isValidExpression(input);
            } // end try
            catch (Exception e)
            {
                throw e; // exceptions are handled by verifyInput
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
                        } // end else
                    } // end while
                    expression += temp;
                    expression += " " ;
                    continue;
                } // end else
                else if (input[i] == '(')
                {
                    stack.Push(input[i].ToString());            
                } // end elif
                else if (input[i] == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        string temp = stack.Pop();

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

                            string temp = stack.Pop();

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
                
                temp = stack.Pop();
                expression += temp;

                if (stack.Count != 0)
                {
                    expression += " ";
                } // end if
            } // end while

            try
            {
                evaluateExpression(expression);
            } // end try
            catch (Exception exception)
            {
                throw exception;
            } // end catch
        } // end method parseInput


        // Verifies the expression entered is syntactically valid
        private string isValidExpression(string input)
        {
            // Variables
            Regex operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  numbers   = new Regex("[0-9.]") ; // regex to detect numbers
            
            bool  noOperator           = true     , // keeps track of whether an operator is allowed
                  noPeriod             = false    , // keeps track of whether a period is allowed
                  omittedMultiplyStart = false    , // used to do the conversion a(b) = a*(b)
                  omittedMultiplyEnd   = false    ; // used to do the conversion (a)b = (a)*b

            int  open                  = 0        , // keeps track of open parentheses
                 i                     = 0        ; // index for parsing input

            // leading negative is allowed
            if (input[0] == '-')
            {
                i = 6;
                input = input.Remove(0, 1);
                input = input.Insert(0, "(0-1)*");
            } // end if

            // verify this is a valid expression
            for (; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    continue; // ignore spaces
                } // end if
                else if (input[i] == '.')
                {
                    if (noPeriod)
                    {
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end if
                    noPeriod = true;
                } // end elif
                else if (operators.IsMatch(input[i].ToString()))
                {
                    if (noOperator && input[i] == '-')
                    {
                        input = input.Remove(i, 1);
                        input = input.Insert(i, "(0-1)*");
                        i += 5;
                        noOperator = false; // next character may be an operator
                        continue;
                    } // end if
                    else if (noOperator)
                    {
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end elif

                    noOperator = true; // next character can't be an operator
                    omittedMultiplyEnd   = false;
                    omittedMultiplyStart = false;
                    noPeriod   = false;
                } // end elif
                else if (input[i] == '(')
                {
                    if (omittedMultiplyStart)
                    {
                        input = input.Insert(i, "*");
                        i++;
                        omittedMultiplyStart = false;
                        omittedMultiplyEnd   = false;
                    } // end if

                    open++;
                    noOperator = true; // open parenthesis can't be followed by operator
                    noPeriod   = false;
                } // end elif
                else if (input[i] == ')')
                {
                    if (open > 0)
                    {
                        open--;
                    } // end if
                    else
                    {
                        throw new ArgumentException(i.ToString() + " " + input);
                    } // end else

                    omittedMultiplyEnd = true;
                    noOperator = false; // next character can be an operator
                    noPeriod   = false;
                } // end elif
                else if (numbers.IsMatch(input[i].ToString())) // parse number into string
                {
                    if (omittedMultiplyEnd)
                    {
                        input = input.Insert(i, "*");
                        i++;
                        omittedMultiplyStart = false;
                        omittedMultiplyEnd   = false;
                    } // end if
                    omittedMultiplyStart = true;
                    noOperator = false; // next character can be an operator
                } // end else
                // Illegal character in string
                else
                {
                    throw new ArgumentException(i.ToString() + " " + input);
                } // end else
            }// end for

            if (open > 0)
            {
                throw new ArgumentException("-1 " + input);
            } // end if

            if (noOperator)
            {
                throw new ArgumentException("-1 " + input);
            } // end if

            return input;
        } // end method isValidExpression


        // Evaluated the postfix expression from parseInput
        private void evaluateExpression(string input)
        {
            // Variables
            Stack<double> stack = new Stack<double>();

            double result = 0;

            string[] expression = input.Split();


            for(int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == " " || expression[i] == "")
                {
                    continue;
                } // end if

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
                            } // end if
                            else
                            {
                                stack.Push(b / a);
                            } // end else
                            break;
                        case ("^"):
                            if (a == 0 && b == 0)
                            {
                                throw new ArithmeticException("0^0");
                            } // end if
                            stack.Push(Math.Pow(b,a));
                            break;
                        default:
                            throw new ArgumentException("-1");
                    } // end switch
                } // end if
            } // end for

            result = stack.Pop();

            resultLabel.Text = "= ";
            resultLabel.Text += result.ToString();
            if(resultLabel.Text.Length > 70)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 7, System.Drawing.FontStyle.Bold);
            } // end if
            else if(resultLabel.Text.Length > 60)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 8, System.Drawing.FontStyle.Bold);
            } // end elif
            else if (resultLabel.Text.Length > 50)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 10, System.Drawing.FontStyle.Bold);
            } // end elif
            else if (resultLabel.Text.Length > 40)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 12, System.Drawing.FontStyle.Bold);
            } // end elif
            else if (resultLabel.Text.Length > 30)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 14, System.Drawing.FontStyle.Bold);
            } // end elif
            else if (resultLabel.Text.Length > 20)
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 16, System.Drawing.FontStyle.Bold);
            } // end elif
            else
            {
                resultLabel.Font = new System.Drawing.Font("Lucida Bright", 18, System.Drawing.FontStyle.Bold);
            } // end else
        } // end method evaluateExpression


        // Exception Handlers:
        // ArgumentException handler
        private void handleArgException(ArgumentException exception)
        {
            // Variables
            int    i_index        = 0                                                    ;

            string s_errorMessage = "There was an issue with the expression you entered.";

            string[] sa_message   = exception.Message.Split()                            ;


            if ((sa_message.Length > 0) && !(int.TryParse(sa_message[0], out i_index)))
            {
                MessageBox.Show(s_errorMessage + " Please try again.", "Invalid expression!");
                return;
            } // end if

            if (sa_message.Length != 2)
            {
                MessageBox.Show(s_errorMessage + " Please try again.", "Invalid Expression!");
                return;
            } // end if

            if (sa_message[1] == "-1")
            {
                s_errorMessage += "\n\nNo expression was entered.";
                MessageBox.Show(s_errorMessage, "Invalid Expression!");
                return;
            } // end if

            if (i_index > 0)
            {
                s_errorMessage += " The error occured at the ";

                i_index++;

                s_errorMessage += i_index.ToString();

                switch (i_index)
                {
                    case (1):
                        s_errorMessage += "st";
                        break;
                    case (2):
                        s_errorMessage += "nd";
                        break;
                    case (3):
                        s_errorMessage += "rd";
                        break;
                    default:
                        s_errorMessage += "th";
                        break;
                } // end switch

                s_errorMessage += " postion in the expression.";
            } // end if

            s_errorMessage += "\n\nThe expression was interpreted as: " + sa_message[1];
            s_errorMessage += "\n\nPlease check the expression, and try again.";

            MessageBox.Show(s_errorMessage, "Invalid Expression!");

        } // end method handleArgException


        // Division by zero handler
        private void handleDivByZeroException()
        {
            MessageBox.Show("The expression you entered contains a division by 0. This is not a legal operation. Please try again.", "Divison by 0 in Expression!");
            resultLabel.Text = display.Text + " = NaN";
            resultLabel.Visible = true;
        } // end method handleDivByZeroException


        // Invalid Operation Handler
        private void handleArithmeticException(ArithmeticException e)
        {
            if (e.Message == "0^0")
            {
                resultLabel.Text = display.Text + " = NaN";
                resultLabel.Visible = true;
            } // end if

            MessageBox.Show("The expression you entered contains an invalid operation, and cannot be evaluated. Please try again.", "Invalid Operation in Expression!");
        } // end method handleArithmeticException


        // Unknown exception handler
        private void handleUnknownException(Exception e)
        {
            string message = "The expression you entered could not be evaluated.\n\nReason:\n";
            if (e.Message != "Stack empty.")
            {
                message += e.Message;
            }
            else
            {
                message += "Your expression contains an illegal operation.";
            }

            message += "\n\nPlease try again.";
            MessageBox.Show(message, "There was a problem with your expression.");
        } // end method handleUnknownException


        // Button click event handlers
        private void digit0Press    (object sender, EventArgs e)
        {
            display.Text += "0";
            enter.Focus();
        } // end method digit0Press
        private void digit1Press    (object sender, EventArgs e)
        {
            display.Text += "1";
            enter.Focus();
        } // end method digit1Press
        private void digit2Press    (object sender, EventArgs e)
        {
            display.Text += "2";
            enter.Focus();
        } // end method digit2Press
        private void digit3Press    (object sender, EventArgs e)
        {
            display.Text += "3";
            enter.Focus();
        } // end method digit3Press
        private void digit4Press    (object sender, EventArgs e)
        {
            display.Text += "4";
            enter.Focus();
        } // end method digit4Press
        private void digit5Press    (object sender, EventArgs e)
        {
            display.Text += "5";
            enter.Focus();
        } // end method digit5Press
        private void digit6Press    (object sender, EventArgs e)
        {
            display.Text += "6";
            enter.Focus();
        } // end method digit6Press
        private void digit7Press    (object sender, EventArgs e)
        {
            display.Text += "7";
            enter.Focus();
        } // end method digit7Press
        private void digit8Press    (object sender, EventArgs e)
        {
            display.Text += "8";
            enter.Focus();
        } // end method digit8Press
        private void digit9Press    (object sender, EventArgs e)
        {
            display.Text += "9";
            enter.Focus();
        } // end method digit9Press
        private void opDivPress     (object sender, EventArgs e)
        {
            display.Text += "/";
            enter.Focus();
        } // end method opDivPress
        private void opMulPress     (object sender, EventArgs e)
        {
            display.Text += "*";
            enter.Focus();
        } // end method opMulPress
        private void opSubPress     (object sender, EventArgs e)
        {
            display.Text += "-";
            enter.Focus();
        } // end method opSubPress
        private void opAddPress     (object sender, EventArgs e)
        {
            display.Text += "+";
            enter.Focus();
        } // end method opAddPress
        private void opExpPress     (object sender, EventArgs e)
        {
            display.Text += "^";
            enter.Focus();
        } // end method opExpPress
        private void openParenPress (object sender, EventArgs e)
        {
            display.Text += "(";
            enter.Focus();
        } // end method openParenPress
        private void closeParenPress(object sender, EventArgs e)
        {
            display.Text += ")";
            enter.Focus();
        } // end method closeParenPress
        private void clearPress     (object sender, EventArgs e)
        {
            display.Text = "";
            enter.Focus();
        } // end method clearPress
        private void backspacePress (object sender, EventArgs e)
        {
            if (display.Text.Length > 0)
            {
                display.Text = display.Text.Remove(display.Text.Length - 1);
            } // end if
            enter.Focus();
        } // end method backspacePress
        private void periodPress    (object sender, EventArgs e)
        {
            display.Text += ".";
            enter.Focus();
        } // end method periodPress


        // Key press event handler
        private void keyboardPress  (object sender, KeyPressEventArgs e)
        {
            char c_temp = e.KeyChar;

            if(sender.Equals(display))
            {
                if(c_temp == '\u001b')
                {
                    display.Text = "";
                } // end if
                return;
            } // end if
            
            switch(c_temp)
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
                case ('('):
                    display.Text += "(";
                    break;
                case (')'):
                    display.Text += ")";
                    break;
                case ('\u001b'):
                    display.Text = "";
                    break;
                case ('\b'):
                    if(display.Text.Length > 0)
                        display.Text = display.Text.Remove(display.Text.Length - 1);
                    break;
                default:
                    break;
            } // end switch
        } // end method keyboardPress 


        // Returns focus to enter button after other buttons are clicked
        private void focusEnterKey  (object sender, EventArgs e)
        {
            enter.Focus();
        } // end method focusEnterKey
    } // end class Calculator
} // end namespace Calculator