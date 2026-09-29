using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    public static BGM instance;
    public AudioClip bgmClip; // Drag in the background music
    private AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        if (instance == null) 
            instance = this;
        else
            Destroy(gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.6f; // Adjustable volume
        audioSource.Play();
        volumeSlider = GameObject.FindGameObjectWithTag("Music").GetComponent<Slider>();
        volumeSlider.value = audioSource.volume;
    }
    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    public void PlayMusic()
    {
        audioSource.Play();
    }
}
