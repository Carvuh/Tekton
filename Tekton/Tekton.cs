using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton {
    class Tekton {

        // GLOBALS
        //

        static string currentProjectName;
        static string hierarchyIndicator = "~";
        static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        static bool isProjectMode = false;
        static bool projectRoot = true;

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
                    // New Project:
                    // [args]
                    // e.g. "-np ProjectName"
                    // -----
                    // We're going to initilize the project creation. This will create the .world file for the project.
                    string projectName = args[1].ToString();
                    isProjectMode = !isProjectMode;
                    currentProjectName = projectName;
                    Directory.CreateDirectory(projectName);
                    Directory.SetCurrentDirectory(projectName);
                    File.WriteAllText(projectName + ".world", "");
                    Console.WriteLine("Creating folder " + projectName + " at directory: " + Directory.GetCurrentDirectory());
                    // Afterwards, we are going to init the project's first envrionment.
                    // Making sure the user is in their correct folder, editing the correct things.
                    TektonProjectCLI(projectName, userName);
                }
            }

            // If the user is inside their project working on it.
            // Keep restarting the parser on enter;
            if (isProjectMode){
                TektonProjectCLI(currentProjectName, userName);
            }

        }

        public static void ImplementCustomHeader(string projectName, string userName) {
            // Create a custom header for the world to see things neatly...
            // Hoping this works. Damn Windows CMD... Always screwing colors up...
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(userName);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(":{0} ", projectName);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(hierarchyIndicator);
            Console.ResetColor();
            Console.Write(" > ");
        }

        public static void TektonProjectCLI(string projectName, string userName) {
            ImplementCustomHeader(projectName, userName);
            string userInput = Console.ReadLine();
            InputParser.InputManager(userInput);
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

        public static string GetCurrentProject() {
            return currentProjectName;
        }

        public static string GetUserName() {
            return userName;
        }

        public static void SetProjectPath(string path) {
            hierarchyIndicator = path;
        }

        public static void StartTekton() {
            Console.Clear();
            GetConsoleInput();  
        }

        // When the process closes.
        static void CurrentDomain_ProcessExit(object sender, EventArgs e) {
            Console.ResetColor();
        }

    }
}
