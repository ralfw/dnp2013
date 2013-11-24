using System.IO;
using System.Linq;
using System.Text;

namespace Leiterspiel
{
  class Program
  {
    static void Main(string[] args)
    {
        var boardDefinition = File.ReadAllText(args[0]);
        var board = Board.Parse(boardDefinition);
        var game = new Game(board);
        game.Play();
    }
  }
}
