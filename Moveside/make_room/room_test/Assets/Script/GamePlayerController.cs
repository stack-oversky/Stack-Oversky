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

    public int blockLimitY = -10;           //�ּ� y��ǥ(�ּ� ������ ��� ����)
    public int sign = 1;                    //��� ���� (����� ������, ������ ����)
    public float speed = 5f;              //����� �����̴� �ӵ�
    public float downSpeed = 0.1f;
    bool crash = false;
    public bool isDrop = false;             //����� ����߸��� ���� �����ϴ� ����
    private Rigidbody2D rigid;              //RibidBody �Ӽ� ������ ����
    public Vector3 startPos;                //���� ��ǥ
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
