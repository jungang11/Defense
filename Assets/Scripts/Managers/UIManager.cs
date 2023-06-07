using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private Canvas popUpCanvas;
    private Stack<PopUpUI> popUpStack;

    private void Awake()
    {
        // 씬 실행시 Resource/UI 폴더의 EventSystem 프리팹을 생성
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
        // 씬 변경시에도 EventSystem이 유지되도록 UIManager의 하위 자식으로 설정
        eventSystem.transform.parent = transform;

        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100;
        popUpStack = new Stack<PopUpUI>();
    }

    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        // 이전에 연 팝업이 있을 때
        if(popUpStack.Count > 0)
        {
            // 이전 UI로 스택의 상위 객체를 확인한 후 비활성화
            PopUpUI prevUI = popUpStack.Peek();
            prevUI.gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popUpUI);
        ui.transform.SetParent(popUpCanvas.transform, false);
        popUpStack.Push(ui);

        // 시간 정지
        Time.timeScale = 0f;
        return ui;
    }

    public T ShowPopUpUI<T>(string path) where T : PopUpUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowPopUpUI(ui);
    }

    public void ClosePopUpUI()
    {
        PopUpUI ui = popUpStack.Pop();
        // 스택의 가장 위에있는 ui를 반납
        GameManager.Pool.ReleaseUI(ui.gameObject);

        if (popUpStack.Count > 0)
        {
            PopUpUI currentUI = popUpStack.Peek();
            currentUI.gameObject.SetActive(true);
        }
        else
        {
            // Stack이 비었을 경우(팝업 UI가 모두 닫혔을 경우) 시간이 흐르도록 함
            Time.timeScale = 1f;
        }
    }
}
