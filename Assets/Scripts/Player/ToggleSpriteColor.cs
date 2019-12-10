using UnityEngine;
using UnityEngine.Assertions;

#region Requirements

[RequireComponent(typeof(SpriteRenderer))]

#endregion Requirements

public class ToggleSpriteColor : MonoBehaviour
{
    #region Variables

    #region Set In Editor

    [Header("Colors")]
    [SerializeField] private Color fromColor = Color.white;
    [SerializeField] private Color toColor = Color.red;
    [Header("Timers and settings")]
    [SerializeField] private float colorTimer = 0.1f;
    [SerializeField] private bool returnToOriginal = true;
    [SerializeField] private int cycles = 1;

    #endregion Set In Editor

    #region Local

    private SpriteRenderer spriteRenderer;
    private float currentTimer = 0;
    private int currentCycle = 0;
    private bool running = false;
    private bool alreadyReturned = false;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Failed to find SpriteRenderer component.");
    }

    private void Update()
    {
        if(running)
        {
            currentTimer -= Time.deltaTime;

            if(currentTimer <= 0)
            {
                if (currentCycle % 2 != 0)
                {
                    spriteRenderer.color = toColor;
                }
                else
                {
                    spriteRenderer.color = fromColor;
                }

                if(currentCycle < cycles)
                {
                    currentTimer = colorTimer;
                }
                else if( returnToOriginal && !alreadyReturned )
                {
                    currentTimer = colorTimer;
                    alreadyReturned = true;
                }
                else
                {
                    ResetToggle();
                }

                currentCycle++;
            }
        }
    }

    #endregion Unity Functions

    #region Functions

    public void ResetToggle()
    {
        currentCycle = 0;
        running = false;
        alreadyReturned = false;
    }

    public void StartToggle()
    {
        currentTimer = colorTimer;
        running = true;
    }

    #endregion Functions
}
