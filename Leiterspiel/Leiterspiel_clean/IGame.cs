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
        event Action<int> Game_over; //

        void Initialize(Board board); //
        void Set_players(int number_of_players); //
        void Move_player(int number); //

    }

    partial class Game : IGame
    {
        #region IGame
        public event Action<int, int, int> Initialized;
        public event Action<int, int> Player_moved;
        public event Action<int> Game_over;
        public void Initialize(Board board)
        {
            throw new NotImplementedException();
        }

        public void Set_players(int number_of_players)
        {
            throw new NotImplementedException();
        }

        public void Move_player(int number)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
