using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton {
    class Tekton {

        static void Main(string[] args) {
            if (args.Length <= 0) {
                StartTekton();
            }
            else {
                if (args[0] == "-ne") {
                    // -ne = -NewEntry
                    // We're going to append a new entry to the specified wiki page.
                    Console.WriteLine(args[1].ToString());
                }
                if (args[0] == "-np") {
                    // -np = -NewProject
                    // We're going to initilize the project creation.
                    string projectName = args[1].ToString();

                }
            }
        }

        public static void GetConsoleInput() {
            Console.Write("> ");
            string parsedInformation = Console.ReadLine();
        }

        public static void GetConsoleInput(string premessage) {
            string prompt = "> ";
            Console.WriteLine(premessage +  " " + prompt);
            Console.Read();
        }

        public static void StartTekton() {
            Console.Clear();
            GetConsoleInput();   
        }

    }
}
