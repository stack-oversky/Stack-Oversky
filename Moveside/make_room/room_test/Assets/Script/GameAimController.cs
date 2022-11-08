using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAimController : MonoBehaviour
{
    public int blockLimitY = -10;           //�ּ� y��ǥ(�ּ� ������ ��� ����)
    public int sign = 1;                    //��� ���� (����� ������, ������ ����)
    float speed = 5f;              //����� �����̴� �ӵ�
    float downSpeed = 5f;
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
    }

    void BlockMove()
    {
        //�� ������
        if (this.transform.position.x > startPos.x + 2 || this.transform.position.x < startPos.x - 2) //������ ���� �������κ��� +-2
            sign *= -1;
        this.transform.position += new Vector3(speed * sign, 0, 0) * Time.deltaTime;
        
    }
}
