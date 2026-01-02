using UnityEngine;
using UnityEngine.UI;

public class ActivateButton : MonoBehaviour
{
    public void ActivateOtherButton(Button buttonToActivate)
    {
        if (buttonToActivate != null)
        {
            buttonToActivate.interactable = true;
        }
        else
        {
            Debug.LogWarning("Не назначена кнопка для активации!");
        }
    }
}
