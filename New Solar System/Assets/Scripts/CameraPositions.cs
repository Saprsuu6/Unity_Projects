using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking.Types;
using static UnityEngine.GraphicsBuffer;

public class CameraPositions : MonoBehaviour
{
    private GameObject planet;

    private Vector3[] positions = {
        new Vector3(1.5f, 48.4f, -94.48f),
        new Vector3(0f, 0f, 0.8f),
        new Vector3(0f, 0f, 0.8f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 0f, 1f),
        new Vector3(0f, 0f, 12f),
        new Vector3(0f, 0f, 3f),
        new Vector3(0f, 0f, 2f),
        new Vector3(0f, 0f, 2f),
    };

    private Vector3 mainRotation = new Vector3(29f, 17f, 0);

    private int cameraPositionCount = -1;

    private float sensitivity = 3;
    private float verticalLimit = 90;
    private float zoom = 1;
    private float zoomMax = 10;
    private float zoomMin = 1;
    private float X, Y;

    private void Start()
    {
        SetNextCameraPosition();
    }

    private void LateUpdate()
    {
        if (cameraPositionCount > 0)
        {
            #region Scroll
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                zoom -= Input.mouseScrollDelta.y / zoom * Time.timeScale;
                if (zoom < zoomMin) zoom = zoomMin;
                if (zoom > zoomMax) zoom = zoomMax;
            }
            #endregion
            #region Rotation around object
            if (Input.GetKey(KeyCode.Mouse0))
            {
                X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
                Y += Input.GetAxis("Mouse Y") * sensitivity;
                Y = Mathf.Clamp(Y, -verticalLimit, verticalLimit);
                transform.localEulerAngles = new Vector3(-Y, X, 0);
            }
            #endregion

            Vector3 position = planet.transform.position - transform.localRotation * positions[cameraPositionCount] * zoom;
            transform.position = position;
        }
    }

    public void SetNextCameraPosition()
    {
        if (cameraPositionCount >= 8)
        {
            cameraPositionCount = -1;
        }

        cameraPositionCount++;

        switch (cameraPositionCount)
        {
            case 0:
                planet = null;
                SetMainPosition();
                break;
            case 1:
                planet = GameObject.Find("Mercury");
                SetPlanetPosition();
                break;
            case 2:
                planet = GameObject.Find("Venus");
                SetPlanetPosition();
                break;
            case 3:
                planet = GameObject.Find("Earth");
                SetPlanetPosition();
                break;
            case 4:
                planet = GameObject.Find("Mars");
                SetPlanetPosition();
                break;
            case 5:
                planet = GameObject.Find("Jupiter");
                SetPlanetPosition();
                break;
            case 6:
                planet = GameObject.Find("Saturn");
                SetPlanetPosition();
                break;
            case 7:
                planet = GameObject.Find("Uranus");
                SetPlanetPosition();
                break;
            case 8:
                planet = GameObject.Find("Neptune");
                SetPlanetPosition();
                break;
        }
    }

    private void SetMainPosition()
    {
        transform.position = positions[0];
        transform.eulerAngles = mainRotation;
    }

    private void SetPlanetPosition()
    {
        transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
        transform.position = planet.transform.position - transform.localRotation * positions[cameraPositionCount] * zoom;
    }
}
