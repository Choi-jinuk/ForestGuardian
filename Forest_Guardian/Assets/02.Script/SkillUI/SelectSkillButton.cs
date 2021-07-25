using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillButton : MonoBehaviour
{
    public SkillInfo skillInfo;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        ButtonUpdate();
    }

    public void ButtonUpdate()
    {
        int genieLevel = Genie.instance.GetSkillType(skillInfo).level;

        if (genieLevel >= skillInfo.level)
        {
            button.image.color = UpgradeUI.instance.upgradedColor;
        }
        else if (genieLevel + 1 == skillInfo.level)
        {
            button.image.color = UpgradeUI.instance.possibleColor;
        }
        else if (genieLevel < skillInfo.level)
        {
            button.image.color = UpgradeUI.instance.impossibleColor;
        }
    }

    public void OnSelectSkill()
    {
        UpgradeUI.instance.UI_Update(this);
    }


    /*
    public void UpgradeSkillSet(SkillBase upgradeSkill)
    {
        switch (upgradeType)
        {
            case UpgradeType.MOUSEATTACK:
                upgradeSkillInfo = Genie.instance.mouseSkill;
                break;
            case UpgradeType.RUN:
                upgradeSkillInfo = Genie.instance.runSkill;
                break;
            case UpgradeType.DRAG:
                upgradeSkillInfo = Genie.instance.dragSkill;
                break;
            case UpgradeType.DOUBLEJUMP:
                upgradeSkillInfo = Genie.instance.doubleJumpSkill;
                break;
            case UpgradeType.TAKEDOWN:
                upgradeSkillInfo = Genie.instance.takeDownSkill;
                break;
            default:
                break;
        }
    }
    */

}
