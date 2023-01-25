using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public Text MyScoreText;
    private int ScoreNum;

    // Start is called before the first frame update
    void Start()
    {

        ScoreNum = 0;
        MyScoreText.text = "Score : " + ScoreNum;
    }

    private void OnTriggerEnter2D(Collider2D cheese)
    {
        if (cheese.tag == "MyCheese")
        {
            ScoreNum += 1;
            Destroy(cheese.gameObject);
            MyScoreText.text = "Score : " + ScoreNum;
        }
        if (cheese.tag == "Big cheese")
        {
            ScoreNum += 5;
            Destroy(cheese.gameObject);
            MyScoreText.text = "Score : " + ScoreNum;
        }
    }
}
