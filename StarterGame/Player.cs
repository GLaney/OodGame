using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace StarterGame
{
    public class Player : ICharacter
    {
        private string _name;
        private CharType _charType;
        private int _maxLife;
        private int _currentLife;
        private int _baseDamage;
        private int _gold;
        private float _maxWeight;
        private float _currentWeight;
        private Room _currentRoom = null;
        private Room _previousRoom = null;
        private IItemContainer inventory;
        private IItemContainer equippedItems; 

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
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }
        public Room previousRoom
        {
            set
            {
                _previousRoom = value;
            }
            get
            {
                return _previousRoom;
            }
        }

        public Player(string name, int maxHP, int damage, Room room)//constructor
        {
            _charType = CharType.PlayerChar;
            _maxLife = maxHP;
            _currentLife = maxHP;
            _name = name;
            _gold = 100;
            _baseDamage = damage;
            _currentRoom = room;
            inventory = new ItemContainer();
            equippedItems = new ItemContainer();
            _maxWeight = 100;
            _currentWeight = 0;
        }

        public void give(IItem item)//adds item to players inventory if withing weight limit
        {

            if (item != null)
            {
                if ((_currentWeight+item.Weight) <= _maxWeight)
                {
                inventory.put(item, item.Name);
                _currentWeight += item.Weight;
                }
                else
                {
                    warningMessage("You cannot carry any more.");
                }
            }
            else
            {
                warningMessage("No such item.");
            }
            
        }

        public void take(string itemName)//moves item from player inventory to some other container
        {
            
                IItem item = inventory.findItem(itemName);
            if(item != null)
            { 
                float weight = item.Weight;
                _currentWeight -= weight;
                currentRoom.drop(inventory.remove(itemName));
            }
            else
            {
                warningMessage("Item not found.");
            }
        }

        public void showInventory()
        {
            informationMessage("\nInventory:        Weight: " + _currentWeight + "/" + _maxWeight + "\n       NAME       TYPE            " +

                    "WEIGHT          PRICE    QUANTITY      DAMAGE/ARMOR\n" + inventory.contents());
        }

        public void walkTo(string direction)// move from room to room
        {
            Room nextRoom = this._currentRoom.getExit(direction);
            if (nextRoom != null)
            {
                Dictionary<string, Object> userInfo = new Dictionary<string, object>();
                userInfo["room"] = nextRoom;
                //Player will move to next room
                NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterRoom", this, userInfo));
                this._previousRoom = _currentRoom;
                this._currentRoom = nextRoom;
                //Player did move to next room
                NotificationCenter.Instance.postNotification(new Notification("PlayerDidEnterRoom", this, userInfo));

                if (!_currentRoom.tag.Equals("victoryRoom"))
                {
                    this.informationMessage("\n" + this._currentRoom.description());
                }
            }
            else
            {
                this.warningMessage("\nThere is no door to the " + direction + ".");
            }
        }
        
        public void say(string word)// allows player character to say something.
        {
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillSayWord", this, userInfo));//this will be removed/changed later

            informationMessage("\n>>> " + word + "\n");
        }

        public void start(string state)//used to change game from state to state by placing the new state on top of the state stack.
        {
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();

            if(state.Equals("battle"))
            {
                userInfo["state"] = new GameStateBattle();
                informationMessage("Health: " + CurrentLife + "/" + MaxLife);
                foreach(KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())
                {
                    
                    informationMessage(entry.Value.Name + " Health: " + entry.Value.CurrentLife + "/" + entry.Value.MaxLife);
                }
            }
            else if(state.Equals("inventory"))
            {
                userInfo["state"] = new GameStateInventory();
                showInventory();
            }
            else if(state.Equals("shop"))
            {
                userInfo["state"] = new GameStateShop();
                

            }
            else if(state.Equals("dead"))
            {
                userInfo["state"] = new GameStateDead();
            }
            else if (state.Equals("win"))
            {
                userInfo["state"] = new GameStateWin();
            }
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
        }

        public void exit()//removes state from the stack putting player back into normal state
        {


            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new GameStateNormal();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
            this.informationMessage("\n" + this._currentRoom.description());
        }

        public void outputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void coloredMessage(string message, ConsoleColor color)//allows to change message collor.
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            outputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void debugMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Red);
        }

        public void warningMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Yellow);
        }

        public void informationMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Green);
        }

        public int Attack(ICharacter target) // handles all of combat
        {
            
            bool victory = true;// this will be set to false later if an enemy still lives.
            
            if (target != null)
            {
                Weapon weapon = (Weapon)equippedItems.findItem("weapon");
                int damageCalc = _baseDamage;
                if (weapon != null) // if a weapon is equipped usses weapon damage rather than player base damage.
                {
                    damageCalc = weapon.Damage;
                }
                informationMessage("The " + target.Name + " took " + target.TakeDamage(damageCalc) + " damage.");//send damage info to enemy and output damage number.

                foreach (KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())// player receives damage numbers from enemies, applies defences, and takes the damage.
                {
                    if (entry.Value.CurrentLife > 0)// executes for each enemy still alive.
                    {
                        victory = false;
                        
                        informationMessage("You took " + entry.Value.Attack(this) + " damage from the " + entry.Value.Name + ".");// output damage received.
                    }
                   
                    if (CurrentLife <= 0)// determine if player is dead.
                    {
                        dead(entry.Value.Name);// output player death info and end game.
                    }
                }
            }
            informationMessage("\nHealth: " + CurrentLife + "/" + MaxLife);//output player and enemy health info.
            foreach (KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())
            {
                informationMessage(entry.Value.Name + " Health: " + entry.Value.CurrentLife + "/" + entry.Value.MaxLife);
            }
            if (victory)
            {
                foreach (KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())
                {
                    if(entry.Key.Equals("boss"))//if player killed a mini boss this code notification to remove one of the boss door locks
                    {
                        NotificationCenter.Instance.postNotification(new Notification("PlayerKilledMiniBoss", this));
                    }
                }
                informationMessage("\nYou won the fight!!!");
                currentRoom.removeEnemies();
                exit();
            }
            return 0;
        }

        public int TakeDamage(int damage)// applies defences to number received from enemy and reduces playe health.
        {
            int reduction = 0;

            Armor armor = (Armor)equippedItems.findItem("armor");
            if(armor != null)
            {
                reduction = armor.ArmorValue;
            }
            int finalDmg = damage - reduction;//Calculate damage reduction
            _currentLife -= finalDmg;
            return finalDmg;
        }

        public void dead(string enemy)//output player death info and ends game.
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            outputMessage("You were killed by a " + enemy);
            start("dead");
            NotificationCenter.Instance.postNotification(new Notification("GameOver", this));
        }
        public void win()// output victory message and end game.
        {
            informationMessage("\nAs you walk in the room you see her lying in the corner of the room. When she sees you she " +
                "immediately \nruns, jumps into you arms, and begins to rub her furry face against yours.");
            ConsoleColor bColor = Console.BackgroundColor;
            ConsoleColor fColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            outputMessage("\nCongratulations, you have fought your way through the well to save your precious little kitty!!!");
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
            NotificationCenter.Instance.postNotification(new Notification("GameOver", this));
        }

        public void equip(string name)// equip item by setting equipment dictionary slot equal to an item in inventory. uses dictionary key to limit to one of each equipment type.
        {
            unequip(name);
            IItem item = inventory.findItem(name);
            
            if(item != null)
            {

                if (item.ItemType.Equals("weapon"))
                {
                    equippedItems.put(item, "weapon");
                }
                else if (item.ItemType.Equals("armor"))
                {
                    equippedItems.put(item, "armor");
                }
            }
            else
            {
                warningMessage("Item does not exist");
            }
            
            informationMessage("Equipped:\n" + equippedItems.contents());
        }

        public void unequip(string name)//unequip item by setting equipped value to null.
        {
            IItem item = inventory.findItem(name);

            if (item != null)
            {
                if (item.ItemType.Equals("weapon"))
                {
                    equippedItems.deleteItem("weapon");
                }
                else if (item.ItemType.Equals("armor"))
                {
                    equippedItems.deleteItem("armor");
                }
            }
            else
            {
                warningMessage("Item not equipped.");
            }
            informationMessage("Equipped:\n" + equippedItems.contents());
        }

        public void useItem(string name)//runs the items use function and removes item from inventory if it is a usable item.
        {
            Item item = null;
            if (inventory.findItem(name) is Item)
            {
                item = (Item)inventory.findItem(name);
                item.useItem(this);
                inventory.remove(name);
            }
            else
            {
                warningMessage(name + " cannot be used.");
            }
        }

        public void sellItem(string name)//remove item from inventory and add gold.
        {
            IItem item = inventory.findItem(name);
            if(item != null)
            {
                int value = item.Value;
                inventory.remove(name);
                _gold += value;
            }
            else
            {
                warningMessage("Did not find item.");

            }
            informationMessage("Gold remaining: " + _gold);
        }
        public void buyItem(string name)//remove gold, remove item from shop inventory, and add to player inventory.
        {

            ShopKeep shopkeep = (ShopKeep)currentRoom.getEnemy("shopkeep");
            int price = shopkeep.getPrice(name);

            if(price != 0)
            { 
                if (price <= _gold)
                {
                    _gold -= price;
                    IItem item = shopkeep.sellItem(name);
                    inventory.put(item, item.Name);
                    item.Value = (int)Math.Round(item.Value / 1.2);
                }
                else
                {
                    warningMessage("Cannot afford this item.");

                }
            }
            else
            {
                warningMessage("Item not found.");

            }

            informationMessage("Gold remaining: " + _gold);
            
        }
        public void getTargets() //outputs a list of available attack targets.
        {
            string message = "Enemies: ";
            foreach (KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())
            {
                message = message + entry.Key;
            }
            informationMessage(message);
        }
        public void run()
        {
            foreach (KeyValuePair<string, ICharacter> entry in currentRoom.getEnemies())
            {
                entry.Value.CurrentLife = entry.Value.MaxLife;
            }
            currentRoom = previousRoom;
            exit();
        }
    }

}
