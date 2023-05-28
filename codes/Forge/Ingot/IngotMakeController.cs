using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngotMakeController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] Button[] ingotMakeBtns;
    [SerializeField] Image[] popupImgs;

    public static int selectedBaseIngotIndex = -1;
    public static int selectedSubIngotIndex = -1;
    public static int selectedAdditiveIndex = -1;
    public static string selectedAddtive_UID = "";

    public static int selectedPopupImgIndex;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < ingotMakeBtns.Length; i++)
        {
            int index = i;
            ingotMakeBtns[i].onClick.AddListener(() => this.ShowAddItemPopup(index));
        }
    }

    public void ShowAddItemPopup(int index)
    {
        Debug.Log("ÀÎµ¦½º: " + index);
        selectedPopupImgIndex = index;

        for (int i = 0; i < popupImgs[index].transform.childCount; i++)
        {
            if (index < 2)
            {
                popupImgs[index].transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }


        if (popupImgs[index].gameObject.activeSelf)
        {
            popupImgs[index].gameObject.SetActive(!popupImgs[index].gameObject.activeSelf);
        }
        else
        {
            popupImgs[index].gameObject.SetActive(!popupImgs[index].gameObject.activeSelf);

            if (index < 2)
            {
                for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
                {
                    if (GameManager.playerdata.metalData[i].Amount < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (index == 0)
                        {
                            switch (int.Parse(GameManager.playerdata.metalData[i].UID[3].ToString()))
                            {
                                case 1:
                                    popupImgs[index].transform.GetChild(0).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                    {
                                        amountText1.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                                case 3:
                                    popupImgs[index].transform.GetChild(1).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                    {
                                        amountText2.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                                case 5:
                                    popupImgs[index].transform.GetChild(2).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                    {
                                        amountText3.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                            }
                        }
                        else if (index == 1)
                        {
                            switch (int.Parse(GameManager.playerdata.metalData[i].UID[3].ToString()))
                            {
                                case 2:
                                    popupImgs[index].transform.GetChild(0).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                    {
                                        amountText1.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                                case 4:
                                    popupImgs[index].transform.GetChild(1).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                    {
                                        amountText2.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                                case 6:
                                    popupImgs[index].transform.GetChild(2).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                    {
                                        amountText3.text = GameManager.playerdata.metalData[i].Amount.ToString();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            if (GameManager.playerdata.additiveData[i].UID[0] == 'p')
                            {
                                switch (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()))
                                {
                                    case 1:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                        {
                                            amountText1.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 2:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                        {
                                            amountText2.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 3:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                        {
                                            amountText3.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 4:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText4))
                                        {
                                            amountText4.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 5:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText5))
                                        {
                                            amountText5.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 6:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText6))
                                        {
                                            amountText6.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                switch (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()))
                                {
                                    case 1:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(6).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(6).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                        {
                                            amountText1.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 2:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(7).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                        {
                                            amountText2.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 3:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(8).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(8).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                        {
                                            amountText3.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 4:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(9).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText4))
                                        {
                                            amountText4.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 5:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(10).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText5))
                                        {
                                            amountText5.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                    case 6:
                                        popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(11).gameObject.SetActive(true);
                                        if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(11).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText6))
                                        {
                                            amountText6.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                        }
                                        break;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
                {
                    if (GameManager.playerdata.additiveData[i].Amount < 1)
                    {
                        break;
                    }
                    else
                    {
                        if (GameManager.playerdata.additiveData[i].UID[0] == 'p')
                        {
                            switch (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()))
                            {
                                case 1:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                    {
                                        amountText1.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 2:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                    {
                                        amountText2.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 3:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(4).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                    {
                                        amountText3.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 4:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText4))
                                    {
                                        amountText4.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 5:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(8).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(8).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText5))
                                    {
                                        amountText5.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 6:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(9).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(9).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText6))
                                    {
                                        amountText6.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (int.Parse(GameManager.playerdata.additiveData[i].UID[3].ToString()))
                            {
                                case 1:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText1))
                                    {
                                        amountText1.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 2:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText2))
                                    {
                                        amountText2.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 3:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(6).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(6).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText3))
                                    {
                                        amountText3.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 4:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(7).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText4))
                                    {
                                        amountText4.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 5:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(10).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(10).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText5))
                                    {
                                        amountText5.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                                case 6:
                                    popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(11).gameObject.SetActive(true);
                                    if (popupImgs[index].transform.GetChild(0).GetChild(0).GetChild(11).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText6))
                                    {
                                        amountText6.text = GameManager.playerdata.additiveData[i].Amount.ToString();
                                    }
                                    break;
                            }
                        }

                    }
                }
            }
        }


    }

    /*public void SelectBaseIngot(int index)
    {
        selectedBaseIngotIndex = index+1;

        if(ingotMakeBtns[0].transform.TryGetComponent<Image>(out Image img))
        {
            img.sprite = Resources.Load<Sprite>("m00" + selectedBaseIngotIndex.ToString());
        }
    }*/


    //Á¦ÀÛ ¹öÆ° ´©¸¥ µÚ °á°úÃ¢ °ü·Ã
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] Image rewardImg;
    [SerializeField] Image itemImg;
    [SerializeField] TextMeshProUGUI additiveName;

    public void MakeIngot()
    {
        if(!(selectedBaseIngotIndex == -1 && selectedSubIngotIndex == -1))
        {
            rewardImg.gameObject.SetActive(true);

            switch (selectedBaseIngotIndex)
            {
                case 0:
                    switch (selectedSubIngotIndex)
                    {
                        case 0:
                            itemName.text = "ÁÖ¼® ±¸¸® ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[0].ingotID);
                            GameManager.IngotDataChange("i001", selectedAddtive_UID);
                            break;
                        case 1:
                            itemName.text = "Å©·Ò ÁÖ¼® ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[1].ingotID);
                            GameManager.IngotDataChange("i002", selectedAddtive_UID);
                            break;
                        case 2:
                            itemName.text = "¿À¸£ÄÜ ÁÖ¼® ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[2].ingotID);
                            GameManager.IngotDataChange("i003", selectedAddtive_UID);
                            break;
                    }
                    break;
                case 1:
                    switch (selectedSubIngotIndex)
                    {
                        case 0:
                            itemName.text = "ÁÖ¼® Ã¶ ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[3].ingotID);
                            GameManager.IngotDataChange("i004", selectedAddtive_UID);
                            break;
                        case 1:
                            itemName.text = "Å©·Ò Ã¶ ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[4].ingotID);
                            GameManager.IngotDataChange("i005", selectedAddtive_UID);
                            break;
                        case 2:
                            itemName.text = "¿À¸£ÄÜ Ã¶ ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[5].ingotID);
                            GameManager.IngotDataChange("i006", selectedAddtive_UID);
                            break;
                    }
                    break;
                case 2:
                    switch (selectedSubIngotIndex)
                    {
                        case 0:
                            itemName.text = "ÁÖ¼® ¹Ì½º¶ö ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[6].ingotID);
                            GameManager.IngotDataChange("i007", selectedAddtive_UID);
                            break;
                        case 1:
                            itemName.text = "Å©·Ò ¹Ì½º¶ö ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[7].ingotID);
                            GameManager.IngotDataChange("i008", selectedAddtive_UID);
                            break;
                        case 2:
                            itemName.text = "¿À¸£ÄÜ ¹Ì½º¶ö ÁÖ±«";
                            itemImg.sprite = Resources.Load<Sprite>(GameManager.TableDB.IngotDB[8].ingotID);
                            GameManager.IngotDataChange("i009", selectedAddtive_UID);
                            break;
                    }
                    break;
            }
            for(int i = 0; i < GameManager.TableDB.MaterialDB.Count; i++)
            {
                if (GameManager.TableDB.MaterialDB[i].itemID.Equals(selectedAddtive_UID))
                {
                    additiveName.text = "<color=purple>" + GameManager.TableDB.MaterialDB[i].itemName + " Ã·°¡</color>";
                }
            }
        } else
        {
            Debug.Log("Àç·á°¡ µî·ÏµÇÁö ¾ÊÀ½");
        }

        if (ingotMakeBtns[0].transform.TryGetComponent<Image>(out Image baseMetalImg))
        {
            baseMetalImg.sprite = null;
        }

        if (ingotMakeBtns[1].transform.TryGetComponent<Image>(out Image subMetalImg))
        {
            subMetalImg.sprite = null;
        }

        if (ingotMakeBtns[2].transform.TryGetComponent<Image>(out Image additiveImg))
        {
            additiveImg.sprite = null;
        }
    }


    public void PopupClose()
    {
        rewardImg.gameObject.SetActive(false);

    }







    //·Îµù¹Ù
    [SerializeField] Slider loadingSlider;
    [SerializeField] Image loadingImg;
    private bool isMaking = false;

    public float currentTime;
    public float EndTime = 3f;
    private void Update()
    {
        if (isMaking)
        {
            currentTime += Time.deltaTime;
            loadingSlider.value = Mathf.Lerp(0, 1, currentTime / EndTime);

            if(loadingSlider.value == 1f)
            {
                loadingSlider.value = 0f;
                currentTime = 0f;
                LoadingPopupActive();
                MakeIngot();
            }
        }
    }

    public void LoadingPopupActive()
    {
        loadingImg.gameObject.SetActive(!loadingImg.gameObject.activeSelf);
        isMaking = !isMaking;
    }

    //UI ÃÊ±âÈ­
    [SerializeField] GameObject particle;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject ingotCamera;
    public void InitUI()
    {
        gameObject.SetActive(true);
        particle.SetActive(false);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        ingotCamera.SetActive(true);

        StartCoroutine("FadeOut");
    }

   IEnumerator FadeOut()
    {
        yield return null;

        float startTime = 0f;
        float endTime = 0.5f;

        Color newColor = new Color();
        while(startTime/endTime < 1f)
        {
            startTime += Time.deltaTime;
            
            newColor.a = Mathf.Lerp(1f, 0f, startTime / endTime);
            fadeImg.color = newColor;
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
        StopCoroutine("FadeOut");
    }

    public void ExitUI()
    {
        Color newColor = fadeImg.color;
        newColor.a = 1f;
        fadeImg.color = newColor;

        gameObject.SetActive(false);
        particle.SetActive(true);
        player.SetActive(true);
        ingotCamera.SetActive(false);

        
    }
}
