using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BladeMakeController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject bladeCamera;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] Button ingotSelectButtonImg;

    [SerializeField] Image selectedIngotImg;
    public void ExitUI()
    {
        Color newColor = fadeImg.color;
        newColor.a = 1f;
        fadeImg.color = newColor;

        gameObject.SetActive(false);
        particle.SetActive(true);
        player.SetActive(true);
        bladeCamera.SetActive(false);
        hammer.SetActive(true);

        if (ingotSelectButtonImg.TryGetComponent<Image>(out Image img))
        {
            img.sprite = null;
        }
    }

    public void InitUI()
    {
        gameObject.SetActive(true);
        hammer.SetActive(false);
        particle.SetActive(false);
        bladeCamera.SetActive(true);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        explaneFrame.gameObject.SetActive(true);
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

    //�ֱ� ���� �˾�â ����

    [SerializeField] Image ingotSelectPopup;
    [SerializeField] GameObject content;

    public static int selectedIngotIndex = -1;
    public void PopupOpen()
    {
        ingotSelectPopup.gameObject.SetActive(!ingotSelectPopup.gameObject.activeSelf);

        for(int i = 0; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (GameManager.playerdata.ingotData[i].Amount < 1)
            {
                break;
            }
            else
            {
                content.transform.GetChild(i).gameObject.SetActive(true);

                if (content.transform.GetChild(i).GetChild(0).GetChild(0).TryGetComponent<Image>(out Image ingotImg))        //주괴 이미지 변경
                {
                    ingotImg.sprite = Resources.Load<Sprite>(GameManager.playerdata.ingotData[i].UID);
                }


                if (content.transform.GetChild(i).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))        //주괴 이름 텍스트 변경
                {
                    for (int j = 0; j < GameManager.TableDB.IngotDB.Count; j++)
                    {
                        if (GameManager.TableDB.IngotDB[j].ingotID.Equals(GameManager.playerdata.ingotData[i].UID))
                        {
                            nameText.text = GameManager.TableDB.IngotDB[j].ingotName;
                        }
                    }
                }

                if (content.transform.GetChild(i).GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI additiveText))   //주괴 첨가물 텍스트 변경
                {
                    if (!string.IsNullOrEmpty(GameManager.playerdata.ingotData[i].Additive_UID))
                    {
                        for (int j = 0; j < GameManager.TableDB.MaterialDB.Count; j++)
                        {
                            if (GameManager.TableDB.MaterialDB[j].itemID.Equals(GameManager.playerdata.ingotData[i].Additive_UID))
                            {
                                additiveText.text = "<color=purple>" + GameManager.TableDB.MaterialDB[j].itemName + " 첨가</color>";
                            }
                        }
                    }
                }

                if (content.transform.GetChild(i).GetChild(3).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI valueText))   //주괴 가격 텍스트 변경
                {
                    int totalValue = 0;
                    if (!string.IsNullOrEmpty(GameManager.playerdata.ingotData[i].Additive_UID))        //첨가제 체크
                    {
                        for (int j = 0; j < GameManager.TableDB.MaterialDB.Count; j++)
                        {
                            if (GameManager.TableDB.MaterialDB[j].itemID.Equals(GameManager.playerdata.ingotData[i].Additive_UID))
                            {
                                totalValue += GameManager.TableDB.MaterialDB[j].value;
                            }
                        }
                    }

                    for (int j = 0; j < GameManager.TableDB.IngotDB.Count; j++)
                    {
                        if (GameManager.TableDB.IngotDB[j].ingotID.Equals(GameManager.playerdata.ingotData[i].UID))
                        {
                            totalValue += GameManager.TableDB.IngotDB[j].value;
                        }
                    }

                    valueText.text = totalValue.ToString();
                }


                if (content.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText)) //수량 텍스트 변경
                {
                    amountText.text = GameManager.playerdata.ingotData[i].Amount.ToString();
                }

            }
        }
    }

    //�� ���� ���� �˾�â
    [SerializeField] Image swordTypeSelectPopup;
    [SerializeField] Image explaneFrame;            //���� �˾�â

    public static int selectedSwordTypeIndex = -1;

    [SerializeField] GameObject[] swordTypes;
    [SerializeField] Button[] swordTypeSelectBtns;
    public void SwordTypeSelectPopupOpen()
    {
        if(selectedIngotIndex != -1)
        {
            explaneFrame.gameObject.SetActive(false);
            swordTypeSelectPopup.gameObject.SetActive(true);
        }
    }

    public void TypeSelect(int index)
    {
        selectedSwordTypeIndex = index;
        Debug.Log(index);

        for (int i = 0; i < swordTypes.Length; i++)
        {
            if(swordTypes[i].TryGetComponent<Outline>(out Outline outline))
            {
                if(i == index)
                {
                    outline.enabled = true;            
                } else
                {
                    outline.enabled = false;
                }
            }
        }
    }

    private void Awake()
    {
        for (int i = 0; i < swordTypeSelectBtns.Length; i++)
        {
            int index = i;
            swordTypeSelectBtns[i].onClick.AddListener(() => this.TypeSelect(index));
        }
    }

    //�̴ϰ��� ����

    [SerializeField] GameObject countDownPopup;

    public void ActiveCountDownPopup()
    {
        swordTypeSelectPopup.gameObject.SetActive(false);
        countDownPopup.SetActive(true);
    }

    //����˾�

    [SerializeField] Image resultPopup;

    [SerializeField] TextMeshProUGUI bladeName;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI classText;

    [SerializeField] Image bladeImg;
    public void ActiveResultPopup(int score)
    {
        resultPopup.gameObject.SetActive(true);

        int ingotIndex = -1;
        string rating = "";
        
        for(int i = 0; i < GameManager.TableDB.IngotDB.Count; i++)
        {
            if (GameManager.TableDB.IngotDB[i].ingotID.Equals(GameManager.playerdata.ingotData[selectedIngotIndex].UID))
            {
                ingotIndex = i;
            }
        }

        scoreText.text = "점수: " + score.ToString() + "점";

        if (score <= 4)
        {
            classText.text = "SS급\t: +10 %\nS급\t: +20 %\nA급\t: +70 %";
            rating = "A";
        }
        else if (score <= 12)
        {
            classText.text = "SS급\t: +15 %\nS급\t: +30 %\nA급\t: +55 %";
            rating = "S";
        }
        else
        {
            classText.text = "SS급\t: +25 %\nS급\t: +35 %\nA급\t: +40 %";
            rating = "SS";
        }

        switch (selectedSwordTypeIndex)
        {
            case 0:
                bladeName.text = GameManager.TableDB.IngotDB[ingotIndex].ingotName.Replace("주괴", "") + "롱 소드" + " 날";
                bladeImg.sprite = Resources.Load<Sprite>(BladeType.LongSword.ToString());
                GameManager.BladeDataChange(GameManager.playerdata.ingotData[selectedIngotIndex].UID, (int)BladeType.LongSword, rating, GameManager.playerdata.ingotData[selectedIngotIndex].Additive_UID);
                break;
            case 1:
                bladeName.text = GameManager.TableDB.IngotDB[ingotIndex].ingotName.Replace("주괴", "") + "레이피어" + " 날";
                bladeImg.sprite = Resources.Load<Sprite>(BladeType.Rapier.ToString());
                GameManager.BladeDataChange(GameManager.playerdata.ingotData[selectedIngotIndex].UID, (int)BladeType.Rapier, rating, GameManager.playerdata.ingotData[selectedIngotIndex].Additive_UID);
                break;
            case 2:
                bladeName.text = GameManager.TableDB.IngotDB[ingotIndex].ingotName.Replace("주괴", "") + "블레이드" + " 날";
                bladeImg.sprite = Resources.Load<Sprite>(BladeType.Blade.ToString());
                GameManager.BladeDataChange(GameManager.playerdata.ingotData[selectedIngotIndex].UID, (int)BladeType.Blade, rating, GameManager.playerdata.ingotData[selectedIngotIndex].Additive_UID);
                break;
            case 3:
                bladeName.text = GameManager.TableDB.IngotDB[ingotIndex].ingotName.Replace("주괴", "") + "숏 소드" + " 날";
                bladeImg.sprite = Resources.Load<Sprite>(BladeType.ShortSword.ToString());
                GameManager.BladeDataChange(GameManager.playerdata.ingotData[selectedIngotIndex].UID, (int)BladeType.ShortSword, rating, GameManager.playerdata.ingotData[selectedIngotIndex].Additive_UID);
                break;
        }

        
    }
}
