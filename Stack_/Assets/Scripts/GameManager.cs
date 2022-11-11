using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //이동속도
    public float moveSpeed = 4f;

    //블럭 프리팹
    public GameObject block_L;
    public GameObject block_R;
    public GameObject blockPrefab;

    //시간 관련 변수
    public float spawnSpan = 0.5f;//생성 딜레이
    public float delaySpan = 0.1f;//움직임 딜레이
    public float itemSpan = 3f;//아이템 지속시간(3초)
    public float itemTime = 5f;
    //움직임 딜레이
    float delayDelta1 = 2f;
    float delayDelta2 = 2f;
    //생성 딜레이
    float delta1 = 0f;
    float delta2 = 0f;

    //블럭 방향
    int sign1 = 1;
    int sign2 = -1;

    //아이템 트리거
    bool stopTrigger = false;
    bool fastTrigger = false;
    bool bigTrigger = false;
    bool slowTrigger = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Create();
        UseItem();
        TimeCount();
    }

    void Move()
    {
        if(block_L.transform.position.x < -6.5)     sign1 = 1;
        else if(block_L.transform.position.x > -2)  sign1 = -1;

        if (block_R.transform.position.x > 6.5)     sign2 = -1;
        else if (block_R.transform.position.x < 2)  sign2 = 1;
       
        if (delayDelta1 > delaySpan)
            block_L.transform.position += new Vector3(moveSpeed * sign1 * Time.deltaTime, 0, 0);
        if (delayDelta2 > delaySpan)
            block_R.transform.position += new Vector3(moveSpeed * sign2 * Time.deltaTime, 0, 0);
    }

    void Create()
    {
        Vector3 posL = block_L.transform.position;
        Vector3 posR = block_R.transform.position;

        if(Input.GetKeyDown(KeyCode.Space) && delta1 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posL;
            delta1 = 0f;
            if(bigTrigger)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger = false;
            }
            delayDelta1 = 0f;
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && delta2 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posR;
            newBlock.tag = "Dropping";
            delta2 = 0f;
            if (bigTrigger)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger = false;
            }
            delayDelta2 = 0f;
        }
    }

    void TimeCount()
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
        delayDelta1 += Time.deltaTime;
        delayDelta2 += Time.deltaTime;
        itemSpan -= Time.deltaTime;
    }

    void UseItem()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            stopTrigger = true;
            itemSpan = itemTime;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            fastTrigger = true;
            itemSpan = itemTime;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            slowTrigger = true;
            itemSpan = itemTime;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            bigTrigger = true;
        }
        if (slowTrigger)
            moveSpeed = 2f;
        else if (fastTrigger)
            moveSpeed = 10f;
        else if (stopTrigger)
            moveSpeed = 0f;
        if(itemSpan < 0)
        {
            stopTrigger = false;
            fastTrigger = false;
            slowTrigger = false;
            moveSpeed = 4f;
        }
    }
}
