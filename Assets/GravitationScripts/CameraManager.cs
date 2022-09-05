using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    private void Start()
    {
        cam1 = Camera.allCameras[0].gameObject;
        cam2 = Camera.allCameras[1].gameObject;
    }
    public void SwitchCam()
    {
        if (cam1.active)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }

        else
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }

    }
}
