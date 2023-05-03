using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSound : MonoBehaviour
{

    AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
