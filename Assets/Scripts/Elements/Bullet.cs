using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //! Variables
    [SerializeField] private Soldier.AttackData attack;
    [SerializeField] private GameObject target;
    [SerializeField] private bool enemy;
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 direction;


    //! MonoBehaviour
    void Update() {
        if (target != null) {
            if (target.activeInHierarchy) {
                Vector3 newDirection = (target.transform.position - transform.position).normalized;
                //direction = (0.9f * direction + 0.1f * newDirection).normalized;
                direction = newDirection.normalized;
            }
            else {
                target = null;
            }
        }
        if (target != null && !target.activeInHierarchy) {
                target = null;
        }
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(Config.TAG_ENEMY) && !enemy || other.gameObject.CompareTag(Config.TAG_PLAYER) && enemy) {
            other.gameObject.GetComponent<Soldier>().Defend(attack);
            gameObject.SetActive(false);
        }
    }


    //! Bullet - Public
    public void Initialize(Soldier.AttackData attack, GameObject target, BulletType type) {
        this.attack = attack;
        this.target = target;
        this.enemy = !target.GetComponent<Soldier>().Enemy;
        switch (type) {
            case BulletType.normal:
                transform.localScale = Config.BULLET_SCALE;
                this.velocity = Config.BULLET_VELOCITY;
                break;
            case BulletType.gunner:
                transform.localScale = Config.GUNNER_BULLET_SCALE;
                this.velocity = Config.GUNNER_BULLET_VELOCITY;
                break;
            case BulletType.sniper:
                transform.localScale = Config.SNIPER_BULLET_SCALE;
                this.velocity = Config.SNIPER_BULLET_VELOCITY;
                break;
            default:
                Debug.LogWarning($"Bullet type {type} not found");
                break;
        }
        direction = (target.transform.position - transform.position).normalized;
        gameObject.SetActive(true);
    }

    public enum BulletType {
        normal = 0,
        gunner = 1,
        sniper = 2
    }
}
