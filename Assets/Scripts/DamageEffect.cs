using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{

    [SerializeField] Image damageEffect;
    public float effectDuration = .3f;
    void Start()
    {
        damageEffect.enabled = false;
    }

    public void ShowDamageEffect()
    {
        StartCoroutine(DisplayDamage());
    }

    IEnumerator DisplayDamage()
    {
        damageEffect.enabled = true;
        yield return new WaitForSeconds(effectDuration);
        damageEffect.enabled = false;
    }
}
