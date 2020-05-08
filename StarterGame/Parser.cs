using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{

    public enum ParserState { Normal, Battle, Shop, Inventory, Dead, Win}
    public interface IParserState //seperates player input into command and secondword.
    {
        ParserState State { get; }
        void Enter(Parser parser);
        void Exit(Parser parser);
    }
    // The following classes represent the possible game states and change the commands available in each state.
    public class GameStateNormal : IParserState
    {
        public ParserState State { get { return ParserState.Normal; } }

        public GameStateNormal()
        {

        }
        public void Enter(Parser parser)
        {

        }
        public void Exit(Parser parser)
        {

        }
    }
    public class GameStateBattle : IParserState
    {
        public ParserState State { get { return ParserState.Battle; } }

        public GameStateBattle()
        {

        }
        public void Enter(Parser parser)//changes available commands
        {
            Command[] commandArray = { new QuitCommand(), new HelpCommand(), new AttackCommand(), new RunCommand(), new TargetsCommand() };
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }
    public class GameStateInventory : IParserState
    {
        public ParserState State { get { return ParserState.Inventory; } }

        public GameStateInventory()
        {

        }
        public void Enter(Parser parser)
        {
            Command[] commandArray = { new QuitCommand(), new EquipCommand(), new UnequipCommand(), new HelpCommand(), new UseCommand(), new CloseCommand(), new DropCommand() };
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }
    public class GameStateShop : IParserState
    {
        public ParserState State { get { return ParserState.Shop; } }

        public GameStateShop()
        {

        }
        public void Enter(Parser parser)
        {
            Command[] commandArray = { new QuitCommand(), new HelpCommand(), new SellCommand(), new BuyCommand(), new ExitCommand() };
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }
    public class GameStateDead : IParserState
    {
        public ParserState State { get { return ParserState.Dead; } }

        public GameStateDead()
        {

        }
        public void Enter(Parser parser)
        {
            Command[] commandArray = { };
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }
    public class GameStateWin : IParserState
    {
        public ParserState State { get { return ParserState.Win; } }

        public GameStateWin()
        {

        }
        public void Enter(Parser parser)
        {
            Command[] commandArray = { };
            parser.Push(new CommandWords(commandArray));
        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    public class Parser
    {
        private Stack<CommandWords> commands;
        private IParserState _state;
        public IParserState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state.Exit(this);
                _state = value;
                _state.Enter(this);
            }
        }

        public Parser() : this(new CommandWords())
        {

        }

        public Parser(CommandWords newCommands)
        {
            commands = new Stack<CommandWords>();
            _state = new GameStateNormal();
            Push(newCommands);
            NotificationCenter.Instance.addObserver("PlayerWillEnterState", PlayerWillEnterState);
            
        }

        public void PlayerWillEnterState(Notification notification)
        {
            Player player = (Player)notification.Object;
            Dictionary<string, Object> userInfo = notification.userInfo;
            IParserState state = (IParserState)userInfo["state"];
            State = state;
        }

        public void Push(CommandWords newCommands)
        {
            commands.Push(newCommands);
        }

        public void Pop()
        {
            commands.Pop();
        }

        public Command parseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = commands.Peek().get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.secondWord = words[1];
                    }
                    else
                    {
                        command.secondWord = null;
                    }
                }
                else
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                    Console.ForegroundColor = oldColor;
                }
            }
            else
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No words parsed!");
                Console.ForegroundColor = oldColor;
            }
            return command;
        }

        public string description()
        {
            return commands.Peek().description();
        }
    }
}
