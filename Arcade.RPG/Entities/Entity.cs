namespace Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcade.RPG.Components;
using System.Collections.Generic;
using Arcade.RPG.Lib;
using System;

public class Entity : Identity {
    public Dictionary<EnumComponentType, IComponent> components = new();

    public Entity(Dictionary<EnumComponentType, IComponent>? components = null, Guid? id = null, Dictionary<string, string>? tags = null) : base(id, tags) {
        if(components != null) {
            foreach(var component in components) {
                this.AddComponent(component.Key, component.Value);
            }
        }
    }

    public void AddComponent<T>(EnumComponentType type, T component) where T : IComponent {
        this.components[type] = component;
    }

    public T? GetComponent<T>(EnumComponentType type) where T : class, IComponent {
        if(this.components.TryGetValue(type, out var component)) {
            return component as T;
        }
        return null;
    }

    public bool RemoveComponent(EnumComponentType type) {
        return this.components.Remove(type);
    }

    public void Update(RPG game, GameTime gameTime) {
        foreach(var component in this.components.Values) {
            component.Update(game, gameTime, this);
        }
    }

    public void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) {
        Graphics graphicsComponent = GetComponent<Graphics>(EnumComponentType.Graphics);

        graphicsComponent.Draw(game, graphicsDevice, gameTime, spriteBatch, this);
    }
}
