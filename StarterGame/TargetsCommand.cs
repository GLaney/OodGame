using System;
using System.Collections.Generic;

namespace StarterGame
{
    public class TargetsCommand : Command
    {

        public TargetsCommand() : base()
        {
            this.name = "targets";
        }

        override
        public bool execute(Player player)
        {
            string message = "Enemies: ";
            foreach(KeyValuePair<string, ICharacter> entry in player.currentRoom.getEnemies())
            {
                message = message + entry.Key;
            }
            player.informationMessage(message);
            return false;
        }
    }
}
