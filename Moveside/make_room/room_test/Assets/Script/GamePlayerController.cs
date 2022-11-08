using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GamePlayerController : MonoBehaviourPunCallbacks
{

    Vector3 blockPos;
    public GameObject aim;
    public GameObject block;

    GameObject playerAim;

    public int blockLimitY = -10;           //최소 y좌표(최소 지날시 블록 삭제)
    public int sign = 1;                    //블록 방향 (양수면 오른쪽, 음수면 왼쪽)
    public float speed = 5f;              //블록이 움직이는 속도
    public float downSpeed = 0.1f;
    bool crash = false;
    public bool isDrop = false;             //블록을 떨어뜨릴지 말지 조정하는 변수
    private Rigidbody2D rigid;              //RibidBody 속성 조정용 변수
    public Vector3 startPos;                //시작 좌표
    void Start()
    {
        if (photonView.IsMine)
        {
            Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
            if (cp["team"].GetHashCode() == 1)
            {
                startPos = new Vector3(-3.5f, 3, 0);
            }
            else if (cp["team"].GetHashCode() == 2)
            {
                startPos = new Vector3(3.5f, 3, 0);
            }
            playerAim = Instantiate(aim, startPos, Quaternion.identity);
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector3 pos = playerAim.transform.position;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PhotonNetwork.Instantiate(block.name, pos, Quaternion.identity);
            }
        }
    }
}
