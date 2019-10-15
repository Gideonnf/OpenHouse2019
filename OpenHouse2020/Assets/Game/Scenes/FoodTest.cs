using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTest : OVRGrabbable
{
    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Knife"))
        {
            for (int i = 0; i < 2; ++i)
            {
                GameObject.Find("GameManager").GetComponent<ObjectPooler>().SpawnFromPool(this.gameObject.name, new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z + i), this.transform.rotation, new Vector3(this.transform.localScale.x * 0.5f, this.transform.localScale.y * 0.5f, this.transform.localScale.z * 0.5f));
            }
        }
    }
}
