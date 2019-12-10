using UnityEngine;

[System.Serializable]
public class Character
{
    #region Variables

    [Header("General")]
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;

    [Header("Sprites")]
    [SerializeField] private Sprite selectPortrait;

    public string GetName { get => name; }
    public GameObject GetPrefab { get => prefab; }
    public Sprite GetSelectPortrait { get => selectPortrait; }

    #endregion Variables
}
