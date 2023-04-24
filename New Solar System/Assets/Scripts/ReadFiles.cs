using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadFiles : MonoBehaviour
{
    private string path = Directory.GetCurrentDirectory() + "/Music";
    private Music music;

    void Start()
    {
        music = GetComponent<Music>();

        string[] files = Directory.GetFiles(path);

        foreach (string file in files)
        {
            FileStream fs = File.OpenRead(file);
            byte[] songData = File.ReadAllBytes(file);

            //CreateAudioSource(songData, file);

            //music.AudioSource = audio;
        }
    }

    private void CreateAudioSource(byte[] songData, string fileName)
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        AudioClip clip = AudioClip.Create(fileName, songData.Length, 2, 44100, false);

        audios[6].clip = clip;
        Debug.LogWarning(audios[6].clip);
    }
}
