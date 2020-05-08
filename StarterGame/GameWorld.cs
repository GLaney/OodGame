using System;
namespace StarterGame
{ 
	public class GameWorld //creates the world and everything in it (except player)
	{
        private Room _entrance;
        private Room _trigger;
        private Room _fromRoom;
        private Room _toRoom;
        private Room _secret;
        
        public Room Entrance
        {
            get
            {
                return _entrance;
            }
        }

		public GameWorld()
		{
            createWorld();
            
            NotificationCenter.Instance.addObserver("PlayerWillEnterRoom", PlayerWillEnterRoom);
            NotificationCenter.Instance.addObserver("PlayerUsedBomb", PlayerUsedBomb);// observer for unlocking alternate victory path
        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            Room room = (Room)notification.userInfo["room"];
            if(room == _trigger)
            {
                _fromRoom.setExit("west",_toRoom);
                _toRoom.setExit("north", _fromRoom);
            }
        }

        public void createWorld()//creates the world and everything in it (except player)
        {
            //create rooms
            Room starterRoom = new Room("starterRoom");
            starterRoom.roomDescription = "You wake up in a damp room, dimly lit by two torches near a doorway to the east. You " +
                "notice the light \nfrom the flames reflecting off of a small dagger stuck in the ground at the foot of the door.";           

            Room room2 = new Room("room2");
            room2.roomDescription = "As you enter the room your eyes are immediately drawn to a statue in the center of the room. Though" +
                "a portion of the \nstatue is obscured by a dark cloak, which has been draped over it, you can see that it's a carving of\n" +
                "a mother knelt beside her child and pointing towards the door to the North.";            

            Room room3 = new Room("room3");
            room3.roomDescription = "The room is dark, lit only by a single torch in the far corner. You see a door to the north as well " +
                "as a lump of \ncloth laying on the ground a few feet away.";

            Room room4 = new Room("room4");
            room4.roomDescription = "While there is a doorway to the north, you quickly nothice a well lit door to the east. Above the " +
                "eastern door is a \nwooden carving of what looks like a coin.";

            Room room5 = new Room("room5");
            room5.roomDescription = "You immediately notice in the center of the room you see a small white table. The table is very dirty, " +
                "but is still a stark \ncontrast to the browns of the floor and walls. Upon the table there is a vial of some red liquid " +
                "and to the west you see the doorway to the \nnext room.";

            Room forkRoom = new Room("forkRoom");
            forkRoom.roomDescription = "This room has four exits, one in each direction. The western exit, however, was covered by a large " +
                "stone slab. You \ncould never move such a thing by yourself so you turn your attention to the doors to the north and south.";

            Room shop = new Room("shop");
            shop.roomDescription = "You cannot beleive what you're seeing. Inside this well you have stumbled upon what looks to be a " +
                "shop. Merchandise \nis displayed throuughout the room and as you move forward the shopkeep becomes visible. It's a mouse!?!?";

            Room leftRoom1 = new Room("leftRoom1");
            leftRoom1.roomDescription = "";

            Room leftRoom2 = new Room("leftRoom2");
            leftRoom2.roomDescription = "";            

            Room leftBossRoom = new Room("leftBossRoom");
            leftBossRoom.roomDescription = "";

            Room rightRoom1 = new Room("rightRoom1");
            rightRoom1.roomDescription = "";

            Room rightRoom2 = new Room("rightRoom2");
            rightRoom2.roomDescription = "The exit to the room to the west and some chain armor on an armor stand by the northern wall is " +
                "all you see \nin this room.";

            Room rightBossRoom = new Room("rightBossRoom");
            rightBossRoom.roomDescription = "";

            Room returnPassage = new Room("returnPassage");
            returnPassage.roomDescription = "returnPassage";
            returnPassage.roomDescription = "You two exits to the west and north, as well as a pile of large rocks to the east. It " +
                "looks like there may be \nanother doorway behing it. If only you had some way of removing the rocks.";

            Room finalBossRoom = new Room("finalBossRoom");
            finalBossRoom.roomDescription = "finalBossRoom";
            finalBossRoom.roomDescription = "";

            Room victoryRoom = new Room("victoryRoom");
            victoryRoom.roomDescription = "";

            //set room exits
            starterRoom.setExit("east", room2);

            room2.setExit("north", room3);
            room2.setExit("west", starterRoom);

            room3.setExit("north", room4);
            room3.setExit("south", room2);

            room4.setExit("north", room5);
            room4.setExit("east", shop);
            room4.setExit("south", room3);

            room5.setExit("south", room4);
            room5.setExit("west", forkRoom);

            forkRoom.setExit("north", rightRoom1);
            forkRoom.setExit("south", leftRoom1);
            forkRoom.setExit("east", room5);
            forkRoom.setExit("west", finalBossRoom);


            finalBossRoom.setExit("east", forkRoom);
            finalBossRoom.setExit("west", victoryRoom);

            victoryRoom.setExit("east", finalBossRoom);

            rightRoom1.setExit("south", forkRoom);
            rightRoom1.setExit("west", rightRoom2);

            rightRoom2.setExit("east", rightRoom1);
            rightRoom2.setExit("west", rightBossRoom);

            rightBossRoom.setExit("south", returnPassage);
            rightBossRoom.setExit("east", rightRoom2);

            leftRoom1.setExit("north", forkRoom);
            leftRoom1.setExit("west", leftRoom2);

            leftRoom2.setExit("east", leftRoom1);
            leftRoom2.setExit("west", leftBossRoom);

            leftBossRoom.setExit("east", leftRoom2);
            leftBossRoom.setExit("north", returnPassage);

            returnPassage.setExit("north", rightBossRoom);
            returnPassage.setExit("south", leftBossRoom);
            returnPassage.setExit("west", room5);

            shop.setExit("west", room4);

            //Make assignments to special rooms.

            _entrance = leftRoom2;
            _trigger = returnPassage;
            _fromRoom = returnPassage;
            _toRoom = room5;
            _secret = victoryRoom;

            TrapRoom tRoom = new TrapRoom();
            leftRoom1.Delegate = tRoom;
            tRoom.Container = leftRoom1;

            LockedRoom lockedRoom = new LockedRoom("west", 2);
            forkRoom.Delegate = lockedRoom;
            lockedRoom.Container = forkRoom;

            //add cat (win con) to the world
            Cat cat = new Cat("muffin",victoryRoom);

            //add shopkeep to the world
            ShopKeep shopkeep = new ShopKeep("shopkeep", 1000, 20, shop); //add shop and shop inventory
            shop.addShop(shopkeep );
            shopkeep.addToShop(new Weapon("dagger"), 2);
            shopkeep.addToShop(new Weapon("club"), 2);
            shopkeep.addToShop(new Weapon("staff"), 2);
            shopkeep.addToShop(new Weapon("mace"), 2);
            shopkeep.addToShop(new Weapon("sword"), 2);
            shopkeep.addToShop(new Weapon("axe"), 2);
            shopkeep.addToShop(new Armor("cloak"), 2);
            shopkeep.addToShop(new Armor("tunic"), 2);
            shopkeep.addToShop(new Armor("leather"), 2);
            shopkeep.addToShop(new Armor("chain"), 2);
            shopkeep.addToShop(new Armor("scale"), 2);
            shopkeep.addToShop(new Armor("plate"), 2);
            shopkeep.addToShop(new Item("potion"), 5);
            shopkeep.addToShop(new Item("bomb"), 1);

            shopkeep.bulkMarkUp();

            //add enemies to world
            //room 3
            room3.addEnemy(new Enemy("rat", 10, 5, room3));
            //room 4
            room4.addEnemy(new Enemy("rat", 10, 5, room4));
            room4.addEnemy(new Enemy("slime", 15, 4, room4));
            //room5
            //fork room
            //left room1
            //left room2
            leftRoom2.addEnemy(new Enemy("bat", 21, 20, leftRoom2));
            leftRoom2.addEnemy(new Enemy("slime", 21, 20, leftRoom2));
            //left boss room
            leftBossRoom.addEnemy(new Enemy("boss", 42, 25, leftBossRoom));
            leftBossRoom.addEnemy(new Enemy("slime", 21, 20, leftBossRoom));
            leftBossRoom.addEnemy(new Enemy("rat", 21, 20, leftBossRoom));
            //right room1
            rightRoom1.addEnemy(new Enemy("slime", 29, 25, rightRoom1));
            //right room2
            //right boss room
            rightBossRoom.addEnemy(new Enemy("boss", 42, 25, rightBossRoom));
            rightBossRoom.addEnemy(new Enemy("rat", 21, 20, rightBossRoom));
            rightBossRoom.addEnemy(new Enemy("bat", 21, 20, rightBossRoom));
            //return passage
            //final boss room
            finalBossRoom.addEnemy(new Enemy("boss", 120, 25, finalBossRoom));
            //victory room


            //add items to the world
            //starter room
            starterRoom.drop(new Weapon("dagger"));
            //room2
            room2.drop(new Armor("cloak"));
            //room 3
            room3.drop(new Armor("tunic"));
            //room 4
            room4.drop(new Weapon("club"));
            //room5
            room5.drop(new Item("potion"));
            //fork room
            //left room1
            leftRoom1.drop(new Armor("scale"));
            //left room2
            leftRoom2.drop(new Weapon("mace"));
            //left boss room
            leftBossRoom.drop(new Armor("plate"));
            //right room1
            rightRoom1.drop(new Weapon("sword"));
            //right room2
            rightRoom2.drop(new Armor("chain"));
            //right boss room
            rightBossRoom.drop(new Weapon("axe"));
            //return passage
            //final boss room
            //victory room


            

        }
        public void PlayerUsedBomb(Notification notification)// observer for unlocking alternate victory path
        {
            Player player = (Player)notification.Object;
            player.currentRoom.setExit("east",_secret);
            player.informationMessage("The bomb cleared away the rock, revealing a doorway.");
        }
    }
}

