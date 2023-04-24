using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static void Play(AudioSource playSource)
    {
        playSource.Play();
    }
}
