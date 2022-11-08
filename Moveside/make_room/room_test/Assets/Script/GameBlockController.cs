using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlockController : MonoBehaviour
{
    public int blockLimitY = -10;           //�ּ� y��ǥ(�ּ� ������ ��� ����)
    public int sign = 1;                    //��� ���� (����� ������, ������ ����)
    public float speed = 0.1f;              //����� �����̴� �ӵ�
    public float downSpeed = 0.1f;
    bool crash = false;
    public bool isDrop = false;             //����� ����߸��� ���� �����ϴ� ����
    private Rigidbody2D rigid;              //RibidBody �Ӽ� ������ ����
    public Vector3 startPos;                //���� ��ǥ

    //Ÿ�� �����Ϸ��� ����
    public enum blockType
    {
        Lblock, Rblock, defaultBlock
    };
    public blockType blocktype = blockType.defaultBlock;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    //RigidBody �Ӽ� ������
        startPos = this.transform.position;     //�ʱ�ȭ
     }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
        Under();
    }

    void BlockMove()
    {
        if (!crash)
        {
            this.transform.position -= new Vector3(0, downSpeed, 0) * Time.deltaTime;
            //if (blocktype == blockType.Lblock)
            //{
            //    if (Input.GetKey(KeyCode.A))
            //    {
            //        this.transform.position -= moveSpeed;
            //    }
            //    if (Input.GetKey(KeyCode.D))
            //    {
            //        this.transform.position += moveSpeed;
            //    }
            //}
            //if (blocktype == blockType.Rblock)
            //{
            //    if (Input.GetKey(KeyCode.Keypad4))
            //    {
            //        this.transform.position -= moveSpeed;
            //    }
            //    if (Input.GetKey(KeyCode.Keypad6))
            //    {
            //        this.transform.position += moveSpeed;   
            //    }
            //}   
        }
    }
    void Under()//���� y��ǥ ���� �������� ������Ʈ ����
    {
        if(this.transform.position.y < blockLimitY)
        {
            Destroy(gameObject);
            GameOverFlip.dead++;
            Debug.Log("deadBlock" + GameOverFlip.dead);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//���� �ε������� Drop �±� �߰�
    {
        this.gameObject.tag = "Drop";
        rigid.constraints = RigidbodyConstraints2D.None;
        crash = true;
    }
}
