using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //�̵��ӵ�
    public float moveSpeed = 4f;
    public float moveSpeed2 = 4f;
    //�� ������
    public GameObject block_L;
    public GameObject block_R;
    public GameObject blockPrefab;
    public GameObject seesaw;

    public TMP_Text uiText;
    //�ð� ���� ����
    public float spawnSpan = 0.5f;//���� ������
    public float delaySpan = 0.1f;//������ ������
    public float itemSpan = 3f;//������ ���ӽð�(3��)
    public float itemTime = 5f;
    //������ ������
    float delayDelta1 = 2f;
    float delayDelta2 = 2f;
    //���� ������
    float delta1 = 0f;
    float delta2 = 0f;

    //�� ����
    int sign1 = 1;
    int sign2 = -1;

    //������ Ʈ����
    bool stopTrigger = false;
    bool fastTrigger = false;
    bool bigTrigger1 = false;
    bool bigTrigger2 = false;

    bool slowTrigger = false;

    public Camera cam;
    public Camera otherCam;

    public List<GameObject> blocks = new List<GameObject>();
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
            block_R.transform.position += new Vector3(moveSpeed2 * sign2 * Time.deltaTime, 0, 0);
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
            if(bigTrigger1)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger1 = false;
            }
            delayDelta1 = 0f;
            blocks.Add(newBlock);
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && delta2 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posR;
            newBlock.tag = "Dropping";
            delta2 = 0f;
            if (bigTrigger2)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger2 = false;
            }
            delayDelta2 = 0f;
            blocks.Add(newBlock);
            
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
        if (slowTrigger)
            moveSpeed = 2f;
        else if (fastTrigger)
            moveSpeed = 10f;
        if(itemSpan < 0)
        {
            fastTrigger = false;
            slowTrigger = false;
            moveSpeed = 4f;
            moveSpeed2 = 4f;
        }
    }
    public void stopBlock(int playerIndex)
    {
        if(playerIndex == 1)
        {
            moveSpeed = 0f;
        }
        else if(playerIndex == 2)
        {
            moveSpeed2 = 0f;
        }
        itemSpan = itemTime;
    }
    public void fastBlock(int playerIndex)
    {
        if(playerIndex == 1) 
        {
            moveSpeed = 10f;
        }
        else if (playerIndex == 2)
        {
            moveSpeed2 = 10f;
        }
        fastTrigger = true;
        itemSpan = itemTime;
    }
    public void bigBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            bigTrigger1 = true;
        }
        else if (playerIndex == 2)
        {
            bigTrigger2 = true;
        }
    }
    public void slowBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            moveSpeed = 2f;
        }
        else if (playerIndex == 2)
        {
            moveSpeed2 = 2f;
        }
        itemSpan = itemTime;
    }
}
