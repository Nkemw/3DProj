using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] Button[] weaponBtns;

    [SerializeField] GameObject weaponContent;

    [SerializeField] TextMeshProUGUI selectCountText;

    [SerializeField] GameObject[] mapBtns;
    [SerializeField] GameObject dispatchBtn;

    void Awake()
    {
        for (int i = 0; i < weaponBtns.Length; i++)
        {
            int index = i;
            weaponBtns[i].onClick.AddListener(() => this.SelectWeapon(index));
        }
    }

    public void SelectWeapon(int index)   //index: 0~35
    {
        if (DispatchController.selectedDispatchPopup != -10)
        {
            if (DispatchController.selectedWeaponIndex.Contains(index))
            {
                DispatchController.selectedWeaponIndex.Remove(index);
                SetWeaponFocusFalse(index);
            }
            else
            {
                if (DispatchController.selectedWeaponIndex.Count != 3)
                {
                    DispatchController.selectedWeaponIndex.Add(index);
                    SetWeaponFocusTrue(index);
                }
            }
            CheckDispatchable(index);
            selectCountText.text = DispatchController.selectedWeaponIndex.Count.ToString() + "/3";
        }
        
    }

    public void SetWeaponFocusTrue(int index)
    {
        if (weaponContent.transform.GetChild(index).TryGetComponent<Outline>(out Outline outline))
        {
            Color color;
            ColorUtility.TryParseHtmlString("#00B050", out color);
            outline.effectColor = color;
            outline.effectDistance = new Vector2(5f, 5f);
        }

        if (weaponContent.transform.GetChild(index).GetChild(4).TryGetComponent<Image>(out Image checkIcon))
        {
            checkIcon.gameObject.SetActive(true);
        }
    }

    public void SetWeaponFocusFalse(int index)
    {
        if (weaponContent.transform.GetChild(index).TryGetComponent<Outline>(out Outline outline))
        {

            outline.effectColor = new Color(0f, 0f, 0f);
            outline.effectDistance = new Vector2(3f, 3f);
        }

        if (weaponContent.transform.GetChild(index).GetChild(4).TryGetComponent<Image>(out Image checkIcon))
        {
            checkIcon.gameObject.SetActive(false);
        }
    }

    public void CheckDispatchable(int index)
    {
        int tempBattleForce = 0;
        for(int i = 0; i < DispatchController.selectedMercenaryIndex.Count; i++)
        {
            tempBattleForce += GameManager.TableDB.MercenaryDB[DispatchController.selectedMercenaryIndex[i]].baseCP;
        }
        for (int i = 0; i < DispatchController.selectedWeaponIndex.Count; i++)
        {
            tempBattleForce += GameManager.playerdata.weaponData[DispatchController.selectedWeaponIndex[i]].weaponCP;
        }

        if (tempBattleForce >= 2000)
        {
            if(mapBtns[DispatchController.selectedDispatchPopup].TryGetComponent<Image>(out Image img)){
                img.color = new Color(90f / 255f, 220f / 255f, 90f / 255f);
            }

            if(dispatchBtn.TryGetComponent<Image>(out Image dispatchImg)){
                Color color;
                ColorUtility.TryParseHtmlString("#92D050", out color);
                dispatchImg.color = color;
            }

            if(dispatchBtn.TryGetComponent<Button>(out Button btn))
            {
                btn.enabled = true;
            }
        } else
        {
            if (mapBtns[DispatchController.selectedDispatchPopup].TryGetComponent<Image>(out Image img))
            {
                img.color = new Color(1f, 30f / 255f, 30f / 255f);
            }

            if (dispatchBtn.TryGetComponent<Image>(out Image dispatchImg))
            {
                dispatchImg.color = new Color(120f / 255f, 120f / 255f, 120f / 255f);
            }

            if (dispatchBtn.TryGetComponent<Button>(out Button btn))
            {
                btn.enabled = false;
            }
        }
    }
}
