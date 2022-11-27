using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour
{
    //쌓은 블록 수 기록하는 변수
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
        score = GameObject.Find("user1").GetComponent<user1_block>().cnt
            - GameObject.Find("user1").GetComponent<user1_block>().cnt_drop;
        
        myScore.text = string.Format("Score : {0:D1}", score); //myScore.text는 myScore을 편집한다는 의미. string.Format은 해당 형식으로 문자열 저장
    }

}
