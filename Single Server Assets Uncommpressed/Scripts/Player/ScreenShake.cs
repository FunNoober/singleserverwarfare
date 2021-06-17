using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float dampingSpeed = 1.5f; //How much damping should be applied

    public Vector3 startPos;
    void Start()
    {
        startPos = transform.localPosition;
    }

    
    void Update()
    {
        
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-0.15f, 0.15f) * magnitude;
            float y = Random.Range(-0.15f, 0.15f) * magnitude;

            Vector3 chosenLocation = new Vector3(x, y, startPos.z);
            transform.localPosition = Vector3.Lerp(transform.localPosition, chosenLocation, Time.deltaTime);

            transform.localRotation *= Quaternion.Euler(x, y += 0.1f, transform.localRotation.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
    }
}
