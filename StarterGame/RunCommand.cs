using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class RunCommand : Command
    {
        public RunCommand()//Command to run from battle.
        {
            this.name = "run";
        }
        override
        public bool execute(Player player)
        {
            player.run();
            return false;
        }
    }
}