using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WithItem : MonoBehaviour
{
    public Animator anim;
    public Transform slashTran;

    private float slashScale;

    public Text NumCB;
    public Text NumCR;
    public Text NumMS;
    public Text NumSSc;
    public Text NumSSp;
    public Text NumS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            if(other.gameObject.name == "MoveSpeed(Clone)")
            {
                PlayerMovement.instance.moveSpeed *= 1.2f;

                NumMS.text = PlayerMovement.instance.moveSpeed.ToString();

                Destroy(other.gameObject);
            }
            else if(other.gameObject.name == "SlashSpeed(Clone)")
            {
                PlayerMovement.instance.slashSpeed *= 1.2f;
                anim.speed = PlayerMovement.instance.slashSpeed;

                NumSSp.text = PlayerMovement.instance.slashSpeed.ToString();

                Destroy(other.gameObject);
            }
            else if(other.gameObject.name == "SlashScale(Clone)")
            {
                PlayerMovement.instance.slashScale *= 1.1f;
                slashScale = PlayerMovement.instance.slashScale;
                slashTran.localScale = new Vector3(slashScale, slashScale, 1);

                NumSSc.text = PlayerMovement.instance.slashScale.ToString();

                Destroy(other.gameObject);
            }
            else if(other.gameObject.name == "CriticRate(Clone)")
            {
                PlayerMovement.instance.criticRate += 0.05f;

                NumCR.text = PlayerMovement.instance.criticRate.ToString();

                Destroy(other.gameObject);
            }
            else if(other.gameObject.name == "CriticBonus(Clone)")
            {
                PlayerMovement.instance.cirticBonus += 0.1f;

                NumCB.text = PlayerMovement.instance.cirticBonus.ToString();

                Destroy(other.gameObject);
            }
            else if(other.gameObject.name == "Strength(Clone)")
            {
                PlayerMovement.instance.strength += 5.0f;

                NumS.text = PlayerMovement.instance.strength.ToString();

                Destroy(other.gameObject);
            }
        }
    }
}
