using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image effect;
    public Image health;

    private float effectSpeed = 0.02f;

    public void ChangeHealth(float _health,float _maxHealth)
    {
        health.fillAmount = _health / _maxHealth;
    }

    public void DoHealthEffect()
    {
        StartCoroutine(HealthEffect());
    }

    IEnumerator HealthEffect()
    {
        while(effect.fillAmount > health.fillAmount)
        {
            effect.fillAmount -= effectSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
