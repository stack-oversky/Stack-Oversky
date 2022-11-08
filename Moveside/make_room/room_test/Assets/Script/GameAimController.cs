using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAimController : MonoBehaviour
{
    public int blockLimitY = -10;           //최소 y좌표(최소 지날시 블록 삭제)
    public int sign = 1;                    //블록 방향 (양수면 오른쪽, 음수면 왼쪽)
    float speed = 5f;              //블록이 움직이는 속도
    float downSpeed = 5f;
    bool crash = false;
    public bool isDrop = false;             //블록을 떨어뜨릴지 말지 조정하는 변수
    private Rigidbody2D rigid;              //RibidBody 속성 조정용 변수
    public Vector3 startPos;                //시작 좌표

    //타입 지정하려고 만듬
    public enum blockType
    {
        Lblock, Rblock, defaultBlock
    };
    public blockType blocktype = blockType.defaultBlock;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    //RigidBody 속성 가져옴
        startPos = this.transform.position;     //초기화
     }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }

    void BlockMove()
    {
        //블럭 움직임
        if (this.transform.position.x > startPos.x + 2 || this.transform.position.x < startPos.x - 2) //범위는 시작 지점으로부터 +-2
            sign *= -1;
        this.transform.position += new Vector3(speed * sign, 0, 0) * Time.deltaTime;
        
    }
}
