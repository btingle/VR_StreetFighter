using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
	public Color fillColor;
	public Image fillImage;
	
	protected float fillValue;
	protected Slider slider;
	
    protected virtual void Awake()
	{
		slider = GetComponent<Slider>();
		fillImage.color = fillColor;
	}
	
	protected virtual void Update()
	{
		slider.value = fillValue;
	}
}
