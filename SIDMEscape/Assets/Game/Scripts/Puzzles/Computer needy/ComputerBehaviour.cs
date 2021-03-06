﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBehaviour : MonoBehaviour
{
    float timeTillFire = 0.0f;

    [SerializeField]
    ParticleSystem ps_fire;
    [SerializeField]
    ParticleSystem ps_smoke;
    [SerializeField]
    AudioSource audioSource;

    bool needyEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        timeTillFire = Random.Range(30, 70);

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
           // ps_fire.gameObject.SetActive(false);
            //ps_smoke.gameObject.SetActive(false);

            //randomly pick one of the 2 to render
            //ParticleSystem go = Random.Range(0, 2) != 0 ? ps_fire : ps_smoke;
            ps_smoke.gameObject.SetActive(true);
            SoundManager.instance.playAudio("ComputerFire", audioSource);
            //TO DO SCREEN SPACE SMOKE/FIRE
        }
        else
        {
            timeTillFire -= 1 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wrench")) //if computer hit by a wrench
        {
            Debug.LogError("Wrench hit");

            //reset timer and particles
            timeTillFire = Random.Range(5, 50);

            SoundManager.instance.PauseAudio(audioSource);

            //stop all particles
            ps_fire.gameObject.SetActive(false);
            ps_smoke.gameObject.SetActive(false);

            needyEnabled = true;
        }
    }
}
