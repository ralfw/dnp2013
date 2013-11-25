using System;
using Leiterspiel.domain;

namespace Leiterspiel.contracts
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
}