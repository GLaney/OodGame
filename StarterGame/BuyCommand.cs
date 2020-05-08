using System;
namespace StarterGame
{
    public class BuyCommand : Command //command to purchase item
    {
        public BuyCommand()
        {
            this.name = "buy";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.buyItem(this.secondWord);
            }
            else
            {
                player.warningMessage("\nBuy What?");
            }
            return false;
        }
    }
}