using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : MonoBehaviour
{
    #region Variables
    [Header("Sound effects")]
    [SerializeField] AudioClip bulletExplosion;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [Header("AudioSource")]
    [SerializeField] AudioSource soundEffectSource;
    [SerializeField] AudioSource musicSource;

    public static AudioManager Instance { get; private set; }

    #endregion Variables

    #region Unity Methods
    private void Awake()
    {
        // Create a singleton of this unless there's already one
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Assert.IsNotNull(soundEffectSource, "Sound effect Audio Source not assigned in the editor");
            Assert.IsNotNull(musicSource, "Sound effect Audio Source not assigned in the editor");
            Assert.IsNotNull(soundEffectSource, "Audio Source component could not be found on AudioManager.");
            Assert.IsNotNull(bulletExplosion, "Bullet explosion sound not found on AudioManager.");
            Assert.IsNotNull(bulletExplosion, "MenuMusic not found on AudioManager.");
            Assert.IsNotNull(bulletExplosion, "Game Music explosion sound not found on AudioManager.");
        }
    }
    #endregion Unity Methods

    #region Sound Effects
    public void PlayBulletExplosion()
    {
        soundEffectSource.PlayOneShot(bulletExplosion);
    }
    #endregion
    #region Music
    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    public void PlayGameMusic()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }
    #endregion Music
}
