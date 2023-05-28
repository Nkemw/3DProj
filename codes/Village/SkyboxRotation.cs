using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float rotationSpeed = 1f; // 회전 속도

    private void Update()
    {
        // 회전 각도 계산
        float angle = Time.time * rotationSpeed;

        // 큐브 맵 머티리얼 회전 설정
        RenderSettings.skybox.SetFloat("_Rotation", angle);
    }
}