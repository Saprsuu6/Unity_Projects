using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject atmosphere = null;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject satellite = null;
    [SerializeField]
    private GameObject forSetellite = null;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float wind;

    void Update()
    {
        sphere.transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
        sphere.transform.Rotate(0, Time.deltaTime / speed, 0);

        if (forSetellite != null && satellite != null)
        {
            forSetellite.transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
            satellite.transform.RotateAround(forSetellite.transform.position, forSetellite.transform.up, speed * Time.deltaTime);
            satellite.transform.Rotate(0, Time.deltaTime / speed, 0);
        }

        if (atmosphere != null)
        {
            atmosphere.transform.Rotate(0, Time.deltaTime / wind, 0);
        }
    }
}
