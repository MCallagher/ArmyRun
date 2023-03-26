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

    private bool IsArmyDamaged() {
        int health = 0;
        int totHealth = 0;
        foreach(GameObject soldierObject in PoolManager.instance.GetActiveGameObject<Soldier>()) {
            Soldier soldier = soldierObject.GetComponent<Soldier>();
            if (!soldier.Enemy) {
                health += soldier.Health;
                totHealth += soldier.MaxHealth;
            }
        }
        return health < Config.BONUS_THRESHOLD_DAMAGED * totHealth;
    }

    private Bonus[] GenerateBonuses() {
        Bonus[] bonuses = new Bonus[2];
        bool coin = AdvancedRandom.CoinFlip();
        int first = coin ? 0 : 1;
        int second = coin ? 1 : 0;
        // Valid soldiers
        List<Bonus.BonusType> possibleBonuses = new List<Bonus.BonusType>();
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierMelee)) possibleBonuses.Add(Bonus.BonusType.AddMelee);
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierRanged)) possibleBonuses.Add(Bonus.BonusType.AddRanged);
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierGunner)) possibleBonuses.Add(Bonus.BonusType.AddGunner);
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierSniper)) possibleBonuses.Add(Bonus.BonusType.AddSniper);
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierBumper)) possibleBonuses.Add(Bonus.BonusType.AddBumper);
        if(Progress.instance.IsUnlocked(Progress.UnlockCode.soldierKamikaze)) possibleBonuses.Add(Bonus.BonusType.AddKamikaze);
        int firstChoice = Random.Range(0, possibleBonuses.Count);
        int secondChoice = Random.Range(0, possibleBonuses.Count);
        // Special condition
        bool damaged = IsArmyDamaged();
        if (damaged) {
            bonuses[first] = new Bonus(Bonus.BonusType.Heal);
            bonuses[second] = new Bonus(possibleBonuses[firstChoice]);
        }
        // Standard condition
        else {
            bonuses[first] = new Bonus(possibleBonuses[firstChoice]);
            bonuses[second] = new Bonus(possibleBonuses[secondChoice]);
        }
        return bonuses;
    }
}
