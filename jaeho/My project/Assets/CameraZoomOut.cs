using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
   
    // public float zoomSpeed = 0.4f;
 
     private Camera mainCamera;
 
    // void Start()
    // {
    //     mainCamera = GetComponent<Camera>();
    // }
    
    // void Update()
    // {

    //     Zoom();
    // }
 
    // private void Zoom()
    // {   
    //     Invoke("OnInvoke",15.0f);
    //     //float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
    //     float distance = -1* zoomSpeed; //-1하면 줌아웃, +1하면 줌인
    //     if(distance != 0)
    //     {
    //         Invoke("OnInvoke",15.0f);
    //         mainCamera.fieldOfView += distance;
    //     }
    //     distance=0;
    // }
 
    public float speed = 0.5f; //바뀔 움직일 속도
    public float cameraSize= 5f;  //얼만큼 카메라를 줌인 할지
 
//얼마만큼 증가 감소할지 설정
public float MaxSize=10f;
public float MinSize=-5f;
 
void Update() {
    cameraSize=2.0f;
    
    //줌인 줌아웃 최대 최소값 설정.
    if(cameraSize>=MaxSize)
        cameraSize=MaxSize;
    if(cameraSize<=MinSize)
        cameraSize=MinSize;
 
    mainCamera.orthographicSize=Mathf.Lerp(mainCamera.orthographicSize,cameraSize,Time.deltaTime/speed);
    //Time.deltaTime*2 의 시간만큼 값의 변화를 준다.
}


}