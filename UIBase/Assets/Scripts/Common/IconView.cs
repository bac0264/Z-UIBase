using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconView : MonoBehaviour
{
    [SerializeField] private Image icon;
    
    [SerializeField] private Text value;

    public void SetData(Resource resource)
    {
        icon.sprite = LoadResourceController.GetIconResource(resource.type, resource.id);
        value.text = resource.number.ToString();
    }
}
