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
                direction = (0.9f * direction + 0.1f * newDirection).normalized;
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
    public void InitializeBullet(Soldier.AttackData attack, GameObject target) {
        this.attack = attack;
        this.target = target;
        this.enemy = !target.GetComponent<Soldier>().Enemy;
        velocity = Config.BULLET_VELOCITY;
        direction = (target.transform.position - transform.position).normalized;
        gameObject.SetActive(true);
    }
}
