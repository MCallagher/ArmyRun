using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //! Variables
    [SerializeField] private int damage;
    [SerializeField] private GameObject target;
    [SerializeField] private bool enemy;
    [SerializeField] private float velocity;
    [SerializeField] private Vector3 direction;


    //! MonoBehaviour
    void Update() {
        if (IsOutOfBounds()) {
            RemoveFromGame();
        }
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
            other.gameObject.GetComponent<Soldier>().Health -= damage;
            gameObject.SetActive(false);
        }
    }


    //! Bullet - Public
    public void InitializeBullet(int damage, GameObject target) {
        this.damage = damage;
        this.target = target;
        this.enemy = !target.GetComponent<Soldier>().Enemy;
        velocity = Config.BULLET_VELOCITY;
        direction = (target.transform.position - transform.position).normalized;
        gameObject.SetActive(true);
    }

    //! Bullet - Private
    private bool IsOutOfBounds() {
        bool outOfBoundX = Mathf.Abs(transform.position.x) > Config.WORLD_BOUND_X;
        bool outOfBoundY = transform.position.y > Config.WORLD_BOUND_Y_UP || transform.position.y < Config.WORLD_BOUND_Y_DOWN;
        bool outOfBoundZ = transform.position.z > Config.WORLD_BOUND_Z_FORWARD || transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outOfBoundX || outOfBoundY || outOfBoundZ;
    }

    private void RemoveFromGame() {
        gameObject.SetActive(false);
    }
}
