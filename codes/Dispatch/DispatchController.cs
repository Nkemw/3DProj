using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DispatchController : MonoBehaviour, UIActiveEvent
{
    [SerializeField] GameObject[] dispatchSelectBtns;
    //[SerializeField] GameObject[] dispatchPopups;

    [SerializeField] GameObject mercenaryContent;
    [SerializeField] GameObject weaponContent;
    [SerializeField] TextMeshProUGUI mercenaryCountText;
    [SerializeField] TextMeshProUGUI weaponCountText;

    [SerializeField] GameObject dispatchStartBtn;

    [SerializeField] GameObject NPCImg;
    [SerializeField] GameObject NPCTextFrame;
    [SerializeField] GameObject selectCategoryFrame;

    public static int selectedDispatchPopup = -10;

    public static List<int> selectedMercenaryIndex = new List<int>();
    public static List<int> selectedWeaponIndex = new List<int>();

    private void Awake()
    {
        
        for(int i = 0; i < dispatchSelectBtns.Length; i++)
        {
            int index = i;
            if(dispatchSelectBtns[i].TryGetComponent<Button>(out Button btn))
            {
                btn.onClick.AddListener(() => DispatchBtnClickEvent(index));
            }
        }
    }
    public void InitUI()
    {
        gameObject.SetActive(true);

        NPCImg.SetActive(false);
        NPCTextFrame.SetActive(false);
        selectCategoryFrame.SetActive(false);

        ButtonsInit();
        MercenaryInit();
        WeaponInit();
        DispatchBtnInit();

        IndexInit();
        TextInit();
    }

    public void IndexInit()
    {
        selectedDispatchPopup = -10;
        selectedMercenaryIndex.Clear();
        selectedWeaponIndex.Clear();
    }

    public void TextInit()
    {
        mercenaryCountText.text = "0/3";
        weaponCountText.text = "0/3";
    }

    public void ButtonsInit()
    {
        Color initColor = new Color(120f / 255f, 120f / 255f, 120f / 255f);
        for(int i = 0; i < dispatchSelectBtns.Length; i++)
        {
            if(dispatchSelectBtns[i].TryGetComponent<Image>(out Image img))
            {
                img.color = initColor;
            }
        }
    }

    public void MercenaryInit()
    {
        for(int i = 0; i < FileModule.MAXMERCENARYCOUNT; i++)
        {
            if(mercenaryContent.transform.GetChild(i).TryGetComponent<Outline>(out Outline outline))
            {
                outline.effectColor = new Color(0f, 0f, 0f);
                outline.effectDistance = new Vector2(3f, 3f);
            }

            if (mercenaryContent.transform.GetChild(i).GetChild(3).TryGetComponent<Image>(out Image checkIcon))
            {
                checkIcon.gameObject.SetActive(false);
            }
            mercenaryContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXMERCENARYCOUNT; i++)
        {
            if (GameManager.playerdata.mercenaryData[i].amount != 0)
            {
                mercenaryContent.transform.GetChild(i).gameObject.SetActive(true);

                if(mercenaryContent.transform.GetChild(i).GetChild(1).GetChild(0).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText))
                {
                    amountText.text = GameManager.playerdata.mercenaryData[i].amount.ToString();
                }

                if (mercenaryContent.transform.GetChild(i).GetChild(3).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI forceText))
                {
                    forceText.text = GameManager.TableDB.MercenaryDB[i].baseCP.ToString();
                }

            }
        }
    }

    public void WeaponInit()
    {
        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (weaponContent.transform.GetChild(i).TryGetComponent<Outline>(out Outline outline))
            {
                outline.effectColor = new Color(0f, 0f, 0f);
                outline.effectDistance = new Vector2(3f, 3f);
            }

            if (weaponContent.transform.GetChild(i).GetChild(4).TryGetComponent<Image>(out Image checkIcon))
            {
                checkIcon.gameObject.SetActive(false);
            }

            weaponContent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
        {
            if (!string.IsNullOrEmpty(GameManager.playerdata.weaponData[i].IngotUID))
            {
                weaponContent.transform.GetChild(i).gameObject.SetActive(true);

                if (weaponContent.transform.GetChild(i).GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))
                {
                    nameText.text = GameManager.playerdata.weaponData[i].weaponName;
                }

                if (weaponContent.transform.GetChild(i).GetChild(3).GetChild(1).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI forceText))
                {
                    forceText.text = GameManager.playerdata.weaponData[i].weaponCP.ToString();
                }

                if (weaponContent.transform.GetChild(i).GetChild(4).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI durabilityText))
                {
                    durabilityText.text = GameManager.playerdata.weaponData[i].currentDurability.ToString() + "/" + (GameManager.playerdata.weaponData[i].weaponBaseDurability + GameManager.playerdata.weaponData[i].weaponDurabilityClassBonus + GameManager.playerdata.weaponData[i].weaponPolishingBonus).ToString();
                }
            }
        }
    }

    public void DispatchBtnInit()
    {
        if(dispatchStartBtn.TryGetComponent<Image>(out Image img)){
            img.color = new Color(120f / 255f, 120f / 255f, 120f / 255f);
        }

        if(dispatchStartBtn.TryGetComponent<Button>(out Button btn))
        {
            btn.enabled = false;
        }
    }

    public void ExitUI()
    {
        NPCImg.SetActive(true);
        //NPCTextFrame.SetActive(false);
        selectCategoryFrame.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DispatchBtnClickEvent(int index)
    {
        if (selectedDispatchPopup == -10)
        {
            selectedDispatchPopup = index;
            if(dispatchSelectBtns[index].TryGetComponent<Image>(out Image img))
            {
                img.color = new Color(1f, 30f / 255f, 30f / 255f);
            }
        }
        else
        {
            if (selectedDispatchPopup == index)
            {
                selectedDispatchPopup = -10;

                if (dispatchSelectBtns[index].TryGetComponent<Image>(out Image Img))
                {
                    Img.color = new Color(120f / 255f, 120f / 255f, 120f / 255f);
                }

                MercenaryInit();
                WeaponInit();

                IndexInit();
                TextInit();
                DispatchBtnInit();
            }
            else
            {
                Color prevColor = new Color();
                if (dispatchSelectBtns[selectedDispatchPopup].TryGetComponent<Image>(out Image prevImg))
                {
                    prevColor = prevImg.color;
                    prevImg.color = new Color(120f / 255f, 120f / 255f, 120f / 255f);
                }

                selectedDispatchPopup = index;
                if (dispatchSelectBtns[index].TryGetComponent<Image>(out Image img))
                {
                    img.color = prevColor;
                }
            }
        }
    }


    
}
