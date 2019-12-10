using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// TODO: Code becomes harder to read with an abundance of comments. Try to make code self explanatory and only comment on the more complex parts.

// TODO: Good! This makes it easier to use for someone that doesn't know that a camera is needed
[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    #region Editor variables
    [Header("Camera zoom settings")]
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 40f;
    // TODO: The variable zoomLimiter is never used
    [Header("Camera movement settings")]
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float minYPos = -13f;
    #endregion Editor variables

    #region Unity Objects
    private Vector3 velocity;
    private Camera cam;
    #endregion Unity Objects

    #region Property varables
    public List<Transform> CameraTargets { get; set; }
    #endregion Property variables

    #region Unity functions
    private void Awake()
    {
        CameraTargets = new List<Transform>();
        // TODO: Good! Caching the reference to the camrea is good
        cam = GetComponent<Camera>();

        Assert.IsNotNull(cam, "Could not find camera component on main camera.");
    }

    // TODO: It's a good idea to think about the order of operation. Using LateUpdate for the camera is a smart move.
    private void LateUpdate()
    {
        // TODO: Code is clear by itself 
        if(CameraTargets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }
    #endregion Unity functions

    #region Camera movement
    private void Move()
    {
        // TODO: To not allocate space for another variable, just use the one since centerPoint is not used anywhere else
        Vector3 newPosition = GetTargetsBounds().center + cameraOffset;
        
        // TODO: Code is clear by itself 
        if(newPosition.y < minYPos)
        {
            newPosition.y = minYPos;
        }

        // TODO: No need to create a new vector to change 1 value
        newPosition.z = transform.position.z;

        // TODO: Code is clear by itself 
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        // TODO: Thes method GetGreatestDistance() is only used here and might as well be a part of the Zoom method.
        float greatestDistanceBetweenTargets = 0;
        Bounds bounds = GetTargetsBounds();

        // Check if the size is bigger on the x or the y axis and then return that axis.
        if (bounds.size.x > bounds.size.y)
        {
            greatestDistanceBetweenTargets = bounds.size.x;
        } 
        else
        {
            greatestDistanceBetweenTargets = bounds.size.y;
        }

        float newZoom = Mathf.Clamp(greatestDistanceBetweenTargets, minZoom, maxZoom);
        
        // TODO: Code is clear by itself 
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }
    #endregion Camera movement

    #region Calculations
    // TODO: Would have been clearer to extend the name to "GetTargetsCenterPoint" or something similar.
    // TODO: Not needed
    // private Vector3 GetTargetsCenterPoint()
    // {
    //     // If the CameraTarget list only consists of one then that Transforms position is the center point.
    //     if(CameraTargets.Count == 1)
    //     {
    //         return CameraTargets[0].position;
    //     }

    //     return GetTargetsBounds().center;

    // }

    // TODO: This method would have needed a clearer name
    // TODO: This method is only used once in this class. Doesn't need to be a separate method.
    // private float GetGreatestDistanceBetweenTargets()
    // {
    //     // TODO: This is the second time this code is written, consider extracting it into its own method.
    //     Bounds bounds = GetTargetsBounds();

    //     // Check if the size is bigger on the x or the y axis and then return that axis.
    //     if (bounds.size.x > bounds.size.y)
    //     {
    //         return bounds.size.x;
    //     } 
    //     else
    //     {
    //         return bounds.size.y;
    //     }
    // }

    private Bounds GetTargetsBounds()
    {
        // TODO: Code is clear by itself
        // TODO: Considering the foreach the line after, there's no reason to add the first position twice.
        Bounds bounds = new Bounds();

        // TODO: Code is clear by itself
        foreach (Transform target in CameraTargets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds;
    }

    #endregion Calculations
}
