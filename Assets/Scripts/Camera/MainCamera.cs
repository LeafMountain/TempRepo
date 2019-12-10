using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    #region Editor variables
    [Header("Camera zoom settings")]
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 40f;
    [SerializeField] private float zoomLimiter = 1f;
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
        cam = GetComponent<Camera>();

        Assert.IsNotNull(cam, "Could not find camera component on main camera.");
    }

    private void LateUpdate()
    {
        // Make sure we have some targets to work with
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
        // Get the centerpoint
        Vector3 centerPoint = GetCenterPoint();
        // Offset the camera
        Vector3 newPosition = centerPoint + cameraOffset;
        
        // Make sure that we don't go lower than allowed
        if(newPosition.y < minYPos)
        {
            newPosition.y = minYPos;
        }

        // Create the new position vector
        newPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        // Update the cameras transform.
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        // Get the greatest distance between the players and clamp it between min and max zoom
        float newZoom = Mathf.Clamp(GetGreatestDistance(), minZoom, maxZoom);
        
        // Lerp the cameras orthographic size between it's current zoom(orthographic size) and thew new zoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }
    #endregion Camera movement

    #region Calculations
    private Vector3 GetCenterPoint()
    {
        // If the CameraTarget list only consists of one then that Transforms position is the center point.
        if(CameraTargets.Count == 1)
        {
            return CameraTargets[0].position;
        }

        // Create a new bounds containing the first transform in target position
        Bounds bounds = new Bounds(CameraTargets[0].position, Vector3.zero);

        // Loop through every transform in CameraTargets and add it to the bounds
        foreach (Transform target in CameraTargets)
        {
            bounds.Encapsulate(target.position);
        }

        // Return the center of the bounds
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        // Create a Bounds and add the first target in the CameraTargets list
        Bounds bounds = new Bounds(CameraTargets[0].position, Vector3.zero);
        
        // Loop through every Transform in the cameratarget list and add it to the bounds
        foreach (Transform target in CameraTargets)
        {
            bounds.Encapsulate(target.position);
        }

        // Check if the size is bigger on the x or the y axis and then return that axis.
        if (bounds.size.x > bounds.size.y)
        {
            return bounds.size.x;
        } 
        else
        {
            return bounds.size.y;
        }
    }
    #endregion Calculations
}
