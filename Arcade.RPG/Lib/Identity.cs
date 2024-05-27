using System;
using System.Collections.Generic;

namespace Arcade.RPG.Lib;
public class Identity(Guid? id = null, Dictionary<string, string>? tags = null) {
    public Guid Id { get; set; } = id ?? Guid.NewGuid();
    public Dictionary<string, string> Tags { get; set; } = tags ?? new Dictionary<string, string>();

    public Identity AddTag(string key, string? value) {
        this.Tags[key] = !string.IsNullOrEmpty(value) ? value : key;

        return this;
    }

    public Identity RemoveTag(string key) {
        this.Tags.Remove(key);

        return this;
    }
}