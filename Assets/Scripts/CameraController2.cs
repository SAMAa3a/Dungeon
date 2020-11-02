using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    //used in PlayerAttack
    /*
    public IEnumerator CameraShake(float _maxTime,float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        float shakeTime = 0.0f;

        while(shakeTime < _maxTime)
        {
            float x = Random.Range(-1f, 1f) * _amount;
            float y = Random.Range(-1f, 1f) * _amount;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            shakeTime += Time.deltaTime;

            yield return new WaitForSeconds(0f);
        }
    }
    */

    private Transform target;

    [SerializeField] private float smoothSpeed;

    [SerializeField] private float minX, maxX, minY, maxY;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        //Method 1 tradition method of move camera
        //transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);  //transform.position : camera current position

        //smoothly move camera
        //first parameter is the current position
        //second parameter is the target position
        //third parameter is speed
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

        //limit the range
        //Mathf.Clamp(testValue, 0, 10);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                         Mathf.Clamp(transform.position.y, minY, maxY),
                                         transform.position.z);
    }
}
