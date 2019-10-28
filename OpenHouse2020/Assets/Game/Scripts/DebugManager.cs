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
    public GameObject debugText;

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
        setDebugColor(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change the cube debug color
    public void setDebugColor(Color color)
    {
        var cubeRenderer = debugCube.GetComponent<Renderer>();

        cubeRenderer.material.SetColor("_Color", color);
    }


    // Use the debug text box the hand to keep track of variables
    public void setDebugText(string newText)
    {
        var debugTextBox = debugText.GetComponent<Text>().text = newText;
    }

}
