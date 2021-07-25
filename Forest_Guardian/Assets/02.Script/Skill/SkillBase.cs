using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnergyInfo
{
    public int tree;
    public int water;
    public int stone;
    public int fire;
    public int light;
}

// 모든 스킬부모
public abstract class SkillBase : MonoBehaviour
{
    public Genie genie; // 플레이어

    public int level; // 스킬 레벨
    public bool inUseSkill; // 스킬 사용중인지
    protected float nextSkillTime; // 다음 스킬 사용가능한 시간

    // 쿨타임 ui
    public CoolTimeIcon coolTimeIcon;

    public void Awake()
    {
        genie = GetComponent<Genie>();
    }

    public abstract void UseSkill(); // 스킬 사용
    public abstract EnergyInfo GetUpgradeEnergy(); // 다음 스킬 업그레이드 에너지
}
