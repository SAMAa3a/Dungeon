using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
            //StartCoroutine(FindObjectOfType<CameraController>().CameraShake(0.4f, 0.4f));
        }
    }

    private void Attack()
    {
        //MousePos(TargetPos) - PlayerCurrentPos
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Atan2 -> Arctan
        //this formula is to calculate the angle of the difference's direction and the horizontal line
        //it return the radius, should turn to degree
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //2D game rotate around the z axis
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
