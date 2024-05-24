using System;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcade.RPG.Components;

public abstract class Component : IComponent {
    public string Id { get; set; }
    public EnumComponentType Type { get; set; }

    protected Component(string? id, EnumComponentType type) {
        if(!Enum.IsDefined(typeof(EnumComponentType), type)) {
            throw new ArgumentException("Invalid component type");
        }

        Id = id ?? Guid.NewGuid().ToString();
        Type = type;
    }

    protected Component(EnumComponentType type) : this(null, type) { }

    public virtual void Update(GameTime gameTime, Entity entity) { }
    public virtual void Draw(SpriteBatch spriteBatch, Entity entity) { }
}
