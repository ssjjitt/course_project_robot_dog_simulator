using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class OpenBatterySlot : MonoBehaviour
{
    public float moveDistance = 0.097f; 
    public float moveSpeed = 0.5f;
    private int count = 0;

    private AudioSource audioSource;

    public string lightName = "eye1Light";
    public string lightName2 = "eye2Light";
    public float fadeDuration = 2f;
    public Color targetColor = new Color(0f, 1f, 1f);
    public Transform[] placementPositions; 

    private Light pointLight;
    private Light pointLight2;

    public Transform slot1;
    public Transform slot2;


    private void Start()
    {
        GameObject lightObj = GameObject.Find(lightName);
        GameObject lightObj2 = GameObject.Find(lightName2);
        pointLight = lightObj.GetComponent<Light>();
        pointLight2 = lightObj2.GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && count == 0)
        {
            MoveObject(moveDistance);
            count++;
        }
        if (slot1.childCount > 0 && slot2.childCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && count == 1)
            {
                MoveObject(-moveDistance);
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                StartCoroutine(FadeInLight(pointLight));
                StartCoroutine(FadeInLight(pointLight2));
                count++;
            }
        }
    }

    void MoveObject(float distance)
    {
        Vector3 targetPosition = transform.position + new Vector3(0, distance, 0);
        StartCoroutine(SmoothMove(transform, targetPosition));
    }

    IEnumerator SmoothMove(Transform objectTransform, Vector3 targetPosition)
    {
        float timeElapsed = 0f;

        Vector3 initialPosition = objectTransform.position;

        while (timeElapsed < moveSpeed)
        {
            objectTransform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / moveSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objectTransform.position = targetPosition;
    }

    private IEnumerator FadeInLight(Light point)
    {
        float timeElapsed = 0f;

        float initialIntensity = point.intensity;
        Color initialColor = point.color;

        point.intensity = 0f;
        point.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        point.enabled = true;

        while (timeElapsed < fadeDuration)
        {
            float t = timeElapsed / fadeDuration;

            point.intensity = Mathf.Lerp(0f, initialIntensity > 0 ? initialIntensity : 1f, t);

            point.color = Color.Lerp(new Color(targetColor.r, targetColor.g, targetColor.b, 0f), targetColor, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        point.intensity = initialIntensity > 0 ? initialIntensity : 1f;
        point.color = targetColor;
    }
}
