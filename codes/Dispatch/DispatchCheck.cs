using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispatchCheck : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject selectFrame;
    [SerializeField] GameObject NPCTextFrame;

    [SerializeField] GameObject[] dispatchFrames;
    [SerializeField] GameObject[] dispatchBtns;

    [SerializeField] GameObject rewardPopup;

    private void Awake()
    {
        for (int i = 0; i < dispatchBtns.Length; i++)
        {
            int index = i;
            if (dispatchBtns[i].TryGetComponent<Button>(out Button btn))
            {
                btn.onClick.AddListener(() => GetDispatchReward(index));
            }
        }
    }

    public void InitUI()
    {
        gameObject.SetActive(true);
        selectFrame.SetActive(false);
        NPCTextFrame.SetActive(false);
        rewardPopup.SetActive(false);

        DispatchFrameInit();
    }

    public void ExitUI()
    {
        gameObject.SetActive(false);
        selectFrame.SetActive(true);
    }

    public void DispatchFrameInit()
    {
        for(int i = 0; i < 3; i++)
        {
            dispatchFrames[i].SetActive(false);
        }

        for(int i = 0; i < 3; i++)
        {
            if(GameManager.playerdata.dispatchData[i].UID[0] == GameManager.playerdata.dispatchData[i].UID[1])
            {
                break;
            } else
            {
                dispatchFrames[i].SetActive(true);

                if(dispatchFrames[i].transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI titleText))
                {
                    titleText.text = GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[i].dispatchUID].dispatchName;
                }

            }
        }

        StartCoroutine("DispatchTimerCount");
    }

    IEnumerator DispatchTimerCount()
    {
        int min = 0;
        int sec = 0;

        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameManager.playerdata.dispatchData[i].UID[0] != GameManager.playerdata.dispatchData[i].UID[1])
                {
                    min = (int)(GameManager.playerdata.dispatchData[i].dispatchTime - GameManager.playerdata.dispatchData[i].progressTime) / 60;
                    sec = (int)(GameManager.playerdata.dispatchData[i].dispatchTime - GameManager.playerdata.dispatchData[i].progressTime) % 60;

                    if (dispatchFrames[i].transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI restTimeText))
                    {
                        restTimeText.text = "남은시간 " + min.ToString() + ":" + sec.ToString();
                    }
                }
            }

            yield return null;
        }
    }

    public void GetDispatchReward(int index)    //파견 데이터 인덱스
    {
        rewardPopup.SetActive(true);

        //보상 내용물 갱신
           
        if(rewardPopup.transform.GetChild(0).GetChild(2).GetChild(0).TryGetComponent<Image>(out Image iconImg1))
        {
            iconImg1.sprite = Resources.Load<Sprite>(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial1);
        }

        if (rewardPopup.transform.GetChild(0).GetChild(2).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI itemText1))
        {
            int itemCount = 0;
            itemCount = (int)GameManager.playerdata.dispatchData[index].progressTime / GameManager.Instance.FindMaterialRow(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial1).minutePerPiece;
            itemText1.text = GameManager.Instance.FindMaterialRow(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial1).itemName + " x " + itemCount.ToString();
        }

        if (rewardPopup.transform.GetChild(0).GetChild(3).GetChild(0).TryGetComponent<Image>(out Image iconImg2))
        {
            iconImg2.sprite = Resources.Load<Sprite>(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial2);
        }

        if (rewardPopup.transform.GetChild(0).GetChild(3).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI itemText2))
        {
            int itemCount = 0;
            itemCount = (int)GameManager.playerdata.dispatchData[index].progressTime / GameManager.Instance.FindMaterialRow(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial2).minutePerPiece;
            itemText2.text = GameManager.Instance.FindMaterialRow(GameManager.TableDB.DispatchDB[GameManager.playerdata.dispatchData[index].dispatchUID].dropMaterial2).itemName + " x " + itemCount.ToString();
        }

        //NPCText 갱신
        NPCTextFrame.SetActive(true);
        if (NPCTextFrame.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI npcText))
        {
            npcText.text = GameManager.TableDB.NPC_DB[1].conver4;
        }

        //Dispatch 데이터 갱신
        GameManager.DispatchDataPush(index);
    }

    public void RewardPopupClose()
    {
        DispatchFrameInit();
        rewardPopup.SetActive(false);
        NPCTextFrame.SetActive(false);
    }
}
