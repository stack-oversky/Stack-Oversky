using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//prefab ?μ ?λ ?€ν¬λ¦?
public class user : MonoBehaviour
{
    int con = 0;



    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * 0);//?μ?????¨μ΄μ§???λ 0
        con = 2;
        //cnt_drop = 0;
    }
   
    void Update()
    {
        Rigidbody2D myRigidbody = GetComponent<Rigidbody2D>();
        this.GetComponent<SpriteRenderer>().color = Color.blue; //λΈλ‘ ?¨μ΄?Έλ¦¬λ©??κΉ λ³?κ²

        // myRigidbody.useGravity = true; //?¨μ΄μ§???λ ?Όμ   *

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "rm")
        {
            GameObject.Find("user1").GetComponent<user1_block>().cnt_drop++;
            Destroy(gameObject);
        }

        //if (con != 0) cu29 μ½λ μ£Όμμ²λ¦¬
        //{
        //con = 0;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //GetComponent<Transform>().position += new Vector3(0, 0, 0);
        // Debug.Log(con);
        //}


        //score ?¨μ΄μ§???κ°???λ???μΌ ???μ ?λ°?΄νΈ ?κΈ° ?ν΄ ?κ·Έ μ§
        //if (collision.collider.gameObject.CompareTag("Ground"))
        //{
        //    GameObject.Find("user1").GetComponent<user1_block>().cnt++;
        //}

        //else if (collision.collider.gameObject.CompareTag("Add"))
        //{
        //    GameObject.Find("user1").GetComponent<user1_block>().cnt++;
        //}

        //else if (collision.collider.gameObject.CompareTag("Drop"))
        //{
        //    GameObject.Find("user1").GetComponent<user1_block>().cnt_drop++;
        //    Destroy(gameObject);
        //}

    }
}