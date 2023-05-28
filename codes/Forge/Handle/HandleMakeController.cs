using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleMakeController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject deskCamera;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject handleExplainImg;

    [SerializeField] Image handleSelectedImg;
    [SerializeField] Image gripSelectedImg;
    [SerializeField] Image resultImg;

    [SerializeField] TextMeshProUGUI selectedHandleText;
    [SerializeField] TextMeshProUGUI selectedGripText;

    public static int selectedHandleIndex = -10;
    public static int selectedGripIndex = -10;

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
        selectedGripIndex = -10;

        handleSelectedImg.sprite = null;
        gripSelectedImg.sprite = null;
        resultImg.sprite = null;

        selectedHandleText.text = "(선택안됨)";
        selectedGripText.text = "(선택안됨)";
    }
    public void InitUI()
    {
        gameObject.SetActive(true);
        deskCamera.SetActive(true);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        handleExplainImg.SetActive(true);

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
            if (string.IsNullOrEmpty(GameManager.playerdata.additiveData[i].UID))
            {
                break;
            }
            else
            {
                if (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()) % 2 == 1)           //핸들 첨가제 체크
                {
                    int contentIndex;

                    if (GameManager.playerdata.additiveData[i].UID[0] == 'p')
                    {
                        contentIndex = -1;
                    }
                    else
                    {
                        contentIndex = 0;
                    }
                    contentIndex += int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString());

                    handleContent.transform.GetChild(contentIndex).gameObject.SetActive(true);

                    if (handleContent.transform.GetChild(contentIndex).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText)) //수량 텍스트 변경
                    {
                        amountText.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                    }
                }

            }
        }
    }



    //그립 선택
    [SerializeField] Image gripSelectPopup;
    [SerializeField] GameObject gripContent;


    public void GripPopupOpen()
    {
        gripSelectPopup.gameObject.SetActive(!gripSelectPopup.gameObject.activeSelf);

        for (int i = 0; i < gripContent.transform.childCount; i++)
        {
            gripContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (string.IsNullOrEmpty(GameManager.playerdata.additiveData[i].UID))
            {
                break;
            }
            else
            {
                if (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()) % 2 == 0)           //핸들 첨가제 체크
                {
                    int contentIndex;

                    if (GameManager.playerdata.additiveData[i].UID[0] == 'p')
                    {
                        contentIndex = -2;
                    }
                    else
                    {
                        contentIndex = -1;
                    }
                    contentIndex += int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString());

                    gripContent.transform.GetChild(contentIndex).gameObject.SetActive(true);

                    if (gripContent.transform.GetChild(contentIndex).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText)) //수량 텍스트 변경
                    {
                        amountText.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                    }
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
        if (selectedGripIndex >= 0 && selectedHandleIndex >= 0)
        {
            loadingImg.gameObject.SetActive(true);
            StartCoroutine(LoadingPopupStart());
        } else
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
        string selectedHandleUID = "";
        string selectedGripUID = "";


        
        rewardImg.SetActive(true);

        rewardText.text = "";
        switch(selectedHandleIndex % 2)
        {
            case 0:
                selectedHandleUID = "p00" + (selectedHandleIndex + 1).ToString();
                rewardText.text += GameManager.Instance.FindMaterialRow(selectedHandleUID).itemName;
                break;
            case 1:
                selectedHandleUID = "b00" + (selectedHandleIndex).ToString();
                rewardText.text += GameManager.Instance.FindMaterialRow(selectedHandleUID).itemName;
                break;
        }

        rewardText.text += " 손잡이";

        GameManager.HandleDataChange(selectedHandleUID, selectedGripUID);

        InitFrameSetting();
    }

    public void RewardPopupClose()
    {
        rewardImg.SetActive(false);
    }
}
