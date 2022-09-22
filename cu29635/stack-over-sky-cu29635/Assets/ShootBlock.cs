using UnityEngine;

public class ShootBlock : MonoBehaviour
{
    public GameObject blockFactory; //blockFactoy
    public GameObject blockPosition; //blockPosition
    public Vector3 start; //start position
    public float move = 0.5f; //move
    public float speed = 0.1f; //

    //block motion [left to right / drop]
    void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //enter key pressed
        {
            //drop
            GameObject block = Instantiate(blockFactory); //make block
            block.transform.position = blockPosition.transform.position; //new block position
        }
        else 
        {   //block moves left to right
            if (this.transform.position.x > start.x + 3 || this.transform.position.x < start.x - 3) //범위는 시작 지점으로부터 +-2
                move *= -1; 
            this.transform.position += new Vector3(speed * move, 0, 0); //block position change
        }
    }

    void Start()
    {
        start = this.transform.position; //block start position;
    }

    // Update is called once per frame
    void Update()
    {
        BlockMove();
    }
}
