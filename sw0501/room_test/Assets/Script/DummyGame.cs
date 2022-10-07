using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class DummyGame : MonoBehaviourPunCallbacks, IPunObservable
{
    int playerspeed = 10;
    Rigidbody rid2D;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(1))
        {
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        rid2D = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMove();
    }

    public void FixedUpdate()
    {
        /*
        float h = Input.GetAxisRaw("Horizontal");
        rid2D.AddForce(Vector2.right * h, ForceMode.Impulse);

        if (rid2D.velocity.x > 3)
            rid2D.velocity = new Vector2(3, rid2D.velocity.y);

        else if (rid2D.velocity.x < -3)
            rid2D.velocity = new Vector2(-3, rid2D.velocity.y);
        */
    }

    
    public void PlayerMove(Player player)
    {
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["team"]);
        if (photonView.IsMine && player.CustomProperties["team"].Equals(1))
        {
            //AddForce() 함수로 물리효과를 이용해서 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rid2D.AddForce(new Vector2(playerspeed, 0), ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rid2D.AddForce(new Vector2(-playerspeed, 0), ForceMode.Force);
            }
            if (Input.GetButtonDown("Jump"))
            {
                rid2D.AddForce(Vector2.up * 12, ForceMode.Impulse);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
