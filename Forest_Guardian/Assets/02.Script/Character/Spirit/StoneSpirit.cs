using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpirit : Spirit
{
    public float knockBackForce = 2f; // 넉백 추가량
    public float knockBackRange = 0.5f; // 넉백 범위

    public override void Attack()
    {
        if (!attackTarget) return; // enemy가 아니면 중단

        if (animator)
        {
            ActionAnimSpeedUpdate();
            animator.SetBool("isAction", true); // 애니메이션에서 던지기 이벤트 실행
        }
    }

    public void KnockBack(float range) // 넉백 범위
    {
        // 적이 나보다 왼쪽에 있으면 왼쪽으로, 아니면 오른쪽으로 방향지정
        Vector2 dir = attackTarget.transform.position.x < transform.position.x ? Vector2.left : Vector2.right;

        // 공격 대상 기준위치에서 넉백 일으킴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackTarget.transform.position, knockBackRange, 1 << LayerMask.NameToLayer("Enemy"));

        int length = Mathf.Min(colliders.Length, maxAttackNum);

        for (int i = 0; i < length; i++)
        {
            Character character = colliders[i].GetComponent<Character>();

            if (character)
            {
                character.Damage(damage);
                character.rigd.velocity += dir * knockBackForce;
            }
        }
        
        /*
        attackTarget.Damage(damage);
        attackTarget.rigd.velocity += dir * knockBackForce;
        */
    }


}
