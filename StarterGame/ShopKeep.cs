﻿using System;
using System.Collections.Generic;

namespace StarterGame
{
	public class ShopKeep : ICharacter
	{
		private string _name;
		private int _maxLife;
		private int _currentLife;
		private int _gold;
		private CharType _charType;
		private int _baseDamage;
		private Room _currentRoom;
		private IItemContainer shopInventory;

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



		public ShopKeep(string name, int maxHP, int damage, Room room)//designated constructor
		{
			_charType = CharType.EnemyChar;
			_maxLife = maxHP;
			_currentLife = maxHP;
			_name = name;
			_gold = 1000;
			_baseDamage = damage;
			_currentRoom = room;
			shopInventory = new ItemContainer();
			NotificationCenter.Instance.addObserver("PlayerDidEnterRoom", PlayerDidEnterRoom);
		}

		public void PlayerDidEnterRoom(Notification notification)
		{
			Player player = (Player)notification.Object;
			if (this.currentRoom == player.currentRoom)
			{
				player.start("shop");
				player.informationMessage("Shopkeep: Welcome to my shop good sir. Have a look at my wares, maybe something" +
					"\nwill cath your eye. I buy and sell all manner of things.\n\n");
				player.informationMessage("\nShop Inventory:        \n   NAME       TYPE            " +
					"WEIGHT          VALUE     Damage/Armor\n" + shopInventory.contents());

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
		public void addToShop(IItem item)
		{
			shopInventory.put(item, item.Name);
		}
		public void markUp(IItem item)
		{
			item.Value = (int)Math.Round(item.Value * 1.2);
		}
		public void bulkMarkUp()
		{
			string[] itemTypes = new string[14] {"dagger", "club", "staff", "mace", "sword", "axe", "cloak", "tunic", 
				"leather", "chain", "scale", "plate", "potion", "bomb"};
			foreach(string type in itemTypes)
			{
				markUp(shopInventory.findItem(type));
			}
		}
		public IItem sellItem(string name)
		{
			return shopInventory.remove(name);
		}
		public int getPrice(string name)
		{
			int price;
			IItem item = shopInventory.findItem(name);
			if (item != null)
			{
				price = item.Value;
			}
			else
			{
				price = 0;
			}
			return price;
		}
	}
}