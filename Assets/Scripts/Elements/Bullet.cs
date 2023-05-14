using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //! Variables
    [SerializeField] private Soldier.AttackData attack;
    [SerializeField] private GameObject target;
    [SerializeField] private bool enemy;
    [SerializeField] private Vector3 direction;
    [SerializeField] private bool homing;
    [SerializeField] private float velocity;

    //! References
    private AudioSource bulletAudio;

    //! MonoBehaviour
    void Awake() {
        bulletAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if(homing) {
            if (target != null) {
                if (target.activeInHierarchy) {
                    Vector3 newDirection = (target.transform.position - transform.position).normalized;
                    direction = newDirection.normalized;
                }
                else {
                    target = null;
                }
            }
            if (target != null && !target.activeInHierarchy) {
                    target = null;
            }
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
    public void Initialize(Soldier.AttackData attack, BulletType type, GameObject target) {
        // Basic initialization
        this.attack = attack;
        transform.localScale = Config.BULLET_SCALE[(int)type];
        this.velocity = Config.BULLET_VELOCITY[(int)type];
        // Target initialization
        this.target = target;
        this.enemy = !target.GetComponent<Soldier>().Enemy;
        this.direction = (target.transform.position - transform.position).normalized;
        this.homing = !enemy;
        gameObject.SetActive(true);
        bulletAudio.volume = Config.SOUND_VOLUME_SHOT * Options.instance.SoundsVolume * Options.instance.EffectsVolume;
        bulletAudio.Play();
    }


    //! BulletType
    public enum BulletType {
        normal = 0,
        gunner = 1,
        sniper = 2
    }
}
