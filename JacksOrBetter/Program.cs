using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JacksOrBetter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Jacks or better";
            bool quit = false;
            GameLogic gl = new GameLogic();

            while(!quit)
            {
                Console.Clear();
                string selection = "";
                gl.prepareDeck();
                gl.drawHand();
                int index = 1;
                Console.WriteLine("Your cards:");
                foreach(var card in gl.GetHand)
                {
                    Console.WriteLine(index.ToString() + ". " + card.Value + " OF " + card.Suit);
                    index++;
                }
                Console.WriteLine("Choose the positions of cards you want to redraw (without spaces). Press enter if you want to keep all cards:");
                selection = Console.ReadLine();
                if (!gl.checkInput(selection))
                {
                    Console.WriteLine("Incorrect syntax! Press enter to restart");
                    Console.ReadKey();
                    continue;

                }
                gl.refillHand(selection);
                index = 1;
                Console.WriteLine("Your new cards:");
                foreach (var card in gl.GetHand)
                {
                    Console.WriteLine(index.ToString() + ". " + card.Value + " OF " + card.Suit);
                    index++;
                }
                Console.WriteLine(gl.evaluateHand(gl.GetHand).ToString());
                selection = "";
                while (!selection.Equals("Y") && !selection.Equals("N"))
                {
                    Console.WriteLine("Play again? Y or N");
                    selection = Convert.ToString(Console.ReadLine().ToUpper());
                    if (selection.Equals("Y"))
                        quit = false;
                    else if (selection.Equals("N"))
                        quit = true;
                    else
                        Console.WriteLine("Invaid selection! Try again.");
                }
            }
        }
    }
}
