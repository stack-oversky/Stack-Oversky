using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject User_block;
    public GameObject blockContainer;


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
            
            //User_block.GetComponent<user1_block_prev>().speed *= 1.1f;
           // GameObject.Find("user1").GetComponent<user1_block>().speed = GameObject.Find("user1").GetComponent<user1_block>().speed * 1.1f;
            //Debug.Log(GameObject.Find("user1").GetComponent<user1_block>().speed);
        }
            

        if (Input.GetKeyDown(KeyCode.W))
        {
            //User_block.GetComponent<user1_block_prev>().speed *= 0.9f;
            //GameObject.Find("user1").GetComponent<user1_block>().speed = GameObject.Find("user1").GetComponent<user1_block>().speed * 0.9f;
            //Debug.Log(GameObject.Find("user1").GetComponent<user1_block>().speed);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyAllBlocks();
            
            User_block.transform.position = new Vector3(0, 2.6f, 0);
            User_block.GetComponent<user1_block_prev>().cnt = 0;
            User_block.GetComponent<user1_block_prev>().cnt_drop = 0;
            User_block.GetComponent<user1_block_prev>().cnt_drop_check = 1;

        }

    }
    
    public void Block_Fast()
    {
        User_block.GetComponent<user1_block>().speed *= 1.1f;
    }
}
