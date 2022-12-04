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


    // Start is called before the first frame update
    void Start() {
        
    }

    public void InitializeBullet(int damage, GameObject target) {
        this.damage = damage;
        this.target = target;
        this.enemy = !target.GetComponent<Soldier>().Enemy;
        velocity = Config.BULLET_VELOCITY;
        direction = (target.transform.position - transform.position).normalized;
        gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update() {
        if (target != null) {
            if (target.activeInHierarchy) {
                direction = (target.transform.position - transform.position).normalized;
            }
            else {
                target = null;
            }
        }
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(Config.TAG_ENEMY) && !enemy || other.gameObject.CompareTag(Config.TAG_PLAYER) && enemy) {
            other.gameObject.GetComponent<Soldier>().Health -= damage;
            gameObject.SetActive(false);
        }
    }
}
