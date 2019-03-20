using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
 
    [SerializeField]
    private PlayerAttack PlayerAttackAsset;
    [SerializeField]
    private CounterManager CurrencyCounter;
    [Header("BASE ATTACK DMG")]
    [SerializeField]
    private int BaseAttackUpgradeCost = 50;
    [SerializeField]
    private int BaseAttackUpgrade = 10;
    [SerializeField]
    private Text BaseAttackCostText;
    [Header("FIRE BALL MANA REDUCTION")]
    [SerializeField]
    private int FireBallManaReducCost = 75;
    [SerializeField]
    private int FireBallManaReduc = 5;
    [SerializeField]
    private Text FireBallManaReducCostText;
    [Header("FIRE BALL DMG UPGRADE")]
    [SerializeField]
    private int FireBallDmgUpgradeCost = 100;
    [SerializeField]
    private int FireBallDmgUpgrade = 15;
    [SerializeField]
    private Text FireballDmgUpgradeCostText;
    [Header("FIRE BALL CD REDUCTION")]
    [SerializeField]
    private int FireBallCDReductionCost = 100;
    [SerializeField]
    private float FireBallCDReduction = 15;
    [SerializeField]
    private Text FireballCDReuctionCostText;
    [Header("BASE ATTACK SPEED UPGRADE")]
    [SerializeField]
    private int BaseAttackSpeedUpgradeCost = 50;
    [SerializeField]
    private float BaseAttackSpeedUgrade = 0.1f;
    [SerializeField]
    private Text BaseAttackSpeedCostText;
    [Header("EXPLOSION DMG UPGRADE")]
    [SerializeField]
    private int ExplosionDmgUpgradeCost = 125;
    [SerializeField]
    private int ExplosionDmgUpgrade = 20;
    [SerializeField]
    private Text ExplosionDmgUpgradeCostText;


    /// <summary>
    /// Inicializálja a shop árakat.
    /// </summary>
    void Start()
    {
        BaseAttackCostText.text =  BaseAttackUpgradeCost.ToString();
        BaseAttackSpeedCostText.text = BaseAttackSpeedUpgradeCost.ToString();
        FireBallManaReducCostText.text = FireBallManaReducCost.ToString();
        FireballDmgUpgradeCostText.text = FireBallDmgUpgradeCost.ToString();
        FireballCDReuctionCostText.text = FireBallCDReductionCost.ToString();
        ExplosionDmgUpgradeCostText.text = ExplosionDmgUpgradeCost.ToString();
    }

    /// <summary>
    /// Ha van elég Gold, növeli az alap támadás sebzését és levonja az upgrade árát.
    /// </summary>
    public void PurchaseStandardAttackUpgrade()
    {
        if (CurrencyCounter.Number >= BaseAttackUpgradeCost)
        {
            PlayerAttackAsset.BaseAttackDamage += BaseAttackUpgrade;
            CurrencyCounter.SubNumber(BaseAttackUpgradeCost);
        }
    }

    /// <summary>
    /// Ha van elég Gold és még nem értük el a minimum támadási sebességet, akkor csökkenti az alap támadások közti időt és levonja az upgrade árát.
    /// </summary>
    public void PurchaseStandardAttackSpeedUpgrade()
    {
        if (CurrencyCounter.Number >= BaseAttackSpeedUpgradeCost && PlayerAttackAsset.StartTimeBtwAttack >= 0.14f)
        {
            PlayerAttackAsset.StartTimeBtwAttack -= BaseAttackSpeedUgrade;
            CurrencyCounter.SubNumber(BaseAttackSpeedUpgradeCost);

            if (PlayerAttackAsset.StartTimeBtwAttack < 0.1f)
            {
                PlayerAttackAsset.StartTimeBtwAttack = 0.1f;
            }
        }
    }

    /// <summary>
    /// Ha van elég Gold, csökkenti a FireBall mana költségét (amíg az >= 0) és levonja az upgrade árát.
    /// </summary>
    public void PurchaseFireBallManaReducUpgrade()
    {
        if (CurrencyCounter.Number >= FireBallManaReducCost )
        {
            if (PlayerAttackAsset.FireBallManaCost - FireBallManaReduc >= 0)
            {
                PlayerAttackAsset.FireBallManaCost -= FireBallManaReduc;
                CurrencyCounter.SubNumber(FireBallManaReducCost);
            }else
            {
                PlayerAttackAsset.FireBallManaCost = 0;
            }
        }
    }

    /// <summary>
    /// Ha van elég Gold, növeli a FireBall sebzését é  levonja az upgrade árát.
    /// </summary>
    public void PurchaseFireBallDmgUpgrade()
    {
        if (CurrencyCounter.Number >= FireBallDmgUpgradeCost)
        {
            PlayerAttackAsset.FireBallDamage += FireBallDmgUpgrade;
            CurrencyCounter.SubNumber(FireBallDmgUpgradeCost);
        }
    }

    public void PurchaseFireBallCDReduction()
    {
        if (CurrencyCounter.Number >= FireBallCDReductionCost && PlayerAttackAsset.StartTimeBtwFireBalls >= 0.5f)
        {
            PlayerAttackAsset.StartTimeBtwFireBalls -= FireBallCDReduction;
            CurrencyCounter.SubNumber(FireBallCDReductionCost);

            if (PlayerAttackAsset.StartTimeBtwFireBalls < 0.5f)
            {
                PlayerAttackAsset.StartTimeBtwFireBalls = 0.5f;
            }
        }
    }



    /// <summary>
    /// Ha van elég Gold, növeli a robbantás sebzését és levonja az upgrade árát.
    /// </summary>
    public void PurchaseExplosionDmgUpgrade()
    {
        if (CurrencyCounter.Number >= ExplosionDmgUpgradeCost)
        {
            PlayerAttackAsset.ExplosionDamage += ExplosionDmgUpgrade;
            CurrencyCounter.SubNumber(ExplosionDmgUpgradeCost);
        }
    }

}
