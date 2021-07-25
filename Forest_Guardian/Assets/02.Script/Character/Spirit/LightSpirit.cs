using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedInfo
{
    public Spirit target;
    public float originValue; // 본래 스피드
    public float speedValue; // 추가된 스피드

    public SpeedInfo(Spirit target, float originValue, float speedValue)
    {
        this.target = target;
        this.originValue = originValue;
        this.speedValue = speedValue;
    }
}

public class LightSpirit : Spirit
{
    public float sppedAmount; // 공격속도 증가값
    public List<SpeedInfo> targetList = new List<SpeedInfo>();

    public override void Init()
    {
        base.Init();

        StopCoroutine(targetCoroutine);

        targetCoroutine = StartCoroutine(TargetUpdate());
    }

    // 주변 공격속도 증가
    public void SpeedUp(Spirit spirit)
    {
        float origin = spirit.attackRate;

        spirit.attackRate = (spirit.attackRate * (1 + sppedAmount / 100));
        float updateValue = spirit.attackRate - origin;
        SpeedInfo speedInfo = new SpeedInfo(spirit, origin, updateValue);
        targetList.Add(speedInfo);

        Debug.Log(spirit + " " + updateValue);

    }

    // 공격속도 원래대로 변경
    public void SpeedDown()
    {
        foreach (var item in targetList)
        {
            if (item == null) continue;

            item.target.attackRate -= item.speedValue;
        }
    }



    // 가장 가까운 정령을 타겟으로 설정
    public IEnumerator TargetUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        Debug.Log("Start");
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, 1 << LayerMask.NameToLayer("Spirit"));

            foreach (var coll in colliders)
            {
                Spirit target = coll.GetComponent<Spirit>(); // 타겟의 enemy 컴포넌트를 참조

                bool inTarget = false; // 타겟이 이미 있는지 확인

                if (target)
                {
                    // 이미 속도를 추가했는지 확인
                    foreach (var item in targetList)
                    {
                        if (item.target == target)
                        {
                            inTarget = true;
                            break;
                        }
                    }

                    // 공격속도가 추가된적 없으면
                    if (!inTarget)
                    {
                        SpeedUp(target);

                    }
                }
            }


            yield return wait;
        }
    }

    private void OnDestroy()
    {
        SpeedDown();
    }

}