using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeIcon : MonoBehaviour
{
    public Image icon;
    

    private void Awake()
    {
        icon = GetComponent<Image>();

        if(icon == null)
        {
            Debug.LogWarning(gameObject.name + "의 이미지가 없습니다.");
            enabled = false;
        }

        icon.fillAmount = 0f;
    }

    public void StartCoolTime(float coolTime)
    {
        StartCoroutine(CoolTimeUpdate(coolTime));
    }
    
    public IEnumerator CoolTimeUpdate(float coolTime)
    {
        float time = 0; // 현재 지난시간

        while (time < coolTime)
        {
            icon.fillAmount = 1 - time / coolTime; // 쿨타임 업데이트
            time += Time.deltaTime; // 지나간 시간 추가
            yield return null;
        }

        icon.fillAmount = 0f;
    }


}
