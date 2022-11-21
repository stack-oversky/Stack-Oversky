using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject playerPrefab;
    private GameObject player;

    void Start()
    {
        // �÷��̾� ���� ���۽� �÷��̾� ������Ʈ ����
        if(PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }
    public void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
    // Update is called once per frame

}
