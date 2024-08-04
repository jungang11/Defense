using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Scriptable Object ������
 * 
 * �����Ϳ����� Scriptable Object�� ������ �����͸� �����ϴ� �۾��� ������ ����������
 * ������ ���忡���� Scriptable ������Ʈ�� ���� ������ �� ����
 * �����ϸ鼭 ������ Scriptable Object Asset �� ����� �����͸� ��밡��
 * 
 * �������·� ����Ǳ� ������ ���� ����� �����ϰ� �����ϴ� ������� ���� �����͸� ������Ʈ�ϴµ� ����
 * ���� ���忡 ���ԵǴ� ����̶� ��ġ�� ������ �����͸� ������ �� ����
 * ��Ƽ���ӿ����� Scriptable Object�� ���ӿ� �߿��� ������ �ִ� �����͸� �����ؼ� �ȵ�
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
