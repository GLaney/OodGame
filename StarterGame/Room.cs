using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

namespace StarterGame
{

    public interface IRoomDelegate
    {
        Room Container { get; set; }// container to hold items within the room.
        Room getExit(string exitNam, Dictionary<string, Room> exits);// gets the room which is connected in the specified direction.
        string getExits();// return all the rooms exits.
    }

    public class LockedRoom : IRoomDelegate //create delegate to lock boss room.
    {
        public string direction;
        public int locks { set; get; }// specify number of locks.
        public Room Container { get; set; }

        public LockedRoom(string exitDirection, int numLocks)
        {
            direction = exitDirection;
            locks = numLocks;
            NotificationCenter.Instance.addObserver("PlayerKilledMiniBoss", PlayerKilledMiniBoss);
        }

        public Room getExit(string exitName, Dictionary<string, Room> exits)
        {
            if(exitName.Equals(direction))
            {
                return Container;
            }
            else
            {
                Room room = null;
                exits.TryGetValue(exitName, out room);
                return room;
            }

            
        }
        public string getExits()
        {
            return "";
        }

        public void PlayerKilledMiniBoss(Notification notification)//removes a lock when a mini boss is killed.
        {
            Player player = (Player)notification.Object;
            locks -= 1;
            if(locks <1)
            {
                Container.Delegate = null;
                player.informationMessage("You hear the sound of stone grinding against stone. It sounds like the sound came from a few rooms behind you.");

            }
        }
    }

    public class TrapRoom : IRoomDelegate //create delegate to trap player in a room
    {

        public string MagicWord { set; get; }
        public Room Container { get; set; }

        public TrapRoom()
        {
            MagicWord = "dagger";
            NotificationCenter.Instance.addObserver("PlayerWillSayWord", PlayerWillSayWord);
        }

        public Room getExit(string exitName, Dictionary<string, Room> exits)
        {
            return Container;
        }
        public string getExits()
        {
            return "\nYou cannot escape until you correctly answer this question:\nWhat was the first weapon you found in this well?";
        }

        public void PlayerWillSayWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if(player.currentRoom == Container)
            {
                string word = (string)notification.userInfo["word"];
                if(word != null)
                {
                    if(word.Equals(MagicWord))
                    {
                        Container.Delegate = null;
                        player.informationMessage("Correct! You may leave.");
                    }
                }
            }
        }
    }

    public class Room
    {
        
        private Dictionary<string, Room> exits;  //each room has a dictionary to store the exits.
        private string _tag;
        private string _roomDescription;
        private IItemContainer itemContainer;
        private Dictionary<string, ICharacter> enemies;
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        private IRoomDelegate _delegate;
        public IRoomDelegate Delegate
        {
            get
            {
                return _delegate;
            }
            set
            {
                _delegate = value;
            }
        }

        public string roomDescription
        {
            get
            {
                return _roomDescription;
            }
            set
            {
                _roomDescription = value;
            }
        }

        public Room() : this("No Tag")        //constructor
        {

        }

        public Room(string tag)           //designated constructor
        {
            exits = new Dictionary<string, Room>();
            this.tag = tag;
            _delegate = null;
            itemContainer = new ItemContainer(_tag + " container");
            enemies = new Dictionary<string, ICharacter>();
        }

        public void drop(IItem item) //put item in rooms item container
        {
            itemContainer.put(item, item.Name);
        }

        public IItem pickup(string itemName)
        {
            return itemContainer.remove(itemName);
        } //removes item from container and returns it

        public void setExit(string exitName, Room room)  //adds another room to the list of exits
        {
            exits[exitName] = room;
        }

        public string getItems()
        {
            return "Items:\n" + itemContainer.contents();
        }

        public Room getExit(string exitName)                     //attempts to get an exit the given name. returns null if it doesn't exist.
        {
            if(_delegate != null)
            {
                return _delegate.getExit(exitName, exits);
            }
            else
            {
                Room room = null;
                exits.TryGetValue(exitName, out room);
                return room;
            }
        }

        public string getExits()         //lists the available exits for the room
        {
            if(_delegate is TrapRoom)
            {
                return _delegate.getExits();
            }
            else
            {
                string exitNames = "\nExits:";
                Dictionary<string, Room>.KeyCollection keys = exits.Keys;
                foreach (string exitName in keys)
                {
                    exitNames += "\n      " + exitName;
                }

                return exitNames;
            }
            
        }

        public string description()     //each room has a room description which is printed to the user upon entering.
        {
            return _roomDescription + "\n " + this.getExits() + "\n" + this.getItems();
        }
        public void addEnemy(Enemy enemy)          //adds another room to the list of exits
        {
            enemies[enemy.Name] = enemy;
        }
        public void addShop(ShopKeep shopKeep )//adds a shop to the room.
        {
            enemies[shopKeep.Name] = shopKeep;

        }
        public ICharacter getEnemy(string name)//returns enemy given enemy name
        {
            ICharacter enemy = null;
            this.enemies.TryGetValue(name, out enemy);
            return enemy;
        }
        public Dictionary<string, ICharacter> getEnemies()//returns a string of enemies in the room.
        {
            return enemies;
        }
        public void removeEnemies()// removes enemies from enemies dictionary to avoid repeating combat with dead enemies.
        {
            foreach (KeyValuePair<string, ICharacter> enemy in enemies)
            {
                enemy.Value.currentRoom = null;
            }
            enemies.Clear();
        }
    }
}
