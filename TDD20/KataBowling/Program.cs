using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KataBowling.operations;

namespace KataBowling
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var ui = new UI();
            var frames = new Frames();
            var map = new Mappings();
            var integration = new Integration(frames);

            integration.Start(game => {
                var vm = map.Map(game);
                ui.Display(vm);
            });

            Application.Run(ui);
        }
    }
}
