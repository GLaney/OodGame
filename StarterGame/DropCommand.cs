namespace StarterGame
{
    public class DropCommand : Command
    {

        public DropCommand() : base()
        {
            this.name = "drop";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.take(this.secondWord);
            }
            else
            {
                player.warningMessage("\nDrop What?");
            }
            return false;
        }
    }
}