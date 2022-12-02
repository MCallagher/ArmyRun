using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wall : MonoBehaviour
{
    //! Variables
    [SerializeField] private Bonus leftBonus;
    [SerializeField] private Bonus rightBonus;
    [SerializeField] private bool claimed;

    //! References
    [SerializeField] private TextMeshProUGUI leftBonusText;
    [SerializeField] private TextMeshProUGUI rightBonusText;

    //! Monobehaviour
    void Update()
    {
        if (IsOutOfBound()) {
            RemoveFromGame();
        }
        MoveForward();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(Config.TAG_PLAYER) && !claimed) {
            claimed = true;
            if (other.gameObject.transform.position.x > 0) {
                rightBonus.ActivateBonus();
            }
            else {
                leftBonus.ActivateBonus();
            }
        }
    }

    //! Wall - Public
    public void InitializeWall() {
        leftBonus = new Bonus();
        rightBonus = new Bonus();
        claimed = false;
        SetBonusText();
        gameObject.SetActive(true);
    }

    //! Wall - Private
    private void SetBonusText() {
        leftBonusText.text = leftBonus.ToString();
        rightBonusText.text = rightBonus.ToString();
    }

    private void MoveForward() {
        transform.Translate(Vector3.back * Config.WORLD_SCROLL_VELOCITY * Time.deltaTime);
    }

    private bool IsOutOfBound() {
        bool outBack = transform.position.z < Config.WORLD_BOUND_Z_BACK;
        return outBack;
    }

    private void RemoveFromGame() {
        gameObject.SetActive(false);
    }
}
