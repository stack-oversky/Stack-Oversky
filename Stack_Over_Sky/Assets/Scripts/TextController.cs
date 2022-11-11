using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    TextMeshProUGUI countText;
    private int countBlock;
    // Start is called before the first frame update
    void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
        countBlock = GameOverFlip.dead;
    }

    // Update is called once per frame
    void Update()
    {
        countBlock = GameOverFlip.dead;
        countText.text = "Dead Blocks : " + countBlock.ToString();
    }
}
