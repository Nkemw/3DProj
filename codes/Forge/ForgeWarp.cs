using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ForgeWarp : MonoBehaviour, WarpObject
{
    private bool playerIsInsideCollider;

    [SerializeField] private Button interactBtn;

    private void Awake()
    {
        interactBtn.onClick.AddListener(LoadNextScene);
    }

    public void LoadNextScene()
    {
        /*GameObject temp;
        temp = GameObject.Find("MiniGameManager");
       
        if(temp.TryGetComponent<MiniGameManager>(out MiniGameManager miniGameManager)){
            miniGameManager.MiniGameCanvasActive();
        }*/

        if (playerIsInsideCollider)
        {
            PlayerController.stateAfterLoadScene = 0;
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

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }*/
}
