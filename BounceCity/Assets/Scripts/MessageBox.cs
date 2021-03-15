using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private static MessageBox instance = null; // The static instance of the singleton

    public float duration; // How many seconds the message will persist for

    Text message;
    Image image;

    // Returns an instance of the singleton
    public static MessageBox Get() 
    {
        if (instance == null)
        {
            instance = (MessageBox)FindObjectOfType(typeof(MessageBox));
        }
        return instance;
    }

    // On Start set the image, message and hide after a set interval
    void Start() 
    {
        image = GetComponent<Image>();

        GameObject textObj = GameObject.Find("MessageText");
        message = textObj.GetComponent<Text>();

        Invoke("HideMessage", duration);
    }

    // Hide the message by making the image transparent and the text to ""
    public void HideMessage() 
    {
        message.text = "";
        image.color = new Color(image.color.r,
                                image.color.g,
                                image.color.b, 0);
    }

    // Set the text to victory message and make the box opaque
    public void Victory() 
    {
        message.text = "VICTORY!";
        image.color = new Color(image.color.r,
                        image.color.g,
                        image.color.b, 100);
    }
}
