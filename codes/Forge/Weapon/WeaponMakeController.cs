using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponMakeController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject deskCamera;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject weaponExplainImg;

    [SerializeField] Image handleSelectedImg;
    [SerializeField] Image bladeSelectedImg;
    [SerializeField] Image resultImg;

    public static int selectedHandleIndex = -10;
    public static int selectedBladeIndex = -10;

    public void ExitUI()
    {
        Color newColor = fadeImg.color;
        newColor.a = 1f;
        fadeImg.color = newColor;



        player.SetActive(true);
        deskCamera.SetActive(false);
        fadeImg.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }

    public void InitFrameSetting()
    {
        selectedHandleIndex = -10;
        selectedBladeIndex = -10;

        handleSelectedImg.sprite = null;
        bladeSelectedImg.sprite = null;
        resultImg.sprite = null;

        //selectedHandleText.text = "(선택안됨)";
        //selectedBladeText.text = "(선택안됨)";
    }
    public void InitUI()
    {
        gameObject.SetActive(true);
        deskCamera.SetActive(true);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        weaponExplainImg.SetActive(true);

        InitFrameSetting();

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


    //핸들 선택
    [SerializeField] Image handleSelectPopup;
    [SerializeField] GameObject handleContent;


    public void HandlePopupOpen()
    {
        handleSelectPopup.gameObject.SetActive(!handleSelectPopup.gameObject.activeSelf);

        for (int i = 0; i < handleContent.transform.childCount; i++)
        {
            handleContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(GameManager.playerdata.handleData[i].Additive_UID_First))
            {
                break;
            }
            else
            {
                handleContent.transform.GetChild(i).gameObject.SetActive(true);

                if (handleContent.transform.GetChild(i).GetChild(0).GetChild(0).TryGetComponent<Image>(out Image handleImg))        //핸들 이미지 변경
                {
                    handleImg.sprite = Resources.Load<Sprite>("Handle");
                }


                if (handleContent.transform.GetChild(i).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))        //핸들 이름 텍스트 변경
                {

                    nameText.text = GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_First).effect).effectName.Substring(0, 2);

                    nameText.text += " ";

                    nameText.text += GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_Second).effect).effectName.Substring(0, 2);

                    nameText.text += " 손잡이";
                }

                if (handleContent.transform.GetChild(i).GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI additiveText))   //핸들 첨가물 텍스트 변경
                {
                    additiveText.text = "특성 : " + GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_First).effect).effectName.Substring(0, 2) + "\n특성 : "
                        + GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_Second).effect).effectName.Substring(0, 2);
                }

                if (handleContent.transform.GetChild(i).GetChild(3).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI valueText))   //핸들 가격 텍스트 변경
                {
                    int totalValue = 0;

                    totalValue += GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_First).value + GameManager.Instance.FindMaterialRow(GameManager.playerdata.handleData[i].Additive_UID_Second).value;

                    valueText.text = string.Format("{0:#,##0}", totalValue);
                }

            }
        }
    }



    //칼날 선택
    [SerializeField] Image bladeSelectPopup;
    [SerializeField] GameObject bladeContent;


    public void BladePopupOpen()
    {
        bladeSelectPopup.gameObject.SetActive(!bladeSelectPopup.gameObject.activeSelf);

        for (int i = 0; i < bladeContent.transform.childCount; i++)
        {
            bladeContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].IngotUID))
            {
                break;
            }
            else
            {
                bladeContent.transform.GetChild(i).gameObject.SetActive(true);

                if (bladeContent.transform.GetChild(i).GetChild(0).GetChild(0).TryGetComponent<Image>(out Image bladeImg))        //칼날 이미지 변경
                {
                    bladeImg.sprite = Resources.Load<Sprite>(GameManager.playerdata.bladeData[i].BladeType.ToString() + "Blade");
                }


                if (bladeContent.transform.GetChild(i).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))        //칼날 이름 텍스트 변경
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

                if (bladeContent.transform.GetChild(i).GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI infoText))   //칼날 정보 텍스트 변경
                {

                    //공격력 텍스트 추가
                    //연마 보너스 - 환한 하늘색 색상 0070C0
                    //최종 공격력 - 환한 보라색 색상 9370DB

                    infoText.text = "공격력 : <color=#9370DB>" + (GameManager.Instance.FindIngotRow(GameManager.playerdata.bladeData[i].IngotUID).atkValue + GameManager.playerdata.bladeData[i].polishingBonus).ToString()
                        + "</color> (<color=red>" + GameManager.Instance.FindIngotRow(GameManager.playerdata.bladeData[i].IngotUID).atkValue.ToString()
                        + "</color>+<color=#0070C0>" + GameManager.playerdata.bladeData[i].polishingBonus.ToString() + "</color>)\n";


                    //내구도 텍스트 추가
                    infoText.text += "내구도 : <color=#9370DB>" + (GameManager.Instance.FindIngotRow(GameManager.playerdata.bladeData[i].IngotUID).durabilityValue + GameManager.playerdata.bladeData[i].polishingBonus).ToString()
                        + "</color> (<color=red>" + GameManager.Instance.FindIngotRow(GameManager.playerdata.bladeData[i].IngotUID).durabilityValue.ToString()
                        + "</color>+<color=#0070C0>" + GameManager.playerdata.bladeData[i].polishingBonus.ToString() + "</color>)\n";

                    //첨가물 텍스트 추가
                    if (!string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].Additive_UID))
                    {
                        for (int j = 0; j < GameManager.TableDB.MaterialDB.Count; j++)
                        {
                            if (GameManager.TableDB.MaterialDB[j].itemID.Equals(GameManager.playerdata.bladeData[i].Additive_UID))
                            {
                                infoText.text += "<color=purple>" + GameManager.TableDB.MaterialDB[j].itemName + " 첨가</color>";
                            }
                        }
                    }
                }

                if (bladeContent.transform.GetChild(i).GetChild(3).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI valueText))   //칼날 가격 텍스트 변경
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

                    valueText.text = string.Format("{0:#,##0}", totalValue);
                }

            }
        }
    }


    //로딩바
    [SerializeField] Slider loadingSlider;
    [SerializeField] GameObject loadingImg;
    private bool isMaking = false;

    public float currentTime;
    public float EndTime = 3f;

    public void LoadingPopupActive()
    {
        if (selectedBladeIndex >= 0 && selectedHandleIndex >= 0)
        {
            loadingImg.gameObject.SetActive(true);
            StartCoroutine(LoadingPopupStart());
        }
        else
        {
            //error
        }
    }

    public IEnumerator LoadingPopupStart()
    {
        currentTime = 0f;
        loadingSlider.value = 0f;

        while (true)
        {
            currentTime += Time.deltaTime;
            loadingSlider.value = Mathf.Lerp(0, 1, currentTime / EndTime);

            if (loadingSlider.value == 1f)
            {
                loadingImg.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }
        RewardPopupOpen();
        StopCoroutine(LoadingPopupStart());
    }



    [SerializeField] GameObject rewardImg;
    [SerializeField] TextMeshProUGUI rewardText;
    public void RewardPopupOpen()
    {
        rewardImg.SetActive(true);
        /*string selectedHandleUID = "";
        string selectedGripUID = "";



        rewardImg.SetActive(true);

        rewardText.text = "";
        switch (selectedHandleIndex % 2)
        {
            case 0:
                selectedHandleUID = "p00" + (selectedHandleIndex + 1).ToString();
                rewardText.text += GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(selectedHandleUID).effect).effectName.Substring(0, 2);
                break;
            case 1:
                selectedHandleUID = "b00" + (selectedHandleIndex).ToString();
                rewardText.text += GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(selectedHandleUID).effect).effectName.Substring(0, 2);
                break;
        }

        rewardText.text += " ";

        switch (selectedGripIndex % 2)
        {
            case 0:
                selectedGripUID = "p00" + (selectedGripIndex + 2).ToString();
                rewardText.text += GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(selectedGripUID).effect).effectName.Substring(0, 2);
                break;
            case 1:
                selectedGripUID = "b00" + (selectedGripIndex + 1).ToString();
                rewardText.text += GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow(selectedGripUID).effect).effectName.Substring(0, 2);
                break;
        }

        rewardText.text += " 손잡이";

        GameManager.HandleDataChange(selectedHandleUID, selectedGripUID);

        InitFrameSetting();*/
    }

    public void RewardPopupClose()
    {
        rewardImg.SetActive(false);
    }
}
