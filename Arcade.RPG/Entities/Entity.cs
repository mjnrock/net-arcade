namespace Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcade.RPG.Components;
using System.Collections.Generic;
using Arcade.RPG.Lib;

public class Entity : Identity {
    private readonly Dictionary<EnumComponentType, IComponent> _components = new();

    public Entity(Dictionary<EnumComponentType, IComponent>? components = null, string? id = null, Dictionary<string, string>? tags = null) : base(id, tags) {
        if(components != null) {
            foreach(var component in components) {
                AddComponent(component.Key, component.Value);
            }
        }
    }

    public void AddComponent<T>(EnumComponentType type, T component) where T : IComponent {
        _components[type] = component;
    }

    public T? GetComponent<T>(EnumComponentType type) where T : class, IComponent {
        if(_components.TryGetValue(type, out var component)) {
            return component as T;
        }
        return null;
    }

    public bool RemoveComponent(EnumComponentType type) {
        return _components.Remove(type);
    }

    public void Update(GameTime gameTime) {
        foreach(var component in _components.Values) {
            component.Update(gameTime, this);
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        var graphicsComponent = GetComponent<Graphics>(EnumComponentType.Graphics);
        var physicsComponent = GetComponent<Physics>(EnumComponentType.Physics);
        if(graphicsComponent != null && physicsComponent != null) {
            graphicsComponent.Draw(spriteBatch, this);
        }
    }
}
