using UnityEngine;

public class ShootBlock : MonoBehaviour
{
    public GameObject blockFactory;
    public GameObject blockPosition;
    public Vector3 start;
    public float move = 0.5f;
    public float speed = 0.1f;

    // Start is called before the first frame update

    void BlockMove()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject block = Instantiate(blockFactory);
            block.transform.position = blockPosition.transform.position;
        }
        else
        {
            if (this.transform.position.x > start.x + 3 || this.transform.position.x < start.x - 3) //범위는 시작 지점으로부터 +-2
                move *= -1;
            this.transform.position += new Vector3(speed * move, 0, 0);
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
