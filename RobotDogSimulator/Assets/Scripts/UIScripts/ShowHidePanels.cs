using UnityEngine;
using UnityEngine.UI;

public class ToggleObjectActive : MonoBehaviour
{
    public void ToggleObject(GameObject objectToActivate)
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(!objectToActivate.activeSelf);
        }
    }
}
