using System;
using System.Collections.Generic;

namespace StarterGame
{
	public interface IItemContainer : IItem
    {
		void put(IItem item, string key);
		IItem remove(string itemName);
		string contents();
		IItem findItem(string itemName);
		void deleteItem(string itemName);
    }

	public class ItemContainer : IItemContainer
	{
		private string _name;
		private string _itemType;
		private int _value;
		private float _weight;
		private int _quantity;
		

		private Dictionary<string, IItem> items;
		public string Name { get {return _name; } }
		public float Weight { get { return _weight; } }
		public int Value { get { return _value; } set { _value = value; } }
		public string ItemType { get { return _itemType; } }
		public int Quantity { get { return _quantity; } set { _quantity = value; } }

		public ItemContainer() : this("NAME MISSING")
		{
		}
		public ItemContainer(string name) : this(name, "Sack")
		{
		}
		public ItemContainer(string name, string itemType) : this(name, itemType, 1)
		{
		}
		public ItemContainer(string name, string itemType, int value) : this( name,  itemType,  value, 0.5f)
		{
		}
		//designated constructor
		public ItemContainer(string name, string itemType, int value, float weight)
		{
			_name = name;
			_itemType = itemType;
			_value = value;
			_weight = weight;
			items = new Dictionary<string, IItem>();
		}

		


		public void put(IItem item, string key)
		{

			if (items.ContainsKey(key))
			{
				items[key].Quantity += 1;
			}
			else
			{
				try
				{
					items[key] = item;
				}
				catch (ArgumentNullException e)
				{
					ConsoleColor oldColor = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(e);
					Console.ForegroundColor = oldColor;

				}
			}
        }

		public IItem remove(string itemName)
		{

			
			IItem item = null;
			items.TryGetValue(itemName, out item);
			if (item != null)
			{
				if(item.Quantity == 1)
				{
					items.Remove(itemName, out item);
				}
				else
				{
					item.Quantity -= 1;
				}
				
			}
			return item;
		}

		public string contents()
        {
			string itemNames = "      ";
			Dictionary<string, IItem>.KeyCollection keys = items.Keys;
			foreach (string itemName in keys)
            {
				itemNames += items[itemName] + "\n      "; 
            }
			return itemNames;
        }

		public IItem findItem(string itemName)
		{
			IItem item = null;
			items.TryGetValue(itemName, out item);
			
			return item;
		}

		public void deleteItem(string itemName)
		{
			items.Remove(itemName);
		}
		
	}
}