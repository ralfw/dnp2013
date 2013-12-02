using System.IO;
using System.Linq;
using System.Text;
using Leiterspiel.contracts;
using Leiterspiel.domain;
using Leiterspiel.portals;

namespace Leiterspiel
{
  class Program
  {
      static void Main(string[] args)
      {
          var ui = new UI();
          var game = new Game();

          Integrate(args, ui, game);

          ui.Show();
      }


      internal static void Integrate(string[] args, IUI ui, IGame game)
      {
          ui.Started += () =>
              {
                  var boardDefinition = File.ReadAllText(args[0]);
                  var board = Board.Parse(boardDefinition);
                  game.Initialize(board);
              };
          game.Initialized += ui.Board_prepared;

          ui.Number_of_players_entered += game.Set_players;
          ui.Rolled_the_dice += game.Move_player;
          game.Player_moved += ui.Update_player_position;
          game.Game_over += ui.Game_over;
      }
  }
}
