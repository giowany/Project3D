using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image image;
    public Sprite audioOn;
    public Sprite audioOff;

    public void ChangeImageBool(bool i)
    {
        if (i) image.sprite = audioOff;
        else image.sprite = audioOn;
    }
}
