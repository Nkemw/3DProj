using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCameraMove : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    private Vector3 cameraOffset = new Vector3(0f, 100f, 0f);

    Camera miniMapCamera;

    private Text text;

    /*private void Awake()
    {
        miniMapCamera = gameObject.GetComponent<Camera>();
        text.text = "Player";
    }*/
    // Update is called once per frame
    void Update()
    {
        //miniMapCamera.transform.position = playerPos.position + cameraOffset;
    }
}
