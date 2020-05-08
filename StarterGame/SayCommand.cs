using System;

namespace StarterGame
{
	public class SayCommand : Command // command to allow player character to say something.
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
