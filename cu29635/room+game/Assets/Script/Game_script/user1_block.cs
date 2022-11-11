using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user1_block : MonoBehaviour
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.1f;
    public float speed = 0.05f;
    public float up = 10;
    Rigidbody2D rig2d;
    int k = 1;
    GameObject block;
    public int cnt = 0;

    // Start is called before the first frame update

    private void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            block = Instantiate(blockFactory);
            //rig2d.AddForce(new Vector2(0,speed), ForceMode2D.Force);
            cnt = cnt + 1;
            k = 0;
            block.transform.position = blockPosition.transform.position; 

        }
        else
        {
            if (this.transform.position.x > start.x + 1.5f || this.transform.position.x < start.x - 1.5f)
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
        }
        if (cnt > 10&&k==0)
        {
            //this.transform.position += new Vector3(0, 1, 0);
            for(int i = 0; i < 100; i++)
            {
                this.transform.position += new Vector3(0, up * Time.deltaTime, 0);
            }
            k = 1;
            
            //rig2d.AddForce(new Vector2(start.x,1), ForceMode2D.Force);

        }

    }
   


    void Start()
    {
        rig2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
