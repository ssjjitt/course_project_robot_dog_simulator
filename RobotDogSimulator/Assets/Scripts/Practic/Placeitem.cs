using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceItem : MonoBehaviour
{
    public OpenBatterySlot script;
    public Transform handPosition;
    public Transform tablePosition;
    public float pickUpDistance = 4f;
    public float placeOnTableDistance = 4f;

    private Camera playerCamera;
    [HideInInspector] public GameObject currentItem;

    public GameObject[] allowedItems;

    private void Start()
    {
        playerCamera = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpDistance))
        {
            if (hit.collider != null && Input.GetMouseButtonDown(0))  
            {
                GameObject hitObject = hit.collider.gameObject;

                if (IsAllowedItem(hitObject) && currentItem == null)
                {
                    PickUpItem(hitObject);
                }
            }
        }

        if (currentItem != null)
        {
            currentItem.transform.position = handPosition.position;
            currentItem.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);

            if (Input.GetMouseButtonDown(1))
            {
                float distanceToTable = Vector3.Distance(playerCamera.transform.position, tablePosition.position);
                if (distanceToTable <= placeOnTableDistance)
                {
                    PlaceItemOnTable();
                }
            }

            if (Input.GetKeyDown(KeyCode.G)) 
            {
                ThrowItem();
            }
        }
    }

    bool IsAllowedItem(GameObject item)
    {
        foreach (GameObject allowedItem in allowedItems)
        {
            if (item == allowedItem)
            {
                return true;
            }
        }
        return false;
    }

    void PickUpItem(GameObject item)
    {
        currentItem = item;

        currentItem.GetComponent<Collider>().enabled = false;
        currentItem.transform.SetParent(handPosition);
        currentItem.transform.localPosition = Vector3.zero;

        currentItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void PlaceItemOnTable()
    {
        if (currentItem != null && tablePosition != null)
        {
            currentItem.transform.SetParent(null);
            currentItem.GetComponent<Collider>().enabled = true;

            currentItem.transform.position = tablePosition.position + new Vector3(0.5f, 0.5f, 0);
            currentItem.transform.rotation = Quaternion.Euler(90, 90, 0);

            currentItem.transform.localScale = Vector3.one;

            script.enabled = true;

            currentItem = null;
        }
    }

    void ThrowItem()
    {
        if (currentItem != null)
        {
            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb == null) 
            {
                rb = currentItem.AddComponent<Rigidbody>();
            }

            rb.linearVelocity = playerCamera.transform.forward * 5f;

            currentItem.GetComponent<Collider>().enabled = true;

            currentItem.transform.SetParent(null);

            currentItem.transform.localScale = Vector3.one;

            currentItem = null;
        }
    }

    internal void ClearCurrentItem()
    {
        currentItem = null;
    }
}
