using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public static ShakeCamera Instance => instance;

    private float shakeTime;        // ī�޶� ��鸲 ���� �ð�
    private float shakeIntensity;   // ī�޶� ��鸲 ����

    private CameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StartCoroutine(ShakeByRotation());
        StopCoroutine(ShakeByRotation());
    }

    IEnumerator ShakeByPosition()
    {
        // ��鸮�� ������ ���� ��ġ
        Vector3 startPosition = Camera.main.transform.position;

        while (shakeTime > 0.0f)
        {
            // �ʱ� ��ġ���� �� ����(Size 1) * shakeIntensity�� ���� �ȿ��� ī�޶� ��ġ ����
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            // �ð� ����
            shakeTime -= Time.deltaTime;

            yield return null;
        }
        Camera.main.transform.position = startPosition;
    }

    IEnumerator ShakeByRotation()
    {
        // ī�޶� ��鸲 ȿ�� ��� ����
        cameraController.isShaking = true;

        // ��鸮�� ������ �ʱ� ��
        Vector3 startRotation = Camera.main.transform.eulerAngles;

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);
            Camera.main.transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        // ��鸮�� ������ ������ ����
        Camera.main.transform.rotation = Quaternion.Euler(startRotation);

        // ī�޶� ��鸲 ȿ�� ����
        cameraController.isShaking = false;
    }

    public ShakeCamera()
    {
        instance = this;
    }
}
