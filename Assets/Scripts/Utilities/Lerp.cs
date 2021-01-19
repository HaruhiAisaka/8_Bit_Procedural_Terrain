using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp 
{
    public bool transformLerpRunning = false;
    public IEnumerator TransformOverTime(Transform transform, Vector3 target, float duration)
    {
        transformLerpRunning = true;
        Vector3 initialCameraPosition = transform.position;
        float t = 0;
        while (Vector3.Distance(transform.position, target) > 0)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initialCameraPosition, target, t / duration);
            yield return null;
        }
        transformLerpRunning = false;
        yield return null;
    }
}
