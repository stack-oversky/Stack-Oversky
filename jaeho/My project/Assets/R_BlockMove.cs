using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_BlockMove : MonoBehaviour
{
   
    Vector3 pos; //현재위치
    float rightMax=2.0f;//좌우 이동한 x 최대값
    private int LimitY=-6; //블록 보여지는 y좌표값

    public float speed=2.0f; //블록 이동속도, 후반부갈수록 빨라지면서 어려워질수도
    void Start()
    {
        pos=transform.position;
    }

    void Update()
    {
        Vector3 v=pos;
        v.x+=rightMax * Mathf.Sin(Time.time*speed);
        transform.position=v;
    }

    void destroy(){  //설정한 y좌표(-6) 으로 내려가면 블록clone한 것 삭제
        if(gameObject.transform.position.y<LimitY){
            Destroy(this);
        }
    }
}
