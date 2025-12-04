using UnityEngine;
using UnityEngine.UI;

public class InventoryUIBinder : MonoBehaviour
{
    public GameObject panel;
    public Text woodText;
    public Text fishText;
    public Text cropText;
    public Text coinText;

    void Start()
    {
        if (InventoryManager.I != null)
        {
            InventoryManager.I.BindUI(panel, woodText, fishText, cropText, coinText);
        }
        else
        {
            Debug.LogWarning("InventoryManager yok! ?lk sahnede olu?tu?undan emin ol.");
        }
    }
}

