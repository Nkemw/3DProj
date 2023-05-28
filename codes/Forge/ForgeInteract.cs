using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeInteract : MonoBehaviour
{
    [SerializeField] private int interactObjNum;
    public static int currentInteractObjNum;

    public static bool playerIsInsideCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ��ȣ�ۿ� ����");
            playerIsInsideCollider = true;
            currentInteractObjNum = interactObjNum;
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInsideCollider = false;
        }
    }
}
