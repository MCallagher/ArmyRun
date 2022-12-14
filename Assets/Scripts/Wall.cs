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
        Bonus[] bonuses = GenerateBonuses();
        leftBonus = bonuses[0];
        rightBonus = bonuses[1];
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

    private bool IsArmyDamaged() {
        int health = 0;
        int totHealth = 0;
        List<Soldier> soldiers = new List<Soldier>();
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<MeleeSoldier>());
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<RangedSoldier>());
        foreach (Soldier soldier in soldiers) {
            if (!soldier.Enemy) {
                health += soldier.Health;
                totHealth += soldier.Constitution;
            }
        }
        return health < Config.BONUS_THRESHOLD_DAMAGED * totHealth;
    }

    private bool IsArmyCrowded() {
        int count = 0;
        List<Soldier> soldiers = new List<Soldier>();
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<MeleeSoldier>());
        soldiers.AddRange(PoolManager.instance.GetActiveEntities<RangedSoldier>());
        foreach (Soldier soldier in soldiers) {
            if (!soldier.Enemy) {
                count++;
            }
        }
        return count >= Config.BONUS_THRESHOLD_CROWDED;
    }

    private Bonus[] GenerateBonuses() {
        Bonus[] bonuses = new Bonus[2];
        bool coin = AdvancedRandom.CoinFlip();
        int first = coin ? 0 : 1;
        int second = coin ? 1 : 0;
        // Special conditions
        bool crowded = IsArmyCrowded();
        bool damaged = IsArmyDamaged();
        if (crowded || damaged) {
            // first bonus
            if (crowded && !damaged) {
                bonuses[first] = new Bonus(Bonus.BonusType.Merge);
            }
            else if (!crowded && damaged) {
                bonuses[first] = new Bonus(Bonus.BonusType.Heal);
            }
            else {
                bonuses[first] = new Bonus(AdvancedRandom.CoinFlip() ? Bonus.BonusType.Merge : Bonus.BonusType.Heal);
            }
            // second bonus
            bonuses[second] = new Bonus(AdvancedRandom.CoinFlip() ? Bonus.BonusType.AddMelee : Bonus.BonusType.AddRanged);
        }
        // Standard conditions
        else {
            bonuses[first] = new Bonus(Bonus.BonusType.AddMelee);
            bonuses[second] = new Bonus(Bonus.BonusType.AddRanged);
        }
        return bonuses;
    }
}
