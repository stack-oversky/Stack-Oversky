using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class user3_block : MonoBehaviour
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.1f;
    public float speed = 0.1f;

    // Start is called before the first frame update

    private void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject block = Instantiate(blockFactory);
            block.transform.position = blockPosition.transform.position;

        }
        else
        {
            if (this.transform.position.x > start.x + 1.5f || this.transform.position.x < start.x - 1.5f)
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
        }
    }


    void Start()
    {
        start = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
