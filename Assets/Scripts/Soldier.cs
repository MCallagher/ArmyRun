using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Soldier : MonoBehaviour {

    //! Variables
    [SerializeField] protected int count;
    [SerializeField] protected int strength;
    [SerializeField] protected int constitution;
    [SerializeField] protected int health;
    [SerializeField] protected bool enemy;
    [SerializeField] protected int baseReward;

    //! Properties
    public int Count {
        get {
            return count;
        }
        set {
            if (value > 0) {
                count = value;
                RecomputeProperties();
            }
            else {
                Debug.LogWarning("Count cannot be set to a negative number: " + value);
            }
        }
    }

    public int Strength {
        get {
            return strength;
        }
    }

    public int Constitution {
        get {
            return constitution;
        }
    }

    public int Health {
        get {
            return health;
        }
        set {
            health = Mathf.Min(Mathf.Max(0, value), constitution);
            if (health == 0) {
                Die();
            }
        }
    }

    public bool Enemy {
        get {
            return enemy;
        }
        set {
            enemy = value;
            soldierRenderer.material = enemy ? enemyMaterial : playerMaterial;
        }
    }

    public int Reward {
        get {
            return count * baseReward;
        }
    }


    //! Components
    protected Rigidbody solderRigidbody;
    protected Renderer soldierRenderer;

    //! References
    [SerializeField] protected Material playerMaterial;
    [SerializeField] protected Material enemyMaterial;
    [SerializeField] protected TextMeshProUGUI countText;
    [SerializeField] protected Slider healthSlider;


    //! MonoBehaviour
    void Awake() {
        solderRigidbody = GetComponent<Rigidbody>();
        soldierRenderer = GetComponent<Renderer>();
        gameObject.SetActive(false);
    }

    void Start() {
        if (enemy) {
            EnemyStart();
        }
        else {
            PlayerStart();
        }
    }

    void Update() {
        if (IsOutOfBound()) {
            Die();
        }
        RefreshUI();
        if (enemy) {
            MoveForward();
            EnemyUpdate();
        }
        else {
            float sideInput = Input.GetAxis("Horizontal");
            float frontalInput = Input.GetAxis("Vertical");
            ControlMovement(sideInput, frontalInput);
            PlayerUpdate();
        }
    }

    void OnCollisionEnter(Collision other) {
        if (enemy) {
            EnemyOnCollisionEnter(other);
        }
        else {
            PlayerOnCollisionEnter(other);
        }
    }


    //! Soldier - Public
    public virtual void InitializeSoldier(int count, bool enemy) {
        this.Enemy = enemy;
        this.Count = count;
        gameObject.SetActive(true);
    }

    public abstract void Merge(List<Soldier> soldier);

    //! Soldier - Protected
    protected virtual void PlayerStart() {
        // No action
    }

    protected virtual void EnemyStart() {
        // No action
    }

    protected virtual void EnemyUpdate() {
        // No action
    }

    protected virtual void PlayerUpdate() {
        // No action
    }

    protected virtual void EnemyOnCollisionEnter(Collision other) {
        // No action
    }

    protected virtual void PlayerOnCollisionEnter(Collision other) {
        // No action
    }

    protected virtual void RefreshUI() {
        countText.text = "" + Count;
        healthSlider.minValue = 0;
        healthSlider.maxValue = Constitution;
        healthSlider.value = Health;
    }

    protected abstract void RecomputeProperties();

    protected abstract void Attack(Soldier target);

    protected virtual void Die() {

        if (Health == 0) {
            if (Enemy) {
                GameManager.instance.Coins += Reward;
            }
            GameObject explosionObject =  PoolManager.instance.GetEntity<ExplosionParticles>();
            explosionObject.transform.position = transform.position;
            ExplosionParticles explosion = explosionObject.GetComponent<ExplosionParticles>();
            explosion.InitializeExplosionParticles(soldierRenderer.material);
        }
        gameObject.tag = Config.TAG_DEFAULT;
        gameObject.SetActive(false);
    }

    protected void MoveForward() {
        if (IsOnGround()) {
            solderRigidbody.velocity = Vector3.back * Config.WORLD_SCROLL_VELOCITY;
        }
    }

    protected void ControlMovement(float sideInput, float frontalInput) {
        if (IsOnGround()) {
            solderRigidbody.velocity = (Vector3.right * sideInput + Vector3.forward * frontalInput) * Config.SOLDIER_SIDE_VELOCITY;
            float z = Mathf.Sign(transform.position.z) * Mathf.Min(Mathf.Abs(transform.position.z), Config.WORLD_BOUND_PLAYER_Z);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
    
    protected bool IsOutOfBound() {
        bool outBottom = transform.position.y < Config.WORLD_BOUND_Y_DOWN;
        bool outBack = transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outBack || outBottom;
    }

    protected bool IsOnGround() {
        float width = soldierRenderer.bounds.size.x / 2;
        float feetY = soldierRenderer.bounds.center.y - soldierRenderer.bounds.size.y / 2;
        bool onRoad = Mathf.Abs(transform.position.x) <= Config.WORLD_ROAD_BOUND_X + width;
        bool onRightHeight = Mathf.Abs(feetY - Config.WORLD_ROAD_Y) < Config.EPS;
        return onRoad && onRightHeight;
    }
}
