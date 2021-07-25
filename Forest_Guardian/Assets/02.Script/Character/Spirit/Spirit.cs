using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spirit : Character
{
    public SpiritInfo spiritInfo; // 정령 정보


    public float attackDistance; // 사거리
    public float attackRate; // 공격속도
    protected float nextAttackTime; // 다음 공격시간
    protected Character attackTarget; // 공격 타겟
    public int maxAttackNum; // 최대 공격가능한 수
    public int damage; // 데미지
    public int heal; // 회복량
    protected Coroutine healCoroutine; // 회복 코루틴
    protected Coroutine targetCoroutine; // 타겟 업데이트 코루틴
    public EnergyInfo needEnergy; // 필요 기운
    public EnergyInfo dropEnergy; // 스테이지가 끝나면 떨어뜨리는 기운
    public Spirit nextLevelSpirit; // 다음 레벨의 정령
    public Transform textureTr; // 텍스쳐 tr
    public Transform boneTr; // bone Tr

    [Header("UI")]
    public Image hpBar; // 체력바
    public GameObject upgradeUI; // 업그레이드 UI
    public Text topText; // 상단 텍스트
    public Text treeText;
    public Text waterText;
    public Text stoneText;
    public Text fireText;
    public Text lightText;

    [Header("Effect")]
    public AudioClip spawnSound; // 스폰할 때 사운드
    public ParticleSystem spawnParticle; // 스폰할 때 파티클
    public List<AudioClip> hitSounds = new List<AudioClip>(); // 피격당할 때 사운드
    public ParticleSystem hitParticle; // 피격당할 때 파티클
    public AudioClip deathSound; // 사망할 때 사운드
    public ParticleSystem deathParticle; // 사망할 때 파티클

    public override void Init()
    {
        base.Init();

        targetCoroutine = StartCoroutine(AttackTargetUpdate());
        InvokeRepeating("AutoHeal", 0, 10f);
        hpBar.gameObject.SetActive(false);
        UpgradeUIUpdate();
        upgradeUI.SetActive(false);

        // spiritInfo = ScriptableObject.CreateInstance<SpiritInfo>();

        if (spawnSound && audioSource)
        {
            audioSource.PlayOneShot(spawnSound);
        }

        if (spawnParticle)
        {
            spawnParticle.Play();
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        HpBarEnable(); // 체력바 활성화
        CancelInvoke("HpBarDisable"); // 이전에 있던 비활성화 Invoke는 제거
        Invoke("HpBarDisable", 2f); // 2초뒤에 체력바 비활성화
        hpBar.fillAmount = (float)hp / maxHp; // 체력바 업데이트
        
        if(hitSounds.Count > 0 && audioSource)
        {
            int rand = Random.Range(0, hitSounds.Count);
            audioSource.PlayOneShot(hitSounds[rand]);
        }

        if (hitParticle)
        {
            hitParticle.Play();
        }
    }

    public override void Die()
    {
        base.Die();

        if (deathSound && audioSource)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (deathParticle)
        {
            deathParticle.Play();
        }
    }

    // 체력바 활성화
    void HpBarEnable()
    {
        hpBar.gameObject.SetActive(true);
    }

    // 체력바 비활성화
    void HpBarDisable()
    {
        hpBar.gameObject.SetActive(false);
    }

    // 업그레이드 ui 업데이트
    public void UpgradeUIUpdate()
    {
        if (nextLevelSpirit)
        {
            treeText.text = nextLevelSpirit.needEnergy.tree.ToString();
            waterText.text = nextLevelSpirit.needEnergy.water.ToString();
            stoneText.text = nextLevelSpirit.needEnergy.stone.ToString();
            fireText.text = nextLevelSpirit.needEnergy.fire.ToString();
            lightText.text = nextLevelSpirit.needEnergy.light.ToString();
        }
        else
        {
            topText.text = "최대 레벨입니다";
            treeText.enabled = false;
            waterText.enabled = false;
            stoneText.enabled = false;
            fireText.enabled = false;
            lightText.enabled = false;
        }
    }

    // 업그레이드
    public void Upgrade()
    {
        if (Genie.instance == null) return;


        Instantiate(nextLevelSpirit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // 업그레이드가 가능한지 체크
    public bool UpgradeEnergyCheck()
    {
        EnergyManager energyManager = Genie.energyManager;

        if (energyManager == null) return false;

        EnergyInfo skillEnergy = nextLevelSpirit.needEnergy;

        if (energyManager.energyInfo.tree >= skillEnergy.tree)
        {
            if (energyManager.energyInfo.water >= skillEnergy.water)
            {
                if (energyManager.energyInfo.light >= skillEnergy.light)
                {
                    if (energyManager.energyInfo.rock >= skillEnergy.stone)
                    {
                        if (energyManager.energyInfo.fire >= skillEnergy.fire)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    // 공격
    public virtual void Attack() { }

    public void NextAttackTimeUpdate()
    {
        nextAttackTime = Time.time + attackRate;
    }

    // 자동 회복
    public void AutoHeal()
    {
        Heal(heal);
    }

    // 에니저 떨어뜨리기
    public void DropEnergy()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance); // 공격 사거리 표시
    }

    // 공격속도에 맞게 던지는 애니메이션 속도 변경
    public void ActionAnimSpeedUpdate()
    {
        float temp = 1;

        if (attackRate < 1)
        {
            temp = 1 - attackRate;
            temp = temp / attackRate + 1;
        }

        animator.SetFloat("actionSpeed", temp);
    }

    // 근처에 적이 있는지 리턴
    public bool IsNearEnemy()
    {
        return Physics2D.OverlapCircle(transform.position, attackDistance, 1 << LayerMask.NameToLayer("Enemy"));
    }

    // 근처의 적을 바라보도록 뒤집기
    public void Flip()
    {
        if (textureTr == null || boneTr == null) return;

        // texture 뒤집기
        Vector3 scale = textureTr.localScale;
        float amount = scale.x;
        scale.x = attackTarget.transform.position.x > transform.position.x ? amount : -amount;
        textureTr.localScale = scale;

        // bone 뒤집기
        scale = boneTr.localScale;
        amount = scale.x;
        scale.x = attackTarget.transform.position.x > transform.position.x ? amount : -amount;
        boneTr.localScale = scale;
    }

    // 가장 가까운 적을 타겟으로 설정
    public IEnumerator AttackTargetUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        while (true)
        {
            if (nextAttackTime <= Time.time)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackDistance, 1 << LayerMask.NameToLayer("Enemy"));
                float nearSqrDist = attackDistance * attackDistance;
                Transform nearTarget = null;

                for (int i = 0; i < colliders.Length; i++)
                {
                    float dist = (colliders[i].transform.position - transform.position).sqrMagnitude;

                    if (nearSqrDist >= dist)
                    {
                        nearTarget = colliders[i].transform;
                        nearSqrDist = dist;
                    }
                }

                if (nearTarget)
                {
                    attackTarget = nearTarget.GetComponent<Character>(); // 공격 타겟의 enemy 컴포넌트를 참조

                    if (attackTarget)
                    {
                        // Flip();
                        Attack();
                        NextAttackTimeUpdate();
                    }
                }
            }

            yield return wait;
        }
    }

    private void OnMouseEnter()
    {
        Invoke("HpBarEnable", 1f); // 마우스 올리면 1초 뒤에 체력바 활성화
    }

    private void OnMouseExit()
    {
        CancelInvoke("HpBarEnable");
        HpBarDisable();
    }

    
}
