using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class ChatManager0 : MonoBehaviourPunCallbacks
{
    public GameObject MsgContent;
    public TMP_Text ChatLog;
    public TMP_InputField MsgInputField;
    public ScrollRect sr;

    string UserName;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        sr = GameObject.FindObjectOfType<ScrollRect>();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && MsgInputField.isFocused == false)
        {
            MsgInputField.ActivateInputField();
            if (Input.GetKeyDown(KeyCode.Return)) SendMessage();

        }
    }


    public void SendMessage()
    {
        if (MsgInputField.text.Equals(""))
        {
            MsgInputField.DeactivateInputField();
            return;
        }
        int teamid = Launcher.Instance.rTeamNum();
        string msg;


        if (teamid == 1)
            msg = string.Format("<color=blue>[team1] {0} : {1}", PhotonNetwork.LocalPlayer.NickName, MsgInputField.text + "</color>");
        else if (teamid == 2)
            msg = string.Format("<color=red>[team2] {0} : {1}", PhotonNetwork.LocalPlayer.NickName, MsgInputField.text + "</color>");
        else if (teamid == 3)
            msg = string.Format("<color=#27692A>[team3] {0} : {1}", PhotonNetwork.LocalPlayer.NickName, MsgInputField.text + "</color>");
        else if (teamid == 4)
            msg = string.Format("<color=#9BEE56>[team4] {0} : {1}", PhotonNetwork.LocalPlayer.NickName, MsgInputField.text + "</color>");
        else
            msg = string.Format("[¥Î±‚∆¿] {0} : {1}", PhotonNetwork.LocalPlayer.NickName, MsgInputField.text);

        photonView.RPC("ReceiveMsg", RpcTarget.AllBuffered, msg);
        //ReceiveMsg(msg);
        MsgInputField.text = "";
    }
     
    public void ChatLogReset()
    {
        ChatLog.text = "";
    }

    public void Broadcast(string msg)
    {
        ReceiveMsg("<color=yellow>" + msg + "</color>");
    }

    [PunRPC]
    void ReceiveMsg (string msg)
    {
        ChatLog.text += msg + "\n";
        sr.verticalScrollbar.value = -0.50f;
        sr.verticalNormalizedPosition = -1.0f;
    }
}
