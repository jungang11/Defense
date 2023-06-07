using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private void Awake()
    {
        // 씬 실행시 Resource/UI 폴더의 EventSystem 프리팹을 생성
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");

        // 씬 변경시에도 EventSystem이 유지되도록 UIManager의 하위 자식으로 설정
        eventSystem.transform.parent = transform;
    }
}
