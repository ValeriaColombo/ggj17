using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAlpha : MonoBehaviour
{
    public float minAlpha = 0.3f;

	void OnEnable()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var color = renderer.color;
        color.a = Random.Range(minAlpha, 1f);
        renderer.color = color;
	}
}
