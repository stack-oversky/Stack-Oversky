using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //�̵��ӵ�
    public float moveSpeed = 4f;
    public float moveSpeed2 = 4f;
    //�� ������
    public GameObject block_L;
    public GameObject block_R;
    public GameObject blockPrefab;
    public GameObject seesaw;

    public TMP_Text uiText;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public float score = 0;

    //�ð� ���� ����
    public float spawnSpan = 0.5f;//���� ������
    public float delaySpan = 0.1f;//������ ������
    public float itemSpan = 3f;//������ ���ӽð�(3��)
    public float itemTime = 5f;
    //������ ������
    float delayDelta1 = 2f;
    float delayDelta2 = 2f;
    //���� ������
    float delta1 = 0f;
    float delta2 = 0f;
    // ���� �ð�
    public float gameTime = 20f;
    //�� ����
    int sign1 = 1;
    int sign2 = -1;
    
    //������ Ʈ����
    bool stopTrigger = false;
    bool fastTrigger = false;
    bool bigTrigger1 = false;
    bool bigTrigger2 = false;

    bool slowTrigger = false;

    public Camera cam;
    public Camera otherCam;

    public List<GameObject> blocks = new List<GameObject>();
    public List<GameObject> droppedBlocks = new List<GameObject>();

    public GameObject resultPanel;
    public TMP_Text player1Score;
    public TMP_Text player2Score;
    public TMP_Text player1Name;
    public TMP_Text player2Name;

    bool isGame = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadyGame());
        StartCoroutine(CalScore());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Create();
        UseItem();
        TimeCount();
        CalScore();
    }
    public IEnumerator CalScore()
    {
        while (true)
        {
            for (int i = 0; i < droppedBlocks.Count; i++)
            {
                if (score < droppedBlocks[i].gameObject.transform.position.y + 3)
                {
                    float currentScore = droppedBlocks[i].gameObject.transform.position.y + 3;
                    currentScore = Mathf.Round(currentScore * 100) * 0.01f;
                    StartCoroutine(SetScore(currentScore));
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator SetScore(float currentScore)
    {
        float duration = 0.5f; // ī���ÿ� �ɸ��� �ð� ����. 


        float offset = (currentScore - score) / duration;
        while (score < currentScore)
        {

            score += offset * Time.fixedDeltaTime;
            score = Mathf.Round(score * 100) * 0.01f;
            scoreText.text = "Score : " + score.ToString() + "m";
            yield return null;
        }
        score = currentScore;
        scoreText.text = "Score : " + score.ToString() + "m";
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

        if(Input.GetKeyDown(KeyCode.Space) && delta1 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posL;
            delta1 = 0f;
            if(bigTrigger1)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger1 = false;
            }
            delayDelta1 = 0f;
            blocks.Add(newBlock);
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && delta2 > spawnSpan)
        {
            GameObject newBlock = Instantiate(blockPrefab) as GameObject;
            newBlock.transform.position = posR;
            newBlock.tag = "Dropping";
            delta2 = 0f;
            if (bigTrigger2)
            {
                newBlock.transform.localScale *= 1.4f;
                bigTrigger2 = false;
            }
            delayDelta2 = 0f;
            blocks.Add(newBlock);
            
        }
    }

    void TimeCount()
    {
        delta1 += Time.deltaTime;
        delta2 += Time.deltaTime;
        delayDelta1 += Time.deltaTime;
        delayDelta2 += Time.deltaTime;
        itemSpan -= Time.deltaTime;
        if (isGame)
        {
            gameTime -= Time.deltaTime;
        }
        timeText.text = string.Format("{0:N2}",gameTime);
    }

    void UseItem()
    {
        if (slowTrigger)
            moveSpeed = 2f;
        else if (fastTrigger)
            moveSpeed = 10f;
        if(itemSpan < 0)
        {
            fastTrigger = false;
            slowTrigger = false;
            moveSpeed = 4f;
            moveSpeed2 = 4f;
        }
    }
    public void stopBlock(int playerIndex)
    {
        if(playerIndex == 1)
        {
            moveSpeed = 0f;
        }
        else if(playerIndex == 2)
        {
            moveSpeed2 = 0f;
        }
        itemSpan = itemTime;
    }
    public void fastBlock(int playerIndex)
    {
        if(playerIndex == 1) 
        {
            moveSpeed = 10f;
        }
        else if (playerIndex == 2)
        {
            moveSpeed2 = 10f;
        }
        itemSpan = itemTime;
    }
    public void bigBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            bigTrigger1 = true;
        }
        else if (playerIndex == 2)
        {
            bigTrigger2 = true;
        }
    }
    public void slowBlock(int playerIndex)
    {
        if (playerIndex == 1)
        {
            moveSpeed = 2f;
        }
        else if (playerIndex == 2)
        {
            moveSpeed2 = 2f;
        }
        itemSpan = itemTime;
    }
    public void GameEnd(string score1,string score2,string player1,string player2)
    {
        resultPanel.SetActive(true);
        StartCoroutine(CountScore(score1, score2));
        player1Name.text = player1;
        player2Name.text = player2;
    }
    public IEnumerator CountScore(string score1,string score2)
    {
        float duration = 3f; // ī���ÿ� �ɸ��� �ð� ����. 

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
        while(currentScore1 <= player1ScoreFloat)
        {
            currentScore1 += offset1 * Time.fixedDeltaTime;
            player1Score.text = currentScore1.ToString("F2") + "m";
        }
        while (currentScore2 <= player2ScoreFloat)
        {
            currentScore2 += offset2 * Time.fixedDeltaTime;
            player2Score.text = currentScore2.ToString("F2") + "m";
        }
        currentScore1 = player1ScoreFloat;
        currentScore2 = player2ScoreFloat;
        player1Score.text = currentScore1.ToString("F2") + "m";
        player2Score.text = currentScore2.ToString("F2") + "m";
    }
}
