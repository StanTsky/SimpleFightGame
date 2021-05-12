using System;

namespace SimpleFightGame
{
    public class player
    {
        public int Health { get; set; }
        public string Name { get; set; }
        public player()
        {
            Name = "Player";  // Initial player name
            Health = 100;     // Initial player health
        }
        public void Attack (enemy E, int Dmg)
        {
            E.Health-=Dmg;
            if (E.Health <= 0)
            {
                Console.WriteLine($"{E.Name} died!");
            }
            else
            {
                Console.WriteLine($"\t {E.Name} has {E.Health} health left!");
            }
        }
    }
    abstract public class enemy
    {       
        public string Name { get; set; }
        public int Health { get; set; }
        public int Type { get; set; }

        public abstract void Speak(player P);

        public virtual void Attack(player P, int Dmg)
        {
            P.Health -= Dmg;
            if (P.Health <= 0)
            {
                Console.WriteLine($"{P.Name} died!");
            }
            else
            {
                Console.WriteLine($"\t {P.Name} has {P.Health} health left!");
            }
        }
    }
    class enemy1 : enemy
    {
        public enemy1()
        {
            Health = 100;
            Type = 1;
            Name = "Enemy1";
        }
        public override void Speak(player P)
        {
            Console.WriteLine($"{Name}: Prepare to die, {P.Name}! I'm type {Type}!");
        }

        public override void Attack(player P, int Dmg)
        {
            base.Attack(P, Dmg);        // fulfills override requirement
        }
    }
    class enemy2: enemy
    {
        public enemy2()
        {
            Health = 100;
            Type = 2;
            Name = "Enemy2";
        }
        public override void Speak(player P)
        {
            Console.WriteLine($"{Name}: Haha! You'll lose! I'm type {Type}!\n");
        }

        public override void Attack(player P, int Dmg)
        {
            base.Attack(P, Dmg);        // fulfills override requirement
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();  // random number generator
            enemy e1 = new enemy1();    // create enemies
            enemy e2 = new enemy2();
            player p = new player();    // create player

            int dmg;                    // damage amount
            int choice;                 // choice
            string newGame;             // decision to play again

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

                } while (e1.Health>0 & e2.Health>0 & p.Health>0);   // keep playing until someone dies
                Console.Write("Play again?(y/n) ");
                newGame = Console.ReadLine().ToLower();             // get user's decision
            } while (newGame == "y");
        }
    }
}
