using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //이동속도
    public float moveSpeed = 4f;
    public float moveSpeed2 = 4f;
    //블럭 프리팹
    public GameObject block_L;
    public GameObject block_R;
    public GameObject blockPrefab;
    public GameObject blockPrefab2;

    public GameObject seesaw;
    public GameObject[] itemSprite1 = new GameObject[4];
    public GameObject[] itemSprite2 = new GameObject[4];
    public GameObject[] statusPlayer1 = new GameObject[4];
    public GameObject[] statusPlayer2 = new GameObject[4];

    public TMP_Text uiText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public float score;

    //시간 관련 변수
    public float spawnSpan = 0.5f;//생성 딜레이
    public float delaySpan = 0.1f;//움직임 딜레이
    public float itemSpan = 3f;//아이템 지속시간(3초)

    public float itemTime = 5f;
    //움직임 딜레이
    float delayDelta1 = 2f;
    float delayDelta2 = 2f;
    //생성 딜레이
    float delta1 = 0f;
    float delta2 = 0f;
    // 게임 시간
    public float gameTime = 20f;
    //블럭 방향
    int sign1 = 1;
    int sign2 = -1;
    
    //아이템 트리거
    bool stopTrigger = false;
    bool fastTrigger = false;
    bool bigTrigger1 = false;
    bool bigTrigger2 = false;

    bool slowTrigger = false;

    public Camera cam;
    public Camera otherCam;

    public List<GameObject> blocks = new List<GameObject>();
    public List<GameObject> blocks2 = new List<GameObject>();

    public List<GameObject> droppedBlocks = new List<GameObject>();

    public GameObject resultPanel;
    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public TMP_Text player1Name;
    public TMP_Text player2Name;

    bool isGame = false;



    private float itemSpan1 = 0f;
    private float stopSpan1 = 0f;
    private float fastSpan1 = 0f;
    private float slowSpan1 = 0f;
    private bool isStop1 = false;

    private float itemSpan2 = 0f;
    private float stopSpan2 = 0f;
    private float fastSpan2 = 0f;
    private float slowSpan2 = 0f;
    private bool isStop2 = false;

    private float blockY;

    public bool goLobby = false;
    public GameObject endButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadyGame());
        StartCoroutine(CalScore());
        StartCoroutine(CameraMove());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Create();
        UseItem();
        TimeCount();
    }
    public IEnumerator CameraMove()
    {
        while (true)
        {
            if (cam.transform.position.y < blockY + 1f)
            {
                cam.transform.position += new Vector3(0, Time.deltaTime, 0);
            }
            if (cam.transform.position.y > blockY - 1f)
            {
                cam.transform.position -= new Vector3(0, Time.deltaTime, 0);

            }
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator CalScore()
    {
        while (true)
        {
            for (int i = 0; i < droppedBlocks.Count; i++)
            {
                float maxY = 0;
                if (maxY < droppedBlocks[i].gameObject.transform.position.y + 3)
                {
                    maxY = droppedBlocks[i].gameObject.transform.position.y + 3;
                }
                if (maxY > score)
                {
                    maxY = Mathf.Round(maxY * 100) * 0.01f;
                    StartCoroutine(SetScore(maxY));
                }
                blockY = maxY;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
    public IEnumerator SetScore(float currentScore)
    {
        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 


        float offset = (currentScore - score) / duration;
        while (score < currentScore)
        {

            score += offset * Time.fixedDeltaTime;
            score = Mathf.Round(score * 100) * 0.01f;
            scoreText.text = score.ToString() + "m";
            yield return null;
        }
        score = currentScore;
        scoreText.text = score.ToString() + "m";
    }

    public IEnumerator ReadyGame()
    {
        Time.timeScale = 0;
        uiText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        uiText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        uiText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        uiText.text = "Start!";
        yield return new WaitForSecondsRealtime(1f);
        uiText.text = "";
        isGame = true;
        score = 0;
}
    void Move()
    {
        if(block_L.transform.position.x < -6.5)     sign1 = 1;
        else if(block_L.transform.position.x > -2)  sign1 = -1;

        if (block_R.transform.position.x > 6.5)     sign2 = -1;
        else if (block_R.transform.position.x < 2)  sign2 = 1;
       
        if (delayDelta1 > delaySpan)
            block_L.transform.position += new Vector3(moveSpeed * sign1 * Time.deltaTime, 0, 0);
        if (delayDelta2 > delaySpan)
            block_R.transform.position += new Vector3(moveSpeed2 * sign2 * Time.deltaTime, 0, 0);
    }

    void Create()
    {
        Vector3 posL = block_L.transform.position;
        Vector3 posR = block_R.transform.position;

        if(Input.GetKeyDown(KeyCode.Z) && delta1 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posL;
            delta1 = 0f;
            if(bigTrigger1)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger1 = false;
                statusPlayer1[1].gameObject.SetActive(false);
            }
            delayDelta1 = 0f;
            blocks.Add(newBlock);
        }
        if(Input.GetKeyDown(KeyCode.Slash) && delta2 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab2) as GameObject;
            newBlock.transform.position = posR;
            newBlock.tag = "Dropping";
            delta2 = 0f;
            if (bigTrigger2)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger2 = false;
                statusPlayer2[1].gameObject.SetActive(false);
            }
            delayDelta2 = 0f;
            blocks2.Add(newBlock);
            
        }
    }

    void TimeCount()
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
        delayDelta1 += Time.deltaTime;
        delayDelta2 += Time.deltaTime;
        itemSpan -= Time.deltaTime;
        // player1 time count
        if(stopSpan1>0)
        {
            stopSpan1 -= Time.deltaTime;
            if(stopSpan1 <= 0)
            {
                moveSpeed = 4f;
                statusPlayer1[0].SetActive(false);
            }
        }
        if(fastSpan1>0)
        {
            fastSpan1 -= Time.deltaTime;
            if(fastSpan1 <=0)
            {
                moveSpeed = 4f;
                statusPlayer1[2].SetActive(false);
            }
        }
        if(slowSpan1>0)
        {
            slowSpan1 -= Time.deltaTime;
            if(slowSpan1 <=0)
            {
                moveSpeed = 4f;
                statusPlayer1[3].SetActive(false);
            }
        }
        // player2 time count
        if (stopSpan2>0)
        {
            stopSpan2 -= Time.deltaTime;
            if (stopSpan2 <= 0)
            {
                moveSpeed2 = 4f;
                statusPlayer2[0].SetActive(false);
            }
        }
        if(fastSpan2>0)
        {
            fastSpan2 -= Time.deltaTime;
            if(fastSpan2<=0)
            {
                moveSpeed2 = 4f;
                statusPlayer2[2].SetActive(false);
            }
        }
        if(slowSpan2>0)
        {
            slowSpan2 -= Time.deltaTime;
            if(slowSpan2<=0)
            {
                moveSpeed2 = 4f;
                statusPlayer2[3].SetActive(false);
            }
        }


        if (isGame)
        {
            gameTime -= Time.deltaTime;
        }
        timeText.text = string.Format("{0:N2}",gameTime);
    }

    void UseItem()
    {
    }
    public void stopBlock(int playerIndex)
    {
        if(playerIndex == 1)
        {
            moveSpeed = 0f;
            stopSpan1 = itemTime;
            statusPlayer1[0].SetActive(true);
        }
        else if(playerIndex == 2)
        {
            moveSpeed2 = 0f;
            stopSpan2 = itemTime;
            statusPlayer2[0].SetActive(true);
        }
    }
    public void fastBlock(int playerIndex)
    {
        if(playerIndex == 1)
        {
            moveSpeed = 10f;
            fastSpan1 = itemTime;
            statusPlayer1[2].SetActive(true);
        }
        else if(playerIndex == 2)
        {
            moveSpeed2 = 10f;
            fastSpan2 = itemTime;
            statusPlayer2[2].SetActive(true);
        }
    }
    public void bigBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            bigTrigger1 = true;
            statusPlayer1[1].gameObject.SetActive(true);
        }
        else if (playerIndex == 2)
        {
            bigTrigger2 = true;
            statusPlayer2[1].gameObject.SetActive(true);
        }
    }
    public void slowBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            moveSpeed = 2f;
            slowSpan1 = itemTime;
            statusPlayer1[3].SetActive(true);
        }
        else if (playerIndex == 2)
        {
            moveSpeed2 = 2f;
            slowSpan2 = itemTime;
            statusPlayer2[3].SetActive(true);
        }
    }
    public void GameEnd(string score1,string score2,string player1,string player2)
    {
        resultPanel.SetActive(true);
        endButton.SetActive(false);
        StartCoroutine(CountScore(score1, score2));
        player1Name.text = player1;
        player2Name.text = player2;
    }
    public IEnumerator CountScore(string score1,string score2)
    {
        float duration = 3f; // 카운팅에 걸리는 시간 설정. 

        float player1ScoreFloat = float.Parse(score1);
        float player2ScoreFloat = float.Parse(score2);
        float offset1 = player1ScoreFloat / duration;
        float offset2 = player2ScoreFloat / duration;
        float currentScore1 = 0;
        float currentScore2 = 0;
        float offset;
        if (offset1 > offset2)
        {
            offset = offset2;
            while (currentScore2 < player2ScoreFloat)
            {

                currentScore1 += offset * Time.fixedDeltaTime;
                currentScore2 += offset * Time.fixedDeltaTime;
                player1Score.text = currentScore1.ToString("F2") + "m";
                player2Score.text = currentScore2.ToString("F2") + "m";
                yield return null;
            }
            currentScore2 = player2ScoreFloat;
            player2Score.text = currentScore2.ToString("F2") + "m";
            while (currentScore1 < player1ScoreFloat)
            {
                currentScore1 += offset1 * Time.fixedDeltaTime;
                player1Score.text = currentScore1.ToString("F2") + "m";
            }
            currentScore1 = player1ScoreFloat;
            player1Score.text = currentScore1.ToString("F2") + "m";
        }
        else
        {
            offset = offset1;
            while (currentScore1 < player1ScoreFloat)
            {

                currentScore1 += offset * Time.fixedDeltaTime;
                currentScore2 += offset * Time.fixedDeltaTime;
                player1Score.text = currentScore1.ToString("F2") + "m";
                player2Score.text = currentScore2.ToString("F2") + "m";
                yield return null;
            }
            currentScore1 = player1ScoreFloat;
            player1Score.text = currentScore1.ToString("F2") + "m";
            while (currentScore2 < player2ScoreFloat)
            {
                currentScore2 += offset2 * Time.fixedDeltaTime;
                player2Score.text = currentScore2.ToString("F2") + "m";
            }
            currentScore2 = player2ScoreFloat;
            player2Score.text = currentScore2.ToString("F2") + "m";

        }



        while (currentScore1 < player1ScoreFloat || currentScore2 < player2ScoreFloat)

        {

            currentScore1 += offset * Time.fixedDeltaTime;
            currentScore2 += offset * Time.fixedDeltaTime;
            player1Score.text = currentScore1.ToString("F2") + "m";
            player2Score.text = currentScore2.ToString("F2") + "m";
            yield return null;
        }
        while(currentScore1 < player1ScoreFloat)
        {
            currentScore1 += offset1 * Time.fixedDeltaTime;
            player1Score.text = currentScore1.ToString("F2") + "m";
        }
        while (currentScore2 < player2ScoreFloat)
        {
            currentScore2 += offset2 * Time.fixedDeltaTime;
            player2Score.text = currentScore2.ToString("F2") + "m";
        }
        currentScore1 = player1ScoreFloat;
        currentScore2 = player2ScoreFloat;
        player1Score.text = currentScore1.ToString("F2") + "m";
        player2Score.text = currentScore2.ToString("F2") + "m";
        endButton.SetActive(true);
    }
    public void EndButton()
    {
        Application.Quit();
    }
}
