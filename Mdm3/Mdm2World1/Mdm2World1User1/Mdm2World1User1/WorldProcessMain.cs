using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mdm {
    namespace World {

        static class WorldProcessMain {
            internal static WorldClass World;
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main() {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                World = new WorldClass();
                Application.Run(new Form1());
            }
        }
    }
}