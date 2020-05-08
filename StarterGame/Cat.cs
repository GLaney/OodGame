using System;
using System.Collections.Generic;

namespace StarterGame
{
	public class Cat : ICharacter //Cat NPC is the goal of the game. Trigger victory condition when player enters same room as Cat.
	{
		private string _name;
		private int _maxLife;
		private int _currentLife;
		private int _baseDamage;
		private Room _currentRoom;
		private CharType _charType;


		public string Name { get; set; }
		public int MaxLife { get; set; }
		public int CurrentLife { get; set; }
		public int BaseDamage { get; }
		public Room currentRoom { get; set; }
		public CharType CharType { get; set; }

		public Cat(string name, Room room)//Triggers win con if player in same room as cat.
		{
			_name = name;
			_currentRoom = room;
			_maxLife = 1;
			_currentLife = _maxLife;
			_baseDamage = 0;
			_charType = CharType.NPChar;
			NotificationCenter.Instance.addObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
		}
		public void PlayerDidEnterRoom(Notification notification)
		{
			Player player = (Player)notification.Object;
			if (_currentRoom == player.currentRoom)
			{
				
				player.win();
			}

		}

		public int Attack(ICharacter target)
		{
			int totalDmg = _baseDamage;//Calculate outgoing damage
			return target.TakeDamage(totalDmg);
		}
		public int TakeDamage(int damage)
		{
			int reduction = 0;
			int finalDmg = damage - reduction;//Calculate damage reduction
			_currentLife -= finalDmg;
			return finalDmg;
		}
	}
}
