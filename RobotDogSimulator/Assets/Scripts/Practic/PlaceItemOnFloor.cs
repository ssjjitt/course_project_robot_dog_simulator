using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlaceItemOnFloor : MonoBehaviour
{
    public Transform handPosition; 
    public GameObject objectToDrop; 

    public GameObject itemPrefab; 

    private MainInput _input;

    public static event Action<GameObject> OnDogSpawned;

    void Start()
    {
        _input = new MainInput();
    }

    private void OnEnable()
    {
        _input?.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    void Update()
    {
        if (handPosition.childCount > 0) 
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (objectToDrop == null)
                {
                    Debug.LogWarning("Объект не найден!");
                    return;
                }

                Ray ray = Camera.main.ScreenPointToRay(_input.Mouse.Position.ReadValue<Vector2>());
                if (Physics.Raycast(ray, out var hit))
                {
                    Destroy(objectToDrop);

                    GameObject newObject = Instantiate(itemPrefab, hit.point, Quaternion.identity);
                    OnDogSpawned?.Invoke(newObject);

                    newObject.transform.localScale = new Vector3(1f, 1f, 1f); 
                    newObject.transform.rotation = Quaternion.identity; 

                    Debug.Log("Объект установлен на пол в позицию " + hit.point);
                }
                else
                {
                    Debug.LogWarning("Луч в мир не попал, объект не поставлен.");
                }
            }
        }
    }
}
