using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Game
    {
        Player player;
        Parser parser;
        
        bool playing;
        Queue<Command> commandQueue;
        GameWorld gameWorld = new GameWorld();
        public Game()
        {

            playing = false;
            parser = new Parser(new CommandWords());  // for seperating player input into command and secondword
            player = new Player("Hero", 100, 1, gameWorld.Entrance);//create player and determine starting location and base stats.
            
            

            commandQueue = new Queue<Command>(); //command queue for ease of testing

            //adding commands to the command queue
            //TakeCommand tc = new TakeCommand();
            //tc.secondWord = "Cloak";
            //commandQueue.Enqueue(tc);
            //InventoryCommand ic = new InventoryCommand();
            //commandQueue.Enqueue(ic);
            //EquipCommand ec = new EquipCommand();
            //ec.secondWord = "Cloak";
            //commandQueue.Enqueue(ec);
            //GoCommand gc = new GoCommand();
            //gc.secondWord = "East";
            //commandQueue.Enqueue(gc);
            //gc = new GoCommand();
            //gc.secondWord = "North";
            //commandQueue.Enqueue(gc);
            //gc = new GoCommand();
            //gc.secondWord = "North";
            //commandQueue.Enqueue(gc);
            //gc = new GoCommand();
            //gc.secondWord = "West";
            //commandQueue.Enqueue(gc);
            //gc = new GoCommand();
            //gc.secondWord = "West";
            //commandQueue.Enqueue(gc);

        }
        



        

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished && playing)
            {
                NotificationCenter.Instance.addObserver("GameOver", GameOver); //game over if player wins or dies
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n>");
                Command command = parser.parseCommand(Console.ReadLine().ToLower());
                Console.ForegroundColor = ConsoleColor.Green;
                if (command == null)
                {
                    player.warningMessage("I don't understand..."); //message given if player inputs bad command
                }
                else
                {
                    finished = command.execute(player); //executes command received from player
                }
            }
        }


        public void start() //output welcome message and process command queue
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            playing = true;
            player.outputMessage(welcome());
            processCommandQueue();
        }

        public void end() // output goodbye message
        {
            playing = false;
            player.outputMessage(goodbye());
        }

        public string welcome() // game start welcome message
        {
            return "If ever you feel lost, ask for help and you shall receive it.\n\n\n\nYou try to catch her as she loses her footing by the well, but you aren't fast enough to save \n" +
                "her. As she falls into the darkness below, you immediately begin to climb down to rescue her. After \n" +
                "climbing only a few feet down your foot slips on one of the wet rocks that make up the wall of the \n" +
                "well and begin to plummet after her.\n...\n\n\n\n\n\n" + player.currentRoom.description();
        }

        public string goodbye()//goodbye message
        {
            return "\nThank you for playing, Goodbye. \n";
        }
        public void processCommandQueue() //processes the command queue for testing
        {
            while(commandQueue.Count > 0)
            {
                Command command = commandQueue.Dequeue();
                player.outputMessage(">" + command);
                command.execute(player);
            }
        }

        public void GameOver(Notification notification) //breaks playing loop
        {
            playing = false;
        }
        

    }
}
