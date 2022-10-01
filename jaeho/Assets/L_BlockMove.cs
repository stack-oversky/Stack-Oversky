using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_BlockMove : MonoBehaviour
{
    Vector3 pos; //현재위치
    float leftMax=2.0f;//좌우 이동한 x 최대값
    public int LimitY=-6; //블록 보여지는 y좌표값
    public float speed=2.0f; //블록 이동속도, 후반부갈수록 빨라지면서 어려워질수도
    void Start()
    {
        pos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v=pos;  //위치
        v.x-=leftMax * Mathf.Sin(Time.time*speed);
        transform.position=v;
    }

    void destroy(){  //설정한 y좌표(-6) 으로 내려가면 블록clone한 것 삭제
        if(this.transform.position.y<LimitY){
            Destroy(gameObject);
        }
    }
}

//밑에 함수처럼 구현할 수 도 있다. (좌우로 이동하는 방법2)
//     void Start()
// {
//     currentPosition = transform.position.x;
// }
// void Update()
// {
//     currentPosition += Time.deltaTime * direction;

//     if (currentPosition >= rightMax)

//     {

//         direction *= -1;

//         currentPosition = rightMax;

//     }

//     //현재 위치(x)가 우로 이동가능한 (x)최대값보다 크거나 같다면

//     //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 우로 이동가능한 (x)최대값으로 설정

//     else if (currentPosition <= leftMax)

//     {
//         direction *= -1;
//         currentPosition = leftMax;
//     }

//     //현재 위치(x)가 좌로 이동가능한 (x)최대값보다 크거나 같다면
//     //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 좌로 이동가능한 (x)최대값으로 설정
//     transform.position = new Vector3(currentPosition, 0, 0);
//     //"Stone"의 위치를 계산된 현재위치로 처리
// }

