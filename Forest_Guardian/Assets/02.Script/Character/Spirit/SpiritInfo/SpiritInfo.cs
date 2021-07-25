using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpiritInfo", menuName = "SpiritInfo/Create SpiritInfo")]
public class SpiritInfo : ScriptableObject
{
    public string spiritName; // 정령 이름
    public int level; // 레벨
    [TextArea()] public string description; // 설명
    public int hp; // 최대 체력
    public float heal; // 10초당 회복 값
    public EnergyInfo needEnergy; // 필요 기운
    public EnergyInfo dropEnergy; // 스테이지가 끝나면 떨어뜨리는 기운


}
