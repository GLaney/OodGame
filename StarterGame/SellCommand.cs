using System;
namespace StarterGame
{
    public class SellCommand : Command
    {
        public SellCommand()//Command to sell an item in the shop.
        {
            this.name = "sell";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.sellItem(this.secondWord);
            }
            else
            {
                player.warningMessage("\nSell What?");
            }
            return false;
        }
    }
}