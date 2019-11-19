using UnityEngine;
using System.Collections;

public class MonitorObject : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.StartsWith(this.gameObject.tag) && other.gameObject.tag.Contains("place"))
        {
            this.gameObject.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
            this.transform.position = new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z);
        }
    }
}
