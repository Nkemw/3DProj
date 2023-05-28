using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BladeSelect : MonoBehaviour
{
    [SerializeField] Button[] bladeBtns;

    [SerializeField] Button bladeSelectButtonImg;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < bladeBtns.Length; i++)
        {
            int index = i;
            bladeBtns[i].onClick.AddListener(() => this.SelectBlade(index));
        }
    }

    public void SelectBlade(int index)   //index: 0~35
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (bladeSelectButtonImg.TryGetComponent<Image>(out Image img))
            {
                img.sprite = Resources.Load<Sprite>(GameManager.playerdata.bladeData[index].BladeType.ToString() + "Blade");
                PolishController.selectedBladeIndex = index;
            }
        }
        else
        {

            for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
            {
                if (string.IsNullOrEmpty(GameManager.playerdata.bladeData[i].IngotUID))
                {
                    break;
                }
                else
                {
                    bladeBtns[int.Parse(GameManager.playerdata.ingotData[i].UID[3].ToString())].transform.parent.gameObject.SetActive(true);
                    if (bladeBtns[int.Parse(GameManager.playerdata.ingotData[i].UID[3].ToString())].transform.parent.GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText))
                    {
                        amountText.text = GameManager.playerdata.ingotData[i].Amount.ToString();
                    }

                }
            }
        }




    }
}
