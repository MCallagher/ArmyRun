using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Soldier : MonoBehaviour {

    //! Variables
    // value of this soldier
    [SerializeField] protected int count;
    // Phisical properties
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    // Side
    [SerializeField] protected bool enemy;

    //! Properties
    public int Count {
        get {
            return count;
        }
        protected set {
            if (value > 0) {
                count = value;
                RecomputeProperties();
            }
            else {
                Debug.LogWarning("Count cannot be set to a negative number: " + value);
            }
        }
    }

    public int MaxHealth {
        get {
            return maxHealth;
        }
    }

    public int Health {
        get {
            return health;
        }
        protected set {
            health = Mathf.Min(Mathf.Max(0, value), MaxHealth);
            if (health == 0) {
                Die();
            }
        }
    }

    public bool Enemy {
        get {
            return enemy;
        }
        protected set {
            enemy = value;
            soldierRenderer.material = enemy ? enemyMaterial : playerMaterial;
        }
    }


    //! Components
    protected Rigidbody soldierRigidbody;
    protected Renderer soldierRenderer;
    protected GameObject waypoint;

    //! References
    [SerializeField] protected Material playerMaterial;
    [SerializeField] protected Material enemyMaterial;
    [SerializeField] protected TextMeshProUGUI countText;
    [SerializeField] protected Slider healthSlider;


    //! MonoBehaviour
    void Awake() {
        soldierRigidbody = GetComponent<Rigidbody>();
        soldierRenderer = GetComponent<Renderer>();
        waypoint = GameObject.Find(Config.WAYPOINT_NAME);
        gameObject.SetActive(false);
    }

    void Update() {
        if (IsOutOfBound()) {
            Die();
        }
        RefreshUI();
        ControlMovement();
    }


    //! Soldier - Public
    public virtual void InitializeSoldier(int count, bool enemy) {
        this.Count = count;
        this.Enemy = enemy;
        gameObject.SetActive(true);
        // Reset phisics
        soldierRigidbody.velocity = new Vector3(0f,0f,0f); 
        soldierRigidbody.angularVelocity = new Vector3(0f,0f,0f);
    }

    public abstract void Attack(Soldier target);

    public virtual void Defend(AttackData attack) {
        Health -= attack.Intensity;
    }

    public virtual void Heal() {
        Health = MaxHealth;
    }

    //! Soldier - Protected
    protected virtual void RefreshUI() {
        countText.text = "" + Count;
        healthSlider.minValue = 0;
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = Health;
    }

    protected abstract void RecomputeProperties();

    protected virtual void Die() {
        if (Health == 0) {
            GameObject explosionObject =  PoolManager.instance.GetEntity<ExplosionParticles>();
            explosionObject.transform.position = transform.position;
            ExplosionParticles explosion = explosionObject.GetComponent<ExplosionParticles>();
            explosion.InitializeExplosionParticles(soldierRenderer.material);
        }
        gameObject.tag = Config.TAG_DEFAULT;
        gameObject.SetActive(false);
    }

    protected void ControlMovement() {
        if(!enemy) {
            if(IsOnGround()) {
                Vector3 direction = (waypoint.transform.position - transform.position).normalized;
                float magnitude = (waypoint.transform.position - transform.position).magnitude;
                soldierRigidbody.velocity = Mathf.Pow(magnitude, 2) * direction * Config.SOLDIER_WAYPOINT_RECALL_VELOCITY;
            }
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


    //! Enumerators - AttackType, Faction
    public enum AttackType {
        Slash = 0,
        Bullet = 1
    }
}