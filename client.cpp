#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <algorithm>
#include <stdio.h>
#include <unistd.h>
#include <sys/wait.h>
#include <fstream>


using namespace std;

// Prototypes:
string waitForCommand();

vector<string> vectorizeCommand(string);

bool isCommandValid(vector<string>&);

void printHello();
void printPrompt();
void printHelp();
void printSpecificHelp(string);

void printError(string, int, int);
void printArgError(string s_cmdName, string s_expected, string s_received);

int executeCommand(vector<string>&); // helper method
int executeCommand(string);
int executeCommand(string, string);
int executeCommand(string, string, string);

// Globals:
int returnValue = 0;

int main(int argc, char* argv[])
{
	// Variables:
	int			   i_exitCode = 0;

	bool		   b_validCmd = false;

	string         s_cmd      = "";

	vector<string> commandVector;


	printHello();
	
	

	do
	{
		printPrompt();

		s_cmd = waitForCommand();
		commandVector = vectorizeCommand(s_cmd);

		b_validCmd = isCommandValid(commandVector);

		if (b_validCmd)
		{
			i_exitCode = executeCommand(commandVector);
		} // end if

		commandVector.clear();
	} while (i_exitCode == 0);

	return ::returnValue;
} // end method main

// Prints an initially introduction to the program
void printHello()
{
	cout << "Welcome to CWU Shell." << endl;
	cout << "Enter the command --h, or --help for information about the commands." << endl;
	cout << "To quit CWU Shell, enter the command exit or press CTRL+C." << endl;
} // end method printHello

// Prints the prompt for the user to enter a command
void printPrompt()
{
	cout << "cwushell> ";
} // end method printPrompt

// waits for the user to enter the next command
string waitForCommand()
{
	// Variables:
	string s_cmd = "";


	getline(cin, s_cmd);

	return s_cmd;
} // end method waitForCommand

// Turns the command into a vector of strings
vector<string> vectorizeCommand(string s_cmd)
{
	// Variables: 
	int i_start           = 0,
	    i                 = 0;

	bool b_noSpaceAllowed = false;

	vector<string> args;
	

	for( ; s_cmd[i] != '\0'; i++)
	{
		if (s_cmd[i] == ' ' && !b_noSpaceAllowed)
		{
			b_noSpaceAllowed = true;
			string s_temp = s_cmd.substr(i_start, i - i_start);			

			args.push_back(s_temp);
			i_start = i + 1;
		} // end if
		else if (s_cmd[i] != ' ')
		{
			b_noSpaceAllowed = false;
		} // end elif
	} // end for

	if (!b_noSpaceAllowed)
	{
		string s_temp = s_cmd.substr(i_start, i - i_start);

		args.push_back(s_temp);
	} // end if	

	// convert command to lower case to prevent error in validate function
	std::transform(args.at(0).begin(), args.at(0).end(), args.at(0).begin(), ::tolower);

	return args;
} // end method vectorizeCommand

