using UnityEngine;

public class TakeBattary : MonoBehaviour
{
    public Transform handPosition; 
    public Transform[] placementPositions; 
    public GameObject batteryPrefab; 

    private int currentSlotIndex = 0; 

    void Update()
    {
        if (handPosition.childCount > 0 && currentSlotIndex < placementPositions.Length)
        {
            GameObject heldObject = handPosition.GetChild(0).gameObject;

            if (heldObject.name.Contains("battaries_obj"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    PlaceBatteryInPosition(heldObject);
                }
            }
        }
    }

    void PlaceBatteryInPosition(GameObject objectInHand)
    {
        if (currentSlotIndex >= placementPositions.Length)
        {
            Debug.LogWarning("Нет доступных слотов для батарейки!");
            return;
        }

        Destroy(objectInHand);

        GameObject newBattery = Instantiate(batteryPrefab, placementPositions[currentSlotIndex]);

        Vector3 localPos;
        if (currentSlotIndex == 1) 
        {
            placementPositions[1].localPosition = new Vector3(0.000001f, -0.000464f, 0.00059f);
            localPos = new Vector3(-0.00044f, 0.000003f, -0.00001f);
        }
        else
        {
            localPos = new Vector3(-0.0000133485f, -0.0007374077f, 0.0006633371f);
        }

        newBattery.transform.localPosition = localPos;
        newBattery.transform.localRotation = Quaternion.Euler(-90, 0, -91.026f);
        newBattery.transform.localScale = new Vector3(2f, 1f, 2f);

        currentSlotIndex++;
    }
}
