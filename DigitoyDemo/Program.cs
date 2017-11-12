using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitoyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMechanics game = new GameMechanics();
            Random rn = new Random();
            List<int>[] players = { new List<int>(), new List<int>(), new List<int>(), new List<int>()};
            players[rn.Next(0, 3)].Add(game.drawCard(1)[0]);
            //deal extra card to a "random" player

            foreach (var player in players)
            {
                player.AddRange(game.drawCard(14));
            }//initialise game with dealing cards to players


            players = players.OrderBy(x => game.HeuristicScore(x)).Reverse().ToArray();
            //order players by most advantageous to least

            //evaluate heuristics score of hand dealed to eachplayer
            foreach (var player in players)
            {
                Console.WriteLine("Skor:" + game.HeuristicScore(player) + "\n");

                foreach (var card in player)
                {
                    Console.WriteLine(GameMechanics.cards[card]);
                }

                Console.WriteLine("------------------------");
            }
            Console.WriteLine("çıkmak için bir tuşa basınız...");
            Console.ReadKey();
        }
    }
}
