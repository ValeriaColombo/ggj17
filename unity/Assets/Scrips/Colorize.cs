using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colorize : MonoBehaviour
{
    public List<Image> images;

	public void ApplyColor(Color color)
    {
		foreach(var img in images)
        {
            img.color = color;
        }
	}
}
