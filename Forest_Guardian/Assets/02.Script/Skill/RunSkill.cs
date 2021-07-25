using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSkill : SkillBase
{
    // 스킬 정보
    public RunSkillInfo[] runSkillInfos = new RunSkillInfo[3];

    // 현재 레벨정보
    public RunSkillInfo CurrentInfo { get { return runSkillInfos[level - 1]; } }

    [Header("Effect Sound")]
    public SoundManager audioSource;

    private Vector2 dashVelocity = Vector2.zero;

    public override void UseSkill()
    {
        if (level <= 0) return; // 스킬 안배웠으면 중단
        if (inUseSkill) return; // 이미 사용중이면 중단
        if (genie.dragSkill.inUseSkill) return; // 포탄 발사중이면 중단
        if (genie.Velocity.x == 0) return; // 이동중이 아니면 중단
        if (nextSkillTime > Time.time) return; // 쿨타임이면 중단

        inUseSkill = true;

        genie.trailRenderer.startColor = CurrentInfo.startColor;
        genie.trailRenderer.endColor = CurrentInfo.endColor;
        genie.trailRenderer.startWidth = 0.3f;
        genie.trailRenderer.endWidth = 0f;
        if (genie.trailRenderer)
            genie.trailRenderer.enabled = true;

        // 이동방향으로 속도 증가
        Vector2 velocity = genie.rigd.velocity;
        velocity.x = velocity.normalized.x * CurrentInfo.dashSpeed;
        velocity.y = 0f;
        
        genie.rigd.velocity = velocity;
        dashVelocity = velocity; // 속도 저장

        //genie.audioSource.PlayOneShot(CurrentInfo.dashSound);
        audioSource.PlayEffect(CurrentInfo.dashSound);

        nextSkillTime = Time.time + CurrentInfo.coolTime;

        if (coolTimeIcon)
        {
            coolTimeIcon.StartCoolTime(CurrentInfo.coolTime);
        }

        Invoke("SkillEnd", CurrentInfo.dashTime);

        // genie.audioSource.PlayOneShot(CurrentInfo.runSound); // 효과음 플레이
        // Instantiate(CurrentInfo.runEffect, transform.position, Quaternion.identity); // 이펙트 생성
    }

    private void Update()
    {
        if (inUseSkill)
        {
            genie.rigd.velocity = dashVelocity;
        }
    }

    public void SkillEnd()
    {
        inUseSkill = false;
        if (genie.trailRenderer)
            genie.trailRenderer.enabled = false;
    }

    public override EnergyInfo GetUpgradeEnergy()
    {
        return runSkillInfos[level].upgradeEnergy;
    }
}
