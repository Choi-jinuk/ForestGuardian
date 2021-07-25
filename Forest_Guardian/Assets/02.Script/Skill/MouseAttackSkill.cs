using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseAttackSkill : SkillBase
{
    // 스킬 정보
    public MouseAttackSkillInfo[] mouseAttackInfos = new MouseAttackSkillInfo[4];

    // 현재 레벨정보
    public MouseAttackSkillInfo CurrentInfo { get { return mouseAttackInfos[level-1]; } }

    [Header("Effect Sound")]
    public SoundManager audioSource;

    public override void UseSkill()
    {
        if (level <= 0) return; // 스킬을 안배웠으면 중단
        if (nextSkillTime > Time.time) return; // 쿨타임이면 중단

        nextSkillTime = Time.time + CurrentInfo.coolTime; // 쿨타임 업데이트

        if (coolTimeIcon)
        {
            coolTimeIcon.StartCoolTime(CurrentInfo.coolTime);
        }

        Vector2 mousePos = Genie.GetMousePos(); // 마우스 위치 반환

        // 마우스 위치에 지정된 범위만큼 적들을 반환받음
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, CurrentInfo.attackRadius, 1 << LayerMask.NameToLayer("Enemy"));

        if (colliders.Length == 0) return; // 적이 없으면 중단

        // 모든 적들에게 데미지
        foreach (var coll in colliders)
        {
            coll.GetComponent<Character>().Damage(CurrentInfo.attackDamage);
        }

        audioSource.PlayEffect(CurrentInfo.attackSound); // 공격 효과음 실행

        GameObject effect = Instantiate(CurrentInfo.attackEffect, mousePos, Quaternion.identity); // 공격 이펙트 생성
        // Destroy(particle.gameObject, particle.startLifetime); // 이펙트 지속시간 끝나면 삭제
        

    }

    public override EnergyInfo GetUpgradeEnergy()
    {
        return mouseAttackInfos[level].upgradeEnergy;
    }


}
