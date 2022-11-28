using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public static PlayerSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject playerPrefab;
    private GameObject player;
  
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }

    }

    public void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }
}


