using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PolishedBladeSelect : MonoBehaviour
{
    [SerializeField] Button[] handleBtns;

    [SerializeField] Button handleSelectButtonImg;
    //[SerializeField] Image resultImg;
    void Awake()
    {
        for (int i = 0; i < handleBtns.Length; i++)
        {
            int index = i;
            handleBtns[i].onClick.AddListener(() => this.SelectMaterial(index));
        }
    }

    public void SelectMaterial(int index)   //index: 0~35
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (handleSelectButtonImg.TryGetComponent<Image>(out Image img))
            {
                img.sprite = Resources.Load<Sprite>(GameManager.playerdata.bladeData[index].BladeType.ToString() + "Blade");

                WeaponMakeController.selectedBladeIndex = index;
            }
        }
    }
}
