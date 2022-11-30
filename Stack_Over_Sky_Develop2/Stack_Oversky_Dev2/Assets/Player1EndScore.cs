using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player1EndScore : MonoBehaviour
{
    public int user1Score;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public Text Max_Score;
    public GameObject score1;
    public GameObject score2;
    public GameObject score3;
    public GameObject score4;

    int s1;
    int s2;
    int s3;
    int s4;

    int maxScore;
    public GameObject maxUser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        s1 = score1.GetComponent<user1_block_prev>().score;
        s2 = score2.GetComponent<user1_block_prev>().score;
        s3 = score3.GetComponent<user1_block_prev>().score;
        s4 = score4.GetComponent<user1_block_prev>().score;

        player1.text = score1.GetComponent<user1_block_prev>().score.ToString();
        player2.text = score2.GetComponent<user1_block_prev>().score.ToString();
        player3.text = score3.GetComponent<user1_block_prev>().score.ToString();
        player4.text = score4.GetComponent<user1_block_prev>().score.ToString();

        
        if(s1>=s2 && s1>= s3 && s1 >= s4)
        {
            maxScore = s1;
            maxUser = score1;
        }

        else if (s1 >= s2 && s1 >= s3 && s1 >= s4)
        {
            maxScore = s2;
            maxUser = score2;
        }

        else if (s1 >= s2 && s1 >= s3 && s1 >= s4)
        {
            maxScore = s3;
            maxUser = score3;
        }

        else
        {
            maxScore = s4;
            maxUser = score4;
        }

        Max_Score.text = maxUser.GetPhotonView().Owner.NickName + " : " + maxScore.ToString();
    }
}
