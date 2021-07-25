using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProejctile : Projectile
{
    public Fire fire;

    public override void HitAction()
    {
        Fire chaFire = lastHitCha.gameObject.AddComponent<Fire>(); // Fire 스크립트 추가
        chaFire.Init(fire.damage, fire.damageDelay, fire.damageNum);
        chaFire.AddStatus(lastHitCha);
    }

}
