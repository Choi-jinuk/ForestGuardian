using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Spawn")]
    public Transform leftSpawnSpot;
    public Transform rightSpawnSpot;
    public GameObject enemyList;

    [Header("EnemyPrefab")]
    public GameObject warrior;
    public GameObject archer;

    [Header("EnemyMaxNum")]
    public int warriorNum;
    public int archerNum;

    [Header("Background")]
    public SpriteRenderer background1;
    public SpriteRenderer background2;
    public SpriteRenderer background3;
    public SpriteRenderer background4;

    [Header("RoundHouseBackground")]
    public Sprite rhBackground1;
    public Color rhBackground2;
    public Color rhColor;

    [Header("FastPaceBackground")]
    public Sprite fpBackground1;
    public Color fpBackground2;
    public Color fpColor;

    [Header("DirectionalLight")]
    public Light directionalLight;

    [Header("Timer")]
    public TimePanel timePanel;

    //-----------------------------------ObjectPool-----------------------------------
    GameObject[] leftWarriorPool;
    GameObject[] rightWarriorPool;

    GameObject[] leftArcherPool;
    GameObject[] rightArcherPool;
    //-----------------------------------ObjectPool-----------------------------------

    Queue<GameObject> spawnList = new Queue<GameObject>();

    public int enemyCount;

    private void Start()
    {
        //최대 개수에 맞게 오브젝트풀을 생성하여 할당
        //최대 개수는 한쪽 당 최대 개수 ex. 3을 입력했을 경우 좌3 우3 으로 총 6마리

        leftWarriorPool = new GameObject[warriorNum];
        leftArcherPool = new GameObject[archerNum];
        rightWarriorPool = new GameObject[warriorNum];
        rightArcherPool = new GameObject[archerNum];

        GameObject leftTemp;
        GameObject rightTemp;

        Quaternion left = Quaternion.Euler(0, 180.0f, 0);
        Quaternion right = Quaternion.Euler(0, 0, 0);

        for (var i = 0; i < warriorNum ; i++)
        {
            leftTemp = Instantiate(warrior);
            rightTemp = Instantiate(warrior);
            leftTemp.GetComponent<BerserkerEenmy>().stageManager = this;
            rightTemp.GetComponent<BerserkerEenmy>().stageManager = this;
            leftTemp.SetActive(false);
            rightTemp.SetActive(false);
            leftTemp.transform.rotation = left;
            rightTemp.transform.rotation = right;

            leftTemp.transform.SetParent(enemyList.transform);
            rightTemp.transform.SetParent(enemyList.transform);

            leftWarriorPool[i] = leftTemp;
            rightWarriorPool[i] = rightTemp;
        }

        for (var i = 0; i < archerNum; i++)
        {
            leftTemp = Instantiate(archer);
            rightTemp = Instantiate(archer);
            leftTemp.GetComponent<ArcherEnemy>().stageManager = this;
            rightTemp.GetComponent<ArcherEnemy>().stageManager = this;
            leftTemp.SetActive(false);
            rightTemp.SetActive(false);
            leftTemp.transform.rotation = left;
            rightTemp.transform.rotation = right;

            leftTemp.transform.SetParent(enemyList.transform);
            rightTemp.transform.SetParent(enemyList.transform);

            leftArcherPool[i] = leftTemp;
            rightArcherPool[i] = rightTemp;
        }
    }


    public void ChangeBackGround(bool state)
    {
        if (state) //스테이지 시작
        {
            directionalLight.color = fpColor;

            background1.sprite = fpBackground1;
            background2.sprite = fpBackground1;
            background3.sprite = fpBackground1;
            background4.color = fpBackground2;

            StartCoroutine(Spawn());
        }
        else //스테이지 종료
        {
            directionalLight.color = rhColor;

            background1.sprite = rhBackground1;
            background2.sprite = rhBackground1;
            background3.sprite = rhBackground1;
            background4.color = rhBackground2;
        }
    }

    public void SpawnWarrior(int num)
    {
        if (num > warriorNum) num = warriorNum; //최대개수를 넘었을 경우 최대로 소환

        for (var i = 0; i < num; i++)
        {
            leftWarriorPool[i].transform.position = leftSpawnSpot.position;
            rightWarriorPool[i].transform.position = rightSpawnSpot.position;
            spawnList.Enqueue(leftWarriorPool[i]);
            spawnList.Enqueue(rightWarriorPool[i]);
        }
    }
    public void SpawnArcher(int num)
    {
        if (num > archerNum) num = archerNum; //최대개수를 넘었을 경우 최대로 소환

        for (var i = 0; i < num; i++)
        {
            leftArcherPool[i].transform.position = leftSpawnSpot.position;
            rightArcherPool[i].transform.position = rightSpawnSpot.position;
            spawnList.Enqueue(leftArcherPool[i]);
            spawnList.Enqueue(rightArcherPool[i]);
        }
    }

    IEnumerator Spawn()
    {
        while (spawnList.Count != 0)
        {
            spawnList.Dequeue().SetActive(true);
            enemyCount++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ReduceEnemyCount()
    {
        enemyCount--;
        if (enemyCount <= 0) //스테이지 종료
        {
            ChangeBackGround(false);
            timePanel.StageClear();
        }
    }
}
