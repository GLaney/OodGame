using System;
namespace StarterGame
{
    public class UseCommand : Command
    {
        public UseCommand()//Command to use an item
        {
            this.name = "use";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.useItem(this.secondWord);
            }
            else
            {
                player.warningMessage("\nUse What?");
            }
            return false;
        }
    }
}