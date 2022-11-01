using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class user1_block : MonoBehaviourPunCallbacks
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.1f;
    public float speed = 0.1f;

    // Start is called before the first frame update

    public void BlockShoot()
    {

        Debug.Log("User1 Space");
        GameObject block = PhotonNetwork.Instantiate("block",Vector3.zero,Quaternion.identity,0);
        //block.GetComponent<block_motion>().view = photonView;
        block.transform.position = blockPosition.transform.position;

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
        /*
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(1))
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
