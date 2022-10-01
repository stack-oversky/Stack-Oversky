using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_BlockShoot: MonoBehaviour
{
    public GameObject RblockPrefab; //prefab으로 사용할 L_gameobject변수
    

    void Start()
    {
        
    }

   
    void Update()
    {
        
        Vector3 curpos=this.transform.position;  //블록 움직일 때 현위치 저장하는 변수

        if(Input.GetKeyDown(KeyCode.Return)) //Input.GetKey 메소드는 true 값을 반환. 입력받으면 true로 if문 실행
        { //Input.GetKeyDown은 키를 누르는 순간 True값 한번만 반환.
          //꾹 누르는 것이 아니라 연타를 해야 True값 여러번 반환.
            GameObject clone=Instantiate(RblockPrefab);
            clone.GetComponent<SpriteRenderer>().color=Color.green; //블록 떨어트리면 색깔 변하게
          
        }

       // transform.position=curpos;
        
    }
}
