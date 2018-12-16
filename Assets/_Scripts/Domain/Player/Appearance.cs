using UnityEngine;
using System;

[Serializable]
public class Appearance {

    public enum Color {
        NONE,
        RED, 
        BLUE, 
        GREEN, 
        YELLOW, 
        PURPLE
    }

    public enum SkinColor {
        NONE,
        LIGHTEST,
        LIGHT,
        MEDIUM,
        DARK,
        DARKEST
    }

    public enum Sex {
        MALE,
        FEMALE
    }
    
    public Color color;
    public SkinColor skinColor;
    public Sex sex;
}