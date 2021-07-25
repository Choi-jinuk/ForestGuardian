using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : CharacterStatus
{
    [Range(0, 100f)]
    public float amount = 50; // 느려지는 퍼센트값
    protected float updateValue; // 원래 수치

    protected override void AddAction()
    {
        float origin = myTarget.MoveSpeed;
        myTarget.MoveSpeed = (myTarget.MoveSpeed * (1 - amount / 100));

        updateValue = origin - myTarget.MoveSpeed;
    }

    protected override void RemoveAction()
    {
        myTarget.MoveSpeed += updateValue;
        Destroy(this);
    }
}
