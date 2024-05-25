namespace Arcade.RPG.Configs;

using Microsoft.Xna.Framework;

public class ColorThreshold {
    public float Value { get; set; }
    public Color Color { get; set; }

    public ColorThreshold(float value, Color color) {
        Value = value;
        Color = color;
    }
}