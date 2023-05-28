using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingManager : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;

    string loadSceneName;

    [SerializeField] TextMeshProUGUI tipText;
    [SerializeField] TextMeshProUGUI loadingMent;

    private void Awake()
    {
        loadingSlider.value = 0;

        switch (PlayerController.stateAfterLoadScene)
        {
            case 0:
                loadSceneName = "ForgeScene";
                break;

            case 1:
                loadSceneName = "ShopScene";
                break;

            case 2:
                loadSceneName = "VillageScene";
                break;
        }

        tipText.text = GameManager.TableDB.LoadingDB[Random.Range(0, GameManager.TableDB.LoadingDB.Count)].loadingTip;
        loadingMent.text = GameManager.TableDB.LoadingDB[Random.Range(0, GameManager.TableDB.LoadingDB.Count)].loadingMent;
        StartCoroutine("LoadAsyncScene");
    }


    [SerializeField] private float loadTime = 2f;
    private float currentTime = 0;
    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(0.05f);
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(loadSceneName);
        asyncScene.allowSceneActivation = false;


        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;

            loadingSlider.value = Mathf.Lerp(0, 1, currentTime / loadTime);

            if (loadingSlider.value == 1f)
            {
                asyncScene.allowSceneActivation = true;
            }
        }
    }
}
