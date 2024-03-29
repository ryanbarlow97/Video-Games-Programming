using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SaveSlotManager : MonoBehaviour
{
    public UserDetailsInput nameInputManager;
    public GameObject saveSlotCanvas;
    private TransitionEffect transitionEffect;

    public Button saveSlot1;
    public Button saveSlot2;
    public Button saveSlot3;

    private void Start()
    {
        transitionEffect  = FindObjectOfType<TransitionEffect>();

        saveSlot1.onClick.AddListener(() => LoadOrCreateSave(1));
        saveSlot2.onClick.AddListener(() => LoadOrCreateSave(2));
        saveSlot3.onClick.AddListener(() => LoadOrCreateSave(3));

        UpdateSaveSlotButtons();
    }

    private void LoadOrCreateSave(int slotNumber)
    {
        if (SaveManager.SaveExists(slotNumber))
        {
            // Load the main game scene.
            // Pass the save slot number to the MainGameLoader script in the next scene
            transitionEffect.LoadScene("MainGame");
            SceneManager.sceneLoaded += (loadedScene, mode) =>
            {
                MainGameLoader mainGameLoader = FindObjectOfType<MainGameLoader>();
                if (mainGameLoader != null)
                {
                    mainGameLoader.saveSlotNumber = slotNumber;
                }
            };
        }
        else
        {
            // Show the name input panel and disable the save slot canvas
            nameInputManager.gameObject.SetActive(true);
            nameInputManager.ShowNameInput(this, slotNumber);

            saveSlotCanvas.SetActive(false);

        }
    }

    public void UpdateSaveSlotButtons()
    {
        UpdateSaveSlotButton(1, saveSlot1);
        UpdateSaveSlotButton(2, saveSlot2);
        UpdateSaveSlotButton(3, saveSlot3);
    }

    private void UpdateSaveSlotButton(int slotNumber, Button slotButton)
    {
        if (SaveManager.SaveExists(slotNumber))
        {
            slotButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Save Slot {slotNumber}: {SaveManager.Load(slotNumber).playerName} \n \n Hearts: {SaveManager.Load(slotNumber).playerHearts} \n Score: {SaveManager.Load(slotNumber).playerScore} \n Meteors Destoyed: {SaveManager.Load(slotNumber).meteorsDestroyed}";
        }
        else
        {
            slotButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Create Save Slot {slotNumber}";
        }
    }
}
