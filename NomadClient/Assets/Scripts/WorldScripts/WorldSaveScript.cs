using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WorldSaveScript : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text worldTitle;
    public TMP_Text lastPlayed;
    public WorldData worldData;

    public GameObject selectedImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        selectedImage.SetActive(true);
        GameManager.instance.SelectedWorld(worldData);
    }
}
