using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    BlockGenerator blockGen;

    public Camera cam;
    float preHeight;

    void Start()
    {
        blockGen = GameObject.Find("BlockGenerator").GetComponent<BlockGenerator>();
        preHeight = blockGen.max_height;
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 조정은 아직 미완
        if (blockGen.max_height > preHeight)
        {
            preHeight = blockGen.max_height;
            cam.orthographicSize += 0.3f;
        }
    }
}
