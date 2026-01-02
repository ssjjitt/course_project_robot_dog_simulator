using UnityEngine;
using System.Collections;

public class DogDancing : MonoBehaviour
{
    public GameObject dogPrefab;
    private Animator animator;
    public float animationSmoothTime = 0.1f;
    private bool isDancing = false;
    public AudioClip ladyGagaSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (ladyGagaSound == null)
        {
            Debug.Log("Выберите музыку");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("танцуем");
            if (dogPrefab != null)
            {
                animator = dogPrefab.GetComponent<Animator>();

                if (audioSource != null && ladyGagaSound != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                    StartCoroutine(SmoothAnimationTransition(true));
                }
            }
            else
            {
                Debug.LogError("Собака из инспектора не назначена!");
            }
        }

        // Допустим, остановить можно по отпусканию пробела, ебаный вариант
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (audioSource != null && ladyGagaSound != null && audioSource.isPlaying)
            {
                audioSource.Stop();
                StartCoroutine(SmoothAnimationTransition(false));
            }
        }
    }

    private IEnumerator SmoothAnimationTransition(bool dancing)
    {
        float target = dancing ? 1f : 0f;
        float current = animator.GetFloat("isDancing");

        float timeElapsed = 0f;
        while (timeElapsed < animationSmoothTime)
        {
            animator.SetFloat("isDancing", Mathf.Lerp(current, target, timeElapsed / animationSmoothTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        animator.SetFloat("isDancing", target);
        isDancing = dancing;
    }
}
