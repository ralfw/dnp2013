using System;
using Leiterspiel.contracts;
using Leiterspiel.domain;
using NUnit.Framework;

namespace Leiterspiel.tests
{
    [TestFixture]
    public class test_Integration
    {
        [Test]
        public void UI_with_domain()
        {
            var ui = new MockUI();
            var game = new Game();
            
            Program.Integrate(new[]{"leiterspielbrett1.txt"}, ui, game);

            ui.Fire_Started();
            Assert.AreEqual("5;6;30", ui.Received);

            ui.Fire_Number_of_players_entered();
            Assert.AreEqual("0;0", ui.Received);

            ui.Fire_Rolled_the_dice();
            Assert.AreEqual("1;0", ui.Received);
        }


        [Test]
        public void Domain_with_UI()
        {
            var ui = new MockUI();
            var game = new MockGame();

            Program.Integrate(new[] { "leiterspielbrett1.txt" }, ui, game);

            game.Fire_Game_over();
            Assert.AreEqual("1", ui.Received);
        }



        class MockUI : IUI
        {
            public event Action Started;
            public event Action<int> Number_of_players_entered;
            public event Action<int> Rolled_the_dice;


            public void Fire_Started()
            {
                Started();
            }

            public void Fire_Number_of_players_entered()
            {
                Number_of_players_entered(2);
            }

            public void Fire_Rolled_the_dice()
            {
                Rolled_the_dice(3);
            }


            public string Received;


            public void Show()
            {
                throw new NotImplementedException();
            }

            public void Board_prepared(int number_of_rows, int number_of_cols, int goalIndex)
            {
                Received = string.Format("{0};{1};{2}", number_of_rows, number_of_cols, goalIndex);
            }

            public void Update_player_position(int player, int position)
            {
                Received = string.Format("{0};{1}", player, position);
            }

            public void Game_over(int winning_player)
            {
                Received = winning_player.ToString();
            }
        }


        class MockGame : IGame
        {
            public event Action<int, int, int> Initialized;
            public event Action<int, int> Player_moved;
            public event Action<int> Game_over;


            public void Fire_Game_over()
            {
                Game_over(1);
            }


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
        }
    }
}