using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [Header("STATPANEL")]
    [SerializeField]
    private Text StatBaseAttackDamage;
    [SerializeField]
    private Text StatBaseAttackSpeed;
    [SerializeField]
    private Text StatFireBallDamage;
    [SerializeField]
    private Text StatFireBallManaCost;
    [SerializeField]
    private Text StatFireBallCoolDown;
    [SerializeField]
    private Text StatHealAmount;
    [SerializeField]
    private Text StatHealCost;
    [SerializeField]
    private Text StatHealCoolDown;
    [SerializeField]
    private Text StatExplosionDamage;
    [SerializeField]
    private Text StatExplosionManaCost;
    [SerializeField]
    private Text StatExplosionCoolDown;
    [Header("INFOPANEL")]
    [SerializeField]
    private Text enemyNumber;
    [SerializeField]
    private Text mana;
    [SerializeField]
    private Text health;
    [SerializeField]
    private Text waveCount;
    [SerializeField]
    private Text currency;
    [Header("SCRIPTABLEOBJECTS")]
    [SerializeField]
    private CounterManager EnemyCounter;
    [SerializeField]
    private CounterManager ManaCounter;
    [SerializeField]
    private CounterManager HealthCounter;
    [SerializeField]
    private WaveCounter WaveCounter;
    [SerializeField]
    private CounterManager CurrencyCounter;
    [SerializeField]
    private PlayerAttack PlayerattackAasset;

    /// <summary>
    /// Kezdeti feliratkozás és érték resetek.
    /// </summary>
    private void OnEnable()
    {
        EnemyCounter.ChangeDataEvent.AddListener(UiUpdate);
        ManaCounter.ChangeDataEvent.AddListener(UiUpdate);
        HealthCounter.ChangeDataEvent.AddListener(UiUpdate);
        WaveCounter.ChangeDataEvent.AddListener(UiUpdate);
        CurrencyCounter.ChangeDataEvent.AddListener(UiUpdate);
        EnemyCounter.Reset();
        CurrencyCounter.Reset();
        ManaCounter.Reset();
        WaveCounter.Reset();
        WaveCounter.WaveNumber = 1;
        HealthCounter.Reset(HealthCounter.MaximumValue);
        UiUpdate();
        StatPanelUpdate();
    }


    private void OnDisable()
    {
        EnemyCounter.ChangeDataEvent.RemoveListener(UiUpdate);
        ManaCounter.ChangeDataEvent.RemoveListener(UiUpdate);
        HealthCounter.ChangeDataEvent.RemoveListener(UiUpdate);
        WaveCounter.ChangeDataEvent.RemoveListener(UiUpdate);
    }

    /// <summary>
    /// infopanel updatelő
    /// </summary>
    public void UiUpdate()
    {
        enemyNumber.text = "Enemies : " + EnemyCounter.Number + "/" + EnemyCounter.MaximumValue;
        mana.text = "Mana : " + ManaCounter.Number + "/" +ManaCounter.MaximumValue;
        currency.text = "Gold : " + CurrencyCounter.Number + "/" + CurrencyCounter.MaximumValue;
        health.text = "Health : " + HealthCounter.Number + "/" + HealthCounter.MaximumValue;
        waveCount.text = "Wave :" + WaveCounter.WaveNumber + " " + WaveCounter.Number;
    }


    /// <summary>
    /// Stat Panel updatelő
    /// </summary>
    public void StatPanelUpdate()
    {
        StatBaseAttackDamage.text = "Damage : " + PlayerattackAasset.BaseAttackDamage;
        StatBaseAttackSpeed.text = "Attack Speed : " + PlayerattackAasset.StartTimeBtwAttack;
        StatFireBallDamage.text = "Damage : " + PlayerattackAasset.FireBallDamage;
        StatFireBallManaCost.text = "Mana Cost : " + PlayerattackAasset.FireBallManaCost;
        StatFireBallCoolDown.text = "Key : Right Mouse Button \n\rCoolDown : " + PlayerattackAasset.StartTimeBtwFireBalls;
        StatHealAmount.text = "Heal : " + PlayerattackAasset.HealAmount;
        StatHealCost.text = "Mana Cost : " + PlayerattackAasset.HealManaCost;
        StatHealCoolDown.text = "Key : F \n\rCoolDown : " + PlayerattackAasset.StartTimeBtwHeals;
        StatExplosionDamage.text = "Damage : " + PlayerattackAasset.ExplosionDamage;
        StatExplosionManaCost.text = "Mana Cost : " + PlayerattackAasset.ExplosionManaCost;
        StatExplosionCoolDown.text = "Key : E\n\rCoolDown : " + PlayerattackAasset.StartTimeBtwExplosions;
    }
}

