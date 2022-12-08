using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticles : MonoBehaviour {

    //! References
    private ParticleSystem particles;
    private ParticleSystemRenderer particlesRenderer;


    //! MonoBehaviour
    void Awake() {
        particles = GetComponent<ParticleSystem>();
        particlesRenderer = GetComponent<ParticleSystemRenderer>();
    }

    void Update() {
        MoveForward();
    }


    //! ExplosionParticles - Public
    public void InitializeExplosionParticles(Material material) {
        var main = particles.main;
        particlesRenderer.material.color = material.color;
        gameObject.SetActive(true);
        particles.Play();
        Invoke("RemoveFromGame", main.duration + main.startLifetime.constant);
    }

    //! ExplosionParticles - Private
    private void RemoveFromGame() {
        gameObject.SetActive(false);
    }

    private void MoveForward() {
        transform.Translate(Vector3.back * Config.WORLD_SCROLL_VELOCITY * Time.deltaTime);
    }
}
