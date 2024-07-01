namespace Arcade.RPG.Components;

using System;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Component : IComponent {
    public string id { get; set; }
    public EnumComponentType type { get; set; }

    protected Component(string? id, EnumComponentType type) {
        if(!Enum.IsDefined(typeof(EnumComponentType), type)) {
            throw new ArgumentException("Invalid component type");
        }

        this.id = id ?? Guid.NewGuid().ToString();
        this.type = type;
    }

    protected Component(EnumComponentType type) : this(null, type) { }

    public virtual void Update(RPG game, GameTime gameTime, Entity entity) { }
    public virtual void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) { }
}