using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandleSelect : MonoBehaviour
{
    [SerializeField] Button[] handleBtns;

    [SerializeField] Button handleSelectButtonImg;
    [SerializeField] Image resultImg;

    [SerializeField] TextMeshProUGUI selectedHandleText;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < handleBtns.Length; i++)
        {
            int index = i;
            handleBtns[i].onClick.AddListener(() => this.SelectMaterial(index));
        }
    }

    public void SelectMaterial(int index)   //index: 0~5
    {

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (handleSelectButtonImg.TryGetComponent<Image>(out Image img))
            {

                switch (index % 2)
                {
                    case 0:
                        img.sprite = Resources.Load<Sprite>("p00" + (index + 1).ToString()); //1 3 5
                        selectedHandleText.text = "<color=blue>" + GameManager.Instance.FindMaterialRow("p00" + (index + 1).ToString()).itemName + " (★" + GameManager.Instance.FindMaterialRow("p00" + (index + 1).ToString()).starValue + ")</color>\n<color=purple>특성 : " + GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow("p00" + (index + 1).ToString()).effect).effectName.Substring(0, 2) + "</color>";
                        break;
                    case 1:
                        img.sprite = Resources.Load<Sprite>("b00" + (index).ToString());
                        selectedHandleText.text = "<color=blue>" + GameManager.Instance.FindMaterialRow("b00" + (index).ToString()).itemName + " (★" + GameManager.Instance.FindMaterialRow("b00" + (index).ToString()).starValue + ")</color>\n<color=purple>특성 : "+ GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow("b00" + (index).ToString()).effect).effectName.Substring(0, 2) + "</color>";
                        break;
                }

                HandleMakeController.selectedHandleIndex = index;

                if(HandleMakeController.selectedGripIndex >= 0) {
                    resultImg.sprite = Resources.Load<Sprite>("Handle");
                }
            }
        }

    }
}
