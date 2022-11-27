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
    public float speed = 0.1f;

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

        //움직이는 것이 아니기 때문에 PhotonView 고정시킬 필요 없음

        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(this.tag))
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
