﻿using System;

namespace StarterGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new Game();
            game.start();
            game.play();
            game.end();
        }
    }
}