// Checks if the syntax of the command is valid
bool isCommandValid(vector<string>& commandArgs)
{
	// Variables: 
	string s_commandName     = commandArgs.at(0);

	bool   b_returnValue     = false;

	int    i_commandArgsSize = commandArgs.size();


	if (s_commandName == "exit")
	{
		if (i_commandArgsSize != 1 && i_commandArgsSize != 2)
		{
			printError(s_commandName, i_commandArgsSize - 1, 3);
		} // end if

		b_returnValue = true;
	} // end if
	else if (s_commandName == "mv")
	{
		if (i_commandArgsSize != 3)
		{
			printError("mv", i_commandArgsSize - 1, 2);
		} // end if
		else
		{ 
			b_returnValue = true;
		} // end else
	} // end elif
	else if (s_commandName == "ls")
	{
		if (i_commandArgsSize != 1)
		{
			printError("ls", i_commandArgsSize - 1, 0);
		} // end if
		else
		{
			b_returnValue = true;
		} // end else
	} // end elif
	else if (s_commandName == "cmp")
	{
		if (i_commandArgsSize != 3)
		{
			printError("cmp", i_commandArgsSize - 1, 2);
		} // end if
		else
		{
			b_returnValue = true;
		} // end else
	} // end elif
	else if (s_commandName == "tail")
	{
		if (i_commandArgsSize != 2 && i_commandArgsSize != 3)
		{
			printError("tail", i_commandArgsSize - 1, 4);
		} // end if
		else
		{
			b_returnValue = true;
		} // end else
	} // end elif
	else if (s_commandName == "cat")
	{
		if (i_commandArgsSize != 2)
		{
			printError("cat", i_commandArgsSize - 1, 1);
		} // end if
		else
		{
			b_returnValue = true;
		} // end else
	} // end elif
	else if (s_commandName == "--h" || s_commandName == "--help")
	{
		if(i_commandArgsSize == 1)
		{
			printHelp();
		} // end if
		else if(i_commandArgsSize == 2)
		{
			printSpecificHelp(commandArgs.at(1));
		} // end else
		else
		{
			printError(s_commandName, i_commandArgsSize - 1, 3);
		}
	} // end elif
	else if (s_commandName == "clear")
	{
		b_returnValue = true;
	} // end elif
	else
	{
		printError(s_commandName, -1, -1);
	} // end else

	return b_returnValue;
} // end method isCommandValid

// Alerts the user that the syntax of the last command was invalid
void printError(string s_cmdName, int i_argReceived, int i_argExpected)
{
	switch (i_argExpected)
	{
		case -1:
			cout << endl << "The command " << s_cmdName << " does not exist. Enter the command --h or --help for help." << endl;
			break;
		case 0:
			cout << endl << "The command " << s_cmdName << " takes 0 arguements, you entered " << i_argReceived << ". Enter the command --h or --help for help." << endl;
			break;

		case 1:
			cout << endl << "The command " << s_cmdName << " takes 1 arguement, you entered " << i_argReceived << ". Enter the command --h or --help for help." << endl;
			break;

		case 2:
			cout << endl << "The command " << s_cmdName << " takes 2 arguements, you entered " << i_argReceived << ". Enter the command --h or --help for help." << endl;
			break;

		case 3:
			cout << endl << "The command " << s_cmdName << " takes 0 or 1 arguements, you entered " << i_argReceived << ". Enter the command --h or --help for help." << endl;
			break;

		case 4:
			cout << endl << "The command " << s_cmdName << " takes 1 or 2 arguements, you entered " << i_argReceived << ". Enter the command --h or --help for help." << endl;
			break;
	} // end switch	
} // end method printError

// Alerts the user that an arguement error occurred
void printArgError(string s_cmdName, string s_expected, string s_received)
{
	cout << endl << "The command " << s_cmdName << " received an invalid arguement: \"" << s_received << "\" expected: " << s_expected << endl;
} // end method printArgError

// Prints the help menu to the console
void printHelp()
{
	cout << endl << "Help: " << endl;
	cout << "command name <arguments>    -- description of command" << endl << endl;
	cout << "exit <n>                    -- terminates the shell, optional parameter n will" << endl;
	cout << "                               be the exit code of the shell alternatively" << endl;
	cout << "                               press CTRL+C." << endl << endl;
	cout << "cat <filename>              -- will print the contents of the file to the" << endl;
	cout << "                               console." << endl << endl;
	cout << "mv <filename1> <filename2>  -- moves the contents of file with path filename1" << endl;
	cout << "                               into file with filename2 e.g.: mv a.txt b.txt" << endl << endl;
	cout << "ls                          -- lists the contents of the current directory" << endl;
	cout << "                               e.g.: ls " << endl << endl;
	cout << "cmp <filename1> <filename2> -- compares the contents of files with paths" << endl;
	cout << "                               filename1 and filename2 and returns the first" << endl;
	cout << "                               byte where there is a difference." << endl;
	cout << "                               e.g.: cmp a.txt b.txt" << endl << endl;
	cout << "tail <filename> <n>         -- prints the last n lines from the file with path" << endl;
	cout << "                               filename e.g.: tail a.txt 3 parameter <n> is" << endl;
	cout << "                               optional, ommittingit will default n to 5." << endl << endl;
	cout << "help <command>              -- displays the help, optional parameter <command>" << endl;
	cout << "                               name of command to display help for, omitting" << endl;
	cout << "                               this will show help for all commands" << endl << endl;
	cout << "clear                       -- clears the console window." << endl << endl;
} // end method printHelp

