using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject User_block; //기본 블록 오브젝트 불러오기 
   // public float speed =GameObject.Find("user1").GetComponent<user1_block>().speed; //기본 블록 오브젝트 스피드 변수 불러오기

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            User_block.GetComponent<user1_block>().speed *= 1.1f;
           // GameObject.Find("user1").GetComponent<user1_block>().speed = GameObject.Find("user1").GetComponent<user1_block>().speed * 1.1f;
            //Debug.Log(GameObject.Find("user1").GetComponent<user1_block>().speed);
        }
            

        if (Input.GetKeyDown(KeyCode.W))
        {
            User_block.GetComponent<user1_block>().speed *= 0.9f;
            //GameObject.Find("user1").GetComponent<user1_block>().speed = GameObject.Find("user1").GetComponent<user1_block>().speed * 0.9f;
            //Debug.Log(GameObject.Find("user1").GetComponent<user1_block>().speed);

        }

    }
}
