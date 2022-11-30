using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.Jobs;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEditor;
using System.Threading;
using Unity.VisualScripting;
using System;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject menubutton;

    public GameObject roomListPanel;
    public RoomButton joinRoomButton;
    private List<RoomButton> allJoinRoom = new List<RoomButton>();
    public TMP_Text joinRoomText;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public GameObject loadingPanel;
    public TMP_Text loadingText;

    public GameObject roomPanel;
    public TMP_Text roomName;
    private List<TMP_Text> roomPlayerName = new List<TMP_Text>();
    public TMP_Text team1PlayerLabel;
    public TMP_Text team2PlayerLabel;
    public TMP_Text waitPlayerLabel;
    public TMP_Text roomNoticeText;
    public GameObject startButton;

    public GameObject nameInputPanel;
    public TMP_InputField nameInput;
    private bool hasNickname;

    public GameObject errorPanel;
    public TMP_Text errorText;

    public GameObject roomNameInputPanel;
    public TMP_InputField roomNameInputText;

    public string levelToPlay;
    void Start()
    {
        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "��Ʈ��ũ ������";
        PhotonNetwork.ConnectUsingSettings();
        Screen.SetResolution(1200, 800, false);
    }

    // ��Ʈ��ũ ���� ������ ����
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        
        loadingText.text = "�κ� ������";
    }
    // �κ� ���� ������ ����
    public override void OnJoinedLobby()
    {
        CloseMenu();
        menubutton.SetActive(true);
        // �г��� ���ٸ� ����, �ִٸ� �ǳʶ�
        if(!hasNickname)
        {
            CloseMenu();
            nameInputPanel.SetActive(true);
            if(PlayerPrefs.HasKey("playerName"))
            {
                nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            nameInput.text = PlayerPrefs.GetString("playerName");
        }
        PhotonNetwork.LocalPlayer.CustomProperties = new Hashtable() { { "team", 0 }, { "score", "0" } };
    }
    public void SetNickname()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            PlayerPrefs.SetString("playerName", nameInput.text);
            hasNickname = true;

            CloseMenu();
            menubutton.SetActive(true);
        }
    }
    void CloseMenu()
    {
        menubutton.SetActive(false);
        roomListPanel.SetActive(false);
        loadingPanel.SetActive(false);
        nameInputPanel.SetActive(false);
        roomPanel.SetActive(false);
        errorPanel.SetActive(false);
        roomNameInputPanel.SetActive(false);
    }
    public void OpenLobby()
    {
        CloseMenu();
        roomListPanel.SetActive(true);
    }
    // ���� �κ� ���� �Լ�

    //�� ����Ʈ�� ��ȭ�� ���������� �Լ� ȣ��. roomList���� ��ü �� ����Ʈ�� �ƴ� �������ִ� roomList�� ����
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // cachedRoomList�� ���̸��� key������ roominfo�� �������
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            string name = info.Name;
            // ������ ���̶��
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(name);
            }
            else
            {
                cachedRoomList[name] = info;
            }
        }
        UpdateRoom(cachedRoomList);
    }
    public void UpdateRoom(Dictionary<string, RoomInfo> cachedRoomList)
    {
        foreach (RoomButton rb in allJoinRoom)
        {
            Destroy(rb.gameObject);
        }
        allJoinRoom.Clear();
        // cachedRoomList�� ����� roomButton�� ����
        foreach (KeyValuePair<string, RoomInfo> roomInfo in cachedRoomList)
        {
            RoomButton newButton = Instantiate(joinRoomButton, joinRoomButton.transform.parent);
            newButton.SetButtonDetails(roomInfo.Value);
            newButton.gameObject.SetActive(true);
            allJoinRoom.Add(newButton);
        }
    }
    // ����� ��ư
    public void CreateRoom()
    {
        CloseMenu();
        roomNameInputPanel.gameObject.SetActive(true);
    }

    // �� �̸� ������ Ȯ��
    public void SetRoomName()
    {
        if (!string.IsNullOrEmpty(roomNameInputText.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            options.CustomRoomProperties = new Hashtable() { { "team1", 0 }, { "team2", 0 }};
            
            PhotonNetwork.CreateRoom(roomNameInputText.text, options);

            CloseMenu();
            loadingPanel.SetActive(true);
            loadingText.text = "�� ������";
        }
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CloseMenu();
        errorText.text = "���� : " + message;
        errorPanel.SetActive(true);
    }
    public void CloseErrorPanel()
    {
        CloseMenu();
        menubutton.SetActive(true);
    }
    public override void OnJoinedRoom()
    {
        CloseMenu();
        roomPanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        ListAllPlayer();
    }
    public void JoinRoom(RoomInfo inputinfo)
    {
        PhotonNetwork.JoinRoom(inputinfo.Name);
        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "�� ������";
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;


        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
    }
    /// /////////////////// ���� �� ���� �Լ� ////////////////////



    public void MoveWait()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["team"] = 0;
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
    }
    public void MoveTeam1()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        if (!cp["team"].Equals(1))
        {
            if (cp2["team1"].Equals(1))
            {
                TMP_Text newText = Instantiate(roomNoticeText, roomNoticeText.transform.parent);
                newText.text = "1���� ������!";
                newText.gameObject.SetActive(true);

                Destroy(newText, 2f);
            }
            else
            {
                cp["team"] = 1;
                PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
            }
        }
    }
    public void MoveTeam2()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        if (!cp["team"].Equals(2))
        {
            if (cp2["team2"].Equals(1))
            {
                TMP_Text newText = Instantiate(roomNoticeText, roomNoticeText.transform.parent);
                newText.text = "2���� ������!";
                newText.gameObject.SetActive(true);

                Destroy(newText, 2f);
            }
            else
            {
                cp["team"] = 2;
                PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
            }
        }
    }
    public void LeaveRoomList()
    {
        CloseMenu();
        menubutton.gameObject.SetActive(true);
    }

    public void LeaveRoom()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["team"] = 0;
        PhotonNetwork.LeaveRoom();
        CloseMenu();
        loadingText.text = "���� ������ ��";
        loadingPanel.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        CloseMenu();
        menubutton.SetActive(true);
    }
    

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        ListAllPlayer();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ListAllPlayer();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListAllPlayer();
    }
    private void ListAllPlayer()
    {
        foreach (TMP_Text player in roomPlayerName)
        { 
            Destroy(player.gameObject);
        }
        roomPlayerName.Clear();
        Player[] players = PhotonNetwork.PlayerList;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        cp2["team1"] = 0;
        cp2["team2"] = 0;
        // �÷��̾� ���¸��� currentRoom ���� ����
        for(int i=0;i < players.Length; i++)
        {
            Hashtable cp = players[i].CustomProperties;
            if (cp["team"].Equals(1)) // team1
            {
                TMP_Text newPlayerLabel = Instantiate(team1PlayerLabel, team1PlayerLabel.transform.parent);
                newPlayerLabel.text = players[i].NickName;
                newPlayerLabel.gameObject.SetActive(true);
                roomPlayerName.Add(newPlayerLabel);

                cp2["team1"] = cp2["team1"].GetHashCode() + 1;
            }
            else if(cp["team"].Equals(2)) // team2
            {
                TMP_Text newPlayerLabel = Instantiate(team2PlayerLabel, team2PlayerLabel.transform.parent);
                newPlayerLabel.text = players[i].NickName;
                newPlayerLabel.gameObject.SetActive(true);
                roomPlayerName.Add(newPlayerLabel);

                cp2["team2"] = cp2["team2"].GetHashCode() + 1;
            }
            else // ���
            {
                TMP_Text newPlayerLabel = Instantiate(waitPlayerLabel, waitPlayerLabel.transform.parent);
                newPlayerLabel.text = players[i].NickName;
                newPlayerLabel.gameObject.SetActive(true);
                roomPlayerName.Add(newPlayerLabel);
            }
            
        }
        if (cp2["team1"].Equals(1) && cp2["team2"].Equals(1)) 
        {
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
            else
            {
                startButton.SetActive(false);
            }
        }
        else { startButton.SetActive(false); }
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
