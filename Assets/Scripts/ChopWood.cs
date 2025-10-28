using UnityEngine;
using UnityEngine.UI;

public class ChopWood : MonoBehaviour
{
    public KeyCode chopKey = KeyCode.E;
    public float holdTime = 1.2f;
    public Slider chopSlider;

    private GameObject interactText;
    private bool playerInRange = false;
    private float timer = 0f;

    private static ChopWood active = null;   
    private static int choppedTrees = 0;     

    void Start()
    {
        interactText = GameObject.Find("InteractText");
        if (interactText) interactText.SetActive(false);

        if (chopSlider)
        {
            chopSlider.gameObject.SetActive(false);
            chopSlider.minValue = 0; chopSlider.maxValue = 1; chopSlider.value = 0;
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        
        if (active != null && active != this) return;

        
        if (Input.GetKeyDown(chopKey) && active == null)
            active = this;

      
        if (active == this && Input.GetKey(chopKey))
        {
            timer += Time.deltaTime;
            if (chopSlider)
            {
                chopSlider.gameObject.SetActive(true);
                chopSlider.value = timer / holdTime;
            }
            if (timer >= holdTime)
                ChopTree();
        }

      
        if (active == this && Input.GetKeyUp(chopKey))
            ResetProgress(releaseActive: true);
    }

    void ChopTree()
    {
        choppedTrees++;
        Debug.Log("Tree Chopped: " + choppedTrees);

        if (interactText) interactText.SetActive(false);
        if (chopSlider) chopSlider.gameObject.SetActive(false);

        active = null; 
        Destroy(gameObject);
    }

    void ResetProgress(bool releaseActive)
    {
        timer = 0f;
        if (chopSlider) chopSlider.value = 0f;
        if (releaseActive && active == this) active = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        if (interactText) interactText.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (interactText) interactText.SetActive(false);
        if (chopSlider) { chopSlider.gameObject.SetActive(false); chopSlider.value = 0; }
        ResetProgress(releaseActive: true);
    }
}


