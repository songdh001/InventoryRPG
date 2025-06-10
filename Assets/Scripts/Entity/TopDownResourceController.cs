using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangDelay = 0.5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;
    private UIManager uiManager;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float CurrentCoin;
    public float MaxHealth => statHandler.GetStat(StatType.Health);


    public AudioClip damageClip;


    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.GetStat(StatType.Health);
        CurrentCoin = statHandler.GetStat(StatType.Coin);
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
        uiManager.CoinCount(CurrentCoin);
    }

    public bool ChangeHealth(float change)
    {
        if(change ==0 || timeSinceLastChange < healthChangDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0: CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            if(damageClip != null)
            {
                SoundManager.PlayClip(damageClip);
            }
        }

        if(CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    public void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action) 
    {
        OnChangeHealth -= action;
    }


}
