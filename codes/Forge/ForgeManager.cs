using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForgeManager : MonoBehaviour, WarpObject, UIControl
{
    [SerializeField] Canvas deskCanvas;     //������ ���� ĵ����
    [SerializeField] Canvas anvil;          //����,���� ���� ĵ����
    [SerializeField] Canvas stoveCanvas;    //�ֱ� �߰� ĵ����
    [SerializeField] Canvas bucketCanvas;   //÷���� �߰� ĵ����
    [SerializeField] Canvas storageCanvas;  //â�� ĵ����
    [SerializeField] Canvas polishingCanvas; //���� ĵ����

    [SerializeField] Button interactBtn;

    private void Awake()
    {
        interactBtn.onClick.AddListener(UIActive);
    }
    public void UIActive()
    {
        Debug.Log("UIActive ����");
        if (ForgeInteract.playerIsInsideCollider)
        {
            switch (ForgeInteract.currentInteractObjNum)
            {
                case (int)ForgeInteractObj.Desk:
                    if (deskCanvas.TryGetComponent<SelectCanvas>(out SelectCanvas selectCanvas))
                    {
                        selectCanvas.InitUI();
                    }
                    break;

                case (int)ForgeInteractObj.Anvil:
                    if (anvil.TryGetComponent<BladeMakeController>(out BladeMakeController bladeController))
                    {
                        bladeController.InitUI();
                    }
                    break;

                case (int)ForgeInteractObj.Stove:
                    if(stoveCanvas.TryGetComponent<IngotMakeController>(out IngotMakeController ingotController)){
                        ingotController.InitUI();
                    }
                    //stoveCanvas.gameObject.SetActive(true);
                    break;

                case (int)ForgeInteractObj.Bucket:
                    bucketCanvas.gameObject.SetActive(true);
                    break;

                case (int)ForgeInteractObj.Storage:
                    storageCanvas.gameObject.SetActive(true);
                    break;
                case (int)ForgeInteractObj.Polishing:
                    if (polishingCanvas.TryGetComponent<PolishController>(out PolishController polishController))
                    {
                        polishController.InitUI();
                    }
                    break;

            }
        }
        
    }

    public void LoadNextScene()
    {
        PlayerController.stateAfterLoadScene = 2;
        PlayerController.stateBeforeLoadVillageScene = 0;
        SceneManager.LoadScene("LoadingScene");
    }

}
