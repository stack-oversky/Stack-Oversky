using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockmotion : MonoBehaviour
{
    //떨어지는 속도
    public float blockspeed = 3;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //drop direction
        Vector3 dir = Vector3.down;
        // block position
        transform.position += dir * blockspeed * Time.deltaTime;
    }
}