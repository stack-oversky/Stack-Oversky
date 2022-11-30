using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countTime : MonoBehaviour
{
    
    float sec;
    int min=0;
    [SerializeField]
    public Text TimerText;
    Transform endpanel;


    void Start()
    {
        endpanel = GameObject.Find("gameoverparents").transform.Find("GameOverPanel");
        //myScore = GameObject.Find("user1_block").GetComponent<Text>();
    }

    void Update()
    { 
        
        if (Nowmin() == 1)
        {
            GameObject.Find("gameoverparents").transform.Find("GameOverPanel").gameObject.SetActive(true);
           
        }
        else
        {
            Timer();
        }


    }


   int Nowmin()
    {
        return (int)min;
    }
       


    //?????? ????? ??????? ????????????? ??
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
