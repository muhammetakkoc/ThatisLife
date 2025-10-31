using UnityEngine;
using UnityEngine.UI;

public class InventoryUIBinder : MonoBehaviour
{
    public Text woodText;
    public Text fishText;
    public Text cropText;
    public Text coinText;

    void Awake()
    {
        if (InventoryManager.I != null)
            InventoryManager.I.BindUI(gameObject, woodText, fishText, cropText, coinText);
    }
}

