using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bolckPrefabL;         //블럭 프리팹 저장변수
    public GameObject bolckPrefabR;         //블럭 프리팹 저장변수
    GameObject saveIdleBlock1;              //생성된 idle 블럭 저장용
    GameObject saveIdleBlock2;              //위와 같음
    public float plusHeight = 1;            //블럭 생성 높이를 올리는 변수
    public Camera cam;                      //카메라의 상대 좌표에서 블럭 생성
    public float span = 0.5f;               //딜레이
    float delta1 = 0.5f;                    //초 세는 변수
    float delta2 = 0.5f;                    //초 세는 변수
    bool isBlockExist1 = false;             //블럭 존재 확인
    bool isBlockExist2 = false;             //블럭 존재 확인

    // Update is called once per frame
    void Update()
    {
        DeltaTime();//초 세기
        CreateBlock();//블럭 생성
    }

    void BlockDown_L(GameObject b1)//블럭 낙하 함수
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delta1 = 0.0f;
            b1.GetComponent<BlockController>().isDrop = true;
            isBlockExist1 = false;
        }
    }

    void BlockDown_R(GameObject b2)//블럭 낙하 함수 2
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            delta2 = 0.0f;
            b2.GetComponent<BlockController>().isDrop = true;
            isBlockExist2 = false;
        }
    }

    void CreateBlock()
    {
        if (isBlockExist1 == false && this.span < this.delta1)//블럭이 없을 때 saveIdleBlock1에 BlockController 속성을 가져와서 생성
        {
            GameObject idelBlock = Instantiate(bolckPrefabL) as GameObject;
            idelBlock.transform.position = new Vector3(-3.5f, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().startPos = new Vector3(-3, cam.transform.position.y + 3f, 0);
            saveIdleBlock1 = idelBlock;
            isBlockExist1 = true;
        }
        else if (isBlockExist1)//블럭이 있을때 saveIdleBlock 를 전달
            BlockDown_L(saveIdleBlock1);

        if (isBlockExist2 == false && this.span < this.delta2)//블럭이 없을 때 saveIdleBlock2에 BlockController 속성을 가져와서 생성
        {
            GameObject idelBlock = Instantiate(bolckPrefabR) as GameObject;
            idelBlock.transform.position = new Vector3(3.5f, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().startPos = new Vector3(3, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().sign *= -1;
            saveIdleBlock2 = idelBlock;
            isBlockExist2 = true;
        }
        else if (isBlockExist2)//블럭이 있을때 saveIdleBlock 를 전달
            BlockDown_R(saveIdleBlock2);
    }

    void DeltaTime()//블럭 생성 딜레이 넣기 위한 코드 현재 0.5초(수정가능)
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
    }
}
