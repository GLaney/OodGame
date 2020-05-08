namespace StarterGame
{
    public class DropCommand : Command // command to drop item into room.
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