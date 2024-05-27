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
            TileXRadius = 20,
            TileYRadius = 15,
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

    public bool SyncWithSubject() {
        if(this.Viewport.Subject == null) {
            return false;
        }

        Components.Physics physics = this.Viewport.Subject.GetComponent<Components.Physics>(EnumComponentType.Physics);

        this.Viewport.TileX = physics.Position.X;
        this.Viewport.TileY = physics.Position.Y;

        return true;
    }
    public bool IsWithinViewport(Vector2 position, float buffer = 0.0f) {
        float tileX = this.Viewport.TileX;
        float tileY = this.Viewport.TileY;
        float tileXRadius = this.Viewport.TileXRadius;
        float tileYRadius = this.Viewport.TileYRadius;

        float leftBound = tileX - tileXRadius;
        float rightBound = tileX + tileXRadius;
        float topBound = tileY - tileYRadius;
        float bottomBound = tileY + tileYRadius;

        return position.X >= leftBound - buffer && position.X <= rightBound + buffer && position.Y >= topBound - buffer && position.Y <= bottomBound + buffer;
    }
}
