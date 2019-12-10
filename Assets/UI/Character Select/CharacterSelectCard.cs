using UnityEngine;

#region Requirements

[RequireComponent(typeof(SpriteRenderer))]

#endregion Requirements

public class CharacterSelectCard : MonoBehaviour
{
    #region Variables

    [Header("GameObjects")]
    [SerializeField] private SpriteRenderer characterSelectSprite;

    public Sprite SetCharacterPortrait { set => characterSelectSprite.sprite = value; }

    #endregion Variables
}
