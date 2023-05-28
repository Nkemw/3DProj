using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSelect : MonoBehaviour
{
    [SerializeField] Button[] MaterialBtns;

    [SerializeField] Button[] TypeSelectBtns;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < MaterialBtns.Length; i++)
        {
            int index = i;
            MaterialBtns[i].onClick.AddListener(() => this.SelectMaterial(index));
        }
    }

    public void SelectMaterial(int index)
    {
        gameObject.SetActive(!gameObject.activeSelf);

        switch (IngotMakeController.selectedPopupImgIndex)
        {
            case 0:
                if (TypeSelectBtns[0].transform.TryGetComponent<Image>(out Image img))
                {
                    img.sprite = Resources.Load<Sprite>("m00" + (index*2+1).ToString());
                    IngotMakeController.selectedBaseIngotIndex = index;
                }
                 break;
            case 1:
                if (TypeSelectBtns[1].transform.TryGetComponent<Image>(out Image img2))
                {
                    img2.sprite = Resources.Load<Sprite>("m00" + (index*2+2).ToString());
                    IngotMakeController.selectedSubIngotIndex = index;
                }
                break;
            case 2:
                if (TypeSelectBtns[2].transform.TryGetComponent<Image>(out Image img3))
                {
                    if (index < 2)
                    {
                        img3.sprite = Resources.Load<Sprite>("p00" + (index+1).ToString());
                        IngotMakeController.selectedAddtive_UID = "p00" + (index + 1).ToString();
                    }
                    else if(index < 4)
                    {
                        img3.sprite = Resources.Load<Sprite>("b00" + (index-1).ToString());
                        IngotMakeController.selectedAddtive_UID = "b00" + (index - 1).ToString();
                    } else if(index < 6)
                    {
                        img3.sprite = Resources.Load<Sprite>("p00" + (index-1).ToString());
                        IngotMakeController.selectedAddtive_UID = "p00" + (index - 1).ToString();
                    } else if(index < 8)
                    {
                        img3.sprite = Resources.Load<Sprite>("b00" + (index-3).ToString());
                        IngotMakeController.selectedAddtive_UID = "b00" + (index - 3).ToString();
                    } else if(index < 10)
                    {
                        img3.sprite = Resources.Load<Sprite>("p00" + (index-3).ToString());
                        IngotMakeController.selectedAddtive_UID = "p00" + (index - 3).ToString();
                    } else if(index < 12)
                    {
                        img3.sprite = Resources.Load<Sprite>("b00" + (index-5).ToString());
                        IngotMakeController.selectedAddtive_UID = "b00" + (index - 5).ToString();
                    }

                    IngotMakeController.selectedAdditiveIndex = index;
                }
                break;
        }
    }
}
