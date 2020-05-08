using System;
namespace StarterGame
{
    public class CloseCommand : Command
    {
        public CloseCommand()//Command to close Inventory.
        {
            this.name = "close";
        }
        override
        public bool execute(Player player)
        {
            player.exit();
            return false;
        }
    }
}