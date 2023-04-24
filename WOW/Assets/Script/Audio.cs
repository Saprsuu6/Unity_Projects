using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource money;
    public AudioSource forestDay;
    public AudioSource forestNight;

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        money = audioSources[0];
        forestDay = audioSources[1];
        forestNight = audioSources[2];

        if (!GameSettings.MuteAllSounds)
        {
            forestDay.Play();
        }
    }
    private void LateUpdate()
    {
        forestDay.volume = forestNight.volume = GameSettings.Music;
        money.volume = GameSettings.Sounds;
    }
}
