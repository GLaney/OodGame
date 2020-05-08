using System;
namespace StarterGame
{
	public class Enemy : ICharacter
	{
		private string _name;
		private int _maxLife;
		private int _currentLife;
		private CharType _charType;
		private int _baseDamage;
		private Room _currentRoom;

		
		public string Name
		{
			set
			{
				_name = value;
			}
			get
			{
				return _name;
			}
		}
		public int MaxLife
		{
			set
			{
				_maxLife = value;
			}
			get
			{
				return _maxLife;
			}
		}
		public int CurrentLife
		{
			set
			{
				_currentLife = value;
			}
			get
			{
				return _currentLife;
			}
		}

		public CharType CharType
		{
			set
			{
				_charType = value;
			}
			get
			{
				return _charType;
			}
		}
		public int BaseDamage
		{
			get
			{
				return _baseDamage;
			}
		}
		public Room currentRoom
		{
			set
			{
				_currentRoom = value;
			}
			get
			{
				return _currentRoom;
			}
		}


		
		public Enemy(string name, int maxHP, int damage, Room room)//designated constructor
		{
			_charType = CharType.EnemyChar;
			_maxLife = maxHP;
			_currentLife = maxHP;
			_name = name;
			_baseDamage = damage;
			_currentRoom = room;
			NotificationCenter.Instance.addObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
		}

		public void PlayerDidEnterRoom(Notification notification)//changes state and begins battle if player enters same room.
		{
			
			
		Player player = (Player)notification.Object;
			if (this.currentRoom == player.currentRoom)
			{
				ConsoleColor oldColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				player.outputMessage("A " + Name + " attacks you as you enter the room!!!\n");
				Console.ForegroundColor = oldColor;

				player.start("battle");
				
				
			}
			
			
		}

		public int Attack(ICharacter target)//sends damage to player
		{
			int totalDmg = _baseDamage ;//Calculate outgoing damage
			return target.TakeDamage(totalDmg);
		}
		public int TakeDamage(int damage)//receives damage from player and applies defenses if any.
		{
			int reduction = 0;
			int finalDmg = damage - reduction;//Calculate damage reduction
			_currentLife -= finalDmg;
			return finalDmg;
		}
	}
}