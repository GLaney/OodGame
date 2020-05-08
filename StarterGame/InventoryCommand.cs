using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class InventoryCommand : Command
    {
        public InventoryCommand()//Command to open player inventory.
        {
            this.name = "inventory";
        }
        override
        public bool execute(Player player)
        {
            player.start("inventory");
            return false;
        }
    }
}