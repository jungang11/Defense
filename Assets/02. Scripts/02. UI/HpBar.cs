using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] EnemyController enemy;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = enemy.HP;
        slider.value = enemy.HP;
        enemy.OnChangedHP.AddListener(SetValue);
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
}
