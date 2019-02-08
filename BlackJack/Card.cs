// class to creaate one card

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
    class Card
    {
        public String Suit { get; set; }
        public int Number { get; set; }    //used for counting the hand
        String DisplayValue { get; set; }  //used for display the card for the user

        //empy constructor
        public Card()
        {

        } 

        //class constructor - asignes display value for each card
        public Card(string suit, int number)
        {
            Suit = suit;
            Number = number;
            switch (number)
            {
                case 11:
                    DisplayValue = "J";
                    break;
                case 12:
                    DisplayValue = "Q";
                    break;
                case 13:
                    DisplayValue = "K";
                    break;
                case 1:
                    DisplayValue = "A";
                    break;
                default:
                    DisplayValue = number.ToString();
                    break;

            }           
        }
        
        //formating the display 
        public override string ToString()
        {
            return "     [" +Suit+ DisplayValue + Suit + "]";
        }
    }
}
