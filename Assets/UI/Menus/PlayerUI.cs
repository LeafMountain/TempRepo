using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image healthBar;

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        Assert.IsNotNull(healthBar, $"Healthbar not assigned on {gameObject.name}");
    }

    #endregion Unity Functions

    #region Functions

    public void UpdatePlayerHealth(float fillPercentage)
    {
        healthBar.fillAmount = fillPercentage;
    }

    #endregion Function
}
