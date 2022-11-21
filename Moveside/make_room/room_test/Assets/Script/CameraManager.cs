using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float upSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dropped")
        {
            this.transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dropped")
        {
            this.transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);
        }
    }
}
