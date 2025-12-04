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

    // ??? EKLEND?
    private AudioSource waterAudio;       // loop sesi
    public AudioClip waterFillDoneSound;  // iste?e ba?l?: dolunca k?sa ses

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>(); // EK
        waterGainText.SetActive(false);
    }

    void Update()
    {
        if (!player || !energyCounter) return;

        if (hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f && waterGainText)
                waterGainText.SetActive(false);
        }

        // Mesafe d???nda ise sesi durdur
        if (Vector2.Distance(player.position, transform.position) > interactDistance)
        {
            holdTimer = 0f;
            if (progressSlider) progressSlider.value = 0f;

            if (waterAudio && waterAudio.isPlaying)
                waterAudio.Stop();   // EK

            return;
        }

        // Bas?l? tutma
        if (Input.GetKey(key))
        {
            holdTimer += Time.deltaTime;

            // Su sesi ba?las?n
            if (waterAudio && !waterAudio.isPlaying)
                waterAudio.Play();   // EK

            if (progressSlider)
                progressSlider.value = holdTimer / holdTime;

            if (holdTimer >= holdTime)
            {
                // SU EKLEME
                energyCounter.water += waterAmount;
                energyCounter.water = Mathf.Clamp(energyCounter.water, 0f, 100f);

                // GUI
                if (waterGainText) waterGainText.SetActive(true);
                hideTimer = 2f;

                // Dolma sesi ? EK
                if (waterFillDoneSound)
                    AudioSource.PlayClipAtPoint(waterFillDoneSound, transform.position);

                // RESET
                holdTimer = 0f;
                if (progressSlider) progressSlider.value = 0f;
            }
        }
        else
        {
            // E tu?u b?rak?ld? ? sesi kapat
            holdTimer = 0f;
            if (progressSlider) progressSlider.value = 0f;

            if (waterAudio && waterAudio.isPlaying)
                waterAudio.Stop();    // EK
        }
    }
}
