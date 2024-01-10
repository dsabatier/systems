using Noodlepop.Economy.Samples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddIngredientButton : MonoBehaviour
{
    [SerializeField]
    private Kitchen _kitchen;

    [SerializeField]
    private IngredientAsset _ingredientAsset;

    private void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = "Add " + _ingredientAsset.name;
    }

    public void HandleClick(int i)
    {
        if(i == 0)
            _kitchen.SetSlotA(_ingredientAsset.Create());
        else if(i == 1)
            _kitchen.SetSlotB(_ingredientAsset.Create());
        else if(i == 2)
            _kitchen.SetSlotC(_ingredientAsset.Create());
    }
}
