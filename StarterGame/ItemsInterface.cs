using System;

namespace StarterGame
{
    public interface IItem
    {
        string Name { get; }
        float Weight { get; }
        int Value { get; set; }
        string ItemType { get; }
        int Quantity { get; set; }
        string ToString(); //all iitem tostrings are almost identical. each has small difference base on item types
    }

    public class Armor : IItem
    {
        private float _weight;
        private int _value;
        private string _itemType;
        private int _armorValue;
        private string _name;
        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public float Weight
        {
            get
            {
                return _weight;
            }
        }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public string ItemType
        {
            get
            {
                return _itemType;
            }
        }
        public int ArmorValue
        {
            get
            {
                return _armorValue;
            }
        }
        
        //constructor
        
        public Armor(string name)
        {
            _itemType = "armor";
            _quantity = 1;

            switch (name)
            {
                case "cloak":
                    _armorValue = 3;
                    _weight = 2.0f;
                    _value = 15;
                    _name = name;
                    break;

                case "tunic":
                    _armorValue = 5;
                    _weight = 4.0f;
                    _value = 25;
                    _name = name;
                    break;

                case "leather":
                    _armorValue = 8;
                    _weight = 6.0f;
                    _value = 45;
                    _name = name;
                    break;

                case "chain":
                    _armorValue = 12;
                    _weight = 9.0f;
                    _value = 75;
                    _name = name;
                    break;

                case "scale":
                    _armorValue = 16;
                    _weight = 12.0f;
                    _value = 100;
                    _name = name;
                    break;

                case "plate":
                    _armorValue = 20;
                    _weight = 15.0f;
                    _value = 120;
                    _name = name;
                    break;
            }
            
        }

        override
            public string ToString()//outputs item information with spacing to line up in menus. includes armor values.
        {
            string space1 = "";
            string space2 = "";
            string space3 = "";
            string space4 = "";
            string space5 = "";

            switch(_name)
            {
                case "cloak":
                    space1 = "      ";
                    space2 = "               ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "               ";
                    break;

                case "tunic":
                    space1 = "      ";
                    space2 = "               ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "               ";
                    break;

                case "leather":
                    space1 = "    ";
                    space2 = "               ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "               ";
                    break;

                case "chain":
                    space1 = "      ";
                    space2 = "               ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "              ";
                    break;

                case "scale":
                    space1 = "      ";
                    space2 = "              ";
                    space3 = "            ";
                    space4 = "         ";
                    space5 = "              ";
                    break;

                case "plate":
                    space1 = "      ";
                    space2 = "              ";
                    space3 = "            ";
                    space4 = "         ";
                    space5 = "              ";
                    break;
            }

            return this._name + space1 + this._itemType + space2 + this._weight + space3
                + this._value + "G" + space4 + this.Quantity + space5 + this.ArmorValue;
        } 
    }

    public class Weapon : IItem
    {
        private float _weight;
        private int _value;
        private string _itemType;
        private int _damage;
        private string _name;
        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public float Weight
        {
            get
            {
                return _weight;
            }
        }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public string ItemType
        {
            get
            {
                return _itemType;
            }
        }
        public int Damage
        {
            get
            {
                return _damage;
            }
        }
        
        //constructor
        
        public Weapon(string name)
        {
            _itemType = "weapon";
            _quantity = 1;

            switch(name)
            {
                case "dagger":
                    _damage = 4;
                    _weight = 2.0f;
                    _value = 15;
                    _name = name;
                    break;

                case "club":
                    _damage = 7;
                    _weight = 8.0f;
                    _value = 25;
                    _name = name;
                    break;

                case "staff":
                    _damage = 10;
                    _weight = 6.0f;
                    _value = 40;
                    _name = name;
                    break;

                case "mace":
                    _damage = 14;
                    _weight = 12.0f;
                    _value = 60;
                    _name = name;
                    break;

                case "sword":
                    _damage = 19;
                    _weight = 9.0f;
                    _value = 85;
                    _name = name;
                    break;

                case "axe":
                    _damage = 25;
                    _weight = 15.0f;
                    _value = 100;
                    _name = name;
                    break;
            }

        }
        override
            public string ToString()//outputs item information with spacing to line up in menus. includes damage values.
        {
            string space1 = "";
            string space2 = "";
            string space3 = "";
            string space4 = "";
            string space5 = "";

            switch (_name)
            {
                case "dagger":
                    space1 = "     ";
                    space2 = "              ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "               ";
                    break;

                case "club":
                    space1 = "       ";
                    space2 = "              ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "               ";
                    break;

                case "staff":
                    space1 = "      ";
                    space2 = "              ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "              ";
                    break;

                case "mace":
                    space1 = "       ";
                    space2 = "             ";
                    space3 = "             ";
                    space4 = "         ";
                    space5 = "              ";
                    break;

                case "sword":
                    space1 = "      ";
                    space2 = "              ";
                    space3 = "            ";
                    space4 = "         ";
                    space5 = "              ";
                    break;

                case "axe":
                    space1 = "        ";
                    space2 = "             ";
                    space3 = "            ";
                    space4 = "         ";
                    space5 = "              ";
                    break;
            }

            return this._name + space1 + this._itemType + space2 + this._weight + space3
                + this._value + "G" + space4 + this.Quantity + space5 + this.Damage;
        }

    }

    public class Item : IItem
    {
        private string _name;
        private float _weight;
        private int _value;
        private string _itemType; 
        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
        public float Weight
        {
            get
            {
                return _weight;
            }
        }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public string ItemType
        {
            get
            {
                return _itemType;
            }
        }

        public Item(string name)
        {
            _quantity = 1;
            switch (name)
            {


                case "potion":
                    _itemType = name;
                    _weight = 1.0f;
                    _value = 10;
                    _name = name;
                    break;

                case "bomb":
                    _itemType = name;
                    _weight = 10.0f;
                    _value = 300;
                    _name = name;
                    break;

            }
        }


        public void useItem(Player player)
        {
            if(Name.Equals("potion"))
            {
                int heal = 40;
                int lifeMissing = player.MaxLife - player.CurrentLife;
                if (lifeMissing <= 0)
                {
                    heal = lifeMissing;

                }

                player.CurrentLife += heal;
                player.informationMessage("You regenerate " + heal + " life.\nHealth: " + player.CurrentLife + "/" + player.MaxLife);

            }
            else if(Name.Equals("bomb"))
            {
                if (player.currentRoom.tag.Equals("returnPassage"))
                {
                    NotificationCenter.Instance.postNotification(new Notification("PlayerUsedBomb", player));
                }
            }

        }

        public void usePotion(Player player)
        {
            player.CurrentLife += 40;
        }

        public void useBomb(Player player)
        {
            if(player.currentRoom.tag.Equals("returnPassage"))
            {
                NotificationCenter.Instance.postNotification(new Notification ("PlayerUsedBomb", player));
            } 
            else
            {
                player.warningMessage("You cannot use that here.");
            }
            
        }

        override
            public string ToString()//outputs item information with spacing to line up in menus. has no damage or armor value.
        {
            string space1 = "";
            string space2 = "";
            string space3 = "";
            string space4 = "";

            switch (_name)
            {
                case "potion":
                    space1 = "     ";
                    space2 = "              ";
                    space3 = "             ";
                    space4 = "         ";
                    break;

                case "bomb":
                    space1 = "       ";
                    space2 = "               ";
                    space3 = "            ";
                    space4 = "         ";
                    break;

            }

            return this._name + space1 + this._itemType + space2 + this._weight + space3
                + this._value + "G" + space4 + this.Quantity;
        }

    }
}