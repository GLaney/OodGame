﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public abstract class Command// uses parser to seperate user input into a command and a second word to be used by the command if needed.
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        public string secondWord { get { return _secondWord; } set { _secondWord = value; } }

        public Command()
        {
            this.name = "";
            this.secondWord = null;
        }

        public bool hasSecondWord()
        {
            return this.secondWord != null;
        }
        override
        public string ToString()
        {
            return name + (hasSecondWord() ? " " + secondWord : "");
        }

        public abstract bool execute(Player player);
    }
}
