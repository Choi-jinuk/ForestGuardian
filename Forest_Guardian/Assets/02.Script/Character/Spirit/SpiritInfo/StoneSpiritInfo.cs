using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StoneSpiritInfo", menuName = "SpiritInfo/Create StoneSpiritInfo/")]
public class StoneSpiritInfo : SpiritInfo
{
    public int damage; // 데미지
    public float attackRate; // 공격속도
    public float attackDistance; // 사거리
    public float attackRange; // 공격 범위
    public float mexAttackNum; // 최대 공격 인원
}