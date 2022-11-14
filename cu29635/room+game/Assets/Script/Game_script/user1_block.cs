using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    

public class user1_block : MonoBehaviour
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.1f;
    public float speed=0.05f;
    public float up = 10;
    Rigidbody2D rig2d;
    int k = 1;
    GameObject block;

    public int cnt;
    public int cnt_drop;
    public int cnt_drop_check;
    // Start is called before the first frame update

    private void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            block = Instantiate(blockFactory);
            //rig2d.AddForce(new Vector2(0,speed), ForceMode2D.Force);
            cnt=cnt+1;
            k = 0;
            block.transform.position = blockPosition.transform.position;

           
        }
        else
        {
            if (this.transform.position.x > start.x + 1.5f || this.transform.position.x < start.x - 1.5f)
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
        }


        if (cnt > 7 && k == 0)
        {
            //this.transform.position += new Vector3(0, 1, 0);
            for (int i = 0; i < 100; i++)
            {
                this.transform.position += new Vector3(0, up * Time.deltaTime, 0);
            }
            k = 1;

            //rig2d.AddForce(new Vector2(start.x,1), ForceMode2D.Force);

        }

    }

    private void CameraDown()
    {
        if (cnt_drop==cnt_drop_check)
        {
            for (int i = 0; i < 100; i++)
            {
                this.transform.position += new Vector3(0, -up * Time.deltaTime, 0);
            }
            k = 1;
            cnt_drop_check++;
        }
        
    }


    void Start()
    {
        rig2d = gameObject.GetComponent<Rigidbody2D>();
        cnt = 0;
        cnt_drop = 0;
        cnt_drop_check = 1;
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
        CameraDown();
    }
}
