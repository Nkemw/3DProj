using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour, WarpObject
{
    public void LoadNextScene()
    {
        PlayerController.stateAfterLoadScene = 2;
        PlayerController.stateBeforeLoadVillageScene = 1;
        SceneManager.LoadScene("LoadingScene");
    }


    
}
