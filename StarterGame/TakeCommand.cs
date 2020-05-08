using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class TakeCommand : Command
    {

        public TakeCommand() : base()
        {
            this.name = "take";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.give(player.currentRoom.pickup(secondWord));
            }
            else
            {
                player.warningMessage("\nTake What?");
            }
            return false;
        }
    }
}