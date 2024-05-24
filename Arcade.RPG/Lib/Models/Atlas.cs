namespace Arcade.RPG.Lib.Models;

using System;
using System.Collections.Generic;

using Arcade.RPG.Lib.Utility;

using Microsoft.Xna.Framework;

using Newtonsoft.Json;

public class MapData {
    public double Sw { get; set; }
    public double Sh { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool AutoSize { get; set; }
    public int OffsetX { get; set; }
    public int OffsetY { get; set; }
    public Dictionary<string, object> Algorithms { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int Tw { get; set; }
    public int Th { get; set; }
    public List<List<Tile>> Tiles { get; set; }
}

public class Tile {
    public int X { get; set; }
    public int Y { get; set; }
    public string Data { get; set; }
}

public class Terrain {
    public string Type { get; set; }
    public int? Cost { get; set; }
    public int Mask { get; set; }
    [JsonConverter(typeof(HexColorConverter))]
    public Color Texture { get; set; }
}

public class TerrainWrapper {
    public string Selected { get; set; }
    public Dictionary<string, Terrain> Terrains { get; set; }
}

public class Atlas {
    public MapData Map { get; set; }
    public TerrainWrapper Terrain { get; set; }

    public Tile getTile(int x, int y) {
        return this.Map.Tiles[y][x];
    }

    public void ForEach(Func<Tile, Terrain, bool> callback) {
        for(var i = 0; i < this.Map.Tiles.Count; i++) {
            for(var j = 0; j < this.Map.Tiles[i].Count; j++) {
                Tile tile = this.getTile(j, i);
                Terrain terrain = this.Terrain.Terrains.TryGetValue(tile.Data, out Terrain t) ? t : null;

                callback(tile, terrain);
            }
        }
    }
}