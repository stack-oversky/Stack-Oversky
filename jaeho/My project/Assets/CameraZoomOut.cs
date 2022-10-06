using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
     private float TimeRepeat = 0.1f;
     private float nextTime = 0.0f;
///////////////////////////////
     private float zoomSpeed = 0.1f;
 
     private Camera mainCamera; //카메라 속성(값) 저장하는 변수
   
    void Start()
    {
        mainCamera = GetComponent<Camera>();  //카메라 속성(값) 저장하는 변수
    }
    
    void Update()
    {
        //Time.time은 선언된 시점에서 카운트가 시작.
        if(Time.time > nextTime){  //특정시간(TimeLeft)마다 반복
            nextTime = Time.time + TimeRepeat;
            Zoom();
            }

    }
 
    private void Zoom()  //zoom아웃하는 함수
    {   
        
        float distance = 1* zoomSpeed; //-1하면 줌인, +1하면 줌아웃
        if(distance != 0) 
        {
            mainCamera.fieldOfView += distance; //카메라의 수직시야
        }
        //distance=0;
    }
 


}