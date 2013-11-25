using System;
using System.Collections.Generic;

namespace Leiterspiel
{
    /*
     * Interaktionen
     *      - Start. in: dateiname, out: zeilenzahl, spaltenzahl, zielfeldindex
     *      - Anzahl spieler setzen. in: n, out: -
     *      - Zug durchführen. in: augenzahl, out: spieler, position, spielstand
     */

    interface IUI
    {
        event Action Started; //
        event Action<int> Number_of_players_entered; //
        event Action<int> Rolled_the_dice; //

        void Show(); //
        void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex); //
        void Update_player_position(int player, int position); //
        void Game_over(int winning_player); //
    }
}