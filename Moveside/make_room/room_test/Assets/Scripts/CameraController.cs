using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Camera cam;          //ī�޶�

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Drop �±��� ���� �ݶ��̴��� ������� ī�޶� ���� �̵�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            cam.transform.position += new Vector3(0, 0.02f, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            cam.transform.position += new Vector3(0, 0.2f, 0);
        }
    }
}
