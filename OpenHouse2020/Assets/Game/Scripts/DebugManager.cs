using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is meant for debugging
// We're using an android oculus quest
// so we can't use unity's debug log

public class DebugManager : MonoBehaviour
{

    private static DebugManager instance;

    public static DebugManager Instance {  get { return instance; } }


    public GameObject debugCube;
    public GameObject debugTextObject;

    Text debugText;
    Renderer cubeRenderer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Debug Manager already exist");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //setDebugColor(Color.red);
        debugText = debugTextObject.GetComponentInChildren<Text>();
        cubeRenderer = debugCube.GetComponent<Renderer>();

        //debugText.text = "AOISFHASIOGHA";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change the cube debug color
    public void setDebugColor(Color color)
    {
        cubeRenderer.material.SetColor("_Color", color);
    }


    // Use the debug text box the hand to keep track of variables
    public void setDebugText(string newText)
    {
        setDebugColor(Color.red);

        debugText.text = newText;
    }

}
