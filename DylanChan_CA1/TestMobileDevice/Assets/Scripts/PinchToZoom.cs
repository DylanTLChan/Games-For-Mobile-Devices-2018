using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference for Pinch to Zoom: https://www.youtube.com/watch?v=ae6CyngEG-U&ab_channel=Holistic3d
public class PinchToZoom : MonoBehaviour {

    float previousDistance;
    float zoomSpeed = 1.0f;

	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Began
                                       || Input.GetTouch(1).phase == TouchPhase.Began))
        {
            //Calibrate Distance
            previousDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
        else if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved
                                          || Input.GetTouch(1).phase == TouchPhase.Moved))
        {
            float distance;
            Vector2 t1 = Input.GetTouch(0).position;
            Vector2 t2 = Input.GetTouch(1).position;

            distance = Vector2.Distance(t1, t2);

            //Camera based on Pinch/Zoom
            float pinchAmount = (previousDistance - distance) * zoomSpeed * Time.deltaTime;
            Camera.main.transform.Translate(0, 0, pinchAmount);

            previousDistance = distance;
        }	
	}
}
