using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FramesPerSeconds : MonoBehaviour
{
    [SerializeField]
    private TMP_Text FPSText = null; // The text that will display the FPS

    [SerializeField]
    private TMP_Dropdown dropdown; // The dropdown that will display the FPS

    [SerializeField]
    private GameObject[] FPSObject;  // The object that will display the FPS

    [SerializeField]
    private TextMeshProUGUI ShowFPSText; // The text that will display the FPS status
    private bool showFPS = false; // The FPS status

    [SerializeField]
    private int indexNumber; // The index number of the dropdown

    [SerializeField]
    float deltaTime = 0.0f; // The time between frames

    [SerializeField]
    private List<int> targetFramesPerSecond = new List<int>() { 0, 30, 60, 120 }; // The target FPS list

    //============================================================================================//

    private void Awake()
    {
        int Playerfps = PlayerPrefs.GetInt("FPS", 0); // Get the FPS from the playerprefs
        dropdown.value = Playerfps; // Set the dropdown value to the playerprefs
    }

    private void Start()
    {
        FPSText = GetComponent<TMP_Text>(); // Get the text component
        ShowFPSText.text = (showFPS) ? "ON" : "OFF"; // Set the text to ON or OFF on start
    }

    public void ShowFPS()
    {
        showFPS = !showFPS;
        ShowFPSText.text = (showFPS) ? "ON" : "OFF"; // Set the text to ON or OFF on change
        // Set the FPS object to active or not
        if (FPSObject != null)
        {
            foreach (GameObject g in FPSObject)
            {
                g.SetActive(showFPS);
            }
        }
    }

    //============================================================================================//
    void FPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; // Calculate the time between frames
        float msec = deltaTime * 1000.0f; // Calculate the frame time
        float fps = 1.0f / deltaTime; // Calculate the FPS
        string text = string.Format("{0:0.0} ms ({1:0.} FPS)", msec, fps); // Format the text
        FPSText.text = text.ToUpper(); // Set the text to uppercase
    }

    public void SetFPS() // exposed function to be called from the drop down
    {
        //============================================================================================//
        indexNumber = dropdown.value; // Get the index number of the dropdown
        switch (indexNumber) // Switch the index number
        {
            case 0: // If the index number is 0
                Application.targetFrameRate = targetFramesPerSecond[0]; // Set the target FPS to 0
                PlayerPrefs.SetInt("FPS", indexNumber);
                Debug.Log("FPS: " + targetFramesPerSecond[0]);
                break;
            case 1: // If the index number is 1
                Application.targetFrameRate = targetFramesPerSecond[1]; // Set the target FPS to 30
                PlayerPrefs.SetInt("FPS", indexNumber);
                Debug.Log("FPS: " + targetFramesPerSecond[1]);
                break;
            case 2: // If the index number is 2
                Application.targetFrameRate = targetFramesPerSecond[2]; // Set the target FPS to 60
                PlayerPrefs.SetInt("FPS", indexNumber);
                Debug.Log("FPS: " + targetFramesPerSecond[2]);
                break;
            case 3: // If the index number is 3
                Application.targetFrameRate = targetFramesPerSecond[3]; // Set the target FPS to 120
                PlayerPrefs.SetInt("FPS", indexNumber);
                Debug.Log("FPS: " + targetFramesPerSecond[3]);
                break;
        }
        PlayerPrefs.Save(); // Save the player prefs
    }

    //============================================================================================//
    void Update()
    {
        if (FPSText == null) // If the text is null
            return;

        FPS(); // Call the FPS function
    }

    //============================================================================================//
    void PopulateList()
    {
        //dropdown.ClearOptions(); //Clear pre-added options from the list, remove this line if you want to keep them.
        List<string> newOptions = new List<string>(); // Create a new list of strings
        for (int i = 0; i < targetFramesPerSecond.Count; i++) // Loop through the target FPS list
        {
            newOptions.Add(targetFramesPerSecond[i].ToString()); // Add the target FPS to the list
        }
        dropdown.AddOptions(newOptions); // Add the options to the dropdown
    }
}
