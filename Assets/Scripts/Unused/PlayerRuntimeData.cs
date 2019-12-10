using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;

public class PlayerRuntimeData : MonoBehaviour
{
    #region Variables

    //public CharacterData charData;
    //public PlayerManager playerManager;

    public Dictionary<int, GameObject> activePlayer = new Dictionary<int, GameObject>();

    [NonSerialized] public bool playerDead1;
    [NonSerialized] public bool playerDead2;
    [NonSerialized] public bool playerDead3;
    [NonSerialized] public bool playerDead4;

    public int playerHealth1;
    public int playerHealth2;
    public int playerHealth3;
    public int playerHealth4;

    public List<string> playerId = new List<string>();

    #endregion

    private void Awake()
    {
        //activePlayer = playerManager.players;
    }

    private void Update()
    {
        AssignPlayerData();

        if (playerHealth1 <= 0)
        {
            playerDead1 = true;
        }
        if (playerHealth2 <= 0)
        {
            playerDead2 = true;
        }
        if (playerHealth3 <= 0)
        {
            playerDead3 = true;
        }
        if (playerHealth4 <= 0)
        {
            playerDead4 = true;
        }

    }

    private void AssignPlayerData()
    {
        foreach (var item in activePlayer)
        {
            playerId.Add($"Player {activePlayer.Count}");
        }

        if (playerId.Count >= 4)
        {
            Debug.Log(playerId.Count);
        }

    }
}
