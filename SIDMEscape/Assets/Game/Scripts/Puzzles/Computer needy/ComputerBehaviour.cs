using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBehaviour : MonoBehaviour
{
    float timeTillFire = 0.0f;

    [SerializeField]
    ParticleSystem ps_fire;
    [SerializeField]
    ParticleSystem ps_smoke;

    bool needyEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        timeTillFire = Random.Range(5, 50);

        ps_fire.gameObject.SetActive(false);
        ps_smoke.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTillFire <= 0.0f && needyEnabled)
        {
            needyEnabled = false;

            //stop all particles
            ps_fire.gameObject.SetActive(false);
            ps_smoke.gameObject.SetActive(false);

            //randomly pick one of the 2 to render
            ParticleSystem go = Random.Range(0, 1) != 0 ? ps_fire : ps_smoke;
            go.gameObject.SetActive(true);

            //TO DO SCREEN SPACE SMOKE/FIRE

            ////reset timer
            //timeTillFire = Random.Range(5, 50);
        }
        else
        {
            timeTillFire -= 1 * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wrench")) //if computer hit by a wrench
        {
            Debug.LogError("Wrench hit");

            //reset timer and particles
            timeTillFire = Random.Range(5, 50);

            //stop all particles
            ps_fire.gameObject.SetActive(false);
            ps_smoke.gameObject.SetActive(false);

            needyEnabled = true;
        }
    }
}
