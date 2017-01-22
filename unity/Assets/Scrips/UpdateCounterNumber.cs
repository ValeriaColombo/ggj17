using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCounterNumber : MonoBehaviour
{
    public Image image;
    public Sprite[] sprites;

    public void UpdateNumbers(float value)
    {
        int index = Mathf.FloorToInt(value * sprites.Length);
        image.sprite = sprites[index];
    }
}
