// class used for creating the player and the dealer

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Player
    {        
        public Hand Hand { get; set; }        
        public int Money { get; set; }
        public int Bet { get; set; }
        public bool Active { get; set; }
            
        // empty constructor 
        public Player()
        {

        }

        // class constructor
        public Player(Hand hand, int money)
        {
            Hand = hand;
            Money = money;
            Bet = 25;
            
        }

        // return  the points in the current  hand
        public int Score()
        {
            int count = 0;
            int aceCount = 0; //counting the number of aces - later used to determine if ace is 1 or 11 
                for (int i = 0; i<Hand.Pointer ; i++)
                {
                    if (Hand.Cards[i].Number > 9)
                    {
                        count = count + 10;
                    }
                    else
                    {
                        if (Hand.Cards[i].Number == 1 && count< 11)
                        {
                            count = count + 11;
                            aceCount++;
                        }
                        else
                        {
                            count = count + Hand.Cards[i].Number;
                        }


                    }
                }

            // if total exceeds 21 with ace counted as 11 will revert ace to 1
            if (count > 21)
            {
                while (aceCount >0 && count >21)
                {
                    count = count - 10;
                    aceCount--;
                }

            }
            return count;
        }

    }
}
