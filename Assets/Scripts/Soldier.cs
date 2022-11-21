using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : MonoBehaviour {

    //! Variables
    [SerializeField] protected int health;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected bool enemy;

    //! Components
    protected Rigidbody solderRigidbody;
    protected Renderer soldierRenderer;

    //! References
    [SerializeField] protected Material playerMaterial;
    [SerializeField] protected Material enemyMaterial;


    //! MonoBehaviour
    void Start() {
        ResetSoldier();

        solderRigidbody = GetComponent<Rigidbody>();
        soldierRenderer = GetComponent<Renderer>();
        soldierRenderer.material = enemy ? enemyMaterial : playerMaterial;
    }

    void Update() {
        if (IsOutOfBound()) {
            Die();
        }
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
    public virtual void ResetSoldier() {

    }

    //! Soldier - Protected
    protected virtual void EnemyUpdate() {

    }

    protected virtual void PlayerUpdate() {

    }

    protected virtual void EnemyOnCollisionEnter(Collision other) {

    }

    protected virtual void PlayerOnCollisionEnter(Collision other) {

    }

    protected virtual void Attack(Soldier target) {
        target.Suffer(attackDamage);
    }

    protected virtual void Suffer(int damage) {
        health = Mathf.Max(health - damage, 0);
        if (health == 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Destroy(gameObject);
    }

    //! Soldier - Private
    private void MoveForward() {
        if (IsOnGround()) {
            solderRigidbody.velocity = Vector3.back * Config.WORLD_SCROLL_VELOCITY;
        }
    }

    private void ControlSideways(float sideInput) {
        if (IsOnGround()) {
            solderRigidbody.velocity = Vector3.right * sideInput * Config.SOLDIER_SIDE_VELOCITY;
        }
    }
    
    private bool IsOutOfBound() {
        bool outBottom = transform.position.y < Config.WORLD_BOUND_Y_DOWN;
        bool outBack = transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outBack || outBottom;
    }

    private bool IsOnGround() {
        float width = soldierRenderer.bounds.size.x / 2;
        float feetY = soldierRenderer.bounds.center.y - soldierRenderer.bounds.size.y / 2;
        bool onRoad = Mathf.Abs(transform.position.x) <= Config.WORLD_ROAD_BOUND_X + width;
        bool onRightHeight = Mathf.Abs(feetY - Config.WORLD_ROAD_Y) < Config.EPS;
        return onRoad && onRightHeight;
    }
}
