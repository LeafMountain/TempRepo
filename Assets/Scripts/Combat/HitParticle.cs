using UnityEngine;

public class HitParticle : MonoBehaviour
{
    #region Variables

    #region Set In Editor

    [SerializeField] private float timeUntilDestroy = 0.5f;

    #endregion Set In Editor

    #endregion Variables

    #region Unity Functions

    void Update()
    {
        timeUntilDestroy -= Time.deltaTime;
        if(timeUntilDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion Unity Functions
}
