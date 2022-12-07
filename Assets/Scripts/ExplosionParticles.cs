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


    //! ExplosionParticles - Public
    public void InitializeExplosionParticles(Material material) {
        var main = particles.main;
        particlesRenderer.material.color = material.color;
        gameObject.SetActive(true);
        particles.Play();
        Invoke("RemoveFromGame", main.duration);
    }

    //! ExplosionParticles - Private
    private void RemoveFromGame() {
        gameObject.SetActive(false);
    }
}
