using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpirit : Spirit
{
    [Header("Slow")]
    public SlowBoom slowBoom;
    public float slowAmount = 50f;
    
    public override void Attack()
    {
        if (nextAttackTime > Time.time) return; // 공격시간이 아니면 중단
        if (!attackTarget) return; // 적이 없으면 중단

        if (animator)
        {
            ActionAnimSpeedUpdate();
            animator.SetBool("isAction", true); // 애니메이션에서 던지기 이벤트 실행
        }
    }


    public void Throw()
    {
        if (!attackTarget) return; // 적이 없으면 중단

        SlowBoom boom = Instantiate(slowBoom, attackTarget.transform.position, Quaternion.identity); // 슬로우 생성
        boom.NearSlow(slowAmount); // 근처의 적에게 슬로우 발동
    }



}
