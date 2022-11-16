using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject User_block; //기본 블록 오브젝트 불러오기
    public GameObject blockContainer;


    //모든 블록프리팹 제거
    public void DestroyAllBlocks()
    {
        var blocks = new List<GameObject>();
        foreach (Transform child in blockContainer.transform) blocks.Add(child.gameObject);
        blocks.ForEach(child => Destroy(child));
    }


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

        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyAllBlocks();
            
            User_block.transform.position = new Vector3(0, 2.6f, 0);
            User_block.GetComponent<user1_block>().cnt = 0;
            User_block.GetComponent<user1_block>().cnt_drop = 0;
            User_block.GetComponent<user1_block>().cnt_drop_check = 1;


        }

    }
    
}
