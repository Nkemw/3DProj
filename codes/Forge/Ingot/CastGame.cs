using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastGame : MonoBehaviour
{
    [SerializeField] Button[] btns;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Image ingotImg;

    [SerializeField] GameObject controller;

    private int btnCount;
    private int btnCorrectClick;
    private void Start()
    {

        for (int i = 0; i < btns.Length; i++)
        {
            int temp = i;
            btns[temp].onClick.AddListener(() => this.BtnClickEvent(temp));
        }

        //InitGame();

    }

    public float time = 0;
    public bool isStarted = false;
    private void Update()
    {
        if (isStarted)
        {
            time += Time.deltaTime;

            timeText.text = (10f - time).ToString("F1") + "초";
            if (time >= 10f)
            {
                gameObject.SetActive(false);
                ingotImg.gameObject.SetActive(false);
                isStarted = false;

                if(controller.TryGetComponent<BladeMakeController>(out BladeMakeController con))
                {
                    con.ActiveResultPopup(btnCorrectClick);
                }
            }
        }
    }
    public void InitGame()
    {
        ingotImg.gameObject.SetActive(true);
        gameObject.SetActive(true);
        btnCount = 0;
        btnCorrectClick = 0;

        time = 0;
        isStarted = true;

        scoreText.text = btnCorrectClick.ToString();

        for (int i = 0; i < btns.Length; i++)
        {
            BtnCreate(i, i + 1);
        }
       
    }

    public void BtnClickEvent(int index)
    {
        //Debug.Log("버튼 인덱스: " + index + "  버튼카운트: " + btnCount);
        if (index == btnCount % 3)
        {
            btnCorrectClick++;
            btnCount++;

            BtnCreate(index, 3);
        }
        else
        {
            btnCorrectClick--;
        }

        scoreText.text = btnCorrectClick.ToString();

        Debug.Log("점수: " + btnCorrectClick);
    }

    public void BtnCreate(int index, int offset)
    {
        bool isOverlap = false;
        if (btns[index].transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            text.text = (btnCount + offset).ToString();
        }

        while (true)
        {
            isOverlap = false;
            if (btns[index].TryGetComponent<RectTransform>(out RectTransform rect))
            {
                rect.anchoredPosition = new Vector2(Random.Range(-310f, 310f), Random.Range(-150f, 150f));
            }

            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[index].TryGetComponent<RectTransform>(out RectTransform newRect) && index != i)
                {
                    if (btns[i].TryGetComponent<RectTransform>(out RectTransform oldRect))
                    {
                        if (((newRect.anchoredPosition.x - newRect.rect.width / 2 <= oldRect.anchoredPosition.x + oldRect.rect.width / 2 && newRect.anchoredPosition.x - newRect.rect.width/2 >= oldRect.anchoredPosition.x - oldRect.rect.width/2)||
                            (newRect.anchoredPosition.x + newRect.rect.width / 2 <= oldRect.anchoredPosition.x + oldRect.rect.width / 2 && newRect.anchoredPosition.x + newRect.rect.width / 2 >= oldRect.anchoredPosition.x - oldRect.rect.width / 2)) &&
                            ((newRect.anchoredPosition.y - newRect.rect.height / 2 <= oldRect.anchoredPosition.y + oldRect.rect.height / 2 && newRect.anchoredPosition.y - newRect.rect.height / 2 >= oldRect.anchoredPosition.y - oldRect.rect.height / 2) ||
                            (newRect.anchoredPosition.y + newRect.rect.height / 2 <= oldRect.anchoredPosition.y + oldRect.rect.height / 2 && newRect.anchoredPosition.y + newRect.rect.height / 2 >= oldRect.anchoredPosition.y - oldRect.rect.height / 2)))
                        {
                            isOverlap = true;
                        }
                    }
                    
                }
            }

            if (!isOverlap)
            {
                break;
            }
            
        }
    }
}
