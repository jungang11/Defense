using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Scriptable Object 주의점
 * 
 * 에디터에서는 Scriptable Object를 생성해 데이터를 저장하는 작업이 언제든 가능하지만
 * 배포된 빌드에서는 Scriptable 오브젝트를 새로 생성할 수 없음
 * 개발하면서 만들어둔 Scriptable Object Asset 에 저장된 데이터만 사용가능
 * 
 * 에셋형태로 저장되기 때문에 에셋 번들로 빌드하고 배포하는 방식으로 게임 데이터를 업데이트하는데 사용됨
 * 게임 빌드에 포함되는 방식이라 설치된 게임의 데이터를 변조할 수 있음
 * 멀티게임에서는 Scriptable Object에 게임에 중요한 영향을 주는 데이터를 저장해선 안됨
 */

[CreateAssetMenu (fileName = "TowerData", menuName = "Data/Tower")]
public class TowerData : ScriptableObject
{
    [SerializeField] TowerInfo[] towers;
    public TowerInfo[] Towers { get { return towers; } }

    [Serializable]
    public class TowerInfo
    {
        public Tower tower;

        public int damage;
        public float delay;
        public float range;

        public float buildTime;
        public int buildCost;
        public int sellCost;
    }
}
