using System;

namespace StarterGame
{
	public class SayCommand : Command
	{
		public SayCommand() : base()
		{
            this.name = "say";
		}
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.say(this.secondWord);
            }
            else
            {
                player.warningMessage("\nSay What?");
            }
            return false;
        }
    }
}
