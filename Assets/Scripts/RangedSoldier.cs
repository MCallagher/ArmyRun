using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : Soldier {

    //! Variables
    [SerializeField] protected float range;
    [SerializeField] protected float ratio;
    [SerializeField] protected float refreshTime;

    //! Properties
    public float Range {
        get {
            return range;
        }
    }

    public float Ratio {
        get {
            return ratio;
        }
    }
    

    //! Soldier - public
    public override void InitializeSoldier(int count, bool enemy) {
        range = Config.SOLDIER_RANGED_RANGE;
        ratio = Config.SOLDIER_RANGED_RATIO;
        baseReward = Config.SOLDIER_RANGED_COINS;
        refreshTime = Config.SOLDIER_RANGED_REFRESH_TIME;
        base.InitializeSoldier(count, enemy);
        StartCoroutine(ShootingRoutine());
    }

    public void Merge(List<RangedSoldier> rangedSoldiers) {
        int totHealth = Health;
        int totCount = Count;
        foreach (RangedSoldier rangedSoldier in rangedSoldiers) {
            totHealth += rangedSoldier.Health;
            totCount += rangedSoldier.Count;
            rangedSoldier.Die();
        }
        Count = totCount;
        Health = totHealth;
    }

    public override void Merge(List<Soldier> soldiers) {
        List<RangedSoldier> rangedSoldiers = new List<RangedSoldier>();
        foreach (Soldier soldier in soldiers) {
            if (soldier is RangedSoldier) {
                rangedSoldiers.Add((RangedSoldier) soldier);
            }
        }
        Merge(rangedSoldiers);
    }

    //! Soldier - Protected
    protected override void Attack(Soldier target) {
        GameObject bulletObject = PoolBullet.instance.GetEntity();
        bulletObject.transform.position = transform.position;
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.InitializeBullet(Strength, target.gameObject);
    }

    protected override void RecomputeProperties() {
        float currHealthPrc = constitution != 0 ? health / constitution : 0;
        strength = count * Config.SOLDIER_RANGED_STRENGTH;
        constitution = count * Config.SOLDIER_RANGED_CONSTITUTION;
        health = constitution;
        if (gameObject.activeInHierarchy) {
            health = (int)(currHealthPrc * health);
        }
    }

    //! RangedSoldier - Private
    private IEnumerator ShootingRoutine() {
        GameObject target = null;
        while (true) {
            Debug.Log($"{gameObject.name} | 1 | target: {(target == null ? null : target.name)}");
            while (target == null) {
                yield return new WaitForSeconds(refreshTime);
                target = AcquireTarget();
                Debug.Log($"{gameObject.name} | 2 | target: {(target == null ? null : target.name)}");
            }
            while (target != null) {
                if (!target.activeInHierarchy) {
                    target = null;
                    Debug.Log($"{gameObject.name} | 3 | target: {(target == null ? null : target.name)}");
                }
                else {
                    Attack(target.GetComponent<Soldier>());
                    yield return new WaitForSeconds(1 / Ratio);
                    Debug.Log($"{gameObject.name} | 4 | target: {(target == null ? null : target.name)}");
                }
            }
        }
    }

    private GameObject AcquireTarget() {
        float minDist = Range;
        GameObject target = null;
        foreach (GameObject soldierObject in PoolMeleeSoldier.instance.GetActiveGameObject()) {
            if (soldierObject.GetComponent<Soldier>().Enemy != Enemy) {
                float dist = (transform.position - soldierObject.transform.position).magnitude;
                if (dist < minDist) {
                    minDist = dist;
                    target = soldierObject;
                }
            }
        }
        foreach (GameObject soldierObject in PoolRangedSoldier.instance.GetActiveGameObject()) {
            if (soldierObject.GetComponent<Soldier>().Enemy != Enemy) {
                float dist = (transform.position - soldierObject.transform.position).magnitude;
                if (dist < minDist) {
                    minDist = dist;
                    target = soldierObject;
                }
            }
        }
        return target;
    }
}
