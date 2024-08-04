using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowUI : BaseUI, IDragHandler, IPointerDownHandler
{
    protected override void Awake()
    {
        base.Awake();

        buttons["CloseButton"].onClick.AddListener(() => { GameManager.UI.CloseWindowUI(this); });
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position이 Vector3이기 때문에 eventData.delta를 Vector3로 변환
        transform.position += (Vector3)eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.UI.SelectWindowUI(this);
    }
}
