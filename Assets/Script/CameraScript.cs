using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public static Transform target;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    private float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    private float decreaseFactor = 1.0f;

    Vector3 originalPos;

    public void Shake(float duration)
    {
        if (target == null)
            return;

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        if(shakeDuration < 2f)
            shakeDuration += duration;
    }


    // Update is called once per frame
    void Update ()
    {
        if (target == null)
            return;


        if (shakeDuration > 0)
        {
            Vector3 pos = new Vector3(target.position.x, target.position.y, transform.position.z);
            camTransform.localPosition = pos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            transform.localPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }
}