// Prints the help for a specific command to the console
void printSpecificHelp(string s_commandName)
{
	cout << endl << "Help for: " << s_commandName << endl;
	cout         << "command name <arguments>    -- description of command" << endl;
		
	if (s_commandName == "exit")
	{		
		cout << "exit <n>                    -- terminates the shell, optional parameter n will" << endl;
		cout << "                               be the exit code of the shell alternatively" << endl;
		cout << "                               press CTRL+C." << endl << endl;

	} // end if
	else if (s_commandName == "mv")
	{
		cout << "mv <filename1> <filename2>  -- moves the contents of file with path filename1" << endl;
		cout << "                               into file with filename2 e.g.: mv a.txt b.txt" << endl << endl;
	} // end elif
	else if (s_commandName == "ls")
	{
		cout << "ls                          -- lists the contents of the current directory" << endl;
		cout << "                               e.g.: ls " << endl << endl;
	} // end elif
	else if (s_commandName == "cmp")
	{
		cout << "cmp <filename1> <filename2> -- compares the contents of files with paths" << endl;
		cout << "                               filename1 and filename2 and returns the first" << endl;
		cout << "                               byte where there is a difference." << endl;
		cout << "                               e.g.: cmp a.txt b.txt" << endl << endl;
	} // end elif
	else if (s_commandName == "tail")
	{
		cout << "tail <filename> <n>         -- prints the last n lines from the file with path" << endl;
		cout << "                               filename e.g.: tail a.txt 3 parameter <n> is" << endl;
		cout << "                               optional, ommittingit will default n to 5." << endl << endl;
	} // end elif
	else if (s_commandName == "cat")
	{
		cout << "cat <filename>              -- will print the contents of the file to the" << endl;
		cout << "                               console." << endl << endl;
	} // end elif
	else if (s_commandName == "h" || s_commandName == "help" || s_commandName == "--h" || s_commandName == "--help")
	{
		cout << "help <command>              -- displays the help, optional parameter <command>" << endl;
		cout << "                               name of command to display help for, omitting" << endl;
		cout << "                               this will show help for all commands" << endl << endl;
	} // end elif
	else if (s_commandName == "clear")
	{
		cout << "clear                       -- clears the console window." << endl << endl;
	} // end elif
	else
	{
		cout << endl << s_commandName << " was not recognized by help." << endl << endl;
	} // end else

} // end method printSepcificHelp

// Helper method
int executeCommand(vector<string>& commandVector)
{
	// Variables: 
	int i_returnValue       = 0,
		i_commandVectorSize = commandVector.size();


	if (i_commandVectorSize == 1)
	{
		i_returnValue = executeCommand(commandVector.at(0));
	} // end if

	else if (i_commandVectorSize == 2)
	{
		i_returnValue = executeCommand(commandVector.at(0), commandVector.at(1));
	} // end elif

	else if (i_commandVectorSize == 3)
	{
		i_returnValue = executeCommand(commandVector.at(0), commandVector.at(1), commandVector.at(2));
	} // end elif

	return i_returnValue;
} // end method executeCommand(vector)

// Executes no-arg commands exit and ls
int executeCommand(string s_cmdName)
{
	int i_returnValue = 0;
	
	if (s_cmdName == "exit")
	{
		::returnValue = 0;
		i_returnValue = 1; // exit main loop
	} // end if

	else if (s_cmdName == "ls")
	{
		pid_t pid = fork();
		
		if(pid > 0)
		{
			wait(NULL);
		} // end if
		else if(pid == 0)
		{
			execl("/bin/ls","ls", (char *)0);
			exit(0);
		}// end elif
	} // end elif

	else if (s_cmdName == "clear")
	{
		cout << "\033c";
		printHello();
	} // end elif

	return i_returnValue;
} // end method executeCommand(string)

