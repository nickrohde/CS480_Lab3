/* * * * * * * * * * * * * * * * * *
 * Programmer: Nick Rohde          *
 * Project   : Lab 3 - Calculator  *
 * Class     : CS 480_003          *
 * Instructor: Szilard Vajda       *
 * Date      : 2nd November 2017   *
 *                                 *
 * Variable prefixes used:         *
 * rx_ : Regex object              *
 * d_  : double                    *
 * i_  : integer                   *
 * s_  : string                    *
 * sa_ : string array              *
 * * * * * * * * * * * * * * * * * */


// Includes:
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Calculator_Rohde_Nick
{
    public partial class Calculator : Form
    {
        /// <summary>
        /// Default Constructor. Initialises components, and puts the focus on the enter button.
        /// </summary>
        public Calculator()
        {
            InitializeComponent();
            enter.Focus();
        } // end default constructor


        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
         * verifyInput is called when the user presses the enter key, or clicks the enter button. It first       *
         * checks whether the function contains illegal characters using regex, and throws an exception if the   *
         * input contains illegal characters. Otherwise it sends the input to parseInput for syntax verification *
         * and evaluation. This method also handles all exceptions that may occur during program execution.      *
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
        /// <summary>
        /// Initial input verification, and handles errors during execution.
        /// </summary>
        /// <param name="sender">Unused parameter.</param>
        /// <param name="e">Unused parameter.</param>
        private void verifyInput(object sender, EventArgs e)
        {
            // Variables
            Regex rx_regex = new Regex("[0-9-+/*()^.]"); // used to initially verify input
            

            if (display.Text.Length == 0)
            {
                resultLabel.Text = "= 0";
                return;
            } // end if

            // filter out illegal characters with regex
            if (rx_regex.IsMatch(display.Text))
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

            else // illegal characters detected by regex (this shouldn't happen, but just in case)
            {
                MessageBox.Show("The expression you entered contains illegal characters." +
                                "\n\nLegal characters:\n    Digits: 0-9\n    Operators: + - * / ^ " +
                                "\n\nPlease try again.", "Illegal Characters in Expression!");
            } // end else
        } // end method verifyInput


        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
         * parseInput takes the input string received from verify input, and sends it to isValidExpression.      *
         * Once the expression has been verified to be of proper syntax, this function will create a new string  *
         * with the expression in it. This new string has a special format that can be interpreted by the        *
         * evaluateExpression function. Once the expression has been created, it calls evaluateExpression        *
         * and passes this expression to it.                                                                     *
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
         /// <summary>
         /// Parses user input, and sends it to evaluateExpression if parsing was successful.
         /// </summary>
         /// <param name="s_input">String containing the user input, which will be parsed into an expression.</param>
        private void parseInput(string s_input)
        {
            // Variables
            Stack<string> stack = new Stack<string>() ;

            Regex rx_operators  = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  rx_numbers    = new Regex("[0-9.]") ; // regex to detect numbers

            string s_expression = ""                  ; // expression to be evaluated

            int    i            = 0                   ; // index for parsing the string
                

            // check if expression is valid
            try
            {
                s_input = isValidExpression(s_input); // the returned string will have special format for certain operations
            } // end try
            catch (Exception e)
            {
                throw e; // exceptions are handled by verifyInput
            } // end catch
            
            // turn infix expression into postfix expression
            while(i < s_input.Length)
            {

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Number encountered: Since we are iterating character-by-character   *
                 * the loop will put all numbers into a string, and then add them to   *
                 * the expression altogether.                                          *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                if (rx_numbers.IsMatch(s_input[i].ToString()) || s_input[i] == '.')
                {
                    string s_temp = ""; // temp storage for number
                    while (rx_numbers.IsMatch(s_input[i].ToString()) || s_input[i] == '.')
                    {
                        s_temp += s_input[i].ToString();
                        if (i < s_input.Length-1)      // to avoid ArrayIndexExceptions
                        {
                            i++;
                        } // end if
                        else                         
                        {
                            i++;   // for error handling purposes
                            break; // this prevents an ArrayIndexException from being thrown in the loop
                        } // end else
                    } // end while
                    s_expression += s_temp; // add number to expression
                    s_expression += " " ;   // separator for evaluation purposes
                    continue;               // to prevent i from being incremented twice in a row
                } // end else

                // Push open parentheses on the stack to serve as sentinel when popping stack
                else if (s_input[i] == '(')
                {
                    stack.Push(s_input[i].ToString());            
                } // end elif

                // Closing parentheses require the stack to be popped until the open parenthesis is found
                else if (s_input[i] == ')')
                {
                    // pop the stack until the open parenthesis is found
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        string s_temp = stack.Pop();

                        s_temp += " ";          // separator for evaluation purposes
                        s_expression += s_temp; // add each operator to expression
                    } // end while

                    stack.Pop(); // pop left parenthesis off the stack
                } // end elif

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Operator encountered: Operators are pushed on the stack if stack is *
                 * empty, otherwise, precedence must be considered before adding it to *
                 * the stack.                                                          *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                else if (rx_operators.IsMatch(s_input[i].ToString()))
                {
                    if(stack.Count == 0 || stack.Peek() == "(") // precendence does not matter if stack is empty
                    {                                           // or this operator is in parentheses
                        stack.Push(s_input[i].ToString());
                    } // end if

                    // Stack is not empty and operator is not enclosed in parentheses
                    else
                    {
                        // pop operators from the stack until parenthesis is encountered, or the stack is empty
                        while(stack.Count != 0 && stack.Peek() != "(")
                        {
                            // if this operator is higher precendence than top of stack, we can stop
                            if (((s_input[i] == '*' || s_input[i] == '/') && (stack.Peek() == "+" || stack.Peek() == "-")))
                            {
                                break;
                            } // end if

                            // if this operator is higher precendence than top of stack, we can stop
                            if ((s_input[i] == '^' && (stack.Peek() == "+" || stack.Peek() == "-" || stack.Peek() == "*" || stack.Peek() == "/")))
                            {
                                break;
                            } // end if
                            
                            s_expression += stack.Pop();   // add operator to expression
                            s_expression += " ";           // separator for evaluation purposes
                        } // end while

                        stack.Push(s_input[i].ToString()); // push this operator onto the stack once the higher precedence operators are off

                    } // end else
                } // end elif
                i++;
            } // end while

            // If the stack has more operators, add them to end of expression
            while(stack.Count > 0)
            {
                s_expression += stack.Pop();

                if (stack.Count != 0) // to avoid a space at the end of the expression
                {
                    s_expression += " ";
                } // end if
            } // end while

            // Attempt to evaluate the parsed expression
            try
            {
                evaluateExpression(s_expression);
            } // end try

            // All exceptions are handled by verifyInput
            catch (Exception exception)
            {
                throw exception;
            } // end catch
        } // end method parseInput


        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
         * isValidExpression verifies that the input is syntactically correct. It also handles two special cases *
         * namely, ommitted multiplication symbols and negation (unary negative operator). When a parenthesis is *
         * followed or preceeded by a number (without an operator in between), it inserts a multiplication       *
         * operator into the expression. When a negative symbol is not preceeded by a number, a multiplication   *
         * by -1 is inserted into the expression to handle unary negative operations. If a syntax issues is      *
         * detected, this method will throw an ArgumentException with the index (or -1) and the modified input   *
         * string. If no issues are detected, it returns the modified string to parseInput.                      *
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
         /// <summary>
         /// Verifies that the input string contains a valid expression that can be parsed.
         /// </summary>
         /// <param name="s_input">Input string to be verified.</param>
         /// <returns>A new string that contains a valid expression which can be parsed.</returns>
        private string isValidExpression(string s_input)
        {
            // Variables
            Regex rx_operators = new Regex("[-+/*^]"), // regex to detect arithmetic operators
                  rx_numbers   = new Regex("[0-9.]") ; // regex to detect numbers
            
            bool  b_noOperator           = true      , // keeps track of whether an operator is allowed
                  b_noPeriod             = false     , // keeps track of whether a period is allowed
                  b_omittedMultiplyStart = false     , // used to do the conversion from a(b) to a*(b)
                  b_omittedMultiplyEnd   = false     ; // used to do the conversion from (a)b to (a)*b

            int  i_open                  = 0         , // keeps track of open parentheses
                 i                       = 0         ; // index for parsing input


            // leading negative is allowed
            if (s_input[0] == '-')
            {
                i = 6;
                s_input = s_input.Remove(0, 1);
                s_input = s_input.Insert(0, "(0-1)*");
            } // end if

            // verify this is a valid expression
            while (i < s_input.Length)
            {
                // period encountered
                if (s_input[i] == '.') 
                {
                    if (b_noPeriod) // double period not allowed i.e. "123..1"
                    {
                        throw new ArgumentException(i.ToString() + " " + s_input);
                    } // end if
                    b_noPeriod = true; // period has been encountered, now a number 
                                       // must be encountered next to be valid
                } // end elif

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Operator encountered: First check if it's a unary negative, if so   *
                 * insert a multiplication by -1. Otherwise, check if an operator has  *
                 * already been encountered. If so, it's an invalid expression.        *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                else if (rx_operators.IsMatch(s_input[i].ToString()))
                {
                    if (b_noOperator && s_input[i] == '-')     // unary negative case
                    {
                        s_input = s_input.Remove(i, 1);        // remove the unary negative
                        s_input = s_input.Insert(i, "(0-1)*"); // replace with multiplication by -1
                        i += 6;                                // set i to be behind the inserted piece
                        b_noOperator = false;                  // next character may be an operator
                        b_noPeriod   = false;                  // periods are allowed to follow an operator
                        continue;                              // to avoid the other variable setting to happen
                    } // end if
                    else if (b_noOperator)                     // two operators cannot follow eachother directly
                    {
                        throw new ArgumentException(i.ToString() + " " + s_input);
                    } // end elif

                    b_noOperator = true;            // next character can't be an operator
                    b_omittedMultiplyEnd   = false; // in case the last thing encountered was a parenthesis
                    b_omittedMultiplyStart = false; // in case the last thing encountered was a parenthesis
                    b_noPeriod  = false;            // periods are allowed to follow an operator
                } // end elif

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Open Parenthesis encountered: Check if there was an ommitted        *
                 * multiplication, and insert an operator if so. Otherwise, ignore it  *
                 * and increment the count of open parentheses encountered.            *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                else if (s_input[i] == '(')
                {
                    if (b_omittedMultiplyStart)
                    {
                        s_input = s_input.Insert(i, "*"); // insert a multiplication operator
                        i++;                              // skip over the inserted operator
                        b_omittedMultiplyStart = false;   // in case the last thing encountered was a parenthesis 
                        b_omittedMultiplyEnd   = false;   // in case the last thing encountered was a parenthesis
                    } // end if

                    i_open++;             // keep track of open pairs of parentheses
                    b_noOperator = true ; // open parenthesis can't be followed by operator
                    b_noPeriod   = false; // periods are allowed to follow a parenthesis
                } // end elif

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Close Parenthesis encountered: Check if there was an ommitted       *
                 * multiplication, and insert an operator if so. Otherwise, ignore it  *
                 * and decrement the count of open parentheses encountered.            *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                else if (s_input[i] == ')')
                {
                    if (i_open > 0) // check if there were any open parentheses so far
                    {
                        i_open--;   // close 1 pair 
                    } // end if
                    else            // no open parenthesis preceeded this one, invalid expression
                    {
                        throw new ArgumentException(i.ToString() + " " + s_input);
                    } // end else

                    b_omittedMultiplyEnd = true; // in case next is a number with no operator in between
                    b_noOperator = false;        // next character can be an operator
                    b_noPeriod   = false;        // periods are allowed to follow a parenthesis
                } // end elif

                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                 * Number encountered: Check if there was an ommitted multiplication   *
                 * if so, insert an operator. Otherwise, allow period/operator for     *
                 * next character.                                                     *
                 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
                else if (rx_numbers.IsMatch(s_input[i].ToString())) 
                {
                    if (b_omittedMultiplyEnd) // check if a multiplication was ommitted
                    {
                        s_input = s_input.Insert(i, "*"); // insert operator
                        i++;                              // skip this operator
                        b_omittedMultiplyEnd   = false;   // in case the last thing encountered was a parenthesis
                    } // end if

                    b_omittedMultiplyStart = true; // next character may be an open parenthesis with operator ommitted
                    b_noPeriod   = false;          // next character may be a period
                    b_noOperator = false;          // next character can be an operator
                } // end else

                // Illegal character in the string, invalid expression
                else
                {
                    throw new ArgumentException(i.ToString() + " " + s_input);
                } // end else

                i++;
            }// end for

            // Check if all parentheses were closed, if not, invalid expression
            if (i_open > 0)
            {
                throw new ArgumentException("-1 " + s_input);
            } // end if

            // Check if all operators have 2 operands, if not, invalid expression
            if (b_noOperator)
            {
                throw new ArgumentException("-1 " + s_input);
            } // end if

            return s_input; // return augmented expression to parseInput for processing
        } // end method isValidExpression


        /// <summary>
        /// Evaluates a postfix expression, and displays the result to the user.
        /// </summary>
        /// <param name="s_input">The postfix expression to be evaluated. Each operand/operator must be
        /// separated by a single space to be valid.</param>
        private void evaluateExpression(string s_input)
        {
            // Variables
            Stack<double> d_stack  = new Stack<double>(); // stores operands

            double   d_result      = 0                  ; // stores result

            string[] sa_expression = s_input.Split()    ; // turn expression into array for easier iteration


            // Evaluate the expression left to right
            for(int i = 0; i < sa_expression.Length; i++)
            {
                if(sa_expression[i] == " " || sa_expression[i] == "") // spaces/empty slots are ignored
                {                                                     // (this shouldn't be possible, but it's here just in case)
                    continue;
                } // end if

                double d_temp = 0; // temp storage for numbers

                // check if next is an operand or operator
                if (double.TryParse(sa_expression[i], out d_temp))
                {
                    d_stack.Push(d_temp);  // numbers are put on the stack until an operand is found
                } // end if
                
                else
                {
                    double a, b;           // temp storage for operands

                    if (d_stack.Count > 1) // take top two operands if available
                    {
                        a = d_stack.Pop(); // operand 2
                        b = d_stack.Pop(); // operand 1
                    } // end if
                    else // expression is invalid
                    {
                        throw new ArgumentException("-1");
                    } // end else

                    // evaluate
                    switch (sa_expression[i])
                    {
                        case ("+"):
                            d_stack.Push(b + a);
                            break;
                        case ("-"):
                            d_stack.Push(b - a);
                            break;
                        case ("*"):
                            d_stack.Push(b * a);
                            break;
                        case ("/"):
                            if (a == 0) // division by 0
                            {
                                throw new DivideByZeroException();
                            } // end if
                            else
                            {
                                d_stack.Push(b / a);
                            } // end else
                            break;
                        case ("^"):
                            if (a == 0 && b == 0) // 0^0 is indeterminate
                            {
                                throw new ArithmeticException("0^0");
                            } // end if
                            d_stack.Push(Math.Pow(b,a));
                            break;
                        default: // invalid operation
                            throw new ArgumentException("-1");
                    } // end switch
                } // end if
            } // end for

            d_result = d_stack.Pop(); // result will be last item on the stack

            // stack should now be empty, otherwise the expression was invalid
            if (d_stack.Count > 0)
            {
                throw new ArgumentException("-1");
            } // end if      

            // output result to user
            resultLabel.Text = "= ";
            resultLabel.Text += d_result.ToString();

            // If the result string is very long, adjust font size
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


        // Exception Handlers: All handlers display a message box to the user with a reason why the problem occurred.
        /// <summary>
        /// Handles Argument Exceptions thrown during program execution.
        /// </summary>
        /// <param name="exception">An argument exception that either contains no message, 
        /// or an index and a reason, separated by a single space.</param>
        private void handleArgException(ArgumentException exception)
        {
            // Variables
            int      i_index        = 0                                                    ;

            string   s_errorMessage = "There was an issue with the expression you entered.";

            string[] sa_message     = exception.Message.Split()                            ;


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


        /// <summary>
        /// Handles Division by 0 exceptions.
        /// </summary>
        private void handleDivByZeroException()
        {
            MessageBox.Show("The expression you entered contains a division by 0. This is not a legal operation. Please try again.",
                            "Divison by 0 in Expression!");
            resultLabel.Text = display.Text + "= NaN";
            resultLabel.Visible = true;
        } // end method handleDivByZeroException


        /// <summary>
        /// Handles Arithmetic Exceptions s.a. indeterminate forms. 
        /// </summary>
        /// <param name="e">The exception with an error message. Used iff message is 0^0</param>
        private void handleArithmeticException(ArithmeticException e)
        {
            if (e.Message == "0^0")
            {
                resultLabel.Text = display.Text + " is an indeterminate form";
            } // end if

            MessageBox.Show("The expression you entered contains an invalid operation, and cannot be evaluated. Please try again.",
                            "Invalid Operation in Expression!");
        } // end method handleArithmeticException


        /// <summary>
        /// Catch all exception handler for any other issues that may arise.
        /// </summary>
        /// <param name="e">Exception with message of what happened. Message is displayed to user
        /// iff it is not a stack empty exception.</param>
        private void handleUnknownException(Exception e)
        {
            string message = "The expression you entered could not be evaluated.\n\nReason:\n";

            if (e.Message != "Stack empty.") // if the stack wasn't empty, an unknown issue occurred
            {
                message += e.Message;
            }
            else // if the stack is empty, too many operands were supplied
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


        /// <summary>
        /// Handles keyboard presses. Only handles legal characters, all others are ignored.
        /// </summary>
        /// <param name="sender">Unused parameter.</param>
        /// <param name="e">Key press event which must contain key that was pressed.</param>
        private void keyboardPress  (object sender, KeyPressEventArgs e)
        {
            char c_temp = e.KeyChar; // extract key from event

            if(sender.Equals(display)) // display handles its own events
            {                          // this function only handles esc button
                if(c_temp == '\u001b')
                {
                    display.Text = ""; // esc clears the input field
                } // end if
                return;
            } // end if
            
            // all non-display events are handled here
            switch(c_temp)
            {
                // for digits and operators, the appropriate symbol is inserted into input
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
                case ('\u001b'): // escape button
                    display.Text = "";
                    break;
                case ('\b'):     // backspace button
                    if (display.Text.Length > 0) // if there is input, delete the right most character in input
                    {
                        display.Text = display.Text.Remove(display.Text.Length - 1);
                    }
                    break;
                default:   // all other keys are illegal in expressions
                    break; // pushing them will be ignored
            } // end switch
        } // end method keyboardPress 


        /// <summary>
        /// Returns focus to the enter key after another button has been pushed.
        /// </summary>
        /// <param name="sender">Unused parameter.</param>
        /// <param name="e">Unused parameter.</param>
        private void focusEnterKey  (object sender, EventArgs e)
        {
            enter.Focus();
        } // end method focusEnterKey
    } // end class Calculator
} // end namespace Calculator