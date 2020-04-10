using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEffector : MonoBehaviour
{
    float cameraOriginalSize;
    float range;
    float count;

    public void ShakeScreen(float duration, float magnitude)
    {
        StartCoroutine(ShakeScreenRoutine(duration, magnitude));
    }

    public IEnumerator ShakeScreenRoutine(float duration, float magnitude)
    {
        Vector3 originalPos = DGameSystem.camera.transform.localPosition;

        DGameSystem.camera.GetComponent<DCamera>().enabled = false;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            DGameSystem.camera.transform.localPosition = new Vector3(originalPos.x +x, originalPos.y +y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        DGameSystem.camera.transform.localPosition = originalPos;
        DGameSystem.camera.GetComponent<DCamera>().enabled = true;
    }
}
