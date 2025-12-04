
using UnityEngine;
using UnityEngine.UI;

public class GetWater : MonoBehaviour
{
    public EnergyCounter energyCounter;
    public Transform player;
    public float interactDistance = 2f;
    public KeyCode key = KeyCode.E;
    public float holdTime = 2f;
    public float waterAmount = 50f;
    public Slider progressSlider;
    public GameObject waterGainText;

    private float holdTimer;
    private float hideTimer;

    private AudioSource waterAudio;
    public AudioClip waterFillDoneSound;

    void Awake()
    {
        
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (energyCounter == null)
        {
            energyCounter = FindObjectOfType<EnergyCounter>();
        }

        if (progressSlider == null)
        {
            
            progressSlider = GetComponentInChildren<Slider>();
        }
    }

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>();

        if (waterGainText != null)
            waterGainText.SetActive(false);

        if (progressSlider != null)
        {
            progressSlider.value = 0f;
            progressSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
        if (player == null || energyCounter == null)
        {
            Awake(); 
            if (player == null || energyCounter == null) return;
        }

        
        if (hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f && waterGainText)
                waterGainText.SetActive(false);
        }

        
        if (Vector2.Distance(player.position, transform.position) > interactDistance)
        {
            holdTimer = 0f;
            if (progressSlider) progressSlider.value = 0f;

            if (waterAudio && waterAudio.isPlaying)
                waterAudio.Stop();

            return;
        }

        
        if (Input.GetKey(key))
        {
            holdTimer += Time.deltaTime;

            if (waterAudio && !waterAudio.isPlaying)
                waterAudio.Play();

            if (progressSlider)
            {
                progressSlider.gameObject.SetActive(true);
                progressSlider.value = holdTimer / holdTime;
            }

            if (holdTimer >= holdTime)
            {
                // SU EKLE
                energyCounter.water += waterAmount;
                energyCounter.water = Mathf.Clamp(energyCounter.water, 0f, 100f);

                if (waterGainText)
                {
                    waterGainText.SetActive(true);
                }
                hideTimer = 2f;

                if (waterFillDoneSound)
                    AudioSource.PlayClipAtPoint(waterFillDoneSound, transform.position);

                holdTimer = 0f;
                if (progressSlider) progressSlider.value = 0f;
            }
        }
        else
        {
            holdTimer = 0f;
            if (progressSlider)
            {
                progressSlider.value = 0f;
                
            }

            if (waterAudio && waterAudio.isPlaying)
                waterAudio.Stop();
        }
    }
}

