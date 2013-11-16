using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Leiterspiel
{
  class Program
  {
    static void Main(string[] args)
    {
      Board board = new Board(args[0]);
      Game game = new Game(board);
    }
  }

  class Game
  {
    int CurrentPlayerNumber = -1;
    Player CurrentPlayer;
    List<Player> Players = new List<Player>();
    Board board;

    public Game(Board board)
    {
      this.board = board;
      Initialize();
      NextPlayer();
      while (!PlayStep())
      {
        NextPlayer();
      }
      Finalize();
    }

    private void Initialize()
    {
      Console.WriteLine(string.Format("Spielbrett mit {0} Zeilen und {1} Spalten. Sieger ist, wer zuerst Feld {2} erreicht hat",
        board.Zeilen, board.Spalten, board.Zeilen * board.Spalten));

      Console.WriteLine("Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]");

      int NumberOfPlayers = int.Parse(Console.ReadLine());
      for (int i = 0; i < NumberOfPlayers; i++) Players.Add(new Player());
    }

    private void Finalize()
    {
      Console.WriteLine(string.Format("Spieler {0} hat gewonnen!!!! Gratulation. ", CurrentPlayerNumber));
      Console.ReadLine();
    }

    public void NextPlayer()
    {
      CurrentPlayerNumber = (CurrentPlayerNumber + 1) % Players.Count;
      CurrentPlayer = Players[CurrentPlayerNumber];
    }

    private bool PlayStep()
    {
      int draw = 0;
      string drawstring = "";
      do
      {
        Console.WriteLine(string.Format("Spieler {0}: Position {1}. Gewürfelte Augenzahl: ", CurrentPlayerNumber, CurrentPlayer.Position));
        drawstring = Console.ReadKey().KeyChar.ToString();
        
      } while (!int.TryParse(drawstring, out draw) || (draw < 1 || draw > 6));
     
      CalculateStep(draw);
      Console.WriteLine();
      return HasWon();
    }

    private void CalculateStep(int draw)
    {
      CurrentPlayer.Position = board.CalculateNewPosition(CurrentPlayer.Position + draw);
    }

    private bool HasWon()
    {
      return CurrentPlayer.Position >= board.Zeilen * board.Spalten;
    }
  }

  class Board
  {
    public int Zeilen { get; set; }
    public int Spalten { get; set; }
    Dictionary<int, int> Moves = new Dictionary<int, int>();

    public Board(string filename)
    {
      Load(filename);
    }

    public int CalculateNewPosition(int oldposition)
    {
      int j = 0;
      if (Moves.TryGetValue(oldposition, out j))
      {
        return j;
      }
      else return oldposition;
    }

    public void Load(string Filename)
    {
      using (TextReader f = File.OpenText(Filename))
      {
        string line;
        while ((line = f.ReadLine()) != null)
        {
          if (line.IndexOf("=") >= 0)
          {
            string[] parts = line.Split('=');
            if (parts[0].Trim() == "Spalten") Spalten = int.Parse(parts[1].Trim());
            if (parts[0].Trim() == "Zeilen") Zeilen = int.Parse(parts[1].Trim());
            if (parts[0].Trim() == "Leiter" || parts[0].Trim() == "Schlange") 
            {
                string[] fields = parts[1].Split(',');
                Moves.Add(int.Parse(fields[0].Trim()), int.Parse(fields[1].Trim()));
            }
          }
        }
      }
    }
  }

  class Player
  {
    public int Position { get; set; }
  }
}
