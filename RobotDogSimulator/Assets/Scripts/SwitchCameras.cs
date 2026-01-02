using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    public Transform target;
    public Camera Camera_People;
    public Camera Camera_Orbit;
    public float distance = 2.2f;
    public float speed = 1.0f;
    public float rotationSpeed = 2.0f; 

    private float currentAngleX = 0.4f;
    private float currentAngleY = 0.2f; 
    private bool isOrbitActive = false;

    public void Start()
    {
        Camera_People.gameObject.SetActive(true);
        Camera_Orbit.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            SwitchToOrbitCamera();
            isOrbitActive = true;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            SwitchToPeopleCamera();
            isOrbitActive = false;
        }

        

        if (isOrbitActive)
        {
            if (Input.GetMouseButton(1)) 
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                currentAngleX += mouseX * rotationSpeed;
                currentAngleY -= mouseY * rotationSpeed;

                currentAngleY = Mathf.Clamp(currentAngleY, -50f, 50f);

                Vector3 offset = new Vector3(Mathf.Sin(currentAngleX) * Mathf.Cos(currentAngleY), Mathf.Sin(currentAngleY), Mathf.Cos(currentAngleX) * Mathf.Cos(currentAngleY)) * distance;
                transform.position = target.position + offset;

                transform.LookAt(target);
            }
        }
    }

    public void SwitchToPeopleCamera()
    {
        Camera_People.gameObject.SetActive(true);
        Camera_Orbit.gameObject.SetActive(false);
    }

    public void SwitchToOrbitCamera()
    {
        Camera_People.gameObject.SetActive(false);
        Camera_Orbit.gameObject.SetActive(true);
    }
}
