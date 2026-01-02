using UnityEngine;
using System.Collections;

public class DogMovementController : MonoBehaviour
{
    private GameObject dog;
    public float moveSpeed = 1;
    private Animator animator;
    public float animationSmoothTime = 0.1f;
    public float rotationSpeed = 10f;
    private Rigidbody rb;
    public AudioClip footstepClip;
    private bool isWalking = false;
    private Vector3 inputDirection = Vector3.zero; 
    private Vector3 buttonDirection = Vector3.zero;
    AudioSource[] sources;

    private void Start()
    {
        dog = FindAnyObjectByType<Dog>().gameObject;
        rb = dog.GetComponent<Rigidbody>();
        sources = dog.GetComponents<AudioSource>();
        rb.isKinematic = true;
        animator = dog.GetComponent<Animator>();
        Transform armatureTransform = dog.transform.Find("Armature");
        armatureTransform.rotation = Quaternion.Euler(0f, 0f, 90f);
        Collider dogCollider = dog.GetComponent<Collider>();
        if (dogCollider != null)
        {
            dogCollider.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    private void Update()
    {
        Vector3 keyboardDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.U)) keyboardDirection += dog.transform.forward;
        if (Input.GetKey(KeyCode.J)) keyboardDirection += -dog.transform.forward;
        if (Input.GetKey(KeyCode.K)) keyboardDirection += dog.transform.right; 
        if (Input.GetKey(KeyCode.H)) keyboardDirection += -dog.transform.right;

        inputDirection = keyboardDirection.normalized;

        Vector3 finalDirection = (inputDirection + buttonDirection).normalized;

        if (finalDirection.magnitude > 0.1f)
        {
            Move(finalDirection);
            if (!isWalking)
            {
                StartCoroutine(SmoothAnimationTransition(true));
                if (!sources[1].isPlaying && footstepClip != null)
                {
                    sources[1].clip = footstepClip;
                    sources[1].Play();
                }
            }
        }
        else
        {
            if (isWalking)
            {
                StartCoroutine(SmoothAnimationTransition(false));
                if (sources[1].isPlaying)
                {
                    sources[1].Stop();
                }
            }
        }
    }
    private void Move(Vector3 direction)
    {
        Vector3 newPos = dog.transform.position + direction * moveSpeed * Time.deltaTime;
        dog.transform.position = newPos;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        dog.transform.rotation = Quaternion.RotateTowards(dog.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, targetRotation, Time.deltaTime * 1f);
    }


    private IEnumerator SmoothAnimationTransition(bool walking)
    {
        float target = walking ? 1f : 0f;
        float current = animator.GetFloat("isWalking");

        float timeElapsed = 0f;
        while (timeElapsed < animationSmoothTime)
        {
            animator.SetFloat("isWalking", Mathf.Lerp(current, target, timeElapsed / animationSmoothTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        animator.SetFloat("isWalking", target);
        isWalking = walking;
    }

    public void ButtonMoveForward()
    {
        buttonDirection += dog.transform.forward;
    }

    public void ButtonMoveBackward()
    {
        buttonDirection += -dog.transform.forward;
    }

    public void ButtonMoveLeft()
    {
        buttonDirection += -dog.transform.right;
    }

    public void ButtonMoveRight()
    {
        buttonDirection += dog.transform.right;
    }

    public void ButtonStop()
    {
        buttonDirection = Vector3.zero;
        inputDirection = Vector3.zero;
    }
}
