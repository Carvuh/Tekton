using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton
{
    class InputParser
    {

        public enum parserErrors {ARG_ERROR, TYPE_ERROR};

        static char[] delimiterChars = {' ', ',', '.', ':', ';', '?', '!', '\t'};
        static string[] messageArray;

        public static void ParseInput(string message) {
            message = message.ToLower();
            messageArray = message.Split(delimiterChars);
            Console.WriteLine("Message recieved was!: " + messageArray[0]);
            // Do whatever we need to do to parse something.
            if (messageArray[0] == "addentry") {
                if (messageArray[1] != null)
                {
                    Console.WriteLine("Adding a new Entry to Page of: " + messageArray[1] + "!");
                }
                else {
                    ErrorException(parserErrors.ARG_ERROR);
                }
            }
            // Application Specific Arguemnts ---------------------------
            else if (messageArray[0] == "q" || messageArray[0] == "quit" || messageArray[0] == "exit") {
                Environment.Exit(0);
            }
            else if (messageArray[0] == "clear" || messageArray[0] == "cls") {
                Console.Clear();
            }
            else {
                ErrorException(parserErrors.ARG_ERROR);
            }
        }

        public static void ErrorException(parserErrors error) {
            if (error == parserErrors.ARG_ERROR) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid set of arguments!");
                Console.ResetColor();
                return;
            }
        }

        public static void InputManager(string userInput) {
            ParseInput(userInput);
            Tekton.TektonProjectCLI(Tekton.GetCurrentProject(), Tekton.GetUserName());
        }
    }
}
