using UnityEngine;

public class ShootBlock_left : MonoBehaviour
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.5f;
    public float speed = 0.1f;
   

    void BlockMove()

    {   //block motion [left to right / drop]
        if (Input.GetKeyDown(KeyCode.Space)) //Space key pressed
        {
            GameObject block = Instantiate(blockFactory);
            block.transform.position = blockPosition.transform.position;
        }
        else
        {   //block moves left to right
            if (this.transform.position.x > start.x + 3 || this.transform.position.x < start.x - 3) //+-3
                move *= -1;
            this.transform.position -= new Vector3(speed * move, 0, 0);
        }
    }

    void Start()
    {
        start = this.transform.position; //start position
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
