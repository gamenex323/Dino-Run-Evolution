using GalarySystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager instance;
    [FoldoutGroup("Level Progression")]
    public Levelprogression levelprogression;
    [FoldoutGroup("CharacterController")]
    public THEBADDEST.CharacterController3.CharacterController characterController;
    [FoldoutGroup("UiManager")]
    public UIManager uIManager;
    [FoldoutGroup("TutorialManager")]
    public TutorialManager tutorialManager;
    [FoldoutGroup("GalleryManager")]
    public GalaryManager galaryManager;
    [FoldoutGroup("LevelManager")]
    public LevelManager levelManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
