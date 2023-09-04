using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
// add shell32.dll reference
// or COM Microsoft Shell Controls and Automation
using Shell32;
// At first, Project > Add Reference > COM > Windows Script Host Object Model.
using IWshRuntimeLibrary;


using Mdm.World;

namespace Mdm.Oss.Sys.Script
{

    using System;
    using System.Windows.Forms;
    using System.Reflection;
    using System.CodeDom.Compiler;

    public class PropManager
    {
        public PropManager()
        {
            Shell shell = new Shell();
        }

        private static Shell shell = new Shell();
        private static WshShell wshShell = new WshShell();
        public Shell32.Folder folder;
        public Shell32.FolderItem folderItem;
        public Shell32.ShellLinkObject link;
        IWshRuntimeLibrary.IWshShortcut shorcutA;

        public static IWshShortcut shorcut;
        public string shortcutFileName;
        public string shortcutFolderItemName;
        public string shortcutDescription;
        public string shortcutTargetPath;
        public FolderItem shortcutTargetFolder;
        public string shortcutHotKey;
        public string shortcutVWorkingDirectory;
        public string shortcutArgs;

        private string pathOnly;
        private string filenameOnly;

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue(object src, string propName, object srcValue)
        {
            src.GetType().GetProperty(propName).SetValue(src, srcValue, null);
        }

        public static void ExpressionParse(string exprValue)
        {

        }

        // At first, Project > Add Reference > COM > Windows Script Host Object Model.
        // using IWshRuntimeLibrary;

        public void CreateShortcutTest()
        {
            object shDesktop = (object)"Desktop";
            string shortcutAddress = @"C:\SrtVs501\Code\Input" + @"\Notepad.lnk";
            // string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Notepad.lnk";
            // IWshShortcut shortcut = (IWshShortcut)wshShell.CreateShortcut(shortcutAddress);
            //shortcut.Description = "New shortcut for a Notepad";
            //shortcut.Hotkey = "Ctrl+Shift+N";
            //shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\notepad.exe";
            ////shortcut.RelativePath;
            ////shortcut.FullName;
            //shortcut.Save();
        }

        public Shell32.ShellLinkObject ShortcutReadTargetLink(string shortcutFilename)
        {
            pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            filenameOnly = System.IO.Path.GetFileName(shortcutFilename);
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            if (folder == null)
            {
            }
            else
            {
                FolderItem folderItem = folder.ParseName(filenameOnly);

                if (folderItem != null)
                {


                    Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                    //shortcutFolderItemName = folderItem.Name;
                    //shortcutDescription = link.Description;
                    shortcutTargetPath = link.Path;
                    //shortcutVWorkingDirectory = link.WorkingDirectory;
                    shortcutTargetFolder = link.Target;
                    //shortcutArgs = link.Arguments;

                    //System.IO.File.Delete(shortcutFilename);

                    //CreateShortcut(shortcutTargetPath, shortcutFilename);

                    return link;
                }
            }
            return null;
        }

        public string GetShortcutTargetFile(string shortcutFilename)
        {
            pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            filenameOnly = System.IO.Path.GetFileName(shortcutFilename);

            folder = shell.NameSpace(pathOnly);
            folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                link = (Shell32.ShellLinkObject)folderItem.GetLink;
                // folderItem.
                return link.Path;
            }

            return string.Empty;
        }

