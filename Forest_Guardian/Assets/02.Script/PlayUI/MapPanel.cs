using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    [Header("Stage")]
    public float[] checkPoint; // stage 표기용 x좌표 포인트
    //0 이하 = 5:Fire, 0~1 = 3:Stone, 1~2 = 1:Forest, 2~3 = 2:Water, 3 이상 = 4:Light
    //stage크기 :178            356           534             356               178

    [Header("Player")]
    public Transform player;

    [Header("NarrowImage")]
    public GameObject fireImage;
    public GameObject stoneImage;
    public GameObject forestImage;
    public GameObject waterImage;
    public GameObject lightImage;
    public Text mapText;

    [Header("Map")]
    public RectTransform playerLocationImage;
    public GameObject wideMapPanel;
    public GameObject narrowMapPanel;

    const float stageLength = 835f; //실제 맵 x좌표 최대값
    const float ImageRectLength = 735f; //맵캔버스 RextX좌표 최대값
    float ratioMap; //맵마다 크기가 달라 표현해주기 위한 비율, 이동 속도 비율을 맞추기 위한 변수
    float adjustMap; //이동 속도 비율 적용에 따른 맵 거리 차이를 맞춰주기 위한 변수
    bool isWide = false;

    void Update()
    {
        KeyCodeMEvent();// M버튼을 누르고 있을 경우 미니맵 커지게 하기
    }

    void KeyCodeMEvent()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            narrowMapPanel.SetActive(!narrowMapPanel.activeInHierarchy);
            wideMapPanel.SetActive(!wideMapPanel.activeInHierarchy);
            isWide = !isWide;

            if (isWide)
                StartCoroutine(WideLocation());
        }
    }

    IEnumerator WideLocation()
    {
        while (isWide)
        { 
            playerLocationImage.anchoredPosition = new Vector3((player.position.x * ImageRectLength) / stageLength, 0f, 0f);
            yield return null;
        }
    }
}
