using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProducedHandleSelect : MonoBehaviour
{
    [SerializeField] Button[] handleBtns;

    [SerializeField] Button handleSelectButtonImg;
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
                img.sprite = Resources.Load<Sprite>("Handle");

                WeaponMakeController.selectedHandleIndex = index;
            }
        }
    }
}
