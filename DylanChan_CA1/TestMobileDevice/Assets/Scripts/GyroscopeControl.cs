using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gyroscope reference tutorial: https://www.youtube.com/watch?v=P5JxTfCAOXo
public class GyroscopeControl : MonoBehaviour {

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

	// Use this for initialization
	void Start ()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
        gyroEnabled = EnableGyro();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
           
        }
	}

    private bool EnableGyro()
    {
        //check if device supports gyro or not
        if(SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot = new Quaternion(0, 0, 1, 0);
            return true;
        }

        return false;
    }
}
