using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private void Awake()
    {
        // �� ����� Resource/UI ������ EventSystem �������� ����
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");

        // �� ����ÿ��� EventSystem�� �����ǵ��� UIManager�� ���� �ڽ����� ����
        eventSystem.transform.parent = transform;
    }
}
