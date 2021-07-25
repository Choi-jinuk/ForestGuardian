using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DragShootingSkillInfo", menuName = "SkillInfo/Create DragShootingSkillInfo")]
public class DragShootingSkillInfo : SkillInfo
{
    public int damage; // 데미지
    public float speed; // 날라가는 속도
    public AudioClip sound; // 날라가는 사운드
    public GameObject effect; // 이펙트

    public Color startColor;
    public Color endColor;

}
