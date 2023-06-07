using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    // key값이 이름인 Dictionary 생성. UI는 RectTransform 사용
    protected Dictionary<string, RectTransform> transforms;
    protected Dictionary<string, Button> buttons;
    protected Dictionary<string, TMP_Text> texts;
    // TODO : add ui Component

    protected virtual void Awake()
    {
        BindChildren();
    }

    // UI Binding
    public void BindChildren()
    {
        // Dictionary 초기화
        transforms = new Dictionary<string, RectTransform>();
        buttons = new Dictionary<string, Button>();
        texts = new Dictionary<string, TMP_Text>();

        // GetComponents In Children
        RectTransform[] children = GetComponentsInChildren<RectTransform>();
        foreach(RectTransform child in children)
        {
            string key = child.gameObject.name;

            // 이름이 중복되는 경우가 있을 수 있음.
            if (transforms.ContainsKey(key))
                continue;

            transforms.Add(key, child);

            Button button = child.GetComponent<Button>();
            if(button != null)
                buttons.Add(key, button);

            TMP_Text text = child.GetComponent<TMP_Text>();
            if (text != null)
                texts.Add(key, text);
        }
    }
}
