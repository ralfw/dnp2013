﻿using System.IO;
using System.Linq;
using System.Text;

namespace Leiterspiel
{
  class Program
  {
    static void Main(string[] args)
    {

        var game = new Game();
        var ui = game;

        ui.Started += () =>
            {
                var boardDefinition = File.ReadAllText(args[0]);
                var board = Board.Parse(boardDefinition);
                game.Initialize(board);
            };
        game.Initialized += ui.Board_prepared;

        ui.Show();
        ui.Play();
    }
  }
}
