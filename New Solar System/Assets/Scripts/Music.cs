using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource AudioSource
    {
        set { audios.Add(value); }
    }

    private List<AudioSource> audios = null;
    private AudioSource playNow = null;

    [SerializeField]
    private Slider volume = null;
    [SerializeField]
    private Toggle mute = null;
    [SerializeField]
    private Toggle pause = null;

    private TextMeshProUGUI muteSoundTitle;
    private TextMeshProUGUI stop_playMusicTitle;
    private TextMeshProUGUI soundVolumeTitle;

    private int musicCountNow = 0;
    [SerializeField]
    private int musicCount;

    void Start()
    {
        audios = new List<AudioSource>(GetComponents<AudioSource>());

        muteSoundTitle = GameObject.Find("MuteSoundTitle").GetComponent<TextMeshProUGUI>();
        stop_playMusicTitle = GameObject.Find("PauseMusicTitle").GetComponent<TextMeshProUGUI>();
        soundVolumeTitle = GameObject.Find("SoundVolumeTitle").GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        ChooseMusic();
    }

    private void ChooseMusic()
    {
        if (playNow == null)
        {
            NextRandomSong();
        }

        if (playNow.time == playNow.clip.length)
        {
            playNow = null;
        }
    }

    public void NextRandomSong()
    {
        if (musicCountNow > musicCount - 1)
        {
            musicCountNow = 0;
        }

        if (playNow != null && playNow.isPlaying)
        {
            playNow.Stop();
        }

        playNow = audios[musicCountNow];
        #region Set previous settings
        playNow.volume = volume.value;
        playNow.mute = mute.isOn;
        if (!pause.isOn) playNow.Play();
        #endregion

        musicCountNow++;
    }

    public void MuteUmute(bool toggle)
    {
        if (toggle)
        {
            playNow.mute = true;
            muteSoundTitle.text = "Unmute sound";
        }
        else
        {
            playNow.mute = false;
            muteSoundTitle.text = "Mute sound";
        }
    }

    public void StopPlay(bool toggle)
    {
        if (toggle && playNow.isPlaying)
        {
            playNow.Pause();
            stop_playMusicTitle.text = "Continue music";
        }
        else if (!toggle && !playNow.isPlaying)
        {
            playNow.Play();
            stop_playMusicTitle.text = "Pause music";
        }
    }

    public void ChangeValume(float value)
    {
        double roundedValue = Math.Round(value, 1, MidpointRounding.AwayFromZero);
        soundVolumeTitle.text = "Volume: " + roundedValue * 100 + "%";
        playNow.volume = value;
    }
}
