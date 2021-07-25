using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDownSkill : SkillBase
{

    // 스킬 정보
    public TakeDownSkillInfo[] takeDownSkillInfos = new TakeDownSkillInfo[3];

    // 현재 레벨정보
    public TakeDownSkillInfo CurrentInfo { get { return takeDownSkillInfos[level - 1]; } }

    [Header("Effect Sound")]
    public SoundManager audioSource;

    public override void UseSkill()
    {
        if (level <= 0) return;
        if (inUseSkill) return;
        if (genie.runSkill.inUseSkill) return; // 대쉬중일때는 사용불가능
        if (nextSkillTime > Time.time) return;

        StartCoroutine(TakeDown());
    }

    public override EnergyInfo GetUpgradeEnergy()
    {
        return takeDownSkillInfos[level].upgradeEnergy;
    }

    // 내려찍기 코루틴
    IEnumerator TakeDown()
    {
        WaitUntil jumpUntil = new WaitUntil(() => genie.rigd.velocity.y <= 0);
        WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

        inUseSkill = true;
        
        Vector3 originGenieSize = genie.transform.localScale; // 지니의 본래 사이즈 저장
        float originCamSize = genie.CamSize; // 카메라 본래 사이즈 저장
        genie.coll.isTrigger = true; // 지니 콜라이더 트리거로 변경

        genie.VelocityY = CurrentInfo.velocity; // 점프
        genie.VelocityX = 0;

        StartCoroutine(CamSizeLerp(CurrentInfo.camSize, 0.5f)); // 카메라 사이즈 돌아옴
        
        yield return jumpUntil; // 지니의 점프가 끝나갈때까지 기다림

        genie.transform.localScale *= CurrentInfo.sizeMultiple; // 지니 사이즈 증가
        audioSource.PlayEffect(CurrentInfo.sizeUpSound); // 커질 때 효과음

        // 커질 때 이펙트
        if (CurrentInfo.sizeUpEffect)
        {
            Instantiate(CurrentInfo.sizeUpEffect, transform.position, Quaternion.identity);
        }

        genie.controller.CreateCloud();

        float originGravity = genie.rigd.gravityScale; // 지니의 중력 저장
        genie.rigd.gravityScale = 0f; // 중력 없앰
        genie.VelocityY = 1; // 살짝 올라감

        yield return new WaitForSeconds(0.5f); // 시전시간

        genie.rigd.gravityScale = originGravity; // 중력 복원
        genie.VelocityY = -10; // 아래로 떨어짐
        
        yield return new WaitUntil(() => genie.controller.isGrounded); // 땅에 닿을때까지 기다림
        
        genie.coll.isTrigger = false; // 트리거 풀림

        NearRangeDamage(); // 광역 피해

        genie.damageOn = false; // 무적 on
        genie.controller.moveOn = false; // 이동 끔
        genie.controller.jumpOn = false; // 점프 끔


        audioSource.PlayEffect(CurrentInfo.takeDownSound); // 떨어질 때 효과음

        // 애니메이션 초기화
        genie.controller.anim.SetFloat("Speed", 0f);
        genie.controller.anim.SetFloat("vSpeed", -1);

        // 떨어질 때 이펙트
        Instantiate(CurrentInfo.takeDownEffect, this.transform.position + new Vector3(0f,0.5f,0), Quaternion.identity);

        yield return new WaitForSeconds(0.5f); // 스킬 사용 후 딜레이 시간

        genie.damageOn = true; // 무적 off
        genie.controller.moveOn = true; // 이동 킴
        genie.controller.jumpOn = true; // 점프 킴

        genie.transform.localScale = originGenieSize; // 지니 사이즈 돌아옴
        StartCoroutine(CamSizeLerp(originCamSize, 0.5f)); // 카메라 사이즈 돌아옴

        nextSkillTime = Time.time + CurrentInfo.coolTime;

        if (coolTimeIcon)
        {
            coolTimeIcon.StartCoolTime(CurrentInfo.coolTime);
        }

        inUseSkill = false;
    }

    // 자신 주변 적들에게 데미지 주기
    public void NearRangeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, CurrentInfo.range, 1 << LayerMask.NameToLayer("Enemy"));

        foreach (var enemy in colliders)
        {
            enemy.GetComponent<Character>().Damage(CurrentInfo.damage);
        }
    }

    // 카메라 크기 번경
    IEnumerator CamSizeLerp(float target, float time)
    {
        float lerp = 0;
        float temp = 0;
        
        while (genie.CamSize != target)
        {
            genie.CamSize = Mathf.Lerp(genie.CamSize, target, lerp);
            lerp += Time.deltaTime / time;
            temp += Time.deltaTime;
            yield return null;
        }
    }


}
