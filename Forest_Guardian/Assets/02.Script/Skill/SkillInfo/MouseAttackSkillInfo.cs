using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MouseAttackSkillInfo", menuName = "SkillInfo/Create MouseAttackSkillInfo")]
public class MouseAttackSkillInfo : SkillInfo
{
    public int attackDamage; // 공격력
    public float attackRadius; // 공격 범위
    public AudioClip attackSound; // 공격 효과음
    public GameObject attackEffect; // 공격 이펙트
}
