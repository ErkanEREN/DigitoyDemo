using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitoyDemo
{
    class GameMechanics
    {
        public static string[] cards = {
            "Sarı-1", "Sarı-2", "Sarı-3", "Sarı-4", "Sarı-5", "Sarı-6", "Sarı-7", "Sarı-8", "Sarı-9", "Sarı-10", "Sarı-11", "Sarı-12", "Sarı-13",
            "Mavi-1", "Mavi-2", "Mavi-3", "Mavi-4", "Mavi-5", "Mavi-6", "Mavi-7", "Mavi-8", "Mavi-9", "Mavi-10", "Mavi-11", "Mavi-12", "Mavi-13",
            "Siyah-1", "Siyah-2", "Siyah-3", "Siyah-4", "Siyah-5", "Siyah-6", "Siyah-7", "Siyah-8", "Siyah-9", "Siyah-10", "Siyah-11", "Siyah-12", "Siyah-13",
            "Kırmızı-1", "Kırmızı-2", "Kırmızı-3", "Kırmızı-4", "Kırmızı-5", "Kırmızı-6", "Kırmızı-7", "Kırmızı-8", "Kırmızı-9", "Kırmızı-10", "Kırmızı-11", "Kırmızı-12", "Kırmızı-13",
            "Sahte-Okey"
        };//to convert cards to human-readable format I have manually declared this array

        Random rn;
        int indicator, joker = -1;
        List<int> gameCards;

        public GameMechanics()
        {
            rn = new Random();
            gameCards = Enumerable.Range(0, 52).Concat(Enumerable.Range(0, 52).ToArray()).OrderBy(x => rn.Next()).ToList<int>();
            //indicator = drawCard(1)[0];
            //joker = indicator + 1; 
        }

        //have a heuristic calculated that deter mines each "cards" score by comparing it with other cards by proximity(1-2-3 same color) and being exact multiple of (same rank diff suite) joker will get the maximum

        //public List<string> drawCard(int amount)
        //{
        //    if (gameCards.Count < amount)
        //        return null;

        //    List<string> val = new List<string>();
        //    for (int i = 0; i < amount; i++)
        //    {
        //        val.Add(cards[gameCards.PopAt(rn.Next(0, gameCards.Count - 1))]);
        //    }
        //    gameCards.PopAt(rn.Next(0, gameCards.Count - 1));
        //    return val;
        //}

        public List<int> drawCard(int amount)
        {
            List<int> val = new List<int>();
            for (int i = 0; i < amount; i++)
            {
                val.Add(gameCards.PopAt(rn.Next(0, gameCards.Count - 1)));
            }
            gameCards.PopAt(rn.Next(0, gameCards.Count - 1));
            return val;
        }


        public int HeuristicScore(List<int> hand)
        {
            /*
             * Method intents to score hands heuristic score
             * But an AI heuristics method both written and implemented in less than 2 hours shouldn't be expected to be perfect
             */
            int score = 0;
            int temp = 0;
            foreach (var card in hand)
            {
                score += hand.Where(x =>
                {
                    temp = (card == joker) ? 52 : (card == 52) ? joker : card;
                    //isolating joker and using "fake joker" as the playing card

                    if (temp == 52)
                        return true;
                    //if the card is the joker we can pair it with any card

                    if (temp % 13 == x % 13)
                        return true;
                    //if card matches rank with any other card we can pair them

                    if (temp / 13 == x / 13)
                    {//same suit
                        if (temp % 13 > 10 && x % 13 < 2)
                            return true;
                        //if the rank of card close to end (11,12,13) and if we have a 1 of same suit match

                        if (Math.Abs(temp - x) < 3)
                            return true;
                        //our card is closer to any other of card
                    }
                    return false;
                }).Count();
            }

            return score;
        }
    }
}


static class ListExtension
{//This class should be in utilities namespace but since this project is not "big enough" to have it, having it in here as good as having it anywhere.
    public static T PopAt<T>(this List<T> list, int index)
    {
        T r = list[index];
        list.RemoveAt(index);
        return r;
    }
}
