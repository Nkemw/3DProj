using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DispatchWarp : MonoBehaviour, WarpObject
{
    private bool playerIsInsideCollider;

    [SerializeField] private Button interactBtn;

    private void Awake()
    {
        interactBtn.onClick.AddListener(LoadNextScene);
    }

    public void LoadNextScene()
    {
        

        if (playerIsInsideCollider)
        {
            PlayerController.stateAfterLoadScene = 3;
            SceneManager.LoadScene("LoadingScene");
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInsideCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInsideCollider = false;
        }
    }

}
