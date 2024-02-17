namespace ASCIIFantasy
{
    public class Item
    {
        public static Item instance;

        public string itemName { get; set;}
        public int numberItem { get; set; }
        public int power { get; set; }
        public string description { get; set; }
        public enum ItemType
        {
            HealthPotion,
            ManaPotion,
            ItemBuff
        }

        public ItemType type;

        public void Use(Character character)
        {
            switch (type)
            {
                case ItemType.HealthPotion:
                    Console.WriteLine($" {character.name} used a Health Potion and recovered " + power + " points of health!");
                    character.stats.IncrementHealth(power);
                    break;
                case ItemType.ManaPotion:
                    Console.WriteLine($" {character.name} used a Mana Potion and recovered " + power + " points of mana!");
                    character.stats.IncrementMana(power);
                    break;
                case ItemType.ItemBuff:
                    break;
                default:
                    break;
            }
        }

        public Item CreateNewItem(ItemType type, string _name, int _power, string _description, int number = 0)
        {
            Item item = new();
            item.type = type;
            item.itemName = _name;
            item.numberItem = number;
            item.power = _power;
            item.description = _description;
            return item;
        }

        public static Item CreateInstance()
        {
            if (instance == null)
            {
                instance = new Item();
            }
            return instance;
        }

    }
}
