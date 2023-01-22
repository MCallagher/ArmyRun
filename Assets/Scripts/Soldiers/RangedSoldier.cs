using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSoldier : Soldier {

    //! Variables
    [SerializeField] protected float scanRange;
    [SerializeField] protected float scanTime;
    [SerializeField] protected float shootingRatio;
    [SerializeField] protected int shootingDamage;


    //! Properties
    public float ScanRange {
        get {
            return scanRange;
        }
    }

    public float ScanTime {
        get {
            return scanTime;
        }
    }

    public float ShootingRatio {
        get {
            return shootingRatio;
        }
    }

    public int ShootingDamage {
        get {
            return shootingDamage;
        }
    }


    //! Soldier - public
    public override void InitializeSoldier(int count, bool enemy) {
        scanRange = Config.SOLDIER_RANGED_RANGE;
        scanTime = Config.SOLDIER_RANGED_REFRESH_TIME;
        shootingRatio = Config.SOLDIER_RANGED_RATIO;
        base.InitializeSoldier(count, enemy);
        StartCoroutine(ShootingRoutine());
    }

    public override void Attack(Soldier target) {
        GameObject bulletObject = PoolBullet.instance.GetEntity();
        bulletObject.transform.position = transform.position;
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.InitializeBullet(new AttackData(AttackType.Bullet, ShootingDamage), target.gameObject);
    }

    /*
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
    */

    //! Soldier - Protected
    protected override void RecomputeProperties() {
        shootingDamage = count * Config.SOLDIER_RANGED_STRENGTH;
        maxHealth = count * Config.SOLDIER_RANGED_CONSTITUTION;
        health = maxHealth;
    }

    //! RangedSoldier - Private
    private IEnumerator ShootingRoutine() {
        GameObject target = null;
        while (true) {
            while (target == null) {
                yield return new WaitForSeconds(ScanTime);
                target = AcquireTarget();
            }
            while (target != null) {
                if (!target.activeInHierarchy) {
                    target = null;
                }
                else {
                    Attack(target.GetComponent<Soldier>());
                    yield return new WaitForSeconds(1 / ShootingRatio);
                }
            }
        }
    }

    private GameObject AcquireTarget() {
        float minDist = ScanRange;
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
