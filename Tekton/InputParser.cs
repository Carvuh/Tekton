using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton
{
    class InputParser
    {
        public static void ParseInput(string message) {
            Console.WriteLine("Message recieved was!: " + message);
            // Do whatever we need to do to parse something.
            if (message == "q" || message == "quit" || message == "exit") {
                Environment.Exit(0);
            }
        }

        public static void InputManager(string userInput) {
            ParseInput(userInput);
            Tekton.TektonProjectCLI(Tekton.GetCurrentProject(), Tekton.GetUserName());
        }
    }
}
