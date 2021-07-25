using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : Spirit
{
    [Header("Fire")]
    public FireProejctile fireObject; // 불 오브젝트
    public Transform fireSpawnTr; // 불 생성위치

    public override void Attack()
    {
        if (!attackTarget) return; // enemy가 아니면 중단

        if (animator)
        {
            ActionAnimSpeedUpdate();
            animator.SetBool("isAction", true); // 애니메이션에서 던지기 이벤트 실행
        }
    }

    // 불 발사
    public void Throw()
    {
        FireProejctile fire = Instantiate(fireObject, fireSpawnTr.position, Quaternion.identity);
        
        Vector2 dir = attackTarget.transform.position - fireSpawnTr.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        fire.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}