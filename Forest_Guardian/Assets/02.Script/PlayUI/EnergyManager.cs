using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    [System.Serializable]
    public struct EnergyInfo
    {
        public int tree;
        public int rock;
        public int water;
        public int light;
        public int fire;
    }
    public EnergyInfo energyInfo;

    [Header("Text")]
    public Text TreeText;
    public Text WaterText;
    public Text StoneText;
    public Text FireText;
    public Text LightText;
    private void Start()
    {
        TreeText.text = energyInfo.tree.ToString();
        WaterText.text = energyInfo.water.ToString();
        StoneText.text = energyInfo.rock.ToString();
        FireText.text = energyInfo.fire.ToString();
        LightText.text = energyInfo.light.ToString();
    }
    #region ReduceMethod
    public void ReduceTree(int num)
    {
        energyInfo.tree -= num;
        ChangeButton(TreeText, energyInfo.tree.ToString());
    }
    public void ReduceRock(int num)
    {
        energyInfo.rock -= num;
        ChangeButton(StoneText, energyInfo.rock.ToString());
    }
    public void ReduceWater(int num)
    {
        energyInfo.water -= num;
        ChangeButton(WaterText, energyInfo.water.ToString());
    }
    public void ReduceLight(int num)
    {
        energyInfo.light -= num;
        ChangeButton(LightText, energyInfo.light.ToString());
    }
    public void ReduceFire(int num)
    {
        energyInfo.fire -= num;
        ChangeButton(FireText, energyInfo.fire.ToString());
    }

    #endregion

    #region PlusMethod
    public void PlusTree(int num)
    {
        energyInfo.tree += num;
        ChangeButton(TreeText, energyInfo.tree.ToString());
    }
    public void PlusWater(int num)
    {
        energyInfo.water += num;
        ChangeButton(WaterText, energyInfo.water.ToString());
    }
    public void PlusRock(int num)
    {
        energyInfo.rock += num;
        ChangeButton(StoneText, energyInfo.rock.ToString());
    }
    public void PlusFire(int num)
    {
        energyInfo.fire += num;
        ChangeButton(FireText, energyInfo.fire.ToString());
    }
    public void PlusLight(int num)
    {
        energyInfo.light += num;
        ChangeButton(LightText, energyInfo.light.ToString());
    }

    #endregion

    private void ChangeButton(Text btnText, string text)
    {
        btnText.text = text;
    }
}
