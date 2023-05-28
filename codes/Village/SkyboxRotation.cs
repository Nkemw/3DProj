using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public float rotationSpeed = 1f; // ȸ�� �ӵ�

    private void Update()
    {
        // ȸ�� ���� ���
        float angle = Time.time * rotationSpeed;

        // ť�� �� ��Ƽ���� ȸ�� ����
        RenderSettings.skybox.SetFloat("_Rotation", angle);
    }
}