using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;
    public static ShakeCamera Instance => instance;

    private float shakeTime;        // 카메라 흔들림 지속 시간
    private float shakeIntensity;   // 카메라 흔들림 세기

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

    IEnumerator ShakeByRotation()
    {
        // 카메라 흔들림 효과 재생 시작
        cameraController.isShaking = true;

        // 흔들리기 직전의 초기 값
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

        // 흔들리기 직전의 값으로 변경
        Camera.main.transform.rotation = Quaternion.Euler(startRotation);

        // 카메라 흔들림 효과 종료
        cameraController.isShaking = false;
    }

    public ShakeCamera()
    {
        instance = this;
    }
}
