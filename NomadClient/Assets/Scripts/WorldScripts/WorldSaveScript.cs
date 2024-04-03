using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WorldSaveScript : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text worldTitle;
    public TMP_Text lastPlayed;

    public GameObject selectedImage;

    public static event GameManager.WorldSelectAction OnSelect;

    public void OnPointerClick(PointerEventData eventData)
    {
        selectedImage.SetActive(true);
        OnSelect(worldTitle.text);
    }
}
