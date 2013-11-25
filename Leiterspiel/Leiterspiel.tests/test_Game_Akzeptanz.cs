using System.IO;
using Leiterspiel.domain;
using NUnit.Framework;

namespace Leiterspiel.tests
{
    [TestFixture]
    public class test_Game_Akzeptanz
    {
        /*
            Spielbrett mit 8 Zeilen und 8 Spalten. Sieger ist, wer zuerst Feld 64 erreicht hat
            Neues Leiterspiel. Geben Sie zuerst die Anzahl an Spielern ein. [2 .. 4]: 2
            Spieler 0: Position 0. Gewürfelte Augenzahl: 3
            Spieler 1: Position 0. Gewürfelte Augenzahl: 4
            Spieler 0: Position 11. Gewürfelte Augenzahl: 5
            Spieler 1: Position 4. Gewürfelte Augenzahl: 2
            Spieler 0: Position 16. Gewürfelte Augenzahl: 3
            Spieler 1: Position 6. Gewürfelte Augenzahl: 4
            Spieler 0: Position 19. Gewürfelte Augenzahl: 3
            Spieler 1: Position 10. Gewürfelte Augenzahl: 4
            Spieler 0: Position 52. Gewürfelte Augenzahl: 5
            Spieler 1: Position 6. Gewürfelte Augenzahl: 2
            Spieler 0: Position 57. Gewürfelte Augenzahl: 3
            Spieler 1: Position 8. Gewürfelte Augenzahl: 4
            Spieler 0: Position 60. Gewürfelte Augenzahl: 3
            Spieler 1: Position 12. Gewürfelte Augenzahl: 4
            Spieler 0: Position 63. Gewürfelte Augenzahl: 5
            Spieler 0 hat gewonnen!!!! Gratulation.
        */
        [Test]
        public void Game1()
        {
            var board = Board.Parse(File.ReadAllText("leiterspielbrett2.txt"));
            var sut = new Game();

            int zeilen=0, spalten=0, ziel=0;
            int spieler=-1, position=-1;
            int gewinner=-1;

            sut.Initialized += (z, s, g) => { zeilen = z; spalten = s; ziel = g; };
            sut.Player_moved += (s, p) => { spieler = s; position = p; };
            sut.Game_over += g => { gewinner = g; };

            sut.Initialize(board);
            Assert.AreEqual(8, zeilen);
            Assert.AreEqual(8, spalten);
            Assert.AreEqual(64, ziel);

            sut.Set_players(2);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(0, position);

            sut.Move_player(3);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(0, position);

            sut.Move_player(4);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(11, position);
        
            sut.Move_player(5);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(4, position);

            sut.Move_player(2);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(16, position);

            sut.Move_player(3);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(6, position);

            sut.Move_player(4);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(19, position);

            sut.Move_player(3);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(10, position);

            sut.Move_player(4);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(52, position);

            sut.Move_player(5);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(6, position);

            sut.Move_player(2);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(57, position);

            sut.Move_player(3);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(8, position);

            sut.Move_player(4);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(60, position);

            sut.Move_player(3);
            Assert.AreEqual(1, spieler);
            Assert.AreEqual(12, position);

            sut.Move_player(4);
            Assert.AreEqual(0, spieler);
            Assert.AreEqual(63, position);

            Assert.AreEqual(-1, gewinner);
            sut.Move_player(5);
            Assert.AreEqual(0, gewinner);
        }
    }
}