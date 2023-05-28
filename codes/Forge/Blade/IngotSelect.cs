using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IngotSelect : MonoBehaviour
{
    [SerializeField] Button[] ingotBtns;

    [SerializeField] Button ingotSelectButtonImg;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < ingotBtns.Length; i++)
        {
            int index = i;
            ingotBtns[i].onClick.AddListener(() => this.SelectMaterial(index));
        }
    }

    public void SelectMaterial(int index)   //index: 0~35
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (ingotSelectButtonImg.TryGetComponent<Image>(out Image img))
            {
                //img.sprite = Resources.Load<Sprite>("i00" + (index + 1).ToString());
                img.sprite = Resources.Load<Sprite>(GameManager.playerdata.ingotData[index].UID);
                BladeMakeController.selectedIngotIndex = index;
                Debug.Log("asd" + BladeMakeController.selectedIngotIndex);
            }
        }
        else
        {

            for (int i = 0; i < FileModule.MAXINVENTORYCOUNT; i++)
            {
                if (GameManager.playerdata.ingotData[i].Amount < 1)
                {
                    break;
                }
                else
                {
                    ingotBtns[int.Parse(GameManager.playerdata.ingotData[i].UID[3].ToString())].transform.parent.gameObject.SetActive(true);
                    if (ingotBtns[int.Parse(GameManager.playerdata.ingotData[i].UID[3].ToString())].transform.parent.GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI amountText))
                    {
                        amountText.text = GameManager.playerdata.ingotData[i].Amount.ToString();
                    }

                }
            }
        }




    }
}