        public void TestPath(string[] args)
        {
            const string path = @"C:\link to foobar.lnk";
            Console.WriteLine(GetShortcutTargetFile(path));
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="targetFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public static void CreateShortcut(string targetFile, string shortcutFile)
        {
            CreateShortcut(targetFile, shortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="targetPath">A file you want to make shortcut to</param>
        /// <param name="shortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="description">Shortcut description</param>
        /// <param name="arguments">Command line arguments</param>
        /// <param name="hotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="workingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string targetPath, string shortcutFile, string description,
           string arguments, string hotKey, string workingDirectory)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(targetPath))
                throw new ArgumentNullException("TargetPath");
            if (String.IsNullOrEmpty(shortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create shortcut object: IWshRuntimeLibrary.
            IWshShortcut shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFile);
            // IWshShortcut

            // Assign shortcut properties:
            shorcut.TargetPath = targetPath;
            shorcut.Description = description;
            if (!String.IsNullOrEmpty(arguments))
                shorcut.Arguments = arguments;
            if (!String.IsNullOrEmpty(hotKey))
                shorcut.Hotkey = hotKey;
            if (!String.IsNullOrEmpty(workingDirectory))
                shorcut.WorkingDirectory = workingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }

        /// <summary>
        /// Read Windows Shorcut
        /// </summary>
        public void ShortcutRead()
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(shortcutFileName))
                throw new ArgumentNullException("ShortcutFile");
            ShortcutRead(shortcutFileName);
        }

        /// <summary>
        /// Read Windows Shorcut
        /// </summary>
        /// <param name="shortcutFileName">Path and shorcut file name including file extension (.lnk)</param>
        public void ShortcutRead(string shortcutFileName)
        {
            
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(shortcutFileName))
                throw new ArgumentNullException("ShortcutFile");
            this.shortcutFileName = shortcutFileName;
            // Create shortcut object: IWshRuntimeLibrary.
            IWshShortcut shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFileName);
            // shortcutFolderItemName = folderItem.Name;
            shortcutDescription = shorcut.Description;
            // shortcutTargetPath = shorcut.Path;
            shortcutVWorkingDirectory = shorcut.WorkingDirectory;
            // shortcutTargetFolder = shorcut.Target;
            shortcutArgs = shorcut.Arguments;

            Shell32.ShellLinkObject link = ShortcutReadTargetLink(shortcutFileName);
        }

        /// <summary>
        /// Write Windows Shorcut
        /// </summary>
        public void ShortcutWrite()
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(shortcutFileName))
                throw new ArgumentNullException("ShortcutFile");
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(shortcutTargetPath))
                throw new ArgumentNullException("shortcutTargetPath");

            // Create shortcut object: IWshRuntimeLibrary.
            IWshShortcut shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFileName);
            // IWshShortcut

            // Assign shortcut properties:
            shorcut.TargetPath = shortcutTargetPath;
            if (!String.IsNullOrEmpty(shortcutArgs)) { 
                shorcut.Description = shortcutDescription;
            } else { shorcut.Description = ""; }
            if (!String.IsNullOrEmpty(shortcutArgs))
                shorcut.Arguments = shortcutArgs;
            if (!String.IsNullOrEmpty(shortcutHotKey))
                shorcut.Hotkey = shortcutHotKey;
            if (!String.IsNullOrEmpty(shortcutVWorkingDirectory))
                shorcut.WorkingDirectory = shortcutVWorkingDirectory;

            // Save the shortcut:
            shorcut.Save();

        }

        /// <summary>
        /// Write Windows Shorcut
        /// </summary>
        /// <param name="shortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public void ShortcutWrite(string shortcutFile)
        {
            // Check necessary parameters first:
            if (String.IsNullOrEmpty(shortcutFile))
                throw new ArgumentNullException("ShortcutFile");

            // Create shortcut object: IWshRuntimeLibrary.
            IWshShortcut shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFile);
        }

    }
    public class ScriptEngine
    {
        private static Int32 mScriptEngineCount;
        private static Int32 mScriptCount;
        private string[] mScriptCode;

        static ScriptEngine() {
            mScriptEngineCount = 0;
            mScriptCount = 0;
        }

        public ScriptEngine() {
            mScriptEngineCount += 1;
        }

        public ScriptEngine(string[] scriptCode) : this()
        {
            mScriptCode = scriptCode;

        }
        public void ScriptProcScript(string[] scriptCode, string scriptValue)
        {
            mScriptCode = scriptCode;
            ScriptProc(scriptValue);
        }
        public void ScriptProc(string scriptValue)
        {
            mScriptCount += 1;
            // Loop Find
            // Conditions
            // Loop Preprocess
            // Extract
            // Loop Process
            // Loop Output
            // Loop Postprocess
            //
            // Loop for lines
            // GetLine(1)
            // Parse Line
            // 
        }
        public static void ExpressionParse(string exprValue)
        {
        }
        public static void ExpressionEval(string exprValue)
        {
        }
        public static void ExpressionExec(string exprValue)
        {
        }

        public static object MethodExec(object src, string MethodName, string exprValue)
        {
            MethodInfo method = src.GetType().GetMethod(MethodName);
            object result = method.Invoke(src, new object[] { exprValue });
            return result;
        }
        //public static R ResponseHelper<T, R>(T request, string serviceAction)
        //{
        //    var service = new ContentServiceRef.CMSCoreContentServiceClient();

        //    var func = (Func<T, R>)Delegate.CreateDelegate(typeof(Func<T, R>),
        //                                                  service,
        //                                                  serviceAction);

        //    return func(request);
        //}
        public void InvokeMethod(string methodName, List<object> args)
        {
            GetType().GetMethod(methodName).Invoke(this, args.ToArray());
        }

        public void dummy()
        {
            // *****************************************
            string[] arr = { "jONH", "BOB", "BOB" };
            List<string> list = new List<string>(arr);
            // A.
            // New list here.
            List<string> l = new List<string>();
            l.Add("one");
            l.Add("two");
            l.Add("three");
            l.Add("four");
            l.Add("five");

            // *****************************************
            // B.
            string[] s = l.ToArray();

            string[] array = { "hi", "welcome", "to", "forget code" };
            List<string> list1 = array.ToList();
            foreach (string item in list1)
            {
                Console.WriteLine(item);
            }

            // *****************************************
            // The files used in this example are created in the topic
            // How to: Write to a Text File. You can change the path and
            // file name to substitute text files of your own.

            // Example #1
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");

            // Display the file contents to the console. Variable text is a string.
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
            // *****************************************

            // *****************************************

            // *****************************************
        }

        public class Parent
        {
            List<Child> children = new List<Child>();
            public Child CreateChild()
            {
                Child tmpChild = new Child(this);
                this.children.Add(tmpChild);
                return tmpChild;
            }
        }

        public class Child
        {
            Parent parent;
            public Child(Parent parent)
            {
                this.parent = parent;
            }
        }
    }


    namespace ScriptingInterface
    {
        public interface IScriptType1
        {
            string RunScript(int value);
        }
        class Class2
        {
        }
    }

    namespace ScriptingExample
    {
        class Class1
        {
        }
        static class Program
        {
            /// 
            /// The main entry point for the application.
            /// 
            [STAThread]
            static void Main()
            {

                // Lets compile some code (I'm lazy, so I'll just hardcode it all, i'm sure you can work out how to read from a file/text box instead
                Assembly compiledScript = CompileCode(
                    "namespace SimpleScripts" +
                    "{" +
                    "    public class MyScriptMul5 : ScriptingInterface.IScriptType1" +
                    "    {" +
                    "        public string RunScript(int value)" +
                    "        {" +
                    "            return this.ToString() + \" just ran! Result: \" + (value*5).ToString();" +
                    "        }" +
                    "    }" +
                    "    public class MyScriptNegate : ScriptingInterface.IScriptType1" +
                    "    {" +
                    "        public string RunScript(int value)" +
                    "        {" +
                    "            return this.ToString() + \" just ran! Result: \" + (-value).ToString();" +
                    "        }" +
                    "    }" +
                    "}");

                if (compiledScript != null)
                {
                    RunScript(compiledScript);
                }
            }

            static Assembly CompileCode(string code)
            {
                // Create a code provider
                // This class implements the 'CodeDomProvider' class as its base. All of the current .Net languages (at least Microsoft ones)
                // come with thier own implemtation, thus you can allow the user to use the language of thier choice (though i recommend that
                // you don't allow the use of c++, which is too volatile for scripting use - memory leaks anyone?)
                Microsoft.CSharp.CSharpCodeProvider csProvider = new Microsoft.CSharp.CSharpCodeProvider();

                // Setup our options
                CompilerParameters options = new CompilerParameters();
                options.GenerateExecutable = false; // we want a Dll (or "Class Library" as its called in .Net)
                options.GenerateInMemory = true; // Saves us from deleting the Dll when we are done with it, though you could set this to false and save start-up time by next time by not having to re-compile
                                                 // And set any others you want, there a quite a few, take some time to look through them all and decide which fit your application best!

                // Add any references you want the users to be able to access, be warned that giving them access to some classes can allow
                // harmful code to be written and executed. I recommend that you write your own Class library that is the only reference it allows
                // thus they can only do the things you want them to.
                // (though things like "System.Xml.dll" can be useful, just need to provide a way users can read a file to pass in to it)
                // Just to avoid bloatin this example to much, we will just add THIS program to its references, that way we don't need another
                // project to store the interfaces that both this class and the other uses. Just remember, this will expose ALL public classes to
                // the "script"
                options.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

                // Compile our code
                CompilerResults result;
                result = csProvider.CompileAssemblyFromSource(options, code);

                if (result.Errors.HasErrors)
                {
                    // TODO: report back to the user that the script has errored
                    return null;
                }

                if (result.Errors.HasWarnings)
                {
                    // TODO: tell the user about the warnings, might want to prompt them if they want to continue
                    // runnning the "script"
                }

                return result.CompiledAssembly;
            }

            static void RunScript(Assembly script)
            {
                // Now that we have a compiled script, lets run them
                foreach (Type type in script.GetExportedTypes())
                {
                    foreach (Type iface in type.GetInterfaces())
                    {
                        if (iface == typeof(ScriptingInterface.IScriptType1))
                        {
                            // yay, we found a script interface, lets create it and run it!

                            // Get the constructor for the current type
                            // you can also specify what creation parameter types you want to pass to it,
                            // so you could possibly pass in data it might need, or a class that it can use to query the host application
                            ConstructorInfo constructor = type.GetConstructor(System.Type.EmptyTypes);
                            if (constructor != null && constructor.IsPublic)
                            {
                                // lets be friendly and only do things legitimitely by only using valid constructors

                                // we specified that we wanted a constructor that doesn't take parameters, so don't pass parameters
                                ScriptingInterface.IScriptType1 scriptObject = constructor.Invoke(null) as ScriptingInterface.IScriptType1;
                                if (scriptObject != null)
                                {
                                    //Lets run our script and display its results
                                    MessageBox.Show(scriptObject.RunScript(50));
                                }
                                else
                                {
                                    // hmmm, for some reason it didn't create the object
                                    // this shouldn't happen, as we have been doing checks all along, but we should
                                    // inform the user something bad has happened, and possibly request them to send
                                    // you the script so you can debug this problem
                                }
                            }
                            else
                            {
                                // and even more friendly and explain that there was no valid constructor
                                // found and thats why this script object wasn't run
                            }
                        }
                    }
                }
            }
        }
    }
}