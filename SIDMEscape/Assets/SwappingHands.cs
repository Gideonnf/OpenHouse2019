using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingHands : MonoBehaviour
{
    [SerializeField]
    GameObject LeftHandAnchor;
    [SerializeField]
    GameObject RightHandAnchor;
    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject RightHand;

    [SerializeField]
    float fTimer;

    bool change = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fTimer > 0)
        {
            fTimer -= Time.deltaTime * 1;

            change = true;
        }
        else if (change)
        {
            LeftHandAnchor.SetActive(false);
            RightHandAnchor.SetActive(false);

            LeftHand.SetActive(true);
            RightHand.SetActive(true);

            change = false;
        }
    }
}
