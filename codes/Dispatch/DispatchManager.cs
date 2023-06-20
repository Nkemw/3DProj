using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispatchManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    private void Start()
    {
        goldText.text = string.Format("{0:#,##0}", GameManager.playerdata.Gold);
        if (NPCTextFrame.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI npcText))
        {
            npcText.text = GameManager.TableDB.NPC_DB[1].conver1;
        }

    }


    private void Update()
    {
        for (int i = 0; i < GameManager.playerdata.dispatchData.Length; i++)
        {
            if (GameManager.playerdata.dispatchData[i].progressTime != GameManager.playerdata.dispatchData[i].dispatchTime)
            {
                if (GameManager.playerdata.dispatchData[i].dispatchTime != 0f)
                {
                    GameManager.playerdata.dispatchData[i].progressTime += Time.deltaTime;
                    if(GameManager.playerdata.dispatchData[i].progressTime > GameManager.playerdata.dispatchData[i].dispatchTime)
                    {
                        GameManager.playerdata.dispatchData[i].progressTime = GameManager.playerdata.dispatchData[i].dispatchTime;
                    }
                }
            }
        }
        
    }

    public static int currentDispatchDataIndex = -10;

    [SerializeField] GameObject resultPopup;
    [SerializeField] GameObject NPCImg;
    [SerializeField] GameObject NPCTextFrame;
    [SerializeField] GameObject dispatchFrame;
    [SerializeField] GameObject selectFrame;
    public void StartDispatch()
    {
        GameManager.AddDispatchData(DispatchController.selectedMercenaryIndex, DispatchController.selectedWeaponIndex, DispatchController.selectedDispatchPopup*3);

        OpenResultPopup();
    }
    public void OpenResultPopup()
    {
        resultPopup.SetActive(true);
        NPCImg.SetActive(true);
        NPCTextFrame.SetActive(true);
        dispatchFrame.SetActive(false);

        if (NPCTextFrame.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI npcText))
        {
            npcText.text = GameManager.TableDB.NPC_DB[1].conver3;
        }

        StartCoroutine("DispatchTimeTextCount");
    }
    public void CloseResultPopup()
    {
        StopCoroutine("DispatchTimeTextCount");

        resultPopup.SetActive(false);
        NPCTextFrame.SetActive(false);
        selectFrame.SetActive(true);
    }

    IEnumerator DispatchTimeTextCount()
    {
        int min = 0;
        int sec = 0;

        for(int i = 0; i < 3; i++)
        {
            if (resultPopup.transform.GetChild(0).GetChild(1 + i).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI dispatchedMercenaryText))
            {
                dispatchedMercenaryText.text = "";
            } 
        }

        while (true)
        {
            Debug.Log("파견 데이터 인덱스: " + currentDispatchDataIndex);
            min = (int)(GameManager.playerdata.dispatchData[currentDispatchDataIndex].dispatchTime - GameManager.playerdata.dispatchData[currentDispatchDataIndex].progressTime) / 60;
            sec = (int)(GameManager.playerdata.dispatchData[currentDispatchDataIndex].dispatchTime - GameManager.playerdata.dispatchData[currentDispatchDataIndex].progressTime) % 60;
            for (int i = 0; i < GameManager.playerdata.dispatchData[currentDispatchDataIndex].UID.Length; i++)
            {
                if (GameManager.playerdata.dispatchData[currentDispatchDataIndex].UID[i] != -1)
                {
                    if (resultPopup.transform.GetChild(0).GetChild(1 + i).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI dispatchedMercenaryText))
                    {
                        dispatchedMercenaryText.text = "- " + GameManager.TableDB.MercenaryDB[GameManager.playerdata.dispatchData[currentDispatchDataIndex].UID[i]].mercenaryName + " <size=40>(" + min.ToString() + ":" + sec.ToString() + ")</size>";
                    }
                }
            }

            yield return null;
        }
    }
}
