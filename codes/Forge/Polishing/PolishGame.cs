using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PolishGame : MonoBehaviour
{
    [SerializeField] Image barBackground;
    [SerializeField] Image correctRange;
    [SerializeField] Image aimBar;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject controller;
    public bool isStarted = false;
    float time = 0f;
    float direction = 1;

    int score = -10000;
    // Update is called once per frame
    void Update()
    {

        if (isStarted)
        {
            timeText.text = "남은 시간: " + (10f - time).ToString("F1") + "초";
            time += Time.deltaTime;
            if(time >= 10f)
            {
                timeText.text = 0.ToString();
                isStarted = false;
                time = 0f;
                gameObject.SetActive(false);
                Debug.Log("총 스코어: " + score);

                if(controller.TryGetComponent<PolishController>(out PolishController con))
                {
                    con.ResultPopupActive(score);
                }
                //GameObject obj = GameObject.Find("MiniGameManager");
                //obj.GetComponent<MiniGameManager>().RewardIntoInventory();

            }

            BarMove(moveSpeed);
            if(score == -10000)
            {
                CreateCorrectRange();
                score += 10000;
                //scoreText.text = "Score: " + score.ToString();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckBar();
                CreateCorrectRange();
                //scoreText.text = "Score: " + score.ToString();
            }


        }
    }

    public void GameStart()
    {
        isStarted = true;
        score = -10000;
        time = 0f;
        direction = 1;
    }

    public void CreateCorrectRange() //50 ~ 100
    {
        while (true)
        {
            if (direction < 0)
            {
                correctRange.rectTransform.anchoredPosition = new Vector2(Random.Range(direction * barBackground.rectTransform.rect.width/2, 0f), 0f);
                correctRange.rectTransform.sizeDelta = new Vector2(Random.Range(50f, 150f), 100f);

                if (!(correctRange.rectTransform.anchoredPosition.x - correctRange.rectTransform.rect.width / 2 < -barBackground.rectTransform.rect.width / 2))
                {
                    break;
                }
            } else
            {
                correctRange.rectTransform.anchoredPosition = new Vector2(Random.Range(0f, direction * barBackground.rectTransform.rect.width / 2), 0f);
                correctRange.rectTransform.sizeDelta = new Vector2(Random.Range(50f, 150f), 100f);
                if (!(correctRange.rectTransform.anchoredPosition.x + correctRange.rectTransform.rect.width / 2 > barBackground.rectTransform.rect.width / 2))
                {
                    break;
                }
            }
        }
    }
    public void BarMove(float moveSpeed)
    {
        //Mathf.Clamp(aimBar.rectTransform.anchoredPosition.x, barBackground.rectTransform.anchoredPosition.x - barBackground.rectTransform.rect.width/2, barBackground.rectTransform.anchoredPosition.x + barBackground.rectTransform.rect.width / 2);
        if(direction < 0 && aimBar.rectTransform.anchoredPosition.x - aimBar.rectTransform.rect.width/2 <= barBackground.rectTransform.anchoredPosition.x - barBackground.rectTransform.rect.width / 2)
        {
            direction *= -1;
        } else if(direction > 0 && aimBar.rectTransform.anchoredPosition.x + aimBar.rectTransform.rect.width / 2 >= barBackground.rectTransform.anchoredPosition.x + barBackground.rectTransform.rect.width / 2)
        {
            direction *= -1;
        }
        aimBar.rectTransform.anchoredPosition += new Vector2(direction *moveSpeed * Time.deltaTime, 0f);
    }
    public void CheckBar()
    {
        if ((aimBar.rectTransform.anchoredPosition.x - aimBar.rectTransform.rect.width / 2 >= correctRange.rectTransform.anchoredPosition.x - correctRange.rectTransform.rect.width / 2 &&
           aimBar.rectTransform.anchoredPosition.x - aimBar.rectTransform.rect.width / 2 <= correctRange.rectTransform.anchoredPosition.x + correctRange.rectTransform.rect.width / 2) ||
           (aimBar.rectTransform.anchoredPosition.x + aimBar.rectTransform.rect.width / 2 >= correctRange.rectTransform.anchoredPosition.x - correctRange.rectTransform.rect.width / 2 &&
           aimBar.rectTransform.anchoredPosition.x + aimBar.rectTransform.rect.width / 2 <= correctRange.rectTransform.anchoredPosition.x + correctRange.rectTransform.rect.width / 2))
        {
            if (score < 5)
            {
                score++;
            }
            //Debug.Log("스코어 획득");
        }
        else
        {
            if (score > 0)
            {
                score--;
            }
            //Debug.Log("스코어 감점");
        }
        ScoreTextChange();
        direction *= -1;
    }

    public void ScoreTextChange()
    {
        scoreText.text = "- 공격력 + " + (score).ToString() + "\n- 내구도 + " + (score).ToString();
    }


    public void ClickEvent()
    {
        CheckBar();
        CreateCorrectRange();
    }
}
