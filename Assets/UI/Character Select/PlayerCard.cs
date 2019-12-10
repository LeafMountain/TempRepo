using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    #region Variables

    [SerializeField] private Image characterPortrait;
    [SerializeField] private RectTransform handPointer;
    [SerializeField] private List<RectTransform> characterCards;
    [SerializeField] private GameObject readyText;

    #endregion Variables

    #region Functions

    public void UpdatePortrait(Sprite portrait)
    {
        characterPortrait.color = Color.white;
        characterPortrait.sprite = portrait;
    }

    public void UpdateHandPosition(int id, int playerID)
    {
        Vector3 value = new Vector3((characterCards[id].localPosition.x + -transform.localPosition.x - 50), characterCards[id].position.y - 20 - (playerID * 30));
        handPointer.localPosition = value;
    }

    public void ActivatePlayerCard()
    {
        handPointer.gameObject.SetActive(true);
    }

    public void ToggleReadyText()
    {
        readyText.SetActive(!readyText.activeSelf);
    }

    #endregion Functions
}
