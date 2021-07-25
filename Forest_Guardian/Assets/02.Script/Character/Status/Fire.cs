using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : CharacterStatus
{
    public int damage = 1; // 피해량
    public float damageDelay = 1f; // 데미지 딜레이
    public int damageNum = 3; // 피해입힐 데미지 회수
    protected int damageCount = 0; // 현재 데미지 회수

    public void Init(int damage, float damageDelay, int damageNum)
    {
        this.damage = damage;
        this.damageDelay = damageDelay;
        this.damageNum = damageNum;
    }

    public override void AddStatus(Character target)
    {
        myTarget = target;
        myTarget.statuses.Add(this);
        AddAction();
    }

    protected override void AddAction()
    {
        FireDamage();
    }

    protected override void RemoveAction()
    {
        Destroy(this);
    }
    
    void FireDamage()
    {
        myTarget.Damage(damage);
        damageCount++;

        if(damageNum > damageCount) // 아직 회수가 남아있을 때
        {
            Invoke("FireDamage", damageDelay);
        }
        else
        {
            RemoveStatus();
        }
    }

}