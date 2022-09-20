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

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject menubutton;

    public GameObject roomListPanel;
    public GameObject[] createRoomButton;
    public TMP_Text[] createRoomText;
    public RoomButton[] joinRoomButton;
    private List<RoomButton> allJoinRoom = new List<RoomButton>();
    public TMP_Text joinRoomText;
    private bool[] isRoomExist = new bool[3];
    private RoomInfo[] roominfo = new RoomInfo[3];
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public GameObject loadingPanel;
    public TMP_Text loadingText;

    public GameObject roomPanel;
    public TMP_Text roomName;
    private List<TMP_Text> roomPlayerName = new List<TMP_Text>();
    public TMP_Text team1PlayerLabel;
    public TMP_Text team2PlayerLabel;
    public TMP_Text roomNoticeText;

    public GameObject nameInputPanel;
    public TMP_InputField nameInput;
    private bool hasNickname;

    public GameObject errorPanel;
    public TMP_Text errorText;
    void Start()
    {
        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "네트워크 연결중";
        PhotonNetwork.ConnectUsingSettings();
    }

    // 네트워크 연결 성공시 실행
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        
        loadingText.text = "로비에 접속중";
    }
    // 로비 접속 성공시 실행
    public override void OnJoinedLobby()
    {
        CloseMenu();
        menubutton.SetActive(true);
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
        PhotonNetwork.LocalPlayer.CustomProperties = new Hashtable() { { "team", 1 } };
    }
    void CloseMenu()
    {
        menubutton.SetActive(false);
        roomListPanel.SetActive(false);
        loadingPanel.SetActive(false);
        nameInputPanel.SetActive(false);
        roomPanel.SetActive(false);
        errorPanel.SetActive(false);
    }
    public void OpenLobby()
    {
        CloseMenu();
        roomListPanel.SetActive(true);
    }
    public void CreateRoom1()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.CustomRoomProperties = new Hashtable() { { "team1", 0 }, { "team2",0 }};
        PhotonNetwork.CreateRoom("0", options);

        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "방 생성중";
        isRoomExist[0] = true;
    }
    public void CreateRoom2()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.CustomRoomProperties = new Hashtable() { { "team1", 0 }, { "team2", 0 } };

        PhotonNetwork.CreateRoom("1", options);
        
        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "방 생성중";
        isRoomExist[1] = true;
    }
    public void CreateRoom3()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.CustomRoomProperties = new Hashtable() { { "team1", 0 }, { "team2", 0 } };

        PhotonNetwork.CreateRoom("2", options);

        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "방 생성중";
        isRoomExist[2] = true;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CloseMenu();
        errorText.text = "에러 : " + message;
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
        roomName.text = PhotonNetwork.CurrentRoom.Name + "번 방 ";

        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp2["team1"].Equals(2))
        {
            cp2["team2"] = cp2["team2"].GetHashCode() + 1;
            cp["team"] = 2;
        }
        else
        {
            cp2["team1"] = cp2["team1"].GetHashCode() + 1;
            cp["team"] = 1;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp2);
    }
    public void TeamChange()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp["team"].Equals(1))
        {
            if (cp2["team2"].Equals(2))
            {
                roomNoticeText.text = "2팀은 가득참!";
            }
            else
            {
                cp["team"] = 2;
                cp2["team1"] = cp2["team1"].GetHashCode() - 1;
                cp2["team2"] = cp2["team2"].GetHashCode() + 1;
            }
        }
        else
        {
            if (cp2["team1"].Equals(2))
            {
                roomNoticeText.text = "1팀은 가득참!";
            }
            else
            {
                cp["team"] = 1;
                cp2["team2"] = cp2["team2"].GetHashCode() - 1;
                cp2["team1"] = cp2["team1"].GetHashCode() + 1;
            }
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        PhotonNetwork.CurrentRoom.SetCustomProperties(cp2);
        ListAllPlayer();
        TMP_Text notice = Instantiate(roomNoticeText, roomNoticeText.transform.parent);
        notice.gameObject.SetActive(true);
        Destroy(notice, 0.2f);
    }

    public void SetNickname()
    {
        if(!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            PlayerPrefs.SetString("playerName", nameInput.text);
            hasNickname = true;

            CloseMenu();
            menubutton.SetActive(true);
        }
    }
    public void JoinRoom(RoomInfo inputinfo)
    {
        PhotonNetwork.JoinRoom(inputinfo.Name);
        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "방 참가중";
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;


        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
    }

    public void LeaveRoom()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        Hashtable cp2 = PhotonNetwork.CurrentRoom.CustomProperties;
        if (cp["team"].Equals(1))
        {
            cp2["team1"] = cp2["team1"].GetHashCode() - 1;
        }
        else
        {
            cp2["team2"] = cp2["team2"].GetHashCode() - 1;
        }
        cp["team"] = 0;
        PhotonNetwork.LeaveRoom();
        CloseMenu();
        loadingText.text = "방을 떠나는 중";
        loadingPanel.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        CloseMenu();
        menubutton.SetActive(true);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i=0;i<roomList.Count;i++)
        {
            RoomInfo info = roomList[i];
            string name = info.Name;
            int index = int.Parse(name);
            if(info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
                createRoomButton[int.Parse(info.Name)].SetActive(true);
                joinRoomButton[int.Parse(info.Name)].gameObject.SetActive(false);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
        UpdateRoom(cachedRoomList);
    }
    public void UpdateRoom(Dictionary<string, RoomInfo> cachedRoomList)
    {
        foreach(KeyValuePair<string, RoomInfo> roomInfo in cachedRoomList)
        {
            int index = int.Parse(roomInfo.Key);
            joinRoomButton[index].SetButtonDetails(roomInfo.Value);
            joinRoomButton[index].gameObject.SetActive(true);
            createRoomButton[int.Parse(roomInfo.Key)].SetActive(false);
        }
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
        for(int i=0;i < players.Length; i++)
        {
            Hashtable cp = players[i].CustomProperties;
            if (cp["team"].Equals(1))
            {
                TMP_Text newPlayerLabel = Instantiate(team1PlayerLabel, team1PlayerLabel.transform.parent);
                newPlayerLabel.text = players[i].NickName;
                newPlayerLabel.gameObject.SetActive(true);
                roomPlayerName.Add(newPlayerLabel);
            }
            else if(cp["team"].Equals(2))
            {
                TMP_Text newPlayerLabel = Instantiate(team2PlayerLabel, team2PlayerLabel.transform.parent);
                newPlayerLabel.text = players[i].NickName;
                newPlayerLabel.gameObject.SetActive(true);
                roomPlayerName.Add(newPlayerLabel);
            }
            else
            {
                Debug.Log("error");
            }
            
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
