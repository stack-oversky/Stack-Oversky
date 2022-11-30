using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class user1_block_prev : MonoBehaviourPunCallbacks
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.1f;
    public float speed = 0.05f;
    public float up = 10;
    int k = 1;
    public int cnt;
    public int cnt_drop;
    public int cnt_drop_check;
    float delta;
    GameObject block;
    int tmp;
    float delayTimeBlockShoot = 0.4f;
    int score = 0;
    public GameObject blockContainer; //block prefab 저장 및 관리하는 변수


    // Start is called before the first frame update

    public void BlockShoot()
    {
        //Create PhotonNetwork Object -> management with Photonview
        GameObject block = PhotonNetwork.Instantiate("block", blockPosition.transform.position, Quaternion.identity, 0);
        block.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    public void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && delta > delayTimeBlockShoot)
        {
            block = Instantiate(blockFactory);
            //rig2d.AddForce(new Vector2(0,speed), ForceMode2D.Force);
            cnt = cnt + 1;
            k = 0;
            block.transform.position = blockPosition.transform.position;

            block.name = "block num : " + cnt;
            block.transform.parent = blockContainer.transform;  //blockContainer에 블록 프리팹 저

            delta = 0f;
        }
        else
        {
            if (this.transform.position.x > start.x + 1.5f || this.transform.position.x < start.x - 1.5f)
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
        }
    }
        void timeCount()
        {
            delta += Time.deltaTime;
        }

         void calScore()
        {
            if (cnt_drop == cnt_drop_check)
            {
                k = 1;
                cnt_drop_check++;
            }
        }

    private void calBlockPosition()
    {

        if (score != tmp && score > 7) //score가 바뀌면
        {
            Vector3 destination = new Vector3(0, 2.6f + (score - 7) * 0.7f, 0); //이 위치로 이동
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 10);
        }
        if (score != tmp && score <= 7) //score가 바뀌면
        {
            Vector3 destination = new Vector3(0, 2.6f, 0); //고정된 위치로 이
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, 10);
        }
    }

    void Start()
    {
        start = this.transform.position;

        cnt = 0;
        cnt_drop = 0;
        cnt_drop_check = 1;

        //움직이는 것이 아니기 때문에 PhotonView 고정시킬 필요 없음

        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(this.tag))
        {
            this.photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (this.photonView.IsMine)
        {
            BlockMove();
        }
            timeCount();
            score = cnt - cnt_drop;
            calBlockPosition();
            CameraDown();
    }

    void CameraDown()
    {
        if (cnt_drop == cnt_drop_check)
        {
            for (int i = 0; i < 100; i++)
            {
                this.transform.position += new Vector3(0, -up * Time.deltaTime, 0);
            }
            k = 1;
            cnt_drop_check++;
        }

    }
}