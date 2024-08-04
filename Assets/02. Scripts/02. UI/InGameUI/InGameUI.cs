using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : BaseUI
{
    public Transform followTarget;
    public Vector3 followOffset;

    private void LateUpdate()
    {
        if(followTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }

    public void SetTarget(Transform target)
    {
        followTarget = target;
        if (followTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }

    public void SetOffSet(Vector2 offset)
    {
        followOffset = offset;
        if (followTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
        }
    }
}
