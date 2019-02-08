using System;
using System.Threading;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //play game intro
            Intro();            
            
            //create player and dealer
            Player player = new Player(new Hand(),200);
            Player dealer = new Player(new Hand(), 1000);

            //game loop start here 
            bool gameLoop = true;
            while (gameLoop)
            {
                Deck deck = new Deck();
                //initialize poointers for player and dealer, set the initial bet
                dealer.Hand.Pointer = 0;
                player.Hand.Pointer = 0;
                player.Bet = 25;

                //deal two cards to player              
                Deal(player, ref deck);
                Deal(player, ref deck);

                //deal two card to dealer - first card is visible to player second will be face down 
                Deal(dealer, ref deck);
                Deal(dealer, ref deck);
                dealer.Hand.Pointer--;

                bool stick = false;
                bool bust = false;
                
                //player loop
                while (!stick&&!bust&&gameLoop)
                {
                    player.Active = true;
                    dealer.Active = false;
                    Display(dealer, player);
                    // check if player exceeded 21 - bust
                    if (player.Score() > 21)
                    {
                        bust = true;
                    }
                    else
                    {
                        //check if player has 21 - blacjack - automatic end of turn
                        if (player.Score() == 21)
                        {
                            stick = true;
                        }
                        else
                        {
                            //querying player choice and reacting based on keyboard hit
                            switch (Console.ReadKey().Key.ToString().ToUpper())
                            {
                                case "S"://player sticks
                                    stick = true;
                                    break;
                                case "D"://player doubles
                                    if (player.Hand.Pointer < 3 && player.Money > 49)
                                    {
                                        Message("Bet is doubled...");
                                        Deal(player, ref deck);
                                        stick = true;
                                        player.Bet = 50;
                                        Display(dealer, player);
                                        
                                    }
                                    else
                                    {
                                        Message("Double is not posible at this time...");
                                    }
                                    
                                    break;
                                case "H":
                                    Help();
                                    break;
                                case "Q":
                                    gameLoop = false;
                                    break;

                                default:
                                    //deal a card to player and and advance the deck
                                    Deal(player, ref deck);

                                    //display the current status
                                    Display(dealer, player);
                                    break;                                
                                

                            }
                        }
                    }
                }
                

                //chech if player is bust - if yes dealer won - if player hit quit will exit loop
                if (!bust&&gameLoop)
                {
                    //reset stick and bust
                    stick = false;
                    bust = false;
                    //dealer loop 
                    dealer.Hand.Pointer++;
                    while (!stick && !bust)
                    {
                        player.Active = false;
                        dealer.Active = true;
                        
                        Display(dealer, player);                        
                        if (dealer.Score() > 21)    //check if dealer bust
                        {
                            bust = true;                           
                        }
                        else
                        {
                            if (dealer.Score() == 21)   //check if dealer has 21
                            {
                                stick = true;
                            }
                            else
                            {
                                if (dealer.Score() > 16)    //dealer sticks if 17 or higher is reached
                                {
                                    stick = true;
                                }
                                else
                                {
                                    Deal(dealer, ref deck); //dealer gets card
                                }

                            }
                        }
                        
                        Thread.Sleep(2000); // stop the proccess to simulate dealer flipping the cards
                    }

                    
                    Display(dealer, player); //display the current status

                    
                }

                //displaying the winner
                if (gameLoop)
                {
                    DisplayWinner(dealer, player);
                    Thread.Sleep(2000);
                }
                

                //check if player ran out of money
                if (player.Money <= 0 )
                {
                    Message("You ran out of money...GAME OVER");
                    gameLoop = false; 
                }

                //check if table limit is reached 
                if (dealer.Money <=0)
                {
                    Message("You have won a $1000...CONGRATULATIONS");
                    gameLoop = false; 
                }
            }

            //game closeing
            Outro(player);

        }
        
        //this method displays the menu and current information while game is running
       static void Display(Player dealer, Player player )
        {

            Console.Clear();
            Console.WriteLine("    _______________________________________________________________");
            Console.WriteLine("     - \u2663 -- \u2665 --- \u2666 ---- \u2660 B L A C K J A C K \u2660 ---- \u2666 --- \u2665 -- \u2663 -\n");
            Console.WriteLine("    |Hit - ANYKEY| |Stand - S| |Double - D| |Help - H|  |Quit - Q|");
            Console.WriteLine("\n\n");
            if (player.Active)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta; //changing color if player is active
            }
            Console.Write("{0,15}{1}"," Player:", player.Score());
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < player.Hand.Pointer; i++)
            {
                if(player.Hand.Cards[i].Suit== "\u2666" || player.Hand.Cards[i].Suit == "\u2665")   //changing color for heart and diamonds to red
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; 
                    Console.Write(player.Hand.Cards[i]);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Write(player.Hand.Cards[i]);
                }
                
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("{0,15 }${1}", "Bet:", player.Bet);
            Console.WriteLine("{0,15}${1}", "Total Cash:", player.Money);

            if (player.Score() >21)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("{0,22}","BUSTED");
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine("\n");
            Console.WriteLine( "    _______________________________________________________________");
            Console.WriteLine("\n");
            if (dealer.Active)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta; // changing color if dealer is active
            }
            Console.Write("{0,15}{1}", " Dealer:", dealer.Score());
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < dealer.Hand.Pointer; i++)
            {
                if (dealer.Hand.Cards[i].Suit == "\u2666" || dealer.Hand.Cards[i].Suit == "\u2665") //changing color for heart and diamonds to red
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;     
                    Console.Write(dealer.Hand.Cards[i]);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Write(dealer.Hand.Cards[i]);
                }
                
            }
            if (player.Active)
            {
                Console.Write("     [ ? ]"); //face down card of dealer
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("{0,15}${1}", "Total Cash:", dealer.Money);

            if (dealer.Score() >21)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("{0,22}", "BUSTED");
                Console.ForegroundColor = ConsoleColor.Black;
            }

        }
        
        //method for dealing a card
        static void Deal(Player player, ref Deck deck)
        {
            player.Hand.Cards[player.Hand.Pointer] = deck.Cards[deck.Pointer];
            player.Hand.Pointer++;
            deck.Pointer++;

        }
       
        //method for displaying the winner and managing the bet
        static void DisplayWinner(Player dealer, Player player)
        {
                
            if (player.Score() > 21)
            {
                Message("Dealer wins! Dealing new hand...");
                player.Money = player.Money - player.Bet;
                dealer.Money = dealer.Money + player.Bet;
            }
            else
                if (dealer.Score() > 21) 
                {
                    Message("Player wins! Dealing new hand...");
                    player.Money = player.Money + player.Bet;
                    dealer.Money = dealer.Money - player.Bet;
                }
                else
                {
                    if (dealer.Score() < player.Score())
                    {
                        Message("Player wins! Dealing new hand...");
                        player.Money = player.Money + player.Bet;
                        dealer.Money = dealer.Money - player.Bet;   
                    }
                    else
                    {
                        if (dealer.Score() > player.Score())
                        {
                            Message("Dealer wins! Dealing new hand..");
                            player.Money = player.Money - player.Bet;
                            dealer.Money = dealer.Money + player.Bet;
                        }
                        else
                        {
                            Message("Tie. Dealing new hand...");
                            player.Bet = 25;
                        }
                    }

                }
            
        }
                
        // game intro
        static void Intro()
        {

            string blackJack = " B L A C K J A C K";
            for(int i = 0; i < 18; i++)
            {
                Console.WriteLine("    {0}",blackJack[i]);
                for(int j=0; j<i; j++)
                {
                    Console.Write("  ");
                }
                
                Thread.Sleep(100);
            }
            Thread.Sleep(1000);

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        // game ending 
        static void Outro(Player player)
        {
            string cashOut = " * * * * * * * C A S H I N G * O U T * * * * * * * *";
            string goodbye = " * * * * * * * G O O D B Y E * * * * * * * * * * * *";  // used if player has no money left
            for (int i = 0; i < 50; i++)
            {
                if (player.Money == 0)
                {
                    Console.WriteLine("    {0}", goodbye[i]);
                }
                else
                {
                    Console.WriteLine("    {0}", cashOut[i]);
                }                
                for (int j = 0; j < i; j++)
                {
                    Console.Write("  ");
                }

                Thread.Sleep(100);
            }
            Thread.Sleep(1000);
        }

        //game help and description from txt file
        static void Help()
        {
            Console.Clear();
            Console.WriteLine("    _______________________________________________________________");
            Console.WriteLine("     - \u2663 -- \u2665 --- \u2666 ---- \u2660 B L A C K J A C K \u2660 ---- \u2666 --- \u2665 -- \u2663 -\n");           
            string text = System.IO.File.ReadAllText(@"help.txt");
            Console.WriteLine(text);
            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
        }

        //display messages to player
        static void Message(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n\n");
            Console.Write("{0,40}", s);
            Thread.Sleep(2500);
            Console.ForegroundColor = ConsoleColor.Black;

        }
    }
}
