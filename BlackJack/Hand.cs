//class for createing a hand 

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Hand
    {
        public Card[] Cards { get; set; } 
        public int Pointer { get; set; }

        public Hand()
        {
            Cards = new Card[10];
            Pointer = 0;
        }
    }
}
