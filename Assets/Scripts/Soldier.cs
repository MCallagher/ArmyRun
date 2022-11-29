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
            ControlSideways(sideInput);
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
    protected abstract void EnemyUpdate();

    protected abstract void PlayerUpdate();

    protected abstract void EnemyOnCollisionEnter(Collision other);

    protected abstract void PlayerOnCollisionEnter(Collision other);

    protected virtual void RefreshUI() {
        countText.text = "" + Count;
        healthSlider.minValue = 0;
        healthSlider.maxValue = Constitution;
        healthSlider.value = Health;
    }

    protected abstract void RecomputeProperties();

    protected abstract void Attack(Soldier target);

    protected abstract List<Soldier> Scan(float radius);

    protected virtual void Die() {
        gameObject.tag = Config.TAG_DEFAULT;
        gameObject.SetActive(false);
    }

    protected void MoveForward() {
        if (IsOnGround()) {
            solderRigidbody.velocity = Vector3.back * Config.WORLD_SCROLL_VELOCITY;
        }
    }

    protected void ControlSideways(float sideInput) {
        if (IsOnGround()) {
            solderRigidbody.velocity = Vector3.right * sideInput * Config.SOLDIER_SIDE_VELOCITY;
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
