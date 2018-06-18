using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksOrBetter
{
    public enum HandValue
    {
        Nothing,
        JacksOrBetter,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    class HandEvaluator
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private List<Card> sortedHand;

        public HandEvaluator(Card[] hand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            sortedHand = hand.OrderBy(q => q.Value).ToList();
        }

        public HandValue EvaluateHand()
        {
            getNumberOfSuits();
            if (RoyalFlush())
                return HandValue.RoyalFlush;
            else if (StraightFlush())
                return HandValue.StraightFlush;
            else if (FourOfAKind())
                return HandValue.FourOfAKind;
            else if (FullHouse())
                return HandValue.FullHouse;
            else if (Flush())
                return HandValue.Flush;
            else if (Straight())
                return HandValue.Straight;
            else if (ThreeOfAKind())
                return HandValue.ThreeOfAKind;
            else if (TwoPairs())
                return HandValue.TwoPairs;
            else if (JacksOrBetter())
                return HandValue.JacksOrBetter;

            return HandValue.Nothing;
        }

        private void getNumberOfSuits()
        {
            heartsSum = sortedHand.Count(q => q.Suit == Card.SUIT.HEARTS);
            diamondSum = sortedHand.Count(q => q.Suit == Card.SUIT.DIAMONDS);
            clubSum = sortedHand.Count(q => q.Suit == Card.SUIT.CLUBS);
            spadesSum = sortedHand.Count(q => q.Suit == Card.SUIT.SPADES);
        }

        private bool RoyalFlush()
        {
            return (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5) ?
                (sortedHand.First().Value == Card.VALUE.TEN && sortedHand.Last().Value == Card.VALUE.ACE ?
                    true :
                        false) : 
                false;
        }

        private bool StraightFlush()
        {
            return (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5) ?
                (sortedHand.First().Value + 4 == sortedHand.Last().Value ?
                    true :
                        false) :
                false;
        }

        private bool FourOfAKind()
        {
            return sortedHand.First().Value == sortedHand[4].Value || sortedHand[1].Value == sortedHand.Last().Value;
        }

        private bool FullHouse()
        {
            return (sortedHand[0].Value == sortedHand[1].Value && sortedHand[2].Value == sortedHand[3].Value && sortedHand[3].Value == sortedHand[4].Value)
                || (sortedHand[3].Value == sortedHand[4].Value && sortedHand[0].Value == sortedHand[1].Value && sortedHand[1].Value == sortedHand[2].Value);
        }

        private bool Flush()
        {
            return (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5);
        }

        private bool Straight()
        {
            return sortedHand.First().Value + 4 == sortedHand.Last().Value;
        }

        private bool ThreeOfAKind()
        {
            return sortedHand[0].Value == sortedHand[2].Value || sortedHand[1].Value == sortedHand[3].Value || sortedHand[2].Value == sortedHand[4].Value;
        }

        private bool TwoPairs()
        {
            return (sortedHand[0].Value == sortedHand[1].Value && sortedHand[2].Value == sortedHand[3].Value)
                || (sortedHand[0].Value == sortedHand[1].Value && sortedHand[3].Value == sortedHand[4].Value)
                || (sortedHand[1].Value == sortedHand[2].Value && sortedHand[3].Value == sortedHand[4].Value);
        }

        private bool JacksOrBetter()
        {
            return (sortedHand[0].Value == sortedHand[1].Value && sortedHand[0].Value >= Card.VALUE.JACK)
                || (sortedHand[1].Value == sortedHand[2].Value && sortedHand[1].Value >= Card.VALUE.JACK)
                || (sortedHand[2].Value == sortedHand[3].Value && sortedHand[2].Value >= Card.VALUE.JACK)
                || (sortedHand[3].Value == sortedHand[4].Value && sortedHand[3].Value >= Card.VALUE.JACK);
        }
    }
}
