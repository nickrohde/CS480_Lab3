/* * * * * * * * * * * * * * * * * *
 * Programmer: Nick Rohde          *
 * Project   : Lab 3 - Calculator  *
 * Class     : CS 480_003          *
 * Instructor: Szilard Vajda       *
 * Date      : 2nd November 2017   *
 * * * * * * * * * * * * * * * * * */


// Includes:
using System;
using System.Windows.Forms;

namespace Calculator_Rohde_Nick
{
    static class Client
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Calculator());
        }

    }
}
