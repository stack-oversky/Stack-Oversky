using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class block_motion : MonoBehaviourPunCallbacks
{
    //?®Ïñ¥ÏßÄ???çÎèÑ
    public float blockspeed = 3;

    public GameObject block;
    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.down;
        transform.position += dir * blockspeed * Time.deltaTime;
    }
}
