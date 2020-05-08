using System;
namespace StarterGame
{
    public class EquipCommand : Command
    {
        public EquipCommand()
        {
            this.name = "equip";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                
                player.equip(this.secondWord);
            }
            else
            {
                player.warningMessage("\nEquip What?");
            }
            return false;
        }
    }
}