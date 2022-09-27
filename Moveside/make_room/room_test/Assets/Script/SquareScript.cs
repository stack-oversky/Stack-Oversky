using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SquareScript : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click(Player player)
    {
        // PhotonView 오브젝트를 움직이기 위해서는 Ownership이 자신에게 있어야함!
        photonView.TransferOwnership(player);
        if (player.CustomProperties["team"].Equals(1))
            transform.position = new Vector3(transform.position.x + .5f, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - .5f, transform.position.y);


    }
}
