using System;
using Leiterspiel.contracts;

namespace Leiterspiel.portals
{
    class UI : IUI
    {
        private int _number_of_rows, _number_of_cols, _goalIndex;
        private int _player;
        private int _position;
        private bool _game_over = false;


        public event Action Started;
        public event Action<int> Number_of_players_entered;
        public event Action<int> Rolled_the_dice;


        public void Show()
        {
            Started();
            Message_loop();
        }

        private void Message_loop()
        {
            Ask_for_number_of_players();
            while (!_game_over)
                Roll_dice();
        }

        private void Ask_for_number_of_players()
        {
            Console.WriteLine(string.Format("Spielbrett mit {0} Zeilen und {1} Spalten. Sieger ist, wer zuerst Feld {2} erreicht hat",
                                            _number_of_rows, _number_of_cols, _goalIndex));

            Console.Write("Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]: ");
            var number_of_players = Console.ReadLine();

            Number_of_players_entered(int.Parse(number_of_players));
        }

        private void Roll_dice()
        {
            var draw = 0;
            var drawstring = "";
            do
            {
                Console.Write(string.Format("Spieler {0}: Position {1}. Gewürfelte Augenzahl: ", _player, _position));
                drawstring = Console.ReadKey().KeyChar.ToString();
            } while (!int.TryParse(drawstring, out draw) || (draw < 1 || draw > 6));
            Console.WriteLine();
            this.Rolled_the_dice(draw);
        }



        public void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex)
        {
            _number_of_rows = number_of_rows;
            _number_of_cols = number_of_cols;
            _goalIndex = goalIndex;
        }

        public void Update_player_position(int player, int position)
        {
            _player = player;
            _position = position;
        }

        public void Game_over(int winning_player)
        {
            _game_over = true;
            Console.WriteLine(string.Format("Spieler {0} hat gewonnen!!!! Gratulation. ", _player));
            Console.ReadLine();
        }
    }
}