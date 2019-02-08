//class to create a deck of cards

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Deck
    {
        public Card[] Cards { get; set; }
        public int Pointer { get; set; }


        public Deck()
        {
            //create 52 cards
            Cards = new Card[52];

            // load the four suits
            for (int i = 0; i < 13; i++)
            {
                Cards[i] = new Card("\u2663", i + 1);
            }
            for (int i = 0; i < 13; i++)
            {
                Cards[13 + i] = new Card("\u2666", i + 1);
            }
            for (int i = 0; i < 13; i++)
            {
                Cards[26 + i] = new Card("\u2665", i + 1);
            }
            for (int i = 0; i < 13; i++)
            {
                Cards[39 + i] = new Card("\u2660", i + 1);
            }

            // shuffle cards by doing 52 random card swaps 1000 times
            Random rnd = new Random();
            Card tempCard = new Card();
            int shuffleTime = rnd.Next(1000, 1000);
            while (shuffleTime != 0)
            {
                for (int i = 0; i < 52; i++)
                {
                    int cardPosition = rnd.Next(0, 52);
                    tempCard = Cards[i];
                    Cards[i] = Cards[cardPosition];
                    Cards[cardPosition] = tempCard;
                }
                shuffleTime--;
            }

            //set the deck pointer to the first card
            Pointer = 0;

        }
    }
}
