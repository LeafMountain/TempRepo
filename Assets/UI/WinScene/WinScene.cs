using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject firstPosition;
    [SerializeField] private Text firstPosText;
    [SerializeField] private GameObject secondPosition;
    [SerializeField] private Text secondPosText;
    [SerializeField] private GameObject thirdPosition;
    [SerializeField] private Text thirdPosText;
    [SerializeField] private GameObject fourthPosition;
    [SerializeField] private Text fourthPosText;
    [SerializeField] private Image winnerPortrait;

    #endregion Variables

    #region Unity Functions

    private void OnEnable()
    {
        List<Player> deathList = PlayerManager.instance.DeadPlayers;
        deathList.Reverse();

        for(int i = 0; i < deathList.Count; i++)
        {
            switch(i)
            {
                case 0:
                    firstPosText.text = $"Player {deathList[i].PlayerID}";
                    firstPosition.SetActive(true);
                    break;
                case 1:
                    secondPosText.GetComponentInChildren<Text>().text = $"Player {deathList[i].PlayerID}";
                    secondPosition.SetActive(true);
                    break;
                case 2:
                    thirdPosText.GetComponentInChildren<Text>().text = $"Player {deathList[i].PlayerID}";
                    thirdPosition.SetActive(true);
                    break;
                case 3:
                    fourthPosText.GetComponentInChildren<Text>().text = $"Player {deathList[i].PlayerID}";
                    fourthPosition.SetActive(true);
                    break;
            }
        }

        winnerPortrait.sprite = deathList[0].GetPlayerCharacterScript.GetSelectPortrait;
    }

    #endregion Unity Functions
}
