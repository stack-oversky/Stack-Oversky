using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countTime : MonoBehaviour
{
    

    //게임시작 후 시간 카운트 변수
    float sec;
    int min=0;
    [SerializeField]
    public Text TimerText;


    void Start()
    {
        //myScore = GameObject.Find("user1_block").GetComponent<Text>();
    }

    void Update()
    { 
        Timer();
        
    }

   

    //게임 시작 후 시간 카운트 하는 함
    void Timer()
    {
        sec += Time.deltaTime;

        TimerText.text = string.Format("{0:D2} : {1:D2}", min, (int)sec);

        if ((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

}
