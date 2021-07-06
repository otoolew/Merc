using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

[System.Serializable]
public class Ingredient
{
    public string name;
    public int amount = 1;
    public IngredientUnit unit;
}
