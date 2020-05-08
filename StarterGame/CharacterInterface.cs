using System;
namespace StarterGame
{
	public enum CharType { PlayerChar, EnemyChar, NPChar }


	public interface ICharacter
	{


		string Name { set; get; }
		int MaxLife { set; get; }
		int CurrentLife { set; get; }
		CharType CharType { set; get; }
		int BaseDamage { get; }
		Room currentRoom { get; set; }

		int Attack(ICharacter target);
		int TakeDamage(int value);
		
	}
	
}