using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player1EndScore : MonoBehaviour
{
    public int user1Score;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        user1Score = GameObject.Find("user" + PhotonNetwork.LocalPlayer.CustomProperties["team"]).GetComponent<ScoreUpdate>().score_pu;
        Debug.Log(PhotonNetwork.NickName);
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == "1")
        {
            player1.text = string.Format(PhotonNetwork.LocalPlayer.NickName + "  Score : {0:D1}", user1Score);
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == "2")
        {
            player2.text = string.Format(PhotonNetwork.LocalPlayer.NickName + "  Score : {0:D1}", user1Score);
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == "3")
        {
            player3.text = string.Format(PhotonNetwork.LocalPlayer.NickName + "  Score : {0:D1}", user1Score);
        }
        else if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == "4")
        {
            player4.text = string.Format(PhotonNetwork.LocalPlayer.NickName + "  Score : {0:D1}", user1Score);
        }*/
    }
}
