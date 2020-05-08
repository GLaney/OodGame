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
            foreach(KeyValuePair<string, ICharacter> entry in player.currentRoom.getEnemies())
            {
                entry.Value.CurrentLife = entry.Value.MaxLife;
            }
            player.currentRoom = player.previousRoom;
            player.exit();
            return false;
        }
    }
}