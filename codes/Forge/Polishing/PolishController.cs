using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PolishController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject particle;
    [SerializeField] GameObject polishCamera;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject polishExplainImg;

    public void ExitUI()
    {
        Color newColor = fadeImg.color;
        newColor.a = 1f;
        fadeImg.color = newColor;

        gameObject.SetActive(false);
        particle.SetActive(true);
        player.SetActive(true);
        polishCamera.SetActive(false);
    }

    public void InitUI()
    {
        gameObject.SetActive(true);
        particle.SetActive(false);
        polishCamera.SetActive(true);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        polishExplainImg.SetActive(false);
        resultPopup.gameObject.SetActive(false);

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        yield return null;

        float startTime = 0f;
        float endTime = 0.5f;

        Color newColor = new Color();
        while (startTime / endTime < 1f)
        {
            startTime += Time.deltaTime;

            newColor.a = Mathf.Lerp(1f, 0f, startTime / endTime);
            fadeImg.color = newColor;
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
        StopCoroutine("FadeOut");
    }


    //칼날 선택
    [SerializeField] Image bladeSelectPopup;
    [SerializeField] GameObject content;

    public static int selectedBladeIndex = -1;
    public void PopupOpen()
    {
        bladeSelectPopup.gameObject.SetActive(!bladeSelectPopup.gameObject.activeSelf);

        for (int i = 0; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].IngotUID))
            {
                break;
            }
            else
            {
                content.transform.GetChild(i).gameObject.SetActive(true);

                if (content.transform.GetChild(i).GetChild(0).GetChild(0).TryGetComponent<Image>(out Image ingotImg))        //칼날 이미지 변경
                {
                    ingotImg.sprite = Resources.Load<Sprite>(GameManager.playerdata.bladeData[i].BladeType.ToString() + "Blade");
                }


                if (content.transform.GetChild(i).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))        //칼날 이름 텍스트 변경
                {
                    for (int j = 0; j < GameManager.TableDB.IngotDB.Count; j++)
                    {
                        if (GameManager.TableDB.IngotDB[j].ingotID.Equals(GameManager.playerdata.bladeData[i].IngotUID))
                        {
                            nameText.text = GameManager.TableDB.IngotDB[j].ingotName.Replace("주괴", "");

                            switch ((int)GameManager.playerdata.bladeData[i].BladeType)
                            {
                                case (int)BladeType.LongSword:
                                    nameText.text += "롱 소드 날";
                                    break;
                                case (int)BladeType.Rapier:
                                    nameText.text += "레이피어 날";
                                    break;
                                case (int)BladeType.Blade:
                                    nameText.text += "블레이드 날";
                                    break;
                                case (int)BladeType.ShortSword:
                                    nameText.text += "숏 소드 날";
                                    break;
                            }
                        }
                    }
                }

                if (content.transform.GetChild(i).GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI additiveText))   //주괴 첨가물 텍스트 변경
                {
                    if (!string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].Additive_UID))
                    {
                        for (int j = 0; j < GameManager.TableDB.MaterialDB.Count; j++)
                        {
                            if (GameManager.TableDB.MaterialDB[j].itemID.Equals(GameManager.playerdata.bladeData[i].Additive_UID))
                            {
                                additiveText.text = "<color=purple>" + GameManager.TableDB.MaterialDB[j].itemName + " 첨가</color>";
                            }
                        }
                    }
                }

                if (content.transform.GetChild(i).GetChild(3).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI valueText))   //칼날 가격 텍스트 변경
                {
                    int totalValue = 0;
                    if (!string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].Additive_UID))        //첨가제 체크
                    {
                        for (int j = 0; j < GameManager.TableDB.MaterialDB.Count; j++)
                        {
                            if (GameManager.TableDB.MaterialDB[j].itemID.Equals(GameManager.playerdata.bladeData[i].Additive_UID))
                            {
                                totalValue += GameManager.TableDB.MaterialDB[j].value;
                            }
                        }
                    }

                    for (int j = 0; j < GameManager.TableDB.IngotDB.Count; j++)
                    {
                        if (GameManager.TableDB.IngotDB[j].ingotID.Equals(GameManager.playerdata.bladeData[i].IngotUID))
                        {
                            totalValue += GameManager.TableDB.IngotDB[j].value;
                        }
                    }

                    valueText.text = totalValue.ToString();
                }


            }
        }
    }

    //미니게임 관련

    [SerializeField] GameObject countDownPopup;

    public void ActiveCountDownPopup()
    {
        polishExplainImg.gameObject.SetActive(false);
        countDownPopup.SetActive(true);
    }


    //결과 관련
    [SerializeField] Image resultPopup;
    [SerializeField] TextMeshProUGUI resultText;

    public void ResultPopupActive(int score)
    {
        resultPopup.gameObject.SetActive(true);

        resultText.text = "- 최종 공격력 보너스 : + " + score.ToString() + " (최대 5)\n\n- 최종 내구도 보너스 : + " + score.ToString() + " (최대 5)";

        GameManager.AddBladeBonusScore(selectedBladeIndex, score);
    }
}
