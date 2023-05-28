using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    [SerializeField] Image countImg;
    [SerializeField] TextMeshProUGUI countText;

    float time;
    float easingTime;
    bool isEasing = false;
    int a = 4;

    [SerializeField] int gameType;
    void Update()
    {
        if (gameObject.activeSelf)
        {
            time += Time.deltaTime;

            if (time >= 1f)
            {
                countImg.gameObject.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeInSine);
                a--;
                countText.text = a.ToString();
                isEasing = true;

                time = 0f;
            }
            if (isEasing)
            {
                easingTime += Time.deltaTime;
            }

            if (easingTime >= 0.99f)
            {
                easingTime = 0f;


                countImg.gameObject.LeanScale(Vector3.zero, 0f);
                isEasing = false;
            }

            if (a == 0)
            {
                if (gameType == 1)
                {
                    BladeMakeMiniGameStart();
                } else
                {
                    PolishingMiniGameStart();
                }
            }
        }
    }

    public void Init()
    {
        time = 0f;
        easingTime = 0f;
        a = 0;
        countImg.gameObject.LeanScale(Vector3.zero, 0f);
    }

    [SerializeField] GameObject bladeMakeMinigameImg;
    public void BladeMakeMiniGameStart()
    {
        gameObject.SetActive(false);
        //swordTypeSelectPopup.gameObject.SetActive(false);
        bladeMakeMinigameImg.SetActive(true);
        if (bladeMakeMinigameImg.TryGetComponent<CastGame>(out CastGame castGame))
        {
            castGame.InitGame();
            /*if (castGame.isStarted)
            {
                castGame.gameObject.SetActive(false);
                castGame.isStarted = !castGame.isStarted;
            }*/
        }
    }

    [SerializeField] GameObject polishingMinigameImg;

    public void PolishingMiniGameStart()
    {
        gameObject.SetActive(false);
        polishingMinigameImg.SetActive(true);
        if(polishingMinigameImg.TryGetComponent<PolishGame>(out PolishGame polishgame))
        {
            polishgame.GameStart();
        }
    }
}
