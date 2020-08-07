using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BreakoutSharp.Engine.Directing;

namespace BreakoutSharp {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            using (var game = new BreakoutGame()) {
                SceneManager.SetInitScene(new StageScene());

                game.Run();
            }
        }
    }
}
