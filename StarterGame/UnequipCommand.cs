using System;
namespace StarterGame
{
    public class UnequipCommand : Command
    {
        public UnequipCommand()
        {
            this.name = "unequip";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {

                player.unequip(this.secondWord);
            }
            else
            {
                player.warningMessage("\nUnequip What?");
            }
            return false;
        }
    }
}