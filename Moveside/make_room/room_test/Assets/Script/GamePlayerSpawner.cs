using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GamePlayerSpawner : MonoBehaviour
{
    public static GamePlayerSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    public GameObject playerPrefab;
    private GameObject player;

    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }
    public void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name,Vector3.zero,Quaternion.identity);
    }

}
