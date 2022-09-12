using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public int limitY = -10;                      //최소 y좌표
    public int sign = 1;                    //블록 방향 (양수면 오른쪽, 음수면 왼쪽)
    public float speed = 0.1f;              //블록이 움직이는 속도
    public bool isDown = false;             //블록을 떨어뜨릴지 말지 조정하는 변수
    private Rigidbody2D rigid;              //RibidBody 속성 조정용 변수
    public Vector3 startPos;                //시작 좌표
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    //RigidBody 속성 가져옴
        startPos = this.transform.position;     //초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)//낙하
            rigid.constraints = RigidbodyConstraints2D.None;
        else//블럭 움직임
        {
            if (this.transform.position.x > startPos.x + 2 || this.transform.position.x < startPos.x - 2) //범위는 시작 지점으로부터 +-2
                sign *= -1;
            this.transform.position += new Vector3(speed * sign, 0, 0);
        }
        Under();
    }
    void Under()//일정 y좌표 이하 내려갈시 오브젝트 삭제
    {
        if(this.transform.position.y < limitY)
        {
            Destroy(gameObject);
        }
    }
}
