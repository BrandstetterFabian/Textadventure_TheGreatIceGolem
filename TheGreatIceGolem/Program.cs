using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TheGreatIceGolem
{
    public class Golem
    {
        public int Health { get; set; }
        public int Attack { get; set; }

        public int freezeAttackAmount { get; set; }
        public int freezeAttackDemage {  get; set; }
        public int freezeProbability { get; set; }

        public int HealPotionAmount { get; set; }
        public int HealPotionStrength { get; set; }
    }

    public class Player
    {
        public int Health { get; set; }
        public int Attack { get; set; }

        public int HealPotionAmount { get; set; }
        public int HealPotionStrength {  get; set; }
    }

    internal class Program
    { 
        public static Golem g1 = new Golem();
        public static Player p1 = new Player();

        static void Main(string[] args)
        {
            Intro();
            MainMenu();

            Console.ReadLine();
        }

        static void Intro()
        {
            Console.CursorVisible = false;
            Blue();
            Console.WriteLine("         THE GREAT ICE GOLEM");
            Thread.Sleep(4000);
        }

        static void MainMenu()
        {
            White();
            Console.Clear();
            Console.CursorVisible = true;

            Console.WriteLine("Main Menu");
            Line();
            Console.WriteLine("[1]      Start a new game");
            Line();

            MenuInput();
        }

        static void MenuInput()
        {
            int num;

            do
            {
                Console.Write("Input: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out num) && num < 2 && num > 0)
                {
                    break;
                }

            } while (true);

            if(num == 1)
            {
                StartNewGame();
            }
        }

        static void StartNewGame()
        {
            Console.Clear();
            Console.WriteLine("Start a new game");
            Line();
            Console.WriteLine("Choose a difficulty");
            Console.WriteLine();
            Console.WriteLine("[1]  Easy");
            Console.WriteLine("[2]  Medium");
            Console.WriteLine("[3]  Hard");
            Line();

            int num;

            do
            {
                Console.Write("Input: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out num) && num < 4 && num > 0)
                {
                    break;
                }

            } while (true);

            if (num == 1)
            {
                EasyGame();
            }
            else if (num == 2)
            {
                MediumGame();
            }
            else if(num == 3)
            {
                HardGame(); 
            }
        }

        //Easy Game
        static void EasyGame()
        {
            Console.Clear();
            //Player Settings
            p1.HealPotionStrength = 15;
            p1.HealPotionAmount = 8;

            p1.Health = 100;
            p1.Attack = 20;

             //Golem Settings
            g1.Attack = 10;
            g1.Health = 85;

            g1.freezeAttackDemage = 3;
            g1.freezeAttackAmount = 3;

            g1.HealPotionStrength = 15;
            g1.HealPotionAmount = 3;

            do
            {
                PlayerParameter();
                if (p1.Health < 1 || g1.Health < 1)
                {
                    CheckGameOver();
                    break;
                }
                GolemParameter();

                InputPlayer();
                bool gameOver = CheckGameOver();
                if (gameOver == true) break;
                InputIceGolem();

                White();
            } while (true);

            BackToMainMenu();
        }
        //Medium Game
        static void MediumGame()
        {
            Console.Clear();
            //Player Settings
            p1.HealPotionStrength = 17;
            p1.HealPotionAmount = 4;

            p1.Health = 95;
            p1.Attack = 15;

            //Golem Settings
            g1.Attack = 20;
            g1.Health = 95;

            g1.freezeAttackDemage = 7;
            g1.freezeAttackAmount = 5;

            g1.HealPotionStrength = 10;
            g1.HealPotionAmount = 4;

            do
            {
                PlayerParameter();
                if(p1.Health < 1 || g1.Health < 1)
                {
                    CheckGameOver();
                    break;
                }
                GolemParameter();

                InputPlayer();
                bool gameOver = CheckGameOver();
                if (gameOver == true) break;
                InputIceGolem();

                White();
            } while (true);

            BackToMainMenu();
        }
        static void HardGame()
        {
            Console.Clear();
            Console.WriteLine("UNDER CONSTRUCTION");
            BackToMainMenu();
        }
        //Player
        static void InputPlayer()
        {
            string[] potionSentences = { "As you are feeling week you take a big gulp from your potion", "You take a quick sip from your potion" , "A potion will do you good now"};
            string[] attackSentences = { "As you're feeling strong you punch the ice golem in its face", "You swing wide and hit the ice golem", "You decide to teach the ice golem a lesson"};
        playerAgain:
            Console.WriteLine();
            White();
            Console.WriteLine("---player's turn---");
        again:
            Console.WriteLine();
            Console.WriteLine("Choose 'h' to heal with potion or 'a' to attack");
            Console.Write("> ");

            if(p1.Health < 25)
            {
                p1.Attack -= 5;
            }

            string input = Console.ReadLine();

            //HEAL
            if(input == "h")
            {
                if(p1.HealPotionAmount > 0)
                {
                    p1.Health += p1.HealPotionStrength;
                    p1.HealPotionAmount--;
                    var rand = new Random();
                    Yellow();
                    int randNum = rand.Next(0, potionSentences.Length);
                    Console.WriteLine(potionSentences[randNum]);
                    Thread.Sleep(3000);
                    PlayerParameter();
                    GolemParameter();
                }
                else
                {
                    Red();
                    Console.WriteLine("!no potions left!");
                    White();
                    goto again;
                }
            }
            //ATTACK
            if(input == "a")
            {
                Yellow();
                Console.WriteLine();

                var rand3 = new Random();
                int randNum3 = rand3.Next(0, 20);

                if(p1.Health > 70 && randNum3 == 6 || randNum3 == 12 || randNum3 == 19)
                {                    
                        Console.WriteLine("Wow! Your punch was very strong. The ice golem faded!");
                        g1.Health -= (p1.Attack - 5);
                    
                  Thread.Sleep(3000);

                    PlayerParameter();
                    GolemParameter();

                    goto playerAgain;
                }
                else
                {
                    var rand2 = new Random();
                    int randNum2 = rand2.Next(0,attackSentences.Length);
                    Console.WriteLine(attackSentences[randNum2]);

                    Thread.Sleep(3000);

                    g1.Health -= p1.Attack;
                    PlayerParameter();
                    GolemParameter();
                    
                                   
                }
            }
            //INVALID INPUT
            if(input != "h" && input != "a")
            {
                Red();
                Console.WriteLine("!I'm not familiar with this input!");
                White();
                goto playerAgain;
            }
        }
        //Ice Golem
        static void InputIceGolem()
        {
            string[] attackSentences = {"The ice golem clenches a fist and hits your stomach", "Suddenly the golem kicks you in the face"};
            string[] freezeSentences = { "The ice golem takes a deep breath, blows and freezes you", "Oh no, you can't move! You were frozen by the ice golem" };
            string[] healSentences = { "The golem drinks a heal potion. Golems can also feel week!", "The ice golem takes a sip from the potion to avoid a collapse", "As the golem loses its health, it desides to take a potion" };

        golemAgain:
            CheckGameOver();
            bool freeze = false;
            bool heal = false;

            Console.WriteLine();
            White();
            Console.WriteLine("---ice golem's turn---");
            Console.WriteLine();
            Thread.Sleep(2000);

            
            //FREEZE 
            if(g1.Health < 21)
            {
                var rand1 = new Random();
                int randNum1 = rand1.Next(0, 20);
                
                if(randNum1 % 2 == 0 && g1.freezeAttackAmount > 0)
                {
                    var rand2 = new Random();
                    int randNum2 = rand2.Next(0, freezeSentences.Length);
                    Yellow();
                    Console.WriteLine(freezeSentences[randNum2]);
                    p1.Health -= g1.freezeAttackDemage;
                    g1.freezeAttackAmount--;

                    freeze = true;
                }
                //HEAL
                else if(randNum1 % 2 != 0 && g1.HealPotionAmount > 0 && heal == false)
                {
                   int randNum3 = rand1.Next(0, healSentences.Length);
                    Yellow();
                    Console.WriteLine(healSentences[randNum3]);
                    g1.Health += g1.HealPotionStrength;
                    g1.HealPotionAmount--;

                    heal = true;
                }
            }
            //ATTACK
            if(heal == false)
            {
                var rand3 = new Random();
                int randNum3 = rand3.Next(0, attackSentences.Length);
                Yellow();
                Console.WriteLine(attackSentences[randNum3]);

                Thread.Sleep(3000);
                p1.Health -= g1.Attack;
            }

            if(freeze == true)
            {
                goto golemAgain;
            }
        }

        #region parametersCreatures
        static void PlayerParameter()
        {
            Green();
            Console.WriteLine();
            Console.WriteLine("#################### Player ####################");
            Console.WriteLine();
            Console.WriteLine(" health:         {0}     attack:         {1}", p1.Health, p1.Attack);
            Console.WriteLine(" potionAmount:   {0}     potionStrength: {1}", p1.HealPotionAmount, p1.HealPotionStrength);
            Console.WriteLine("################################################");
            Console.WriteLine();
        }
        static void GolemParameter()
        {
            Blue();
            Console.WriteLine();
            Console.WriteLine("****************** Ice Golem *******************");
            Console.WriteLine();
            Console.WriteLine(" health:         {0}", g1.Health);
            Console.WriteLine(" potionAmount:   {0}     potionStrength: {1}", g1.HealPotionAmount, g1.HealPotionStrength);
            Console.WriteLine("************************************************");
            Console.WriteLine();
        }
        #endregion

        #region Colors
        static void Green() // for player
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void Blue() // for ice golem
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
         
        static void White() // for normal text like menu or input
        {
            Console.ForegroundColor= ConsoleColor.White;
        }

        static void Red() // unknown input or any other unknown things
        {
            Console.ForegroundColor= ConsoleColor.Red;
        }

        static void Yellow() // for any actions that are done
        {
        Console.ForegroundColor= ConsoleColor.Yellow;
        }

        #endregion

        static bool CheckGameOver()
        {
            White();
            if(p1.Health < 1)
            {
                Console.Clear();
                Console.WriteLine("You've lost!");
                Console.WriteLine(" GAME OVER");

                BackToMainMenu();

                return true;
            }
            else if(g1.Health < 1)
            {
                Console.Clear();
                Console.WriteLine("You've won!");
                Console.WriteLine(" GAME OVER");

                BackToMainMenu();

                return true;
            }
            else
            {
                return false;
            }
        }

        static void Line()
        {
            Console.WriteLine("-----------------------------");
        }

        static void BackToMainMenu()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine("[enter] back to main menu");
            Console.ReadLine();
            MainMenu();
        }
    }
}
