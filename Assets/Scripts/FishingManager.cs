using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    public Transform player;
    public float interactDistance = 2f;
    public KeyCode key = KeyCode.E;

    public float holdTime = 10f;
    private float timer = 0f;

    public Slider progressSlider;
    public Text interactText;   // UI Text

    private bool playerInRange = false;
    private bool isFishing = false;
    private float messageTimer = 0f;

    // audios
    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip fishingLoopSound;
    private AudioSource audioSource;

    [Range(0f, 1f)] public float catchChance = 0.5f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        if (progressSlider)
        {
            progressSlider.gameObject.SetActive(false);
            progressSlider.value = 0f;
        }

        if (interactText)
            interactText.text = "";
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(player.position, transform.position);

        // distance check
        if (dist <= interactDistance)
        {
            playerInRange = true;

            if (!isFishing && messageTimer <= 0f && interactText)
                interactText.text = "Press E to Fish";
        }
        else
        {
            ResetFishing();
            if (interactText) interactText.text = "";
            return;
        }

        // timer
        if (messageTimer > 0f)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0f && !isFishing && interactText)
                interactText.text = "Press E to Fish";
        }

        
        if (playerInRange && Input.GetKeyDown(key))
        {
            isFishing = true;
            timer = 0f;
            messageTimer = 0f;

            if (interactText)
                interactText.text = "Fishing...";

            if (progressSlider)
            {
                progressSlider.gameObject.SetActive(true);
                progressSlider.value = 0f;
            }

            if (fishingLoopSound)
            {
                audioSource.clip = fishingLoopSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        
        if (isFishing && Input.GetKey(key))
        {
            timer += Time.deltaTime;

            if (progressSlider)
                progressSlider.value = timer / holdTime;

            if (timer >= holdTime)
                TryCatchFish();
        }

        
        if (isFishing && Input.GetKeyUp(key))
        {
            if (interactText)
                interactText.text = "Canceled";

            messageTimer = 1.5f;
            ResetFishing();
        }
    }

    void TryCatchFish()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        bool success = Random.value <= catchChance;

        if (success)
        {
            InventoryManager.I.AddFish(1);

            if (successSound)
                AudioSource.PlayClipAtPoint(successSound, transform.position);

            if (interactText)
                interactText.text = "Fish Caught!";
        }
        else
        {
            if (failSound)
                AudioSource.PlayClipAtPoint(failSound, transform.position);

            if (interactText)
                interactText.text = "Fish Escaped...";
        }

        messageTimer = 2f;
        ResetFishing();
    }

    void ResetFishing()
    {
        isFishing = false;
        timer = 0f;

        if (audioSource.isPlaying)
            audioSource.Stop();

        if (progressSlider)
        {
            progressSlider.value = 0f;
            progressSlider.gameObject.SetActive(false);
        }
    }
}
