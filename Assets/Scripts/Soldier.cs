using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private bool enemy;

    private Rigidbody solderRigidbody;
    private Renderer soldierRenderer;

    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;

    private static int initHealth = 100;
    private static int initDamage = 10;
    private static float scrollVelocity = 5;
    private static float sideVelocity = 5;

    void Start() {
        health = initHealth;
        damage = initDamage;
        solderRigidbody = GetComponent<Rigidbody>();
        soldierRenderer = GetComponent<Renderer>();
        soldierRenderer.material = enemy ? enemyMaterial : playerMaterial;
    }

    void Update() {
        if (enemy) {
            MoveForward();
        }
        else {
            float sideInput = Input.GetAxis("Horizontal");
            ControlSideways(sideInput);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (gameObject.CompareTag("Player") && other.gameObject.CompareTag("Enemy")) {
            Soldier otherSoldier = other.gameObject.GetComponent<Soldier>();
            int attacks = Mathf.Min(
                Mathf.CeilToInt(health / otherSoldier.damage),
                Mathf.CeilToInt(otherSoldier.health / damage)
            );
            InflictDamage(otherSoldier.damage * attacks);
            otherSoldier.InflictDamage(damage * attacks);
        }
    }

    private void MoveForward() {
        solderRigidbody.velocity = Vector3.back * scrollVelocity;
    }

    private void ControlSideways(float sideInput) {
        solderRigidbody.velocity = Vector3.right * sideInput * sideVelocity;
    }

    public void InflictDamage(int damage) {
        health = Mathf.Max(health - damage, 0);
        if (health == 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
