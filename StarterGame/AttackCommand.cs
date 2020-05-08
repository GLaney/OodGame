using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StarterGame
{
    public class AttackCommand : Command //command to attack an enemy.
    {
        public AttackCommand()
        {
            this.name = "attack";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                Enemy target = (Enemy)player.currentRoom.getEnemy(secondWord);
                if (target != null)
                {
                    player.Attack(target);

                }
                else
                {
                    player.warningMessage("Invalid target.");
                }

            }
            else
            {
                player.warningMessage("\nAttack What?");
            }
            return false;
        }
    }
}