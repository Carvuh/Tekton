using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton
{
    class InputParser
    {

        public enum parserErrors {ARG_ERROR, TYPE_ERROR, DIR_ERROR};

        static char[] delimiterChars = {' ', ',', '.', ':', ';', '?', '!', '\t'};
        static string[] messageArray;
        static string rootFolder;
        static bool enableDebug = false;

        public static void ParseInput(string message) {
            // Doing some init stuff.
            message = message.ToLower();
            messageArray = message.Split(delimiterChars);
            rootFolder = Directory.GetCurrentDirectory();

            if (enableDebug) {
                Console.WriteLine("Message recieved was!: {0}", messageArray[0]);
            }
            // Do whatever we need to do to parse something.
            if (messageArray[0] == "create") {
                if (messageArray[1] != "") {
                    Console.WriteLine("Creating new entry: {0}!", messageArray[1]);
                }
                // If the second argument even remotely contains a path, we are going to treat it like one.
                if (messageArray[1].Contains("/")) {
                    Console.WriteLine("Appending entry to {0}, under project {1}", messageArray[1], Tekton.GetCurrentProject());
                    Tekton.SetProjectPath(messageArray[1]);
                    Directory.CreateDirectory(Directory.GetCurrentDirectory().ToString() + messageArray[1]);
                    // Debating if letting the user decide whether or not he/she wants to, by default, change directories into what they just made.
                }
                else if (messageArray.Length <= 1) {
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
            else if (messageArray[0] == "help") {
                string helpText = File.ReadAllText(rootFolder + "/../../data/help.txt", Encoding.UTF8);
                Console.WriteLine(helpText);
            }
            else if (messageArray[0] == "ls") {
                string dir = Directory.GetCurrentDirectory();
                try {
                    foreach (string f in Directory.GetFiles(dir))
                        Console.WriteLine(f);
                    foreach (string d in Directory.GetDirectories(dir)) {
                        Console.WriteLine(d);
                        // DirSearch(d);
                    }
                }
                catch (System.Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (messageArray[0] == "debug") {
                Console.WriteLine("Debug Mode: {0}", !enableDebug);
                enableDebug = !enableDebug;
            }
            else if (messageArray[0] == "cd"){
                if (messageArray[1] != "" || messageArray[1] != null) {
                    try {
                        Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + messageArray[1]);
                        Tekton.SetProjectPath(messageArray[1]);
                    }
                    catch (DirectoryNotFoundException e) {
                        // Why the hell doesn't this directory exist I'm trying to cd into it? :thinking:
                        Console.WriteLine(e.Message);
                        ErrorException(parserErrors.DIR_ERROR);
                    }
                }
                if (messageArray[1] == ".." || messageArray[1].Contains(".")) {
                    return;
                }
            }
            else {
                // If the input matches NONE of these, then it's considered an argument error.
                ErrorException(parserErrors.ARG_ERROR);
            }
        }

        public static void ErrorException(parserErrors error) {
            if (error == parserErrors.ARG_ERROR) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid set of arguments.");
                Console.ResetColor();
                return;
            }
            if (error == parserErrors.DIR_ERROR) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("That directory does not exist.");
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
