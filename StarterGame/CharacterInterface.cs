using System;
namespace StarterGame
{
	public enum CharType { PlayerChar, EnemyChar, NPChar }


	public interface ICharacter// character interface for player, enemies and various NPCs
	{


		string Name { set; get; }
		int MaxLife { set; get; }
		int CurrentLife { set; get; }
		CharType CharType { set; get; }
		int BaseDamage { get; } // Even non combat NPCs have combat stats in case I want an event in which they fight. (ex. if the player steals from shop.)
		Room currentRoom { get; set; }

		int Attack(ICharacter target);
		int TakeDamage(int value);
		
	}
	
}