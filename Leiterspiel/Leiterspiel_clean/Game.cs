using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leiterspiel
{

    interface IGame
    {
        event Action<int, int, int> Initialized; //
        event Action<int, int> Player_moved; //
        event Action<int> Game_over_event; //

        void Initialize(Board board); //
        void Set_players(int number_of_players); //
        void Move_player(int number); //

    }

    partial class Game : IGame
    {
        Board _board;

        int _current_player_index = -1;
        readonly List<Player> _players = new List<Player>();


        #region IGame
        public event Action<int, int, int> Initialized;
        public event Action<int, int> Player_moved;
        public event Action<int> Game_over_event;

        public void Initialize(Board board)
        {
            _board = board;
            Initialized(_board.Zeilen, _board.Spalten, _board.Zeilen*_board.Spalten);
        }

        public void Set_players(int number_of_players)
        {
            Create_players(number_of_players);
            NextPlayer();
            Player_moved(_current_player_index, _players[_current_player_index].Position);
        }


        public void Move_player(int number)
        {
            CalculateStep(number);
            if (HasWon()) 
                Game_over_event(_current_player_index);
            else
            {
                NextPlayer();
                Player_moved(_current_player_index, _players[_current_player_index].Position);
            }
        }


        private void Create_players(int number_of_players)
        {
            for (int i = 0; i < number_of_players; i++) _players.Add(new Player());
        }
        
        private void NextPlayer()
        {
            _current_player_index = (_current_player_index + 1) % _players.Count;
        }
        #endregion



        private void CalculateStep(int draw)
        {
            _players[_current_player_index].Position = _board.CalculateNewPosition(_players[_current_player_index].Position + draw);
        }

        private bool HasWon()
        {
            return _players[_current_player_index].Position >= _board.Zeilen * _board.Spalten;
        }
    }
}
