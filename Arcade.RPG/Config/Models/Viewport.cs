public class Viewport {
    public int TileBaseWidth{ get; set; } = 32;
    public int TileBaseHeight { get; set; } = 32;
    public int TileX { get; set; } = 0;
    public int TileY { get; set; } = 0;
    public int TileXRadius { get; set; } = 7;
    public int TileYRadius { get; set; } = 5;
    public float Zoom { get; set; } = 1.0f;
}