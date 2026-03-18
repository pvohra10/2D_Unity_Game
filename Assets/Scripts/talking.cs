using UnityEngine;
using TMPro;
using static Utilities.tools;

public class Talking : MonoBehaviour
{
    public string[] message;
    public TextMeshPro textDisplay;
    public bool interactable = false;


    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Set the starting text once when the game begins
        if (message.Length > 0)
        {
            textDisplay.text = message[0];
        }
    }

    private void onEnable(){
        // Reset the text to the initial message when the object is enabled
        if (message.Length > 0)
        {
            textDisplay.text = message[0];
        }
    }

    void Update()
    {
        // Only check for input if we are allowed to interact
        if (!interactable) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            textDisplay.text = message[1];
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            textDisplay.text = message[2];
        }
    }



}