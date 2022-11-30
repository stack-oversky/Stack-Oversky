using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class ChatManager1 : MonoBehaviourPunCallbacks
{
    public GameObject[] ballon = new GameObject[4]; 
    public TMP_Text[] text = new TMP_Text[4];


    public TMP_InputField MsgInputField;


    string UserName;

    Coroutine[] coroutines = new Coroutine[4];

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        for (int i = 0; i < 4; i++)
        {
            ballon[i].SetActive(false);
            text[i].text = "";
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (MsgInputField.text.Equals(""))
        {
            if (Input.GetKeyDown(KeyCode.Return) && MsgInputField.isFocused == false)
            {
                MsgInputField.ActivateInputField();                            
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage();
            }
        }
    }

    public void SendMessage()
    {
        int tid = Launcher.Instance.rTeamNum();
        string msg = MsgInputField.text;

        string color = "<color=black>";
        if (tid == 1) color = "<color=blue>";
        else if (tid == 2) color = "<color=red>";
        else if (tid == 3) color = "<color=#27692A>";
        else if (tid == 4) color = "<color=#9BEE56>";
        msg = color + msg + "</color>";

        photonView.RPC("CreateSpeechBallon", RpcTarget.AllBuffered, msg, tid);

        MsgInputField.text = "";   
    }


    [PunRPC]
    public void CreateSpeechBallon(string msg, int tid)
    {
        int id = tid - 1;
        ballon[id].SetActive(true);
        if (coroutines[id] != null)
            StopCoroutine(coroutines[id]);
        coroutines[id] = StartCoroutine(RemoveChatMessageCoroutine(id));
        text[id].text = msg;
    }

    private IEnumerator RemoveChatMessageCoroutine(int id)
    {
        yield return new WaitForSeconds(5.0f);
        text[id].text = "";
        ballon[id].SetActive(false);
    }
}

