using Arcade.RPG.Entities;

public class Zoom {
    public float Current { get; set; } = 1.0f;
    public float Step { get; set; } = 0.1f;
    public float Min { get; set; } = 1.0f;
    public float Max { get; set; } = 10.0f;
}

public class Viewport {
    public Entity? Subject { get; set; } = null;
    public float TileBaseWidth { get; set; } = 32;
    public float TileBaseHeight { get; set; } = 32;
    public float TileX { get; set; } = 0;
    public float TileY { get; set; } = 0;
    public float TileXRadius { get; set; } = 15;
    public float TileYRadius { get; set; } = 10;
    public Zoom Zoom { get; set; } = new Zoom();
}