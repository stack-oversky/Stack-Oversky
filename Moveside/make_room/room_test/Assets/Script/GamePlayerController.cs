using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;
using Object = System.Object;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GamePlayerController : MonoBehaviourPunCallbacks
{
    private GameObject gameManagerObject;
    private GameManager gameManager;
    GameObject[] player;
    GameObject otherPlayer;
    Photon.Realtime.Player othPlayer;

    public Object[] objects = new Object[2];
    public Camera cam;
    public Camera otherCam;
    public GameObject otherCamObject;
    private bool isSelect;
    private int inputIndex;

    private List<GameObject> currentblocks = new List<GameObject>();

    private bool isGameRun;

    private float recordTime = 2f;
    private int takeItem = 1;
    private int player1Item = -1;
    private bool isItemPlayer1 = false;
    private int player2Item = -1;
    private bool isItemPlayer2 = false;


    void Start()
    {
        if (photonView.IsMine)
        {
            isSelect = true;
            gameManagerObject = GameObject.Find("GameManager");
            gameManager = gameManagerObject.GetComponent<GameManager>();
            cam = gameManager.cam;
            otherCam = gameManager.otherCam;
            isGameRun = true;


            StartCoroutine(FindPlayer());
            StartCoroutine(printBlock());
        }
    }
    void Update()
    {

        if (photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                UseItemPlayer1();
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                UseItemPlayer2();
            }
            SelectPlayer();
            SetItem();
            if(isGameRun) SaveScore();
            if (gameManager.gameTime <= 0 && isGameRun)
            {

                isGameRun = false;
                StartCoroutine(GameEndPlayer());

            }
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
                GameObject newBlock = PhotonNetwork.Instantiate("LBlock", 
                    myBlocks[i].gameObject.transform.position + viewPos,
                    myBlocks[i].gameObject.transform.rotation);
                newBlock.transform.localScale = myBlocks[i].transform.localScale;
                currentblocks.Add(newBlock);
            }
            List<GameObject> myBlocks2 = gameManager.blocks2;
            for (int i = 0; i < myBlocks2.Count; i++)
            {
                GameObject newBlock = PhotonNetwork.Instantiate("RBlock",
                    myBlocks2[i].gameObject.transform.position + viewPos,
                    myBlocks2[i].gameObject.transform.rotation);
                newBlock.transform.localScale = myBlocks2[i].transform.localScale;
                currentblocks.Add(newBlock);
            }
            yield return new WaitForSeconds(0.5f);

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
                othPlayer = otherPlayer.GetPhotonView().Controller;
            }
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
    public void AttackStop()
    {
        int playerIndex = Random.Range(1,3);
        
        otherPlayer.GetPhotonView().RPC("StopBlock", RpcTarget.All, playerIndex);
    }
    public void AttackFast()
    {
        int playerIndex = Random.Range(1, 3);

        otherPlayer.GetPhotonView().RPC("FastBlock", RpcTarget.All, playerIndex);
    }
    public void AttackBig()
    {
        int playerIndex = Random.Range(1, 3);

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
    public void SaveScore()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["score"] = gameManager.score.ToString();
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        Debug.Log(gameManager.score.ToString());
    }
    public void SetItem()
    {
        if(takeItem*2 <=gameManager.score)
        {
            if (!isItemPlayer1)
            {
                player1Item = Random.Range(1, 5);
                gameManager.itemSprite1[player1Item - 1].SetActive(true);
                isItemPlayer1 = true;
            }
            if (!isItemPlayer2)
            {
                player2Item = Random.Range(1, 5);
                gameManager.itemSprite2[player2Item - 1].SetActive(true);
                isItemPlayer2 = true;
            }
            takeItem++;
        }
    }
    public void UseItemPlayer1()
    {
        if (isItemPlayer1)
        {
            if (player1Item == 1)
            {
                AttackStop();
            }
            else if (player1Item == 2)
            {
                AttackBig();
            }
            else if (player1Item == 3)
            {
                AttackFast();
            }
            else if (player1Item == 4)
            {
                SlowBlock(1);
            }
            gameManager.itemSprite1[player1Item - 1].SetActive(false);
            isItemPlayer1 = false;
        }
    }
    public void UseItemPlayer2()
    {
        if (isItemPlayer2)
        {
            if (player2Item == 1)
            {
                AttackStop();
            }
            else if (player2Item == 2)
            {
                AttackBig();
            }
            else if (player2Item == 3)
            {
                AttackFast();
            }
            else if (player2Item == 4)
            {
                SlowBlock(2);
            }
            gameManager.itemSprite2[player2Item - 1].SetActive(false);
            isItemPlayer2 = false;
        }
    }

    public IEnumerator GameEndPlayer()
    {
        gameManager.uiText.text = "Time's Up!";
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        string team1Score, team2Score;
        string team1Name, team2Name;
        if (PhotonNetwork.LocalPlayer.CustomProperties["team"].Equals(1))
        {
            team1Score = (string)PhotonNetwork.LocalPlayer.CustomProperties["score"];
            team2Score = (string)othPlayer.CustomProperties["score"];
            team1Name = PhotonNetwork.LocalPlayer.NickName;
            team2Name = othPlayer.NickName;
        }
        else
        {
            team2Score = (string)PhotonNetwork.LocalPlayer.CustomProperties["score"];
            team1Score = (string)othPlayer.CustomProperties["score"];
            team2Name = PhotonNetwork.LocalPlayer.NickName;
            team1Name = othPlayer.NickName;
        }
        gameManager.GameEnd(team1Score, team2Score,team1Name,team2Name);
        while(!gameManager.goLobby)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }

        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);

    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

    }

}
