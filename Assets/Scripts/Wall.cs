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
            claimed = false;
            if (other.gameObject.transform.position.x > 0) {
                ActivateBonus(false);
            }
            else {
                ActivateBonus(true);
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
    private void ActivateBonus(bool left) {
        if (claimed) {
            return;
        }
        claimed = true;
        Bonus bonus = left ? leftBonus : rightBonus;
        if (bonus.type == BonusType.ExtraArmy) {
            GameManager.instance.BonusExtraArmy(bonus.value);
        }
        else if (bonus.type == BonusType.Heal) {
            GameManager.instance.BonusHeal(bonus.value);
        }
        else if (bonus.type == BonusType.Merge) {
            GameManager.instance.BounsMerge();
        }
        else {
            Debug.LogWarning("Bonus type (" + bonus.type + ") not found");
        }
    }

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


    //! Classes
    class Bonus {
        public BonusType type;
        public int value;

        public Bonus() {
            type = (BonusType) AdvancedRandom.RangeWithWeight(Config.WALL_BONUS_DISTRIBUTION);
            if (type == BonusType.ExtraArmy) {
                value = Random.Range(1, 10);
            }
            else if (type == BonusType.Heal) {
                value = 10 * Random.Range(1, 11);
            }
            else if (type == BonusType.Merge) {
                value = -1;
            }
            else {
                Debug.LogWarning("Bonus type (" + type + ") not found");
            }
        }

        public override string ToString() {
            if (type == BonusType.ExtraArmy) {
                return "Add soldier\n" + value;
            }
            if (type == BonusType.Heal) {
                return "Heal army\n" + value + "%";
            }
            if (type == BonusType.Merge) {
                return "Merge army";
            }
            Debug.LogWarning("Bonus type (" + type + ") not found");
            return "";
        }
    }

    enum BonusType {
        ExtraArmy = 0, 
        Heal = 1,
        Merge = 2 
    }
}
