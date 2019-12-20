using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This highlights objects that are near the hand
/// </summary>
public class OVRHighlight : MonoBehaviour
{
    [Tooltip("The sphere collider to check if it is in range")]
    public SphereCollider detectionRange;
    [Tooltip("The highlight shader to swap to when the object is in range")]
    public Shader highlightShader;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
          
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
