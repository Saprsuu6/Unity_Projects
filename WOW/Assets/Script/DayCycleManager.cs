using Unity.VisualScripting;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float timeOfDay;

    [SerializeField]
    private float dayDuration = 30f;

    [SerializeField]
    private ParticleSystem stars;

    [SerializeField]
    private Light sun;
    [SerializeField]
    private Light moon;

    public AnimationCurve sunCurve;
    public AnimationCurve moonCurve;
    public AnimationCurve skyBoxCurve;

    [SerializeField]
    private Material daySkyBox;
    [SerializeField]
    private Material nightSkyBox;

    private float sunIntensity;
    private float moonIntensity;

    [SerializeField]
    private GameObject myObjAudio;
    private Audio myAudio;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();

        myAudio = myObjAudio.GetComponent<Audio>();

        if (!GameSettings.MuteAllSounds)
        {
            myAudio.forestDay.Play();
        }

        sunIntensity = sun.intensity;
        moonIntensity = moon.intensity;
    }

    private void LateUpdate()
    {
        if (GameSettings.MuteAllSounds)
        {
            if (myAudio.forestDay.isPlaying)
                myAudio.forestDay.Stop();
            if (myAudio.forestNight.isPlaying)
                myAudio.forestNight.Stop();
        }
        else
        {
            if (!myAudio.forestDay.isPlaying)
                myAudio.forestDay.Play();
            if (!myAudio.forestNight.isPlaying)
                myAudio.forestNight.Play();
        }
    }

    void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;

        RenderSettings.skybox.Lerp(nightSkyBox, daySkyBox, skyBoxCurve.Evaluate(timeOfDay));
        RenderSettings.sun = skyBoxCurve.Evaluate(timeOfDay) > 0.1f ? sun : moon;
        DynamicGI.UpdateEnvironment();

        var mainModuleStars = stars.main;
        mainModuleStars.startColor = new Color(1, 1, 1, 1 - skyBoxCurve.Evaluate(timeOfDay));

        sun.transform.localRotation = Quaternion.Euler(timeOfDay * 360f, 180, 0);
        moon.transform.localRotation = Quaternion.Euler(timeOfDay * 360f + 180, 180, 0);

        sun.intensity = sunIntensity * sunCurve.Evaluate(timeOfDay);
        moon.intensity = moonIntensity * moonCurve.Evaluate(timeOfDay);
    }
}
