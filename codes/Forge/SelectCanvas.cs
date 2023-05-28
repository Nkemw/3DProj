using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCanvas : MonoBehaviour, UIActiveEvent
{
    //UI √ ±‚»≠
    [SerializeField] GameObject particle;
    [SerializeField] GameObject player;
    [SerializeField] Image fadeImg;
    [SerializeField] GameObject tableCamera;

    [SerializeField] Button[] typeSelectBtns;

    [SerializeField] Canvas handleCanvas;
    [SerializeField] Canvas weaponCanvas;

    private void Awake()
    {
        for (int i = 0; i < typeSelectBtns.Length; i++)
        {
            int index = i;
            typeSelectBtns[i].onClick.AddListener(() => this.InitSelectCanvas(index));
        }
    }

    public void InitSelectCanvas(int index)
    {
        ExitUI();
        if (index == 0)
        {
            if (handleCanvas.TryGetComponent<HandleMakeController>(out HandleMakeController handleCanvasController))
            {
                handleCanvasController.InitUI();
            }
        } else
        {
            if (weaponCanvas.TryGetComponent<WeaponMakeController>(out WeaponMakeController weaponCanvasController))
            {
                weaponCanvasController.InitUI();
            }
        }
    }
    public void InitUI()
    {
        gameObject.SetActive(true);
        particle.SetActive(false);
        player.SetActive(false);
        fadeImg.gameObject.SetActive(true);
        tableCamera.SetActive(true);

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        yield return null;

        float startTime = 0f;
        float endTime = 0.5f;

        Color newColor = new Color();
        while (startTime / endTime < 1f)
        {
            startTime += Time.deltaTime;

            newColor.a = Mathf.Lerp(1f, 0f, startTime / endTime);
            fadeImg.color = newColor;
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
        StopCoroutine("FadeOut");
    }

    public void ExitUI()
    {
        Color newColor = fadeImg.color;
        newColor.a = 1f;
        fadeImg.color = newColor;

        gameObject.SetActive(false);
        particle.SetActive(true);
        //player.SetActive(true);
        //tableCamera.SetActive(false);
    }

}
