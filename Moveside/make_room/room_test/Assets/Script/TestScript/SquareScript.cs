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
        // ���� ����ȭ!
        GetComponent<Renderer>().material.color =new Color(color1, color2, color3);

    }
    public void Click(Player player,Color color)
    {
        // PhotonView ������Ʈ�� �����̱� ���ؼ��� Ownership�� �ڽſ��� �־����!
        photonView.TransferOwnership(player);

        if (player.CustomProperties["team"].Equals(1))
            transform.position = new Vector3(transform.position.x + .5f, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - .5f, transform.position.y);
        // Ŭ���� �÷��̾��� �÷��� ������
        color1 = color.r;
        color2 = color.g;
        color3 = color.b;
    }

    // �ڽ��� ������ ����ȭ���� Color �ڷ����� ������ �ȵż� float�� ����
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
