using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 상태 (CC기)
public abstract class CharacterStatus : MonoBehaviour
{
    public float time = 1f;
    public float removeTime = 0;
    protected Character myTarget; // 상태가 적용된 캐릭터

    // 상태 추가
    public virtual void AddStatus(Character target)
    {
        myTarget = target;
        myTarget.statuses.Add(this);
        removeTime = Time.time + time;
        AddAction();
        Invoke("RemoveStatus", time);
    }

    protected abstract void AddAction(); // 추가하면서 변경되는 것

    // 상태 제거
    public virtual void RemoveStatus()
    {
        if (myTarget == null) return;

        RemoveAction();
        myTarget.statuses.Remove(this);
    }

    protected abstract void RemoveAction(); // 제거하면서 변경되는 것
}