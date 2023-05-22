using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {

    //! Variables
    private int value;

    //! Properties
    public int Value {
        get {
            return this.value;
        }
        protected set {
            this.value = value;
        }
    }

    //! Components
    private Renderer stoneRenderer;
    private JumpBack jumpController;

    //! References
    [SerializeField] protected Material[] stoneMaterials;


    //! MonoBehaviour
    void Awake() {
        stoneRenderer = GetComponentInChildren<Renderer>();
        jumpController = GetComponent<JumpBack>();
    }

    void OnTriggerEnter(Collider other) {
        if (!jumpController.IsJumping && other.gameObject.CompareTag(Config.TAG_PLAYER)) {
            GameManager.instance.AddStones(value);
            GameObject board = PoolManager.instance.GetEntity<Board>();
            board.transform.position = transform.position;
            board.GetComponent<Board>().Initialize($"+{value}", 36);
            gameObject.SetActive(false);
        }
    }


    //! Stone - Public
    public void Initialize() {
        StoneType type = (StoneType)AdvancedRandom.RangeWithNormalizedWeight(Config.STONE_CHANCE_DISTRIBUTION);
        Initialize(type);
    }

    public void Initialize(StoneType type) {
        value = Config.STONE_VALUE[(int)type];
        stoneRenderer.material = stoneMaterials[(int)type];
        gameObject.SetActive(true);
    }


    //! StoneType
    public enum StoneType {
        Sapphire = 0,
        Emerald = 1,
        Ruby = 2,
        Diamond = 3
    }
}
