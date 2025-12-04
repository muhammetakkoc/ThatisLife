using UnityEngine;

public class SellButton : MonoBehaviour
{
    
    public void OnClickSell()
    {
        if (InventoryManager.I != null)
        {
            InventoryManager.I.SellAll();
        }
        else
        {
            Debug.LogWarning("No Inventory manager");
        }
    }
}
