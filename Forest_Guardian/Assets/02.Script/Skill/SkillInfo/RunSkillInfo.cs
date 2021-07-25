using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RunSkillInfo", menuName = "SkillInfo/Create RunSkillInfo")]
public class RunSkillInfo : SkillInfo
{
    public float dashSpeed = 20f; // 대쉬 속도
    public float dashTime; // 대쉬 시간
    public AudioClip dashSound; // 달리기 효과음
    public Color startColor;
    public Color endColor;
}
