using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#region Requirements

[RequireComponent(typeof(AudioSource))]

#endregion Requirements

public class MainMenu : MonoBehaviour
{
    #region Variables

    [Header("Menu GameObjects")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditScreen;
    [SerializeField] private GameObject helpScreen;
    [SerializeField] private Button creditsBackButton;
    [SerializeField] private Button helpBackButton;
    [SerializeField] private Button playButton;
    [SerializeField] private RectTransform handPointer;
    [Header("Sounds")]
    [SerializeField] private AudioSource menuButtonSoundManager;
    [SerializeField] private AudioClip selectButtonAudio;
    [SerializeField] private AudioClip clickButtonAudio;

    private bool inCredits = false;

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    private void Update()
    {
        if(inCredits)
        {
            if(Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                BackButton();
            }
        }
    }

    #endregion Unity Functions

    #region Functions

    public void PlayButton()
    {
        SceneManager.LoadScene(GameManager.instance.CharacterSelectSceneIndex); // Change to character select!
    }

    public void CreditsButton()
    {
        mainMenu.SetActive(false);
        creditScreen.SetActive(true);
        inCredits = true;
        creditsBackButton.Select();
    }

    public void HelpButton()
    {
        mainMenu.SetActive(false);
        helpScreen.SetActive(true);
        inCredits = true;
        helpBackButton.Select();
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        Debug.Log("Quit");
#else
        Application.Quit();
#endif
    }

    public void BackButton()
    {
        creditScreen.SetActive(false);
        mainMenu.SetActive(true);
        helpScreen.SetActive(false);
        inCredits = false;
        playButton.Select();
    }

    public void PlaySelectAudio()
    {
        menuButtonSoundManager.clip = selectButtonAudio;
        menuButtonSoundManager.Play();
    }

    public void PlayClickAudio()
    {
        menuButtonSoundManager.clip = clickButtonAudio;
        menuButtonSoundManager.Play();
    }

    public void MoveHand(RectTransform test)
    {
        Vector3 value = new Vector3(handPointer.anchoredPosition.x, test.anchoredPosition.y);
        handPointer.localPosition = value;
    }

    #endregion Functions
}
