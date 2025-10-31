using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coinText;      
    public Text messageText;   

    void Start() { Refresh(); if (messageText) messageText.text = ""; }

    
    public void BuySunFlower() { Buy(20, "Sun Flower", giveCrop: 1); }
    public void BuyRedFlower() { Buy(30, "Red Flower", giveCrop: 1); }
    public void BuyPotato() { Buy(45, "Potato", giveCrop: 1); }
    public void BuyBeer() { Buy(20, "Beer", giveCrop: 0); }
    public void BuyEnergy() { Buy(65, "Energy", giveCrop: 0); }
    public void BuyMystery() { Buy(70, "?", giveCrop: 1); }

    void Buy(int price, string itemName, int giveCrop)
    {
        var inv = InventoryManager.I;
        if (inv == null) return;

        if (!inv.TrySpend(price))
        {
            if (messageText) messageText.text = "Not enough coins!";
            Refresh();
            return;
        }

        if (giveCrop > 0) inv.AddCrop(giveCrop);
        if (messageText) messageText.text = "Bought " + itemName + "!";
        Refresh();              
        inv.RefreshUI();        
    }

    void Refresh()
    {
        if (coinText && InventoryManager.I)
            coinText.text = "Coins: " + InventoryManager.I.coins;
    }
}
