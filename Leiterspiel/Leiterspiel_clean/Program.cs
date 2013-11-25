using System.IO;
using System.Linq;
using System.Text;

namespace Leiterspiel
{
  class Program
  {
    static void Main(string[] args)
    {

        var game = new Game();
        var ui = new UI();

        ui.Started += () => {
            var boardDefinition = File.ReadAllText(args[0]);
            var board = Board.Parse(boardDefinition);
            game.Initialize(board);
        };
        game.Initialized += ui.Board_prepared;

        ui.Number_of_players_entered += game.Set_players;
        ui.Rolled_the_dice += game.Move_player;
        game.Player_moved += ui.Update_player_position;
        game.Game_over += ui.Game_over;

        ui.Show();
    }
  }
}
