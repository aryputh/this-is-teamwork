using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{
    [SerializeField] private float constantSpeed = 2.0f;
    private float currentSpeed;
    private bool isSpeedChanging;
    private float speedChangeDuration = 2.0f;
    private float speedChangeCooldown = 2.0f;
    private float lastSpeedChangeTime;

    private void Start()
    {
        currentSpeed = constantSpeed;
        isSpeedChanging = false;
        lastSpeedChangeTime = Time.time;
    }

    private void Update()
    {
        if (!isSpeedChanging && Time.time >= lastSpeedChangeTime + speedChangeCooldown)
        {
            isSpeedChanging = true;
            StartCoroutine(LerpToSpeed(currentSpeed));
        }

        float deltaTimeAdjustedSpeed = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * deltaTimeAdjustedSpeed);
    }

    public void SetConstantSpeed(float speed)
    {
        constantSpeed = speed;
    }

    public void MoveToYLevel(float targetY, float catchupSpeed)
    {
        StartCoroutine(MoveToYLevelCoroutine(targetY, catchupSpeed));
    }

    private IEnumerator MoveToYLevelCoroutine(float targetY, float catchupSpeed)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        while (transform.position.y != targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, catchupSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator LerpToSpeed(float newSpeed)
    {
        float startSpeed = currentSpeed;
        float elapsedTime = 0.0f;

        while (elapsedTime < speedChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            currentSpeed = Mathf.Lerp(startSpeed, newSpeed, elapsedTime / speedChangeDuration);
            yield return null;
        }

        currentSpeed = newSpeed;
        isSpeedChanging = false;
        lastSpeedChangeTime = Time.time;
    }
}
