using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class Player
    {
        private string playerName;
        private int playerCash;

        public Player(string playerName, int playerCash)
        {
            this.playerName = playerName;
            this.playerCash = playerCash;
        }
    }

    class Item
    {
        private string itemName;
        private Monopoly.Type itemType;
        private int itemCost;
        private bool isAvailable;

        public Item(string itemName, Monopoly.Type itemType, int itemCost, bool isAvailable)
        {
            this.itemName = itemName;
            this.itemType = itemType;
            this.itemCost = itemCost;
            this.isAvailable = isAvailable;
        }
    }

    class Monopoly
    {
        public List<Player> players = new List<Player>();
        public List<Item> items = new List<Item>();
        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                players.Add(new Player(p[i], 6000));     
            }
            items.Add(new Item("Ford", Monopoly.Type.AUTO, 500, false));
            items.Add(new Item("MCDonald", Monopoly.Type.FOOD, 250, false));
            items.Add(new Item("Lamoda", Monopoly.Type.CLOTHER, 100, false));
            items.Add(new Item("Air Baltic", Monopoly.Type.TRAVEL, 700, false));
            items.Add(new Item("Nordavia", Monopoly.Type.TRAVEL, 0, false));
            items.Add(new Item("Prison", Monopoly.Type.PRISON, 0, false));
            items.Add(new Item("MCDonald", Monopoly.Type.FOOD, 0, false));
            items.Add(new Item("TESLA", Monopoly.Type.AUTO, 0, false));
        }

        internal List<Tuple<string, int>> GetPlayersList()
        {
            return players;
        }

        internal enum Type
        {
            AUTO,
            FOOD,
            CLOTHER,
            TRAVEL,
            PRISON,
            BANK
        }

        internal List<Tuple<string, Monopoly.Type, int, bool>> GetFieldsList()
        {
            return fields;
        }

        internal Tuple<string, Type, int, bool> GetFieldByName(string v)
        {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        internal bool Buy(int v, Tuple<string, Type, int, bool> k)
        {
            var x = GetPlayerInfo(v);
            int cash = 0;
            switch(k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 500;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 250;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 700;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 100;
                    players[v - 1] = new Tuple<string, int>(x.Item1, cash);
                    break;
                default:
                    return false;
            }
            int i = players.Select((item, index) => new { name = item.Item1, index = index })
                .Where(n => n.name == x.Item1)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = new Tuple<string, Type, int, bool>(k.Item1, k.Item2, v, k.Item4);
             return true;
        }

        internal Tuple<string, int> GetPlayerInfo(int v)
        {
            return players[v - 1];
        }

        internal bool Renta(int v, Tuple<string, Type, int, bool> k)
        {
            var z = GetPlayerInfo(v);
            Tuple<string, int> o = null;
            switch(k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 == 0)
                        return false;
                    o =  GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1,o.Item2 + 250);
                    break;
                case Type.FOOD:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 250);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 250);

                    break;
                case Type.TRAVEL:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 300);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 300);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 100);
                    o = new Tuple<string, int>(o.Item1, o.Item2 + 1000);

                    break;
                case Type.PRISON:
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 1000);
                    break;
                case Type.BANK:
                    z = new Tuple<string, int>(z.Item1, z.Item2 - 700);
                    break;
                default:
                    return false;
            }
            players[v - 1] = z;
            if(o != null)
                players[k.Item3 - 1] = o;
            return true;
        }
    }
}
