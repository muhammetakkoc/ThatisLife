using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager I;

    [HideInInspector] public GameObject panel;
    [HideInInspector] public Text woodText, fishText, cropText, coinText;

    public int wood, fish, crop, coins;

    
    public AudioClip coinClip;   

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        if (panel) panel.SetActive(false);
        RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && panel)
            panel.SetActive(!panel.activeSelf);
    }

    public void AddWood(int x) { wood += x; RefreshUI(); }
    public void AddFish(int x) { fish += x; RefreshUI(); }
    public void AddCrop(int x) { crop += x; RefreshUI(); }

    public void SellAll()
    {
        
        if (wood == 0 && fish == 0 && crop == 0)
        {
            Debug.Log("there is nothing to sell.");
            return;
        }

        //  SES? ÇAL
        if (coinClip != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(coinClip, Camera.main.transform.position);
        }
        else
        {
            Debug.LogWarning("no coinClip or Camera.main ");
        }

        coins += wood * 2 + fish * 5 + crop * 3;
        wood = fish = crop = 0;
        RefreshUI();
    }

    public bool TrySpend(int price)
    {
        if (coins < price) return false;
        coins -= price;
        RefreshUI();
        return true;
    }

    public void RefreshUI()
    {
        if (!woodText || !fishText || !cropText || !coinText) return;

        woodText.text = "Wood: " + wood;
        fishText.text = "Fish: " + fish;
        cropText.text = "Crop: " + crop;
        coinText.text = "Coins: " + coins;
    }

    public void BindUI(GameObject p, Text w, Text f, Text c, Text coin)
    {
        panel = p; woodText = w; fishText = f; cropText = c; coinText = coin;
        if (panel) panel.SetActive(false);
        RefreshUI();
    }
}
