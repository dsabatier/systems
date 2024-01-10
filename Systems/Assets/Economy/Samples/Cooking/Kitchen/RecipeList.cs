using Noodlepop.Economy.Samples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecipeList : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Guid> OnRecipeSelected;

    [SerializeField]
    List<RecipeAsset> _recipes = new List<RecipeAsset>();

    [SerializeField]
    private SimpleButton _buttonPrefab;


    void Start()
    {
        foreach(var recipe in _recipes) {
            SimpleButton button = Instantiate(_buttonPrefab, transform);
            button.SetLabel(recipe.name);
            button.AddListener(() =>
            {
                OnRecipeSelected?.Invoke(recipe.Id);
            });
        }
    }
}
