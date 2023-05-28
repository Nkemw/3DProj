using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ForgeManager : MonoBehaviour, WarpObject, UIControl
{
    [SerializeField] Canvas deskCanvas;     //손잡이 제작 캔버스
    [SerializeField] Canvas anvil;          //단조,주조 제작 캔버스
    [SerializeField] Canvas stoveCanvas;    //주괴 추가 캔버스
    [SerializeField] Canvas bucketCanvas;   //첨가제 추가 캔버스
    [SerializeField] Canvas storageCanvas;  //창고 캔버스
    [SerializeField] Canvas polishingCanvas; //연마 캔버스

    [SerializeField] Button interactBtn;

    private void Awake()
    {
        interactBtn.onClick.AddListener(UIActive);
    }
    public void UIActive()
    {
        Debug.Log("UIActive 실행");
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
