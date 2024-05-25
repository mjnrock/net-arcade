namespace Arcade.RPG.Configs;

using System.Collections.Generic;

using Arcade.RPG.Components;
using Arcade.RPG.Configs.Models;

using Microsoft.Xna.Framework;

public class Config {
    public Settings Settings { get; set; }
    public Viewport Viewport { get; set; }
    public Dictionary<EnumResourceType, Resource> Resources { get; set; }

    public Config() {
        this.Settings = new Settings {
            ArcadeMode = false,
            Difficulty = 1.0f
        };

        this.Viewport = new Viewport {
            TileBaseHeight = 32,
            TileBaseWidth = 32,
            TileX = 0,
            TileY = 0,
            TileXRadius = 7,
            TileYRadius = 5,
            Zoom = new Zoom {
                Current = 1.0f,
                Step = 0.1f,
                Min = 0.1f,
                Max = 20.0f
            }
        };

        this.Resources = new Dictionary<EnumResourceType, Resource> {
            [EnumResourceType.Health] = new Resource {
                ShowBar = true,
                Thresholds = new List<ColorThreshold> {
                    new ColorThreshold(0.8f, Color.DarkGreen),
                    new ColorThreshold(0.65f, Color.Green),
                    new ColorThreshold(0.35f, Color.Gold),
                    new ColorThreshold(0.15f, Color.DarkOrange),
                    new ColorThreshold(0.0f, Color.Red)
                },
                OffsetX = 0,
                OffsetY = -5,
                Width = 24,
                Height = 6
            },
            [EnumResourceType.Mana] = new Resource {
                ShowBar = true,
                Thresholds = new List<ColorThreshold> {
                    new ColorThreshold(0.75f, new Color(51, 51, 102)),
                    new ColorThreshold(0.5f, new Color(85, 85, 136)),
                    new ColorThreshold(0.25f, new Color(153, 153, 204)),
                    new ColorThreshold(0.0f, new Color(204, 204, 255))
                },
                OffsetX = 0,
                OffsetY = -2,
                Width = 20,
                Height = 3
            }
        };
    }
}
