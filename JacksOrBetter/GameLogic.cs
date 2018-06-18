using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JacksOrBetter
{
    class GameLogic
    {
        const int NUM_OF_VALUES = 13;
        const int NUM_OF_CARDS = 52;
        const int HAND_SIZE = 5;
        private Card[] deck;
        private Stack<Card> shuffledDeck;
        private Card[] hand;

        public Card[] GetHand { get { return hand; } }

        public GameLogic()
        {
            deck = new Card[NUM_OF_CARDS];
            shuffledDeck = new Stack<Card>();
            hand = new Card[HAND_SIZE];
        }

        public void prepareDeck()
        {
            int index = 0;
            foreach (Card.SUIT s in Enum.GetValues(typeof(Card.SUIT)))
            {
                foreach (Card.VALUE v in Enum.GetValues(typeof(Card.VALUE)))
                {
                    deck[index] = new Card { Suit = s, Value = v };
                    index++;
                }
            }
            ShuffleDeck();
            foreach(Card element in deck)
            {
                shuffledDeck.Push(element);
            }
        }

        private void ShuffleDeck()
        {
            Random rand = new Random();
            Card temp;
            for(int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i=0; i<NUM_OF_CARDS; i++)
                {
                    int nextCardIndex = rand.Next(NUM_OF_VALUES);
                    temp = deck[i];
                    deck[i] = deck[nextCardIndex];
                    deck[nextCardIndex] = temp;
                }
            }
        }

        public void drawHand(int numberOfCards = HAND_SIZE)
        {
            for(int i=0; i<numberOfCards; i++)
            {
                hand[i] = shuffledDeck.Pop();
            }
        }

        public void refillHand(String change)
        {
           foreach (char pos in change)
           {
                hand[(int)Char.GetNumericValue(pos) - 1] = shuffledDeck.Pop();
           }
        }

        public HandValue evaluateHand(Card[] handToEvaluate)
        {
            HandEvaluator handEvaluator = new HandEvaluator(handToEvaluate);
            return handEvaluator.EvaluateHand();
        }

        public bool checkInput (string input)
        {
            if (input.Equals(null))
                return true;
            else if (input.Length > 5)
                return false;
            else if (input.Distinct().Count() != input.Length)
                return false;
            else
            {
                foreach (char c in input)
                {
                    if (c < '1' || c > '5')
                        return false;
                }
            }
            return true;
        }
    }
}
