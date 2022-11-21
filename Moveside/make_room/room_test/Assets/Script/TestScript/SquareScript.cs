using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SquareScript : MonoBehaviourPunCallbacks, IPunObservable
{
    float color1;
    float color2;
    float color3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 색깔 동기화!
        GetComponent<Renderer>().material.color =new Color(color1, color2, color3);

    }
    public void Click(Player player,Color color)
    {
        // PhotonView 오브젝트를 움직이기 위해서는 Ownership이 자신에게 있어야함!
        photonView.TransferOwnership(player);

        if (player.CustomProperties["team"].Equals(1))
            transform.position = new Vector3(transform.position.x + .5f, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - .5f, transform.position.y);
        // 클릭한 플레이어의 컬러를 가져옴
        color1 = color.r;
        color2 = color.g;
        color3 = color.b;
    }

    // 자신의 색깔을 동기화해줌 Color 자료형은 전달이 안돼서 float로 전달
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(color1);
            stream.SendNext(color2);
            stream.SendNext(color3);
        }
        else
        {
            color1 = (float)stream.ReceiveNext();
            color2 = (float)stream.ReceiveNext();
            color3 = (float)stream.ReceiveNext();
        }
    }
}
