using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour
{
    
    public Text myScore;
    int score = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreUpdate();   
    }

    void scoreUpdate()
    {
        score = GameObject.Find("user1").GetComponent<user1_block_prev>().cnt
            - GameObject.Find("user1").GetComponent<user1_block_prev>().cnt_drop;
        
        myScore.text = string.Format("Score : {0:D1}", score); //myScore.textmyScore
 
    }

}
