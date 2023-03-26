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
        protected set {
            scanRange = Mathf.Max(0, value);
        }
    }

    public float ScanTime {
        get {
            return scanTime;
        }
        protected set {
            scanTime = Mathf.Max(0, value);
        }
    }

    public float ShootingRatio {
        get {
            return shootingRatio;
        }
        protected set {
            shootingRatio = Mathf.Max(0, value);
        }
    }

    public int ShootingDamage {
        get {
            return shootingDamage;
        }
        protected set {
            shootingDamage = Mathf.Max(0, value);
        }
    }

    //! MonoBehaviour
    void Awake() {
        Waypoint = GameObject.Find(Config.WAYPOINT_RANGED_NAME);
        SetupSoldier();
    }


    //! Soldier - public
    public override void Initialize(int level, bool enemy) {
        maxHealth = Config.SOLDIER_RANGED_MAX_HEALTH[level];
        Initialize(level, enemy, maxHealth);
    }

    public override void Initialize(int level, bool enemy, int maxHealth) {
        scanRange = Config.SOLDIER_RANGED_SCAN_RANGE[level];
        scanTime = Config.SOLDIER_RANGED_SCAN_TIME;
        shootingRatio = Config.SOLDIER_RANGED_SHOOTING_RATIO[level];
        shootingDamage = Config.SOLDIER_RANGED_SHOOTING_DAMAGE[level];
        Initialize(level, enemy, scanRange, scanTime, shootingRatio, shootingDamage);
    }

    public virtual void Initialize(int level, bool enemy, float scanRange, float scanTime, float shootingRatio, int shootingDamage) {
        base.Initialize(level, enemy, maxHealth);
        ScanRange = scanRange;
        ScanTime = scanTime;
        ShootingRatio = shootingRatio;
        ShootingDamage = shootingDamage;
    }

    public override void Spawn() {
        base.Spawn();
        StartCoroutine(ShootingRoutine());
    }

    public override void Attack(Soldier target) {
        GameObject bulletObject = PoolBullet.instance.GetEntity();
        bulletObject.transform.position = transform.position;
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Initialize(new AttackData(AttackType.Bullet, ShootingDamage), Bullet.BulletType.normal, target.gameObject);
    }

    //! RangedSoldier - Private
    private IEnumerator ShootingRoutine() {
        GameObject target = null;
        while (!GameManager.instance.GameOver) {
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
        foreach (GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
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
