using SickscoreGames.HUDNavigationSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtManager : MonoBehaviour
{
    LookAtCamera[] filler;
    CanvasLook[] canvas;
    HUDNavigationElement[] hud;
    int currentHudIndex = 0;
    SphereCollider[] sphereColliders;
    AiMovement[] aiMovements;

    private void Awake()
    {
        filler = GetComponentsInChildren<LookAtCamera>(true);
        canvas = GetComponentsInChildren<CanvasLook>(true);
        hud = GetComponentsInChildren<HUDNavigationElement>(true);
        aiMovements = GetComponentsInChildren<AiMovement>(true);
    }

    private void Start()
    {
        // Hide all arrows initially
        HideAllArrows();

        // Get the current HUD index from PlayerPrefs
        if (PlayerPrefs.HasKey("CurrentHudIndex"))
        {
            currentHudIndex = PlayerPrefs.GetInt("CurrentHudIndex");
        }

        // Show the arrows for the first set of active objects
        ShowNextArrows();
    }

    public void GetColliders(Vector3 SizeVariable)
    {
        sphereColliders = ReferenceManager.instance.characterController.openWorldCharacters.transform.GetComponentsInChildren<SphereCollider>();

        for (int i = 0; i < sphereColliders.Length; i++)
        {
            sphereColliders[i].radius = SizeVariable.x;
        }
    }

    void Update()
    {
        for (int i = 0; i < filler.Length; i++)
        {
            if (filler[i] != null && filler[i].gameObject.activeInHierarchy)
            {
                filler[i].transform.LookAt(Camera.main.transform);
            }
        }

        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i] != null && canvas[i].gameObject.activeInHierarchy)
            {
                canvas[i].transform.LookAt(Camera.main.transform);
            }
        }

        for (int i = 0; i < aiMovements.Length; i++)
        {
            if (aiMovements[i] != null && aiMovements[i].gameObject.activeInHierarchy)
            {
                aiMovements[i].CharacterMove();
            }
        }
    }

    // Call this method when an object is turned off
    public void ObjectTurnedOff()
    {
        // Hide the arrow corresponding to the turned off object
        HideArrow(currentHudIndex);

        // Increment the current arrow index
        currentHudIndex++;

        // Save the current index to PlayerPrefs
        PlayerPrefs.SetInt("CurrentHudIndex", currentHudIndex);

        // Show the arrows for the next set of active objects
        ShowNextArrows();
    }

    void ShowNextArrows()
    {
        // Show the next five available arrows
        for (int i = currentHudIndex; i < currentHudIndex + 1 && i < hud.Length; i++)
        {
            ShowArrow(i);
        }
    }

    void HideArrow(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < hud.Length)
        {
            hud[index].gameObject.SetActive(false);
        }
    }

    void ShowArrow(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < hud.Length)
        {
            hud[index].gameObject.SetActive(true);
        }
    }

    void HideAllArrows()
    {
        // Hide all arrows
        foreach (HUDNavigationElement arrow in hud)
        {
            arrow.gameObject.SetActive(false);
        }
    }
}
