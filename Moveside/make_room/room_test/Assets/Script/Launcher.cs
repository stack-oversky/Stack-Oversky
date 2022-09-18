using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.Jobs;

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
    public RoomButton joinRoomButton;
    public RoomButton[] joinRoomButtoon;
    private List<RoomButton> allJoinRoom = new List<RoomButton>();
    public TMP_Text joinRoomText;
    private bool[] isRoomExist = new bool[3];
    private RoomInfo[] roominfo = new RoomInfo[3];
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public GameObject loadingPanel;
    public TMP_Text loadingText;

    public GameObject roomPanel;
    public TMP_Text roomName;

    public GameObject nameInputPanel;
    public TMP_InputField nameInput;
    private bool hasNickname;
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
    }
    void CloseMenu()
    {
        menubutton.SetActive(false);
        roomListPanel.SetActive(false);
        loadingPanel.SetActive(false);
        nameInputPanel.SetActive(false);
        roomPanel.SetActive(false);
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

        PhotonNetwork.CreateRoom("2", options);

        CloseMenu();
        loadingPanel.SetActive(true);
        loadingText.text = "방 생성중";
        isRoomExist[2] = true;
    }
    public override void OnJoinedRoom()
    {
        CloseMenu();
        roomPanel.SetActive(true);
        roomName.text = PhotonNetwork.CurrentRoom.Name + "번 방 ";
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
    }
    public void LeaveRoom()
    {
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
        foreach(RoomButton rb in allJoinRoom)
        {
            Destroy(rb.gameObject);
        }
        allJoinRoom.Clear();


        foreach(KeyValuePair<string, RoomInfo> roomInfo in cachedRoomList)
        {
            int index = int.Parse(roomInfo.Key);
            joinRoomButtoon[index].SetButtonDetails(roomInfo.Value);
            joinRoomButtoon[index].gameObject.SetActive(true);
            createRoomButton[int.Parse(roomInfo.Key)].SetActive(false);
            allJoinRoom.Add(joinRoomButtoon[index]);
            /*
            RoomButton newButton = Instantiate(joinRoomButton, joinRoomButton.transform.parent);
            newButton.SetButtonDetails(roomInfo.Value);
            newButton.gameObject.SetActive(true);
            createRoomButton[int.Parse(roomInfo.Key)].SetActive(false);
            allJoinRoom.Add(newButton);
            */
        }
    }
}
