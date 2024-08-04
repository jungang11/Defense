using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private EventSystem eventSystem;

    private Canvas popUpCanvas;
    private Stack<PopUpUI> popUpStack;

    private Canvas windowCanvas;
    private List<WindowUI> windowList;

    private Canvas inGameCanvas;

    public void OnInit()
    {
        eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
        eventSystem.transform.parent = transform;

        popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        popUpCanvas.gameObject.name = "PopUpCanvas";
        popUpCanvas.sortingOrder = 100;
        popUpStack = new Stack<PopUpUI>();

        windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        windowCanvas.gameObject.name = "WindowCanvas";
        windowCanvas.sortingOrder = 50;
        windowList = new List<WindowUI>();

        inGameCanvas = GameManager.Resource.Instantiate<Canvas>("UI/Canvas");
        inGameCanvas.gameObject.name = "InGameCanvas";
        inGameCanvas.sortingOrder = 0;
    }

    #region PopUpUI
    public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
    {
        if(popUpStack.Count > 0)
        {
            PopUpUI prevUI = popUpStack.Peek();
            prevUI.gameObject.SetActive(false);
        }

        T ui = GameManager.Pool.GetUI<T>(popUpUI);
        ui.transform.SetParent(popUpCanvas.transform, false);
        popUpStack.Push(ui);

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
        GameManager.Pool.ReleaseUI(ui.gameObject);

        if (popUpStack.Count > 0)
        {
            PopUpUI currentUI = popUpStack.Peek();
            currentUI.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    #endregion PopUpUI

    #region WindowUI
    public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowList.Add(windowUI);

        T ui = GameManager.Pool.GetUI(windowUI);
        ui.transform.SetParent(windowCanvas.transform, false);

        RefreshWindowUI();

        return ui;
    }

    public T ShowWindowUI<T>(string path) where T : WindowUI
    {
        T ui = GameManager.Resource.Load<T>(path);
        return ShowWindowUI(ui);
    }

    public void RefreshWindowUI()
    {
        foreach(var window in windowList)
        {
            window.transform.SetAsFirstSibling();
        }
    }

    public void SelectWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowUI.transform.SetAsLastSibling();
    }

    public void CloseWindowUI<T>(T windowUI) where T : WindowUI
    {
        windowList.Remove(windowUI);
        RefreshWindowUI();
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
