using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class QuitCommand : Command // command to quit playing the game.
    {

        public QuitCommand() : base()
        {
            this.name = "quit";
        }

        override
        public bool execute(Player player)
        {
            bool answer = true;
            if (this.hasSecondWord())
            {
                player.outputMessage("\nI cannot quit " + this.secondWord); // lets the player know you cannot specify what to quit.
                answer = false;
            }
            return answer;
        }
    }
}
