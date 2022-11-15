using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;
using Object = System.Object;
using System.Diagnostics;

public class GamePlayerController : MonoBehaviourPunCallbacks , IPunObservable
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    GameObject[] player;
    GameObject otherPlayer;

    public Object[] objects = new Object[2];
    public Camera cam;
    public Camera otherCam;
    public GameObject otherCamObject;
    private bool isSelect;
    private int inputIndex;

    private List<GameObject> currentblocks = new List<GameObject>();
    void Start()
    {
        if (photonView.IsMine)
        {
            isSelect = true;
            gameManagerObject = GameObject.Find("GameManager");
            gameManager = gameManagerObject.GetComponent<GameManager>();
            cam = gameManager.cam;
            otherCam = gameManager.otherCam;

            StartCoroutine(FindPlayer());

            StartCoroutine(printBlock());
        }
    }
    public IEnumerator printBlock()
    {
        GameObject viewSeeSaw;
        Vector3 viewPos;
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(1))
        {
            viewPos = new Vector3(100, 100, 0);
            otherCam.transform.position = new Vector3(-100, -100, -10);
        }
        else
        {
            viewPos = new Vector3(-100, -100, 0);
            otherCam.transform.position = new Vector3(100, 100, -10);
        }
        
        while (true)
        {
            List<GameObject> myBlocks = gameManager.blocks;
            GameObject seeSaw = gameManager.seesaw;
            viewSeeSaw = PhotonNetwork.Instantiate("SeeSaw",
                seeSaw.transform.position + viewPos,
                seeSaw.transform.rotation);
            for(int i=0;i<myBlocks.Count;i++)
            {
                GameObject newBlock = PhotonNetwork.Instantiate("Block", 
                    myBlocks[i].gameObject.transform.position + viewPos,
                    myBlocks[i].gameObject.transform.rotation);
                currentblocks.Add(newBlock);
            }

            yield return new WaitForSeconds(0.1f);

            PhotonNetwork.Destroy(viewSeeSaw);
            for (int i = 0; i < currentblocks.Count; i++)
                PhotonNetwork.Destroy(currentblocks[i]);
            currentblocks.Clear();
        }
    }
    public IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        player = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < player.Length; i++)
        {
            if (!player[i].GetPhotonView().IsMine)
            {
                otherPlayer = player[i];
            }
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(AttackStop());
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine(AttackFast());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(AttackBig());
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(DefenseSlow());
            }
            SelectPlayer();
        }
    }
    public void SelectPlayer()
    {
        if (!isSelect)
        {
            gameManager.uiText.text = "Select Player 1 / 2";
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isSelect = true;
                inputIndex = 1;
                gameManager.uiText.text = "";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                isSelect = true;
                inputIndex = 2;
                gameManager.uiText.text = "";
            }
        }
    }
    public IEnumerator AttackStop()
    {
        isSelect = false;
        int playerIndex = -1;
        while(!isSelect)
        {
            yield return new WaitForEndOfFrame();
        }
        playerIndex = inputIndex;
        gameManager.uiText.text = "";
        
        otherPlayer.GetPhotonView().RPC("StopBlock", RpcTarget.All, playerIndex);
    }
    public IEnumerator AttackFast()
    {
        isSelect = false;
        int playerIndex = -1;
        while (!isSelect)
        {
            yield return new WaitForEndOfFrame();
        }
        playerIndex = inputIndex;
        gameManager.uiText.text = "";

        otherPlayer.GetPhotonView().RPC("FastBlock", RpcTarget.All, playerIndex);
    }
    public IEnumerator AttackBig()
    {
        isSelect = false;
        int playerIndex = -1;
        while (!isSelect)
        {
            yield return new WaitForEndOfFrame();
        }
        playerIndex = inputIndex;
        gameManager.uiText.text = "";

        otherPlayer.GetPhotonView().RPC("BigBlock", RpcTarget.All, playerIndex);
    }
    [PunRPC]
    public void StopBlock(int playerIndex)
    {
        if(photonView.IsMine)
        {
            gameManager.stopBlock(playerIndex);
        }
    }
    [PunRPC]
    public void FastBlock(int playerIndex)
    {
        if (photonView.IsMine)
        {
            gameManager.fastBlock(playerIndex);
        }
    }
    [PunRPC]
    public void BigBlock(int playerIndex)
    {
        if (photonView.IsMine)
        {
            gameManager.bigBlock(playerIndex);
        }
    }

    public IEnumerator DefenseSlow()
    {
        isSelect = false;
        int playerIndex = -1;
        while (!isSelect)
        {
            yield return new WaitForEndOfFrame();
        }
        playerIndex = inputIndex;
        gameManager.uiText.text = "";

        SlowBlock(playerIndex);
    }

    public void SlowBlock(int playerIndex)
    {
        if (photonView.IsMine)
        {
            gameManager.slowBlock(playerIndex);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
