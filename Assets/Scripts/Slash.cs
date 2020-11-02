using UnityEngine;
using UnityEngine.UI;

public class Slash : MonoBehaviour
{
    [SerializeField] private float minDamage, maxDamage;
    private float attackDamage;

    public GameObject damageCanvas;

    private float scoreF = 0f;
    public Text score;

    public void EndAttack()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("We hit the enemy");
            attackDamage = Random.Range(minDamage, maxDamage);

            scoreF += Mathf.Floor(attackDamage);
            score.text = scoreF.ToString();

            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(!enemy/*!other.gameObject.GetComponent<Enemy>()*/.isAttacked)
            {
                enemy/*other.gameObject.GetComponent<Enemy>()*/.TakenDamage(attackDamage);

                DamageNum damagable = Instantiate(damageCanvas, other.transform.position, Quaternion.identity).GetComponent<DamageNum>();
                damagable.ShowUIDamage(Mathf.RoundToInt(attackDamage));
                //can also Mathf.CeliToInt 

                //the repel effect
                //move in the direction of (PlayerPos -> EnemyPos)(EnemyPos - PlayerPos)
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x / 2,
                                                       other.transform.position.y + difference.y / 2);
            }

        }
    }

    
}
