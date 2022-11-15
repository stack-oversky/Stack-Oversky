using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float dropSpeed = 4f;
    private Rigidbody2D rigid;
    bool drop = true;

    private GameObject gameManagerObject;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();


        this.tag = "Dropping";
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Dropper();
        Remover();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        drop = false;
        rigid.constraints = RigidbodyConstraints2D.None;
        this.tag = "Dropped";
    }

    void Dropper()
    {
        if (drop)
        {
            this.transform.position -= new Vector3(0, dropSpeed * Time.deltaTime, 0);
            this.tag = "Dropping";
        }
    }
    void Remover()
    {
        if (this.transform.position.y <= -10)
        {
            gameManager.blocks.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
