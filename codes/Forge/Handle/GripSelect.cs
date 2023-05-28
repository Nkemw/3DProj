using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GripSelect : MonoBehaviour
{
    [SerializeField] Button[] gripBtns;

    [SerializeField] Button gripSelectButtonImg;
    [SerializeField] Image resultImg;

    [SerializeField] TextMeshProUGUI selectedGripText;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < gripBtns.Length; i++)
        {
            int index = i;
            gripBtns[i].onClick.AddListener(() => this.SelectMaterial(index));
        }
    }

    public void SelectMaterial(int index)   //index: 0~5
    {

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gripSelectButtonImg.TryGetComponent<Image>(out Image img))
            {

                switch (index % 2)
                {
                    case 0:
                        img.sprite = Resources.Load<Sprite>("p00" + (index + 2).ToString()); 
                        selectedGripText.text = "<color=blue>" + GameManager.Instance.FindMaterialRow("p00" + (index + 2).ToString()).itemName + " (★" + GameManager.Instance.FindMaterialRow("p00" + (index + 2).ToString()).starValue + ")</color>\n<color=purple>특성 : " + GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow("p00" + (index + 2).ToString()).effect).effectName.Substring(0, 2) + "</color>";
                        break;
                    case 1:
                        img.sprite = Resources.Load<Sprite>("b00" + (index + 1).ToString());
                        selectedGripText.text = "<color=blue>" + GameManager.Instance.FindMaterialRow("b00" + (index + 1).ToString()).itemName + " (★" + GameManager.Instance.FindMaterialRow("b00" + (index + 1).ToString()).starValue + ")</color>\n<color=purple>특성 : " + GameManager.Instance.FindEffectRow(GameManager.Instance.FindMaterialRow("b00" + (index + 1).ToString()).effect).effectName.Substring(0, 2) + "</color>";
                        break;
                }

                HandleMakeController.selectedGripIndex = index;

                if (HandleMakeController.selectedHandleIndex >= 0)
                {
                    resultImg.sprite = Resources.Load<Sprite>("Handle");
                }
            }
        }
    }
}
