using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class TargetsCommand : Command // command to get available attack targets.
    {

        public TargetsCommand() : base()
        {
            this.name = "targets";
        }

        override
        public bool execute(Player player)
        {
            player.getTargets();
            return false;
        }
    }
}
