using UnityEngine;

public class CheckController : MonoBehaviour
{
    public GameObject panelToActivate; 

    void Start()
    {
        CheckForJoystick();
    }

    void Update()
    {
        CheckForJoystick();
    }

    void CheckForJoystick()
    {
        Transform joystick = transform.Find("joystick");
        if (joystick != null)
        {
        joystick.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (panelToActivate != null)
            {
                panelToActivate.SetActive(true);
            }
        }
        else 
        {
            if (panelToActivate != null)
            {
                panelToActivate.SetActive(false); 
            }
        }
    }
}
