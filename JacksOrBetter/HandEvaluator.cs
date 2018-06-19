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
        private const int FULL_HAND = 5;
        private const int FIRST_CARD_POS = 0;
        private const int SECOND_CARD_POS = 1;
        private const int THIRD_CARD_POS = 2;
        private const int FOURTH_CARD_POS = 3;
        private const int FIFTH_CARD_POS = 4;

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
            return Flush() ?
                (sortedHand.First().Value == Card.VALUE.TEN && sortedHand.Last().Value == Card.VALUE.ACE ?
                    true :
                        false) : 
                false;
        }

        private bool StraightFlush()
        {
            return Flush() ?
                (Straight() ? true : false) :
                false;
        }

        private bool FourOfAKind()
        {
            return sortedHand.First().Value == sortedHand[FIFTH_CARD_POS].Value || sortedHand[SECOND_CARD_POS].Value == sortedHand.Last().Value;
        }

        private bool FullHouse()
        {
            return (sortedHand[FIRST_CARD_POS].Value == sortedHand[SECOND_CARD_POS].Value && sortedHand[THIRD_CARD_POS].Value == sortedHand[FOURTH_CARD_POS].Value && sortedHand[FOURTH_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value)
                || (sortedHand[FOURTH_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value && sortedHand[FIRST_CARD_POS].Value == sortedHand[SECOND_CARD_POS].Value && sortedHand[SECOND_CARD_POS].Value == sortedHand[THIRD_CARD_POS].Value);
        }

        private bool Flush()
        {
            return (heartsSum == FULL_HAND || diamondSum == FULL_HAND || clubSum == FULL_HAND || spadesSum == FULL_HAND);
        }

        private bool Straight()
        {
            return sortedHand.First().Value + 1 == sortedHand[SECOND_CARD_POS].Value
                && sortedHand[SECOND_CARD_POS].Value + 1 == sortedHand[THIRD_CARD_POS].Value
                && sortedHand[THIRD_CARD_POS].Value + 1 == sortedHand[FOURTH_CARD_POS].Value
                && sortedHand[FOURTH_CARD_POS].Value + 1 == sortedHand[FIFTH_CARD_POS].Value;
        }

        private bool ThreeOfAKind()
        {
            return sortedHand[FIRST_CARD_POS].Value == sortedHand[THIRD_CARD_POS].Value 
                || sortedHand[SECOND_CARD_POS].Value == sortedHand[FOURTH_CARD_POS].Value 
                || sortedHand[THIRD_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value;
        }

        private bool TwoPairs()
        {
            return (sortedHand[FIRST_CARD_POS].Value == sortedHand[SECOND_CARD_POS].Value && sortedHand[THIRD_CARD_POS].Value == sortedHand[FOURTH_CARD_POS].Value)
                || (sortedHand[FIRST_CARD_POS].Value == sortedHand[SECOND_CARD_POS].Value && sortedHand[FOURTH_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value)
                || (sortedHand[SECOND_CARD_POS].Value == sortedHand[THIRD_CARD_POS].Value && sortedHand[FOURTH_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value);
        }

        private bool JacksOrBetter()
        {
            return (sortedHand[FIRST_CARD_POS].Value == sortedHand[SECOND_CARD_POS].Value && sortedHand[FIRST_CARD_POS].Value >= Card.VALUE.JACK)
                || (sortedHand[SECOND_CARD_POS].Value == sortedHand[THIRD_CARD_POS].Value && sortedHand[SECOND_CARD_POS].Value >= Card.VALUE.JACK)
                || (sortedHand[THIRD_CARD_POS].Value == sortedHand[FOURTH_CARD_POS].Value && sortedHand[THIRD_CARD_POS].Value >= Card.VALUE.JACK)
                || (sortedHand[FOURTH_CARD_POS].Value == sortedHand[FIFTH_CARD_POS].Value && sortedHand[FOURTH_CARD_POS].Value >= Card.VALUE.JACK);
        }
    }
}
