using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    [Header("Sound effects")]
    [SerializeField] private AudioClip hit;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource, "AudioSource component could not be found on playersounds");
    }

    public void PlayHitSound()
    {
        // Make sure that the audiosource is active and enabled and then play the hit sound
        if (audioSource.isActiveAndEnabled)
        {
            audioSource.PlayOneShot(hit);
        }
    }
}
