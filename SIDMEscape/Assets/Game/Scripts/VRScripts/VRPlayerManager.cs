using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerManager : MonoBehaviour
{
    private static VRPlayerManager _instance;
    public static VRPlayerManager Instance { get { return _instance; } }

    /// <summary>
    /// This variable holds a reference to any active tool tip canvas
    /// This is to prevent overlap of canvas
    /// </summary>
    [System.NonSerialized]
    public GameObject toolTipCanvasReference = null;

    [Tooltip("Main Camera Reference")]
    public GameObject mainCameraReference;

    [Tooltip("Offset for tooltip canvas")]
    public float toolTipOffset = 5.0f;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(toolTipCanvasReference != null)
        {
            Vector3 TargetLocation = mainCameraReference.transform.position + (mainCameraReference.transform.forward * toolTipOffset);
            toolTipCanvasReference.transform.position = Vector3.Slerp(toolTipCanvasReference.transform.position, TargetLocation, Time.deltaTime);
        }
    }

    /// <summary>
    /// Get the reference to the main camera
    /// </summary>
    /// <returns></returns>
    public GameObject GetCameraReference()
    {
        return mainCameraReference;
    }

    /// <summary>
    /// Gets the tool tip canvas preference from the player manager
    /// </summary>
    /// <returns></returns>
    public GameObject GetWorldTipCanvas()
    {
        return toolTipCanvasReference;
    }

    /// <summary>
    /// Deletes the tool tip canvas reference so that another one can be made later on
    /// </summary>
    /// <returns></returns>
    public bool DeleteWorldTipCanvas()
    {
        // Nothing to delete
        if (toolTipCanvasReference == null)
            return false;

        Destroy(toolTipCanvasReference);
        toolTipCanvasReference = null;

        return true;
    }

    /// <summary>
    /// Stores the world space canvas for tool tips created
    /// </summary>
    /// <param name="toolTipCanvas"></param>
    /// <returns></returns>
    public bool CreateWorldTooltipCanvas(GameObject toolTipCanvas)
    {
        // If there is already a canvas present
        if (toolTipCanvasReference != null)
            return false;

        // Store the tooltipCanvasReference to the player class
        toolTipCanvasReference = toolTipCanvas;

        return true;
    }

}
