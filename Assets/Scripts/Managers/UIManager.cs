using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private Canvas popUpCanvas;
    private Stack<PopUpUI> popUpStack;

    // Window는 원래 List구조(LinkedList -> c#)로 사용 -> Unity라 사용x
    private Canvas windowCanvas;

    private Canvas inGameCanvas;

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

        // popUpUI는 Stack구조를 사용하기 때문에 windowUI는 따로 작성
        windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.sortingOrder = 50;

        inGameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        inGameCanvas.gameObject.name = "InGameCanvas";
        inGameCanvas.sortingOrder = 0;
    }

    #region PopUpUI
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
    #endregion PopUpUI

    #region WindowUI
    public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    {
        T ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);

        return ui;
    }

    public T ShowWindowUI<T>(string path) where T : WindowUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowWindowUI(ui);
    }

    public void SelectWindowUI<T>(T windowUI) where T : WindowUI
    {
        // Canvas 상에서 위인지 아래인지 transform에서 관리
        // SetAsLastSibling : 계층 구조 상에서 마지막으로 위치
        windowUI.transform.SetAsLastSibling();
    }

    public void CloseWindowUI<T>(T windowUI) where T : WindowUI
    {
        GameManager.Pool.ReleaseUI(windowUI.gameObject);
    }
    #endregion WindowUI

    public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
    {
        T ui = GameManager.Pool.GetUI(inGameUI);
        ui.transform.SetParent(inGameCanvas.transform, false);

        return ui;
    }

    public T ShowInGameUI<T>(string path) where T : InGameUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowInGameUI(ui);
    }

    public void CloseInGameUI<T>(T inGameUI) where T : InGameUI
    {
        GameManager.Pool.ReleaseUI(inGameUI.gameObject);
    }
}
