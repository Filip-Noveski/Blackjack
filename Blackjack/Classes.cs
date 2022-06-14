using System;

namespace Blackjack
{
    public struct Card  // Cards to be played
    {
        public Int16 Value;     // Value of card
        public String Path;     // Path to card's image
        public String Name;     // Name of card
        public Int16 Amount;    // Amount of this card left
    }

    public struct Hand  // Hand of Player or dealer
    {
        public Int16 Value;     // Value of hand
        public Int16 Card1;     // Value of first card
        public Int16 Card2;     // Value of second card
        public Boolean Soft;    // Whether hand has a soft total
        public Boolean Double;  // Whether the player has doubled-down this hand
        public Boolean Valid;   // Whether the hand is valid and the dealer should try and beat it
    }

    public struct Deviation // The deviation the player should make
    {
        public char Move;              // Correct move accoring to deviations
        public int Count;              // The count at which this move should be performed
        public bool Above;             // Whether the deviation is according to above the count or below it
    }
}
