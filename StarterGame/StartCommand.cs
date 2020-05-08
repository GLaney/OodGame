using System;
namespace StarterGame
{
	public class StartCommand : Command
	{
		public StartCommand()
		{
            this.name = "start";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.start(this.secondWord);
            }
            else
            {
                player.warningMessage("\nStart What?");
            }
            return false;
        }
    }
}
