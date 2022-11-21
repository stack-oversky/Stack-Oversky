using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Camera camera;
    private Color color;

    private RaycastHit2D hit;

    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        // Player���� �÷��� �������� ����
        int a = Random.Range(0, 10);
        int b = Random.Range(0, 10);
        int c = Random.Range(0, 10);
        color = new Color(a / 10f, b / 10f, c / 10f);
        camera = Camera.main;
        PV = photonView;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // object Ŭ���� �Լ� �ڼ��Ѱ� squareScript����
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Debug.Log(worldPoint);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null)
                {
                    GameObject clickObject = hit.collider.gameObject;
                    clickObject.GetComponent<SquareScript>().Click(PhotonNetwork.LocalPlayer,color);
                }

            }
        }
    }

    public void Click1()
    {

    }

    [PunRPC]
    public void ClickObject()
    {
        GetComponent<Renderer>().material.color = color;
    }

    public void changeColor(GameObject gameObject)
    {
        Debug.Log(gameObject.gameObject.name);
        gameObject.GetComponent<Renderer>().material.color = color;
    }


}
