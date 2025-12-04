using UnityEngine;
using UnityEngine.UI;

public class ChopWood : MonoBehaviour
{
    public KeyCode chopKey = KeyCode.E;
    public float holdTime = 1.2f;

    public Scrollbar scrollBar;
    public GameObject Panel;

    private GameObject interactText;
    private bool playerInRange = false;
    private float timer = 0f;

    private static ChopWood active = null;
    private static int choppedTrees = 0;

    private Animator animator;
    public InventoryManager inventoryManager;
    public PlayerMovement playermovement;

    // --- SESLER ---
    private AudioSource chopLoopAudio;     // bas?l? tutarken loop sesi
    public AudioClip treeBreakSound;       // a?aç kesilince bir kere çalan ses

    void Start()
    {
        animator = GetComponent<Animator>();
        chopLoopAudio = GetComponent<AudioSource>(); // AUDIO SOURCE buradan al?n?r

        interactText = GameObject.Find("InteractText");
        if (interactText) interactText.SetActive(false);

        if (scrollBar != null)
        {
            scrollBar.gameObject.SetActive(false);
            scrollBar.value = 0f;
        }

        if (Panel != null)
            Panel.SetActive(false);
    }

    void Update()
    {
        if (!playerInRange) return;

        if (active != null && active != this) return;

        // E'ye ilk basma
        if (Input.GetKeyDown(chopKey) && active == null)
        {
            active = this;
            timer = 0f;

            if (scrollBar != null)
            {
                scrollBar.gameObject.SetActive(true);
                scrollBar.value = 0f;
            }

            if (Panel != null)
                Panel.SetActive(true);

            if (playermovement != null)
                playermovement.isChopping = true;
        }

        // ==============================
        // BASILI TUTARKEN
        // ==============================
        if (active == this && Input.GetKey(chopKey))
        {
            timer += Time.deltaTime;

            // --- BURADA LOOP SES? ÇALIYOR ---
            if (chopLoopAudio && !chopLoopAudio.isPlaying)
                chopLoopAudio.Play();

            if (scrollBar != null)
                scrollBar.value = Mathf.Clamp01(timer / holdTime);

            if (timer >= holdTime)
            {
                ChopTree();
            }
        }

        // ==============================
        // TU? BIRAKILINCA SES? DURDUR
        // ==============================
        if (active == this && Input.GetKeyUp(chopKey))
        {
            if (chopLoopAudio && chopLoopAudio.isPlaying)
                chopLoopAudio.Stop();

            ResetProgress(releaseActive: true);

            if (playermovement != null)
                playermovement.isChopping = false;
        }
    }

    void ChopTree()
    {
        choppedTrees++;

        // ENVANTERE ODUN
        if (InventoryManager.I)
            InventoryManager.I.AddWood(1);

        Debug.Log("Tree Chopped: " + choppedTrees);

        // --- A?AÇ TAMAMEN KES?LD???NDE LOOPU DURDUR ---
        if (chopLoopAudio && chopLoopAudio.isPlaying)
            chopLoopAudio.Stop();

        // --- DÜ?ME SES? TEK SEFERL?K ---
        if (treeBreakSound)
            AudioSource.PlayClipAtPoint(treeBreakSound, transform.position);

        if (interactText)
            interactText.SetActive(false);

        if (scrollBar != null)
        {
            scrollBar.gameObject.SetActive(false);
            scrollBar.value = 0f;
        }

        if (Panel != null)
            Panel.SetActive(false);

        if (playermovement != null)
            playermovement.isChopping = false;

        active = null;
        Destroy(gameObject);
    }

    void ResetProgress(bool releaseActive)
    {
        timer = 0f;

        if (scrollBar != null)
        {
            scrollBar.value = 0f;
            scrollBar.gameObject.SetActive(false);
        }

        if (Panel != null)
            Panel.SetActive(false);

        if (releaseActive && active == this)
            active = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (interactText)
            interactText.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        if (interactText)
            interactText.SetActive(false);

        // Alandan ç?k?nca kesme sesi de dursun
        if (chopLoopAudio && chopLoopAudio.isPlaying)
            chopLoopAudio.Stop();

        ResetProgress(releaseActive: true);

        if (playermovement != null)
            playermovement.isChopping = false;
    }
}
