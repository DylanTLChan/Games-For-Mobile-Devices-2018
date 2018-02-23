using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    Touch t1,t2;
    Ray r;
    RaycastHit rayCastHitInfo;
    private Vector3 startPos;
    private Vector3 objectIntialSize;
    private float distanceToSelectedObject;
    private Touchable currentlySelectedObject;
    float timer = 0;
    float startingDis;
    float previousDistance;
    float zoomSpeed = 1.0f;
    bool moved = false;
    
    

    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.touchCount > 0)
        {
            //store touches
            Touch[] touches = Input.touches;
            t1 = touches[0];

            Touch myTouch = Input.GetTouch(0);
          
            //for (int i = 0; i < Input.touchCount; i++){}
           
            startPos = t1.position;
            r = Camera.main.ScreenPointToRay(t1.position);
            Debug.DrawRay(r.origin, 100 * r.direction);

            bool hasHitItem = (Physics.Raycast(r, out rayCastHitInfo));
            Touchable possibleItem;

            if (hasHitItem)
                possibleItem = rayCastHitInfo.collider.gameObject.GetComponent<Touchable>();
            else
                possibleItem = null;

            if (t1.phase == TouchPhase.Began)
            {
                #region Tap
                timer = 0;
                moved = false;
                #endregion

                #region Drag
                distanceToSelectedObject = Vector3.Distance(possibleItem.transform.position,
                                                                    Camera.main.transform.position);
                #endregion

                #region Scale
                   if(Input.touchCount ==  2 && currentlySelectedObject)
                    {
                        t1 = touches[0];
                        t2 = touches[1];

                        startingDis = Vector3.Distance(t1.position, t2.position);
                        objectIntialSize = currentlySelectedObject.transform.localScale;
                    }
                #endregion

                #region Rotate
                
                #endregion

                #region Camera Pinch to Zoom

                   //Reference for Pinch to Zoom: https://www.youtube.com/watch?v=ae6CyngEG-U&ab_channel=Holistic3d

                   //There is a Seperate Script on the Main Camera for this called "PinchToZoom" that works.

                   if (Input.touchCount == 2 && !currentlySelectedObject)
                   {
                       //Calibrate Distance
                       previousDistance = Vector2.Distance(t1.position, t2.position);
                   } 
                #endregion    
                

            }

            if (t1.phase == TouchPhase.Moved)
            {
                moved = true;

                #region Drag

                if (possibleItem != currentlySelectedObject)
                {
                    currentlySelectedObject.DeSelect();
                    currentlySelectedObject = possibleItem;
                    currentlySelectedObject.Select();
                }

                currentlySelectedObject.transform.position = r.origin + distanceToSelectedObject * r.direction;
                #endregion

                #region Scale
                 if (Input.touchCount == 2 && currentlySelectedObject)
                    {
                        t1 = touches[0];
                        t2 = touches[1];

                        float currentDis = Vector3.Distance(t1.position, t2.position);
                        currentlySelectedObject.transform.localScale = objectIntialSize * currentDis/startingDis;
                    }  
                #endregion

                #region Rotate

               if(Input.touchCount == 1 && currentlySelectedObject)
                {
                    t1 = touches[0];
                   //distAngle = thetaNew - thetaStart;

                    //float distAngle;
                    //float startingRotation = currentlySelectedObject.transform.rotation.eulerAngles.x;
                    //currentlySelectedObject.transform.rotation = startingRotation * Quaternion.RotateTowards(Camera.forward,distAngle);
                }

                #endregion

                #region Camera Pinch to Zoom

               //Reference for Pinch to Zoom: https://www.youtube.com/watch?v=ae6CyngEG-U&ab_channel=Holistic3d

               //There is a Seperate Script on the Main Camera for this called "PinchToZoom" that works.


                   if (Input.touchCount == 2 && !currentlySelectedObject)
                   {
                      
                       t1 = touches[0];
                       t2 = touches[1];

                       float distance = Vector3.Distance(t1.position, t2.position);

                       //Camera based on Pinch/Zoom
                       float pinchAmount = (previousDistance - distance) * zoomSpeed * Time.deltaTime;
                       Camera.main.transform.Translate(0, 0, pinchAmount);

                       previousDistance = distance;
                   }	 
               #endregion
            }

            if ((t1.phase == TouchPhase.Ended) && !moved && wasShort())
            {
                #region Currently Selected Object
                if (currentlySelectedObject)
                {
                    currentlySelectedObject.DeSelect();
                    currentlySelectedObject = null;
                }

                if (Physics.Raycast(r, out rayCastHitInfo))
                {
                    currentlySelectedObject = rayCastHitInfo.collider.gameObject.GetComponent<Touchable>();

                    if (currentlySelectedObject)
                        currentlySelectedObject.Select();
                } 
                #endregion
            }
        }

        #region Accelerometer
        //Accelerometer Tutorial https://www.youtube.com/watch?v=XZWNXsjIvrE

        float speed = 0.1f;
        currentlySelectedObject.transform.Translate(Input.acceleration.x * speed, 0, -Input.acceleration.z * speed);
        #endregion
    }

    private bool wasShort()
    {
        return timer < 0.1f;
    }

}

//Connect Wirelessly 
//Using the CMD
//adb tcp 5555
//adb connect (the ip address of your device):5555

