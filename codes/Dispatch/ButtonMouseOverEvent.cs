using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonMouseOverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject dispatchPopup;
    [SerializeField] TextMeshProUGUI battleForceText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dispatchPopup.SetActive(true);

        if (DispatchController.selectedDispatchPopup != -10)
        {
            int totalBattleForce = 0;
            for(int i = 0; i < DispatchController.selectedMercenaryIndex.Count; i++)
            {
                totalBattleForce += GameManager.TableDB.MercenaryDB[DispatchController.selectedMercenaryIndex[i]].baseCP;
            }
            
            for(int i = 0; i < DispatchController.selectedWeaponIndex.Count; i++)
            {
                totalBattleForce += GameManager.playerdata.weaponData[DispatchController.selectedWeaponIndex[i]].weaponCP;
            }

            battleForceText.text = "(" + totalBattleForce.ToString() + "/2000)";
        }
        else
        {
            battleForceText.text = "(0/2000)";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dispatchPopup.SetActive(false);
    }

    
}