// Executes 1 arg commands exit, tail, and cat
int executeCommand(string s_cmdName, string s_arg1)
{
	// Variables: 
	int i_returnValue = 0;

	stringstream stream(s_arg1);


	if (s_cmdName == "exit")
	{		
		if(stream >> ::returnValue)
		{
			i_returnValue = 1;
		}
		else
		{
			printArgError("exit", "an integer (1, 2, 3, ...)." , s_arg1);
			::returnValue = 0;
			i_returnValue = 0;
		} // end if
	} // end if

	else if (s_cmdName == "tail")
	{
		pid_t pid = fork();
		
		if(pid > 0)
		{
			wait(NULL);
		} // end if
		else if(pid == 0)
		{
			cout << endl << execl("/usr/bin/tail","tail", s_arg1.c_str() , "5", (char *)0);
			cout << endl;
			exit(0);
		}// end elif

		// call tail with arg1 and temp
	} // end elif

	else if (s_cmdName == "cat")
	{
		pid_t pid = fork();
		
		if(pid > 0)
		{
			wait(NULL);
		} // end if
		else if(pid == 0)
		{
			execl("/bin/cat","cat", s_arg1.c_str(), (char *)0);
			exit(0);
		}// end elif
		
		// call cat with arg1
	} // end elif

	return i_returnValue;
} // end method executeCommand(string, string)

// Executes 2 arg commands mv, tail, and cmp
int executeCommand(string s_cmdName, string s_arg1, string s_arg2)
{
	// Variables: 
	int i_returnValue = 0;

	stringstream stream(s_arg2);


	if (s_cmdName == "mv")
	{
		fstream file;
		
		file.open(s_arg2.c_str(), ios::in);
		
		if(file.is_open())
		{
			string s_input = "";
			
			file.close();
			
			while(s_input != "y" && s_input != "n")
			{
				cout << endl << "The file " << s_arg2 << " already exists. Do you want to overwrite it? (y/n)";
				cin >> s_input;
				std::transform(s_input.begin(), s_input.end(), s_input.begin(), ::tolower);
			}
			if(s_input == "y")
			{	
				cout << "The file " << s_arg2 << " will be overwritten with the contents of " << s_arg1 << "." << endl;
				pid_t pid = fork();
		
				if(pid > 0)
				{
					wait(NULL);
				} // end if
				else if(pid == 0)
				{
					execl("/bin/mv","mv", s_arg1.c_str(), s_arg2.c_str(), NULL);
					exit(0);
				}
			}
			else
			{
				cout << "Move canceled. The file " << s_arg2 << " was not altered." << endl;
				cin.clear();
				cin.ignore(std::cin.rdbuf()->in_avail());
			}
		}
		else
		{
			pid_t pid = fork();
		
			if(pid > 0)
			{
				wait(NULL);
			} // end if
			else if(pid == 0)
			{
				execl("/bin/mv","mv", s_arg1.c_str(), s_arg2.c_str(), NULL);
				exit(0);
			} // end elif
		} // end else
	} // end if (s_cmdName == "mv")

	else if (s_cmdName == "tail")
	{
		// Variables:
		int temp = -1;


		if (stream >> temp && temp > 0)
		{
			pid_t pid = fork();
		
			if(pid > 0)
			{
				wait(NULL);
			} // end if
			else if(pid == 0)
			{
				execl("/bin/tail","tail", s_arg1.c_str(), s_arg2.c_str(), (char *)0);
				exit(0);
			}// end elif
		} // end if
		else
		{
			printArgError("tail", "a positive integer (1, 2, 3, ...).", s_arg2);
		} // end else
	} // end elif

	else if (s_cmdName == "cmp")
	{
		pid_t pid = fork();
		
		if(pid > 0)
		{
			wait(NULL);
		} // end if
		else if(pid == 0)
		{
			execl("/usr/bin/cmp","cmp", s_arg1.c_str(), s_arg2.c_str(), NULL);
			exit(0);
		}// end elif
		// call cmp with arg1 and arg2
	} // end elif

	return i_returnValue;
} // end method executeCommand(string, string, string)