using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New SkillInfo", menuName = "SkillInfo/Create SkillInfo")]
public class SkillInfo : ScriptableObject
{
    public string skillName; // 스킬 이름
    public Texture icon; // 스킬 아이콘
    public VideoClip video; // 스킬 영상
    public int level; // 스킬 레벨
    [TextArea()] public string description; // 설명
    public EnergyInfo upgradeEnergy; // 업그레이드에 필요한 기운
    public float coolTime; // 쿨타임
}
