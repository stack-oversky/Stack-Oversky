using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject block;

    private void Update()
    {
        Vector3 dir = block.transform.position - this.transform.position + new Vector3(0, -2.6f, 0);
        Vector3 moveVector = new Vector3(0, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }

}