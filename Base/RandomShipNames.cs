using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class RandomShipNames
    {
        private static readonly string[] ShipNames = {
            "Vengeance",
            "Nemesis",
            "Retribution",
            "Hawk",
            "New Horizon",
            "Ability",
            "New Hope",
            "Junk",
            "Destroyer",
            "Leaving Dream",
            "Fate",
            "Fortune",
            "Lost King",
            "Jewel Theif",
            "Shadow Meteor-Storm",
            "Brave Titan",
            "Demon Space-Dog",
            "Old Scallywag",
            "Crooked Star",
            "Grand Serpent",
            "White Wave",
            "Old Space Dog",
            "Drunken James",
            "Plague Lagoon",
            "Lost Lagoon",
            "Cursed Slave",
            "Vanilla Skyline",
            "Cursed Raider",
            "Space Star",
            "Tainted Rose",
            "Mystic Space",
            "Devil’s Heart",
            "Disgraced Anchor",
            "Deadly Squid",
            "Dark Soul",
            "Moon Whisperer"
        }; 
        

        public static string GetRandomShipName()
        {
            var seed = Convert.ToInt32(DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString());
            Random rnd = new Random(seed);

            var output = ShipNames[rnd.Next(0, ShipNames.Length)];

            return output;
        }
    }
}
