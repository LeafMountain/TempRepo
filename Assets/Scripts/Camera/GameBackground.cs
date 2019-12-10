using UnityEngine;
using UnityEngine.Assertions;

public class GameBackground : MonoBehaviour
{
    #region Variables
    Camera cam;

    [Header("Gameobjects")]
    [SerializeField] Transform skyBackground;
    [SerializeField] Transform cloudBackground;
    [SerializeField] Transform mist;
    [SerializeField] Transform clouds;

    [Header("Speed to lerp backgrounds")]
    [SerializeField] float skyBackgroundSpeed = 1f;
    [SerializeField] float cloudBackgroundSpeed = 0.5f;
    [SerializeField] float mistSpeed = 1f;
    [SerializeField] float cloudSpeed = 0.5f;
    #endregion Variables

    #region Unity methods
    void Start()
    {
        cam = Camera.main;
        Assert.IsNotNull(cam, "Couldn't find main camera");
        Assert.IsNotNull(skyBackground, "Sky background not assigned in editor.");
        Assert.IsNotNull(cloudBackground, "Cloud background not assigned in editor.");
        Assert.IsNotNull(mist, "Mist not assigned in editor.");
        Assert.IsNotNull(clouds, "Clouds not assigned in editor.");
    }

    void Update()
    {
        skyBackground.position = Vector3.Lerp(skyBackground.position, new Vector3(cam.transform.position.x, cam.transform.position.y, skyBackground.position.z), skyBackgroundSpeed);
        cloudBackground.position = Vector3.Lerp(cloudBackground.position, new Vector3(cam.transform.position.x, cam.transform.position.y, cloudBackground.position.z), cloudBackgroundSpeed);
        mist.position = Vector3.Lerp(mist.position, new Vector3(cam.transform.position.x, cam.transform.position.y, mist.position.z), mistSpeed);
        clouds.position = Vector3.Lerp(clouds.position, new Vector3(cam.transform.position.x, cam.transform.position.y, clouds.position.z), cloudSpeed);
    }
    #endregion Unity methods
}
