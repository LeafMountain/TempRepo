using UnityEngine;
using UnityEngine.UI;

public class UiHealthValue : MonoBehaviour
{
    #region Variables
    
    public PlayerHealth playerHealth;

    public float maxImageFill = 100f;
    public Image healthBar;
    [Header("Dashbar")]
    [SerializeField] private Image dashBar;
    [SerializeField] private Sprite noDash;
    [SerializeField] private Sprite oneDash;
    [SerializeField] private Sprite twoDash;
    [SerializeField] private Sprite threeDash;
    [Header("Lifebar")]
    [SerializeField] private Image lifeBar;
    [SerializeField] private Sprite oneLife;
    [SerializeField] private Sprite twoLifes;
    [SerializeField] private Sprite threeLifes;

    private float playerMaxHealth;

    #endregion Variables

    #region Unity Functions

    void Update()
    {
        ChangeUiHealth();
        ChangeUIDashes();
        ChangeUILifes();
    }

    #endregion Unity Functions

    #region Functions

    private void ChangeUiHealth()
    {
        healthBar.fillAmount = playerHealth.healthValue / playerHealth.DefaultHealthValue;
    }

    private void ChangeUIDashes()
    {
        if(playerHealth.GetDashes == 3)
        {
            dashBar.sprite = threeDash;
        }
        else if(playerHealth.GetDashes == 2)
        {
            dashBar.sprite = twoDash;
        }
        else if(playerHealth.GetDashes == 1)
        {
            dashBar.sprite = oneDash;
        }
        else
        {
            dashBar.sprite = noDash;
        }
    }

    private void ChangeUILifes()
    {
        int lifesLeft = playerHealth.LifesLeft;
        if(lifesLeft == 3)
        {
            lifeBar.sprite = threeLifes;
        }
        else if(lifesLeft == 2)
        {
            lifeBar.sprite = twoLifes;
        }
        else if (lifesLeft == 1)
        {
            lifeBar.sprite = oneLife;
        }
        else
        {
            lifeBar.color = Color.clear;
        }
    }

    #endregion Functions
}
