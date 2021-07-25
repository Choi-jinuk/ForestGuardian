using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpSkill : SkillBase
{
    // 스킬 정보
    public DoubleJumpSkillInfo[] doubleJumpSkillInfos = new DoubleJumpSkillInfo[1];

    // 현재 레벨정보
    public DoubleJumpSkillInfo CurrentInfo { get { return doubleJumpSkillInfos[level - 1]; } }

    public override void UseSkill()
    {
        if (level <= 0)
        {
            return;
        }

        PlayerController controller = genie.controller;

        float force = controller.jumpForce * CurrentInfo.multiple;
        genie.rigd.AddForce(new Vector2(0, force)); // 점프
        genie.controller.CreateCloud(); // 점프 이펙트
    }

    public override EnergyInfo GetUpgradeEnergy()
    {
        return doubleJumpSkillInfos[level].upgradeEnergy;
    }
}
