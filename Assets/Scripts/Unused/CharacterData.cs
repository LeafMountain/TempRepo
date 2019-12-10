using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/Data/CharacterData", order = 1)]
public class CharacterData : ScriptableObject, ISerializationCallbackReceiver
{
    #region Varibles
    [Header("Movement Values")]
    public float healthValue;
    public float speedValue;
    public float jumpForce;
    public float dashSpeed;

    [Header("Dash Values")]
    public float startSpeed;
    public float dashTime;
    public float startDashTime;

    [Header("Other")]
    public float minSpeed;
    public float maxSpeed;

    [NonSerialized]public float moveHorizontal;
    [NonSerialized]public float moveVertical;

    private float savedHealthValue;

#endregion Variables

    public void OnAfterDeserialize()
    {
        healthValue = savedHealthValue;
    }

    public void OnBeforeSerialize()
    {
        savedHealthValue = healthValue;


    }
}
