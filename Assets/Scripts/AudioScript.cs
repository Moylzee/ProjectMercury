using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioSource audioSource;
    public AudioClip reloadSound;
    public AudioClip dashSound;
    public void ReloadSound(){
        audioSource.PlayOneShot(reloadSound);
    }

    public void ShootSound() {
        audioSource.PlayOneShot(shootSound);
    }

    public void DashSound() {
        audioSource.PlayOneShot(dashSound);
    }
}