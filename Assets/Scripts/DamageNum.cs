using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNum : MonoBehaviour
{
    public Text damageText;
    public float lifeTimer;//text will destroy when time at lifeTimer 
    public float upSpeed;

    private void Start()
    {
        Destroy(gameObject, lifeTimer);
    }

    private void Update()
    {
        transform.position += new Vector3(0, upSpeed * Time.deltaTime, 0);//Text move up in lifeTimer
    }

    public void ShowUIDamage(float _amount)
    {
        damageText.text = _amount.ToString();
    }
}
