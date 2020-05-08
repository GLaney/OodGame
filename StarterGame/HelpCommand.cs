using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class HelpCommand : Command // command to list all currently available commands for the player.
    {
        CommandWords words;

        public HelpCommand() : this(new CommandWords())
        {
        }

        public HelpCommand(CommandWords commands) : base()
        {
            words = commands;
            this.name = "help";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.outputMessage("\nI cannot help you with " + this.secondWord); // lets the player no that it cant give information specific to their secondword.
            }
            else
            {
                player.outputMessage("\n\nYour available commands are: " + words.description());
            }
            return false;
        }
    }
}
