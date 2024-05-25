namespace Arcade.RPG.Configs.Models;

using System.Collections.Generic;

public class Resource {
    public bool ShowBar { get; set; } = false;
    public List<ColorThreshold> Thresholds { get; set; }
    public int OffsetX { get; set; }
    public int OffsetY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}