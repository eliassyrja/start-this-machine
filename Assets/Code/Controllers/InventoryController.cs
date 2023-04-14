using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public GameObject inventoryPanel;
	public GameObject slotPrefab;
	public float gap = 0.5f;

	public List<ItemSlot> itemSlots = new List<ItemSlot>();
	
	public void AddItem(InventoryItem item)
	{
		// Instantiate a new slot and set its parent to the inventory panel
		GameObject newSlot = Instantiate(slotPrefab, inventoryPanel.transform);
		
		// Set the item's icon in the slot
		Image icon = newSlot.GetComponentInChildren<Image>();
		icon.sprite = item.icon;

		// Add the new slot and the item to the list
		itemSlots.Add(new ItemSlot { Item = item, Slot = newSlot });

		// Calculate the total width of the slots and the offset for the first slot
		float totalWidth = (itemSlots.Count - 1) * slotPrefab.GetComponent<RectTransform>().rect.width * gap;
		float startX = -totalWidth / 2;

		// Position each slot relative to the center of the inventory panel
		for (int i = 0; i < itemSlots.Count; i++)
		{
			float xPos = startX + i * slotPrefab.GetComponent<RectTransform>().rect.width * gap;
			itemSlots[i].Slot.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, 0);
		}
		newSlot.GetComponent<HoldButton>().triggerKey = itemSlots.Count.ToString();
	}
}


[System.Serializable]
public class ItemSlot
{
	public InventoryItem Item;
	public GameObject Slot;
}
