using System;
namespace StarterGame
{
    public class ExitCommand : Command
    {
        public ExitCommand() //Command to exit shop.
        {
            this.name = "exit";
        }
        override
        public bool execute(Player player)
        {
            player.exit();
            return false;
        }
    }
}