using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float padding;

    Vector2 moveDir;
    private float zoomScroll;

    public bool isShaking { get; set; }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void LateUpdate()
    {
        if (isShaking) return;

        Move();
        Zoom();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveDir.y * moveSpeed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.right * moveDir.x * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnPointer(InputValue value)
    {
        Vector2 mousePos = value.Get<Vector2>();

        if (mousePos.x <= 0 + padding)
            moveDir.x = -1;
        else if (mousePos.x >= Screen.width - padding)
            moveDir.x = 1;
        else
            moveDir.x = 0;

        if (mousePos.y <= 0 + padding)
            moveDir.y = -1;
        else if (mousePos.y >= Screen.height - padding)
            moveDir.y = 1;
        else
            moveDir.y = 0;

    }

    private void Zoom()
    {
        // forward 로 갈수록 확대
        transform.Translate(Vector3.forward * zoomScroll * zoomSpeed * Time.deltaTime, Space.Self);
    }

    private void OnZoom(InputValue value)
    {
        zoomScroll = value.Get<Vector2>().y;
        Debug.Log(zoomScroll);
    }
}
