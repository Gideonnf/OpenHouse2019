using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This highlights objects that are near the hand
/// </summary>
public class OVRHighlight : MonoBehaviour
{
    [Tooltip("The Base shader to swap to when the object is out range")]
    public Shader standardShader;
    [Tooltip("The Highlight shader to swap to when the object is in range")]
    public Shader highlightShader;
    [Tooltip("Hand Anchor Reference")]
    public GameObject handReference;

    [Header("Shader Settings")]
    [Tooltip("Highlight Color")]
    public Color highlightColor;
    [Tooltip("Highlight Width")]
    public float maxWidth = 0.1f;

    List<Renderer> ObjectRendererList = new List<Renderer>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handReference)
            transform.position = handReference.transform.position;

        foreach (Renderer renderer in ObjectRendererList)
        {
            UpdateShader(renderer);
        }
    }

    protected void UpdateShader(Renderer objectRenderer)
    {
        float width = Mathf.PingPong(Time.time, 0.1f);
        objectRenderer.material.SetFloat("Outlines width", width);
    }

    protected void ConfigureShader(Renderer objectRenderer)
    {
        objectRenderer.material.SetColor("_FirstOutlineColor", highlightColor);
        objectRenderer.material.SetFloat("_FirstOutlineWidth", maxWidth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();
            if (objectRenderer)
            {
                objectRenderer.material.shader = highlightShader;
                ConfigureShader(objectRenderer);
                ObjectRendererList.Add(objectRenderer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactables")
        {
            Renderer objectRenderer = other.gameObject.GetComponent<Renderer>();
            if (objectRenderer)
            {
                ObjectRendererList.Remove(objectRenderer);
                objectRenderer.material.shader = standardShader;
            }
        }

    }
}
