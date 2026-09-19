# Systems

## Overview

Data-driven building blocks for resources, inventories, and conversions in Unity.

The Economy module provides a small set of interfaces for modelling resources and the rules that transform them. It is designed to keep game logic separate from data while allowing systems such as shops, crafting, cooking, inventories, relationship mechanics, and player progression to communicate through explicit contracts.

> **Status:** This module is an evolving part of the `dsabatier/systems` project. The included cooking sample is the best reference for the current API and intended usage.

## Features

- Define resources as data objects with stable `Guid` identifiers.
- Represent resource values independently from resource definitions.
- Describe conversions with data: inputs, output, and conversion identifier.
- Implement conversion rules behind `IResourceConverter`.
- Keep consuming systems decoupled from concrete resource and data-service implementations.
- Build Unity authoring workflows with `ScriptableObject` assets.

## Concepts

### Resources

`IResource` represents a resource definition and extends the project's `IData` contract. Examples include ingredients, meals, currencies, items, or relationship values.

### Resource values

`IResourceValue` represents a value held by a system, inventory, or UI. It points to the resource definition through `ResourceId`, allowing an inventory slot to remain lightweight while the data service supplies the full definition when needed.

### Conversions

`IResourceConversion` describes a transformation using resource IDs:

- `Inputs` — the resource IDs required by the conversion.
- `OutputId` — the resource produced by the conversion.

A conversion can represent a recipe, a crafting recipe, an exchange rate, a shop transaction, or any other operation that transforms resources.

### Converters

`IResourceConverter` executes a conversion:

```csharp
public interface IResourceConverter
{
    bool Convert(
        Guid conversionId,
        Dictionary<Guid, IResourceValue> inputs,
        out IResourceValue result);
}
```

The converter owns the rules for validating inputs and creating the result. This keeps the conversion mechanism independent from the UI, inventory, and data authoring layers.

## Example: cooking

The sample cooking system stores ingredients and meals as Unity assets and recipes as data. A kitchen collects selected ingredient values, passes them to an `IResourceConverter`, and receives a meal when the recipe matches.

A simplified caller looks like this:

```csharp
Dictionary<Guid, IResourceValue> ingredients = new()
{
    { firstIngredient.ResourceId, firstIngredient },
    { secondIngredient.ResourceId, secondIngredient },
    { thirdIngredient.ResourceId, thirdIngredient }
};

if (converter.Convert(recipeId, ingredients, out IResourceValue result))
{
    Meal meal = (Meal)result;
    Debug.Log($"Cooked {meal.Name}");
}
```

See [`Samples/Cooking/Kitchen/Kitchen.cs`](Samples/Cooking/Kitchen/Kitchen.cs) for the complete example, including data lookup, validation, and result creation.

## Project structure

```text
Economy/
├── Economy.cs                         # Core interfaces
├── Noodlepop.Economy.asmdef           # Unity assembly definition
└── Samples/
    ├── Cooking/                       # Recipe and resource conversion example
    └── UI/                            # Supporting sample UI components
```

## Getting started

1. Open the repository in Unity.
2. Ensure the project's data module is available; the Economy assembly references the shared `Noodlepop.Data` contracts.
3. Create resource and conversion assets using the sample asset types as a guide.
4. Implement `IResourceValue` for values stored by your inventory or gameplay system.
5. Implement `IResourceConverter` to validate inputs and produce the output value.
6. Connect your UI or gameplay component to the converter through the interfaces rather than depending on concrete sample classes.

For a working reference, start with the assets and scripts under [`Samples/Cooking`](Samples/Cooking).

## Extending the module

The interfaces are intentionally small so that the same pattern can support different mechanics:

- **Crafting:** consume materials and produce an item.
- **Cooking:** match ingredients against a recipe and create a meal.
- **Shops:** convert currency values into item values.
- **Resource processing:** transform raw resources into refined resources.
- **Progression:** convert experience or other values into unlocks.
- **Relationships:** use typed resource definitions and conversions to model changes between entities.

For more complex rules, keep validation and mutation in the converter or an application service. UI components should select and present values, not contain the rules for whether a conversion is valid.

## Design principles

- **Data and behaviour are separate:** definitions can be authored and changed without rewriting conversion code.
- **Explicit boundaries:** systems communicate through small adapter interfaces.
- **Stable identity:** `Guid` identifiers allow data references to remain independent from their unique context.
- **Composable systems:** inventories, converters, data services, and UI can be replaced independently.


## License

See the repository root for the project's license and contribution information.
