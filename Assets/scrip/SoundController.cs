using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundController : MonoBehaviour
{
    public Slider volumeSlider;
    public static SoundController instance;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    [Header("SOUNNNNN")]
    public AudioClip energy;
    public AudioClip Blocked;
    public AudioClip Landsound;
    public AudioClip hitSound;
    public AudioClip collectCoin;
    public AudioClip collectPwr;
    public AudioClip EndLevel;
    public AudioClip PowerSound;
    public AudioClip speedUpSound;
    public AudioClip teleporter;
    public AudioClip MirrorCat;
    public AudioClip Jump;
    public AudioClip Butt;
    void Start()
    {
        volumeSlider = GameObject.FindGameObjectWithTag("Sound").GetComponent<Slider>();

        if (instance == null)
        {
            instance = this;
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = mixerGroup;
        }
        else
        {
            Destroy(gameObject);
        }
        volumeSlider.value = audioSource.volume;
    }
    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
    }
    public void PlayEnergy()
    {
        audioSource.PlayOneShot(energy);
    }
    public void PlayBlocked()
    {
        audioSource.PlayOneShot(Blocked);
    }
    public void PlayCoin()
    {
        audioSource.PlayOneShot(collectCoin);
    }
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    public void PlayEndLevel()
    {
        audioSource.PlayOneShot(EndLevel);
    }
    public void PlayLandSound()
    {
        audioSource.PlayOneShot(Landsound);
    }
    public void PlayCollectPower()
    {
        audioSource.PlayOneShot(collectPwr);
    }
    public void PlaySpeed()
    {
        audioSource.PlayOneShot(speedUpSound);
    }
    public void PlayPower()
    {
        audioSource.PlayOneShot(PowerSound);
    }
    public void PlayTeleport()
    {
        audioSource.PlayOneShot(teleporter);
    }
    public void PlayMirror()
    {
        audioSource.PlayOneShot(MirrorCat);
    }
    public void PlayJump()
    {
        audioSource.PlayOneShot(Jump);
    }
    public void PlayButt()
    {
        audioSource.PlayOneShot(Butt);
    }
}
