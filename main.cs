/*
--------------------------------------------------------------------
* Name:       Stan Turovsky
* Class:      CPSC 24500
* Assignment: HW 2
* File:       main.cs
* Purpose:    Simple fight game to show use of class overrides in C#
--------------------------------------------------------------------
*/

using System;

namespace SimpleFightGame
{
    public class Player
    {
        public int Health { get; set; }
        public string Name { get; set; }
        public Player()
        {
            Name = "Player";  // Initial player name
            Health = 100;     // Initial player health
        }
        public void Attack(Enemy e, int dmg)
        {
            e.Health -= dmg;
            if (e.Health <= 0)
            {
                Console.WriteLine($"{e.Name} died!");
            }
            else
            {
                Console.WriteLine($"\t {e.Name} has {e.Health} health left!");
            }
        }
    }
    public abstract class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Type { get; set; }

        public abstract void Speak(Player p);

        public virtual void Attack(Player p, int dmg)
        {
            p.Health -= dmg;
            if (p.Health <= 0)
            {
                Console.WriteLine($"{p.Name} died!");
            }
            else
            {
                Console.WriteLine($"\t {p.Name} has {p.Health} health left!");
            }
        }
    }
    class Enemy1 : Enemy
    {
        public Enemy1()
        {
            Health = 100;     // Initial enemy1 health
            Type = 1;         // Enemy Type
            Name = "Enemy1";  // Initial enemy1 name
        }
        public override void Speak(Player p)
        {
            Console.WriteLine($"{Name}: Prepare to die, {p.Name}! I'm type {Type}!");
        }
    }
    class Enemy2 : Enemy
    {
        public Enemy2()
        {
            Health = 100;       // Initial enemy2 health
            Type = 2;           // Enemy Type
            Name = "Enemy2";    // Initial enemy2 name
        }
        public override void Speak(Player p)
        {
            Console.WriteLine($"{Name}: Haha! You'll lose! I'm type {Type}!\n");
        }
    }

    class Program
    {
        static void Main()
        {
            Random rnd = new Random();  // random number generator
            Enemy e1 = new Enemy1();    // create enemies
            Enemy e2 = new Enemy2();
            Player p = new Player();    // create player

            int dmg;                    // damage amount
            int choice;                 // choice
            string newGame;             // decision to play again

            string lineStars = new String('*', 60);

            Console.Write("\n" + lineStars + "\n");
            Console.Write(" This is simple fight game between you and 2 other enemies\n");
            Console.Write(" 1) Enter your name and names for the ememies\n");
            Console.Write(" 2) Press any key to continue each fight attempt\n");
            Console.Write(" 3) In the end you will be prompted if you want to retry\n");
            Console.Write(lineStars + "\n\n");

            Console.Write("What is your name? ");
            p.Name = Console.ReadLine();

            Console.Write("What should we call the 1st enemy? ");
            e1.Name = Console.ReadLine();

            Console.Write("What should we call the 2nd enemy? ");
            e2.Name = Console.ReadLine();

            do
            {                       // Initialize everyone's health to allow replayability
                e1.Health = 50;
                e2.Health = 50;
                p.Health = 100;     // Player needs more health for a more fair fight result

                Console.WriteLine("\n*** NOTE: Press a key to move to the next step *** \n");

                e1.Speak(p);        // Enemy taunts 
                e2.Speak(p);

                do
                {
                    dmg = rnd.Next(1, 26);      // max value is excusive, so adding 1 to max for dmg 1 to 25
                    choice = rnd.Next(1, 5);    // max value is excusive, so adding 1 to max for choice 1 to 4

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine($"{e1.Name} attacks {p.Name} with {dmg} damage!");
                            e1.Attack(p, dmg);
                            break;
                        case 2:
                            Console.WriteLine($"{e2.Name} attacks {p.Name} with {dmg} damage!");
                            e2.Attack(p, dmg);
                            break;
                        case 3:
                            Console.WriteLine($"{p.Name} attacks {e1.Name} with {dmg} damage!");
                            p.Attack(e1, dmg);
                            break;
                        default:
                            Console.WriteLine($"{p.Name} attacks {e2.Name} with {dmg} damage!");
                            p.Attack(e2, dmg);
                            break;
                    }

                    Console.ReadKey();                              // lets you see results

                } while (e1.Health > 0 & e2.Health > 0 & p.Health > 0);   // keep playing until someone dies
                Console.Write("Play again?(y/n) ");
                newGame = Console.ReadLine()?.ToLower();             // get user's decision
            } while (newGame == "y");
        }
    }
}