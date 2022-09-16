using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bolckPrefab;      //블럭 프리팹 저장변수
    GameObject blo1;                    //생성된 idle 블럭 저장용
    GameObject blo2;                    //위와 같음
    public float plusHeight = 1;        //블럭 생성 높이를 올리는 변수
    public float height1 = 3;           //시작 높이
    public float height2 = 3;           //시작 높이
    public float max_height = 3;
    float span = 0.5f;                  //딜레이
    float delta1 = 0.5f;                //초 세는 변수
    float delta2 = 0.5f;                //초 세는 변수
    bool blockExist1 = false;           //블럭 존재 확인
    bool blockExist2 = false;           //블럭 존재 확인

    // Update is called once per frame
    void Update()
    {
        DeltaTime();//초 세기
        CreateBlock();//블럭 생성
        max_height = height1 > height2 ? height1 : height2;
    }

    void GetKEYDown1(GameObject b1)//블럭 낙하 함수
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delta1 = 0.0f;
            b1.GetComponent<BlockController>().isDown = true;
            height1 += plusHeight;
            blockExist1 = false;
        }
    }

    void GetKEYDown2(GameObject b2)//블럭 낙하 함수 2
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            delta2 = 0.0f;
            b2.GetComponent<BlockController>().isDown = true;
            height2 += plusHeight;
            blockExist2 = false;
        }
    }

    void CreateBlock()
    {
        if (blockExist1 == false && this.span < this.delta1)//블럭이 없을 때 bl1에 BlockController 속성을 가져와서 생성
        {
            GameObject bl1 = Instantiate(bolckPrefab) as GameObject;
            bl1.transform.position = new Vector3(-3, height1, 0);
            bl1.GetComponent<BlockController>().startPos = new Vector3(-3, height1, 0);
            blo1 = bl1;
            blockExist1 = true;
        }
        if (blockExist2 == false && this.span < this.delta2)//블럭이 없을 때 bl2에 BlockController 속성을 가져와서 생성
        {
            GameObject bl2 = Instantiate(bolckPrefab) as GameObject;
            bl2.transform.position = new Vector3(3, height2, 0);
            bl2.GetComponent<BlockController>().startPos = new Vector3(3, height2, 0);
            bl2.GetComponent<BlockController>().sign *= -1;
            blo2 = bl2;
            blockExist2 = true;
        }
        if (blockExist1)//블럭이 있을때 낙하
            GetKEYDown1(blo1);
        if (blockExist2)//블럭이 있을때 낙하
            GetKEYDown2(blo2);
    }

    void DeltaTime()//블럭 생성 딜레이 넣기 위한 코드 현재 0.5초(수정가능)
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
    }
}
