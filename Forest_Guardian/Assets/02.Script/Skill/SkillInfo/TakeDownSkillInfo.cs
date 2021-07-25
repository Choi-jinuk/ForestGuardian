using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TakeDownSkillInfo", menuName = "SkillInfo/Create TakeDownSkillInfo")]
public class TakeDownSkillInfo : SkillInfo
{
    public float sizeMultiple; // 크기 배수
    public float velocity; // 점프 높이
    public int damage; // 데미지
    public float range; // 데미지 범위
    public float camSize; // 커질 때 카메라 사이즈
    public AudioClip sizeUpSound; // 커질 때 사운드
    public GameObject sizeUpEffect; // 커질 때 이펙트
    public AudioClip takeDownSound; // 땅에 닿을 때 사운드
    public GameObject takeDownEffect; // 땅에 닿을 때 이펙트
}
