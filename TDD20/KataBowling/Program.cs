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
            var scorer = new Scorer();
            var map = new Mappings();
            var integration = new Integration(frames, scorer);

            ui.On_Clear += integration.New_game;
            ui.On_Pins += integration.Register_roll;
            integration.Result += map.Map;
            map.Result += ui.Display;

            integration.Start();

            Application.Run(ui);
        }
    }
}
