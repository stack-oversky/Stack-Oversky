using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class BlockGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bolckPrefabL;          //�� ������ ���庯��
    public GameObject bolckPrefabR;          //�� ������ ���庯��
    GameObject saveIdleBlock1;              //������ idle �� �����
    GameObject saveIdleBlock2;              //���� ����
    public float plusHeight = 1;            //�� ���� ���̸� �ø��� ����
    public Camera cam;                      //ī�޶��� ��� ��ǥ���� �� ����
    public float span = 0.5f;               //������
    float delta1 = 0.5f;                    //�� ���� ����
    float delta2 = 0.5f;                    //�� ���� ����
    bool isBlockExist1 = false;             //�� ���� Ȯ��
    bool isBlockExist2 = false;             //�� ���� Ȯ��

    bool isBlockExist = false;

    // Update is called once per frame
    void Update()
    {
        //DeltaTime();//�� ����
        //CreateBlock();//�� ����
    }
    void BlockDown_L(GameObject b1)//�� ���� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            delta1 = 0.0f;
            b1.GetComponent<BlockController>().isDrop = true;
            isBlockExist1 = false;
        }
    }

    void BlockDown_R(GameObject b2)//�� ���� �Լ� 2
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
        if (isBlockExist1 == false && this.span < this.delta1)//���� ���� �� saveIdleBlock1�� BlockController �Ӽ��� �����ͼ� ����
        {
            GameObject idelBlock = Instantiate(bolckPrefabL) as GameObject;
            idelBlock.transform.position = new Vector3(-3.5f, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().startPos = new Vector3(-3, cam.transform.position.y + 3f, 0);
            saveIdleBlock1 = idelBlock;
            isBlockExist1 = true;
        }
        else if (isBlockExist1)//���� ������ saveIdleBlock �� ����
            BlockDown_L(saveIdleBlock1);

        if (isBlockExist2 == false && this.span < this.delta2)//���� ���� �� saveIdleBlock2�� BlockController �Ӽ��� �����ͼ� ����
        {
            GameObject idelBlock = Instantiate(bolckPrefabR) as GameObject;
            idelBlock.transform.position = new Vector3(3.5f, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().startPos = new Vector3(3, cam.transform.position.y + 3f, 0);
            idelBlock.GetComponent<BlockController>().sign *= -1;
            saveIdleBlock2 = idelBlock;
            isBlockExist2 = true;
        }
        else if (isBlockExist2)//���� ������ saveIdleBlock �� ����
            BlockDown_R(saveIdleBlock2);
    }

    void DeltaTime()//�� ���� ������ �ֱ� ���� �ڵ� ���� 0.5��(��������)
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
    }
}
