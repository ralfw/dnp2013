using System;
using System.Collections.Generic;
using Leiterspiel.contracts;

namespace Leiterspiel.domain
{
    class Game : IGame
    {
        Board _board;

        int _current_player_index = -1;
        readonly List<int> _player_positions = new List<int>();


        public event Action<int, int, int> Initialized;
        public event Action<int, int> Player_moved;
        public event Action<int> Game_over;


        public void Initialize(Board board)
        {
            _board = board;
            Initialized(_board.Zeilen, _board.Spalten, _board.Zeilen*_board.Spalten);
        }

        public void Set_players(int number_of_players)
        {
            Create_players(number_of_players);
            NextPlayer();
            Player_moved(_current_player_index, _player_positions[_current_player_index]);
        }


        public void Move_player(int number)
        {
            CalculateStep(number);
            if (HasWon()) 
                Game_over(_current_player_index);
            else
            {
                NextPlayer();
                Player_moved(_current_player_index, _player_positions[_current_player_index]);
            }
        }


        private void Create_players(int number_of_players)
        {
            for (int i = 0; i < number_of_players; i++) _player_positions.Add(0);
        }
        
        private void NextPlayer()
        {
            _current_player_index = (_current_player_index + 1) % _player_positions.Count;
        }

        private void CalculateStep(int draw)
        {
            _player_positions[_current_player_index] =_board.CalculateNewPosition(_player_positions[_current_player_index] + draw);
        }

        private bool HasWon()
        {
            return _player_positions[_current_player_index] >= _board.Zeilen * _board.Spalten;
        }
    }
}
