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

    // Start is called before the first frame update

    public void BlockShoot()
    {
        //Create PhotonNetwork Object -> management with Photonview
        GameObject block = PhotonNetwork.Instantiate("block", blockPosition.transform.position, Quaternion.identity,0);
        block.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
    }

    public void BlockMove()
    {
        {
            if (this.transform.position.x > start.x + 1.5f || this.transform.position.x < start.x - 1.5f)
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
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
        if (this.photonView.IsMine){
            BlockMove();
        }
        CameraDown();
    }

    private void CameraDown()
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
