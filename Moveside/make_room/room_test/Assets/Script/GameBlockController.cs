using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlockController : MonoBehaviour
{
    public int blockLimitY = -10;           //최소 y좌표(최소 지날시 블록 삭제)
    public int sign = 1;                    //블록 방향 (양수면 오른쪽, 음수면 왼쪽)
    public float speed = 0.1f;              //블록이 움직이는 속도
    public float downSpeed = 0.1f;
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
    void Under()//일정 y좌표 이하 내려갈시 오브젝트 삭제
    {
        if(this.transform.position.y < blockLimitY)
        {
            Destroy(gameObject);
            GameOverFlip.dead++;
            Debug.Log("deadBlock" + GameOverFlip.dead);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//블럭이 부딛혔을때 Drop 태그 추가
    {
        this.gameObject.tag = "Drop";
        rigid.constraints = RigidbodyConstraints2D.None;
        crash = true;
    }
}
