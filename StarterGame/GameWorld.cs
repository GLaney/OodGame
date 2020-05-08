using System;
namespace StarterGame
{ 
	public class GameWorld
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
            NotificationCenter.Instance.addObserver("PlayerUsedBomb", PlayerUsedBomb);
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

            //player.informationMessage("The player is in " + player.currentRoom.tag);
            //player.informationMessage("The player is going to " + room.tag);

        }


        public void createWorld()
        {

            Room starterRoom = new Room("starterRoom");
            starterRoom.roomDescription = "You wake up in a damp room, dimly lit by two torches near a doorway to the east. You " +
                "notice the light \nfrom the flames reflecting off of a small dagger stuck in the ground at the foot of the door.";
            

            Room room2 = new Room("room2");
            room2.roomDescription = "As you enter the room your eyes are immediately drawn to a statue in the center of the room. Though\n" +
                "a portion of the statue is obscured by a dark cloak, which has been draped over it, you can see that it's a carving of\n" +
                "a mother knelt beside her child and pointing towards the door to the North.";
            

            Room room3 = new Room("room3");
            room3.roomDescription = "room3";
            


            Room room4 = new Room("room4");
            room4.roomDescription = "room4";

            Room room5 = new Room("room5");
            room5.roomDescription = "room5";

            Room forkRoom = new Room("forkRoom");
            forkRoom.roomDescription = "forkRoom";

            Room shop = new Room("shop");
            shop.roomDescription = "shop";

            Room leftRoom1 = new Room("leftRoom1");
            leftRoom1.roomDescription = "leftRoom1";
            //leftRoom1.addEnemy(new Enemy("Rat", 10, 2, leftRoom1));


            Room leftRoom2 = new Room("leftRoom2");
            leftRoom2.roomDescription = "leftRoom2";
            //leftRoom2.addEnemy(new Enemy("Rat", 10, 2, leftRoom2));


            Room leftBossRoom = new Room("leftBossRoom");
            leftBossRoom.roomDescription = "leftBossRoom";
            //leftBossRoom.addEnemy(new Enemy("Rat", 10, 2, leftBossRoom));


            Room rightRoom1 = new Room("rightRoom1");
            rightRoom1.roomDescription = "rightRoom1";
            //rightRoom1.addEnemy(new Enemy("Rat", 10, 2, rightRoom1));


            Room rightRoom2 = new Room("rightRoom2");
            rightRoom2.roomDescription = "rightRoom2";
            //rightRoom2.addEnemy(new Enemy("Rat", 10, 2, rightRoom2));


            Room rightBossRoom = new Room("rightBossRoom");
            rightBossRoom.roomDescription = "rightBossRoom";
            //rightBossRoom.addEnemy(new Enemy("Rat", 10, 2, rightBossRoom));


            Room returnPassage = new Room("returnPassage");
            returnPassage.roomDescription = "returnPassage";

            Room finalBossRoom = new Room("finalBossRoom");
            finalBossRoom.roomDescription = "finalBossRoom";
            //finalBossRoom.addEnemy(new Enemy("Rat", 10, 2, finalBossRoom));


            Room victoryRoom = new Room("victoryRoom");
            victoryRoom.roomDescription = "victoryRoom";

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


            //add shopkeep to the world
            ShopKeep shopkeep = new ShopKeep("shopkeep", 1000, 20, shop); //add shop and shop inventory
            shop.addShop(shopkeep );
            shopkeep.addToShop(new Weapon("dagger"));
            shopkeep.addToShop(new Weapon("club"));
            shopkeep.addToShop(new Weapon("staff"));
            shopkeep.addToShop(new Weapon("mace"));
            shopkeep.addToShop(new Weapon("sword"));
            shopkeep.addToShop(new Weapon("axe"));
            shopkeep.addToShop(new Armor("cloak"));
            shopkeep.addToShop(new Armor("tunic"));
            shopkeep.addToShop(new Armor("leather"));
            shopkeep.addToShop(new Armor("chain"));
            shopkeep.addToShop(new Armor("scale"));
            shopkeep.addToShop(new Armor("plate"));
            shopkeep.addToShop(new Item("potion"));
            shopkeep.addToShop(new Item("bomb"));
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


            //temporary items
            starterRoom.drop(new Item("potion"));
            starterRoom.drop(new Item("potion"));

        }
        public void PlayerUsedBomb(Notification notification)
        {
            Player player = (Player)notification.Object;
            player.currentRoom.setExit("east",_secret);
            player.informationMessage("The bomb cleared away the rock, revealing a doorway.");
        }

    }
}

