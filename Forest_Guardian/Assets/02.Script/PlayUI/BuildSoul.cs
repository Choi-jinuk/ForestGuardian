using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSoul : MonoBehaviour
{
    [System.Serializable]
    public struct SoulList
    {
        public GameObject Tree;
        public GameObject Rock;
        public GameObject Water;
        public GameObject Light;
        public GameObject Fire;
    }

    [Header("SOUL")]
    public SoulList pointerList;
    public SoulList prefabList;

    [Header("SUMMONLIGHT")]
    public GameObject summonLight;

    [Header("EnergyManager")]
    public EnergyManager energyManager;

    [Header("BOOL")]
    public bool isClick = false;
    public bool buildPossible = false;

    Vector3 startPos;
    GameObject soul;
    GameObject pointer;
    public GameObject spot;


    #region 건설 메소드

    void Update()
    {
        if (isClick)
        {
            BuildPhase();

            if (Input.GetMouseButtonDown(0))
            {
                if (buildPossible) Build();
                else BuildInitial();
            
            }

        }
    }
    void BuildPhase()
    {
        pointer.transform.position = GetMousePos();
    }

    Vector2 GetMousePos()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return MousePosition;
    }

    public void Build()
    {
        GameObject obj = Instantiate(soul);
        obj.transform.position = spot.transform.position;

        if (obj.transform.position.x > 0.0f)
        {
            obj.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        EnergyReduce(soul);

        spot.SetActive(false);
        summonLight.SetActive(false);

        BuildInitial();
    }
    public void BuildInitial()
    {
        pointer.transform.position = startPos;
        pointer.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 100.0f/255.0f);
        isClick = false;
        Cursor.visible = true;
        buildPossible = false;
    }

    void EnergyReduce(GameObject _soul)
    {
        if (_soul == prefabList.Tree)
        {
            energyManager.ReduceTree(1);
        }
        else if (_soul == prefabList.Water)
        {
            energyManager.ReduceWater(1);
        }
        else if (_soul == prefabList.Rock)
        {
            energyManager.ReduceRock(1);
        }
        else if (_soul == prefabList.Fire)
        {
            energyManager.ReduceFire(1);
        }
        else if (_soul == prefabList.Light)
        {
            energyManager.ReduceLight(1);
        }
    }

    #endregion

    #region 버튼 콜백 메소드
    public void OnClickForest()
    {
        if (!isClick)
        {
            if (energyManager.energyInfo.tree > 0)
            {
                pointer = pointerList.Tree;
                soul = prefabList.Tree;
                PointerInitialize();
            }
        }
    }
    public void OnClickWater()
    {
        if (!isClick)
        {
            if (energyManager.energyInfo.water > 0)
            {
                pointer = pointerList.Water;
                soul = prefabList.Water;
                PointerInitialize();
            }
        }
    }
    public void OnClickStone()
    {
        if (!isClick)
        {
            if (energyManager.energyInfo.rock > 0)
            {
                pointer = pointerList.Rock;
                soul = prefabList.Rock;
                PointerInitialize();
            }
        }
    }
    public void OnClickFire()
    {
        if (!isClick)
        {
            if (energyManager.energyInfo.fire > 0)
            {
                pointer = pointerList.Fire;
                soul = prefabList.Fire;
                PointerInitialize();
            }
        }
    }
    public void OnClickLight()
    {
        if (!isClick)
        {
            if (energyManager.energyInfo.light > 0)
            {
                pointer = pointerList.Light;
                soul = prefabList.Light;
                PointerInitialize();
            }
        }
    }

    public void PointerInitialize()
   {
        isClick = true;
        Cursor.visible = false;
        startPos = pointer.transform.position;
    }
    #endregion
}
