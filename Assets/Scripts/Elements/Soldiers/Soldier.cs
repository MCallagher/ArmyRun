using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Soldier : MonoBehaviour {

    //! Variables
    // value of this soldier
    [SerializeField] protected int level;
    // Phisical properties
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    // Side
    [SerializeField] protected bool enemy;
    // Waypoint
    [SerializeField] protected GameObject waypoint;


    //! Properties
    public int Level {
        get {
            return level;
        }
        protected set {
            level = Mathf.Max(0, value);
            for(int i = 1; i < Config.MERGE_LEVEL_LIMIT; i++) {
                soldierLevelObjects[i].SetActive(i == level);
            }
        }
    }

    public int MaxHealth {
        get {
            return maxHealth;
        }
        protected set {
            maxHealth = Mathf.Max(0, value);
        }
    }

    public int Health {
        get {
            return health;
        }
        protected set {
            health = Mathf.Clamp(value, 0, MaxHealth);
            if (health == 0) {
                Die();
            }
            float healthPrc = (float)Health / MaxHealth;
            Color healthColor = (healthPrc * soldierColor) + ((1 - healthPrc) * damageColor);
            foreach(MeshRenderer renderer in soldierLevelObjects[0].GetComponentsInChildren<MeshRenderer>()) {
                renderer.material = soldierMaterial;
                renderer.material.color = healthColor;
            }
        }
    }

    public bool Enemy {
        get {
            return enemy;
        }
        protected set {
            enemy = value;
            soldierMaterial = enemy ? enemyMaterial : playerMaterial;
            soldierColor = soldierMaterial.color;
        }
    }

    public GameObject Waypoint {
        get {
            return waypoint;
        }
        protected set {
            waypoint = value;
        }
    }

    //! Components
    protected Rigidbody soldierRigidbody;
    protected Renderer soldierRenderer;
    protected GameObject[] soldierLevelObjects;
    protected Material soldierMaterial;
    protected Color soldierColor;
    protected AudioSource soldierAudio;

    //! References
    [SerializeField] protected Material playerMaterial;
    [SerializeField] protected Material enemyMaterial;
    [SerializeField] protected Color damageColor;


    //! MonoBehaviour
    void Awake() {
        Waypoint = GameObject.Find(Config.WAYPOINT_NAME);
        SetupSoldier();
    }

    void Update() {
        FollowWaypoint();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(Config.TAG_STONE)) {
            soldierAudio.volume = Config.SOUND_VOLUME_STONE * Options.instance.SoundsVolume * Options.instance.EffectsVolume;
            soldierAudio.Play();
            //soldierAudio.PlayOneShot(a);
        }
    }


    //! Soldier - Public
    public abstract void Initialize(int level, bool enemy);

    public virtual void Initialize(int level, bool enemy, int maxHealth) {
        Level = level;
        Enemy = enemy;
        MaxHealth = maxHealth;
        Health = MaxHealth;
    }

    public abstract void Attack(Soldier target);

    public virtual void Defend(AttackData attack) {
        Health -= attack.Intensity;
    }

    public virtual void Heal() {
        Health = MaxHealth;
    }

    public virtual void Push(Vector3 force) {
        soldierRigidbody.AddForce(force, ForceMode.Impulse);
    }

    public virtual void Spawn() {
        gameObject.SetActive(true);
        soldierRigidbody.velocity = new Vector3(0f,0f,0f); 
        soldierRigidbody.angularVelocity = new Vector3(0f,0f,0f);
    }


    //! Soldier - Protected
    protected virtual void Die() {
        if (Health == 0) {
            GameObject explosionObject =  PoolManager.instance.GetEntity<ExplosionParticles>();
            explosionObject.transform.position = transform.position;
            ExplosionParticles explosion = explosionObject.GetComponent<ExplosionParticles>();
            explosion.InitializeExplosionParticles(soldierRenderer.material);
            if(Enemy) {
                for(int i = 0; i < SimpleMath.Pow(3, Level); i++) {
                    GameObject stoneObject = PoolManager.instance.GetEntity<Stone>();
                    stoneObject.transform.position = transform.position;
                    Stone stone = stoneObject.GetComponent<Stone>();
                    stone.Initialize();
                }
            }
        }
        gameObject.SetActive(false);
    }

    protected void FollowWaypoint() {
        if(!enemy && IsOnGround()) {
            Vector3 direction = (Waypoint.transform.position - transform.position).normalized;
            float magnitude = (Waypoint.transform.position - transform.position).magnitude;
            soldierRigidbody.velocity = Mathf.Pow(magnitude, 2) * direction * Config.WAYPOINT_RECALL_VELOCITY;
        }
    }

    protected bool IsOutOfBound() {
        bool outBottom = transform.position.y < Config.WORLD_BOUND_Y_DOWN;
        bool outBack = transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outBack || outBottom;
    }

    protected bool IsOnGround() {
        float halfHeight = soldierRenderer.bounds.size.y / 2;
        return Physics.Raycast(transform.position, Vector3.down, halfHeight + Config.EPS);
    }


    protected void SetupSoldier() {
        soldierRigidbody = GetComponent<Rigidbody>();
        soldierRenderer = GetComponent<Renderer>();
        soldierAudio = GetComponent<AudioSource>();
        soldierLevelObjects = new GameObject[Config.MERGE_LEVEL_LIMIT];
        for(int i = 0; i < Config.MERGE_LEVEL_LIMIT; i++) {
            soldierLevelObjects[i] = transform.GetChild(i).gameObject;
        }
        gameObject.SetActive(false);
    }

    //! Subclass - AttackData
    public class AttackData {
        //! Variables
        private AttackType type;
        private int intensity;

        //! Properties
        public AttackType Type {
            get {
                return type;
            }
        }
        public int Intensity {
            get {
                return intensity;
            }
        }

        //! AttackData - Public
        public AttackData(AttackType type, int intensity) {
            this.type = type;
            this.intensity = intensity;
        }
    }


    //! Enumerators - AttackType
    public enum AttackType {
        Slash = 0,
        Bullet = 1,
        Explosion = 2
    }
}
