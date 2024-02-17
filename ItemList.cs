using ASCIIFantasy;
using System;

class ItemList
{
    public static ItemList  instance;

    public List<Item> listItem = new();

   Item starterPotion = Item.CreateInstance().CreateNewItem(Item.ItemType.HealthPotion, "Starter Potion",5,"A potion given to new adventurers");
   Item smallPotion = Item.CreateInstance().CreateNewItem(Item.ItemType.HealthPotion, "Small Potion",10, "A small potion created by Miguel the mage");
   Item advencedPotion = Item.CreateInstance().CreateNewItem(Item.ItemType.HealthPotion, "Advanced Potion",15, "An advanced potion created by a master");
   Item ultimatePotion = Item.CreateInstance().CreateNewItem(Item.ItemType.HealthPotion, "Ultimate Potion",30, "The Ultimate potion created by the GOAT himself The Sorcerer King.\nFew are the people who layed their hands on it");

    public ItemList()
    {
        listItem.Add(starterPotion);
        listItem.Add(smallPotion);
        listItem.Add(advencedPotion);
        listItem.Add(ultimatePotion);
    }

    public static ItemList CreateInstance()
    {
        if (instance == null)
        {
            instance = new();
        }
        return instance;
    }
}