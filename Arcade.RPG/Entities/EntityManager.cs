namespace Arcade.RPG.Entities;

using System;
using System.Collections;
using System.Collections.Generic;

using Arcade.RPG.Lib;

public class EntityManager : Identity, IEnumerable<Entity> {
    public List<Entity> entities { get; set; } = new List<Entity>();
    public List<Entity> cache { get; set; } = new List<Entity>();

    public EntityManager() : base() { }
    public EntityManager(List<Entity>? entities, List<Entity>? cache) : base() {
        this.entities = entities ?? new List<Entity>();
        this.cache = cache ?? new List<Entity>();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
    public IEnumerator<Entity> GetEnumerator() {
        foreach(Entity entity in this.entities) {
            yield return entity;
        }
    }
    public IEnumerator<Entity> GetCacheEnumerator() {
        foreach(Entity entity in this.cache) {
            yield return entity;
        }
    }

    public int Size {
        get { return this.entities.Count; }
    }
    public int CacheSize {
        get { return this.cache.Count; }
    }

    public EntityManager Add(Entity entity) {
        this.entities.Add(entity);

        return this;
    }
    public EntityManager Add(List<Entity> entities) {
        this.entities.AddRange(entities);

        return this;
    }
    public EntityManager Remove(Entity entity) {
        this.entities.Remove(entity);

        return this;
    }
    public EntityManager Remove(List<Entity> entities) {
        foreach(Entity entity in entities) {
            this.entities.Remove(entity);
        }

        return this;
    }
    public bool Has(Entity entity) {
        return this.entities.Contains(entity) ? true : false;
    }
    public EntityManager Clear() {
        this.entities.Clear();

        return this;
    }

    public EntityManager WriteCache(Entity entity) {
        this.cache.Add(entity);

        return this;
    }
    public EntityManager WriteCache(List<Entity> entities) {
        this.cache.AddRange(entities);

        return this;
    }
    public EntityManager ClearCache() {
        this.cache.Clear();

        return this;
    }
    public EntityManager Reset() {
        this.entities.Clear();
        this.cache.Clear();

        return this;
    }

    public EntityManager Filter(Func<Entity, bool> filter) {
        var results = new List<Entity>();

        foreach(Entity entity in this.entities) {
            if(filter(entity)) {
                results.Add(entity);
            }
        }

        return new EntityManager(entities: results, cache: this.cache);
    }
    public EntityManager Map(Func<Entity, Entity> map) {
        var results = new List<Entity>();

        foreach(Entity entity in this.entities) {
            results.Add(map(entity));
        }

        return new EntityManager(entities: results, cache: this.cache);
    }
    public EntityManager Reduce(Func<Entity, Entity, Entity> reduce, Entity accumulator) {
        Entity result = accumulator;

        for(int i = 0; i < this.entities.Count; i++) {
            result = reduce(result, this.entities[i]);
        }

        return new EntityManager(entities: new List<Entity> { result }, cache: this.cache);
    }
    public EntityManager Sort(Func<Entity, Entity, int> sort) {
        this.entities.Sort((a, b) => sort(a, b));

        return this;
    }
    public EntityManager SortCache(Func<Entity, Entity, int> sort) {
        this.cache.Sort((a, b) => sort(a, b));

        return this;
    }

    public Entity Get(int index) {
        return this.entities[index];
    }
    public Entity GetCache(int index) {
        return this.cache[index];
    }

    public bool Find(Func<Entity, bool> filter, out Entity entityOut) {
        foreach(Entity entity in this.entities) {
            if(filter(entity)) {
                entityOut = entity;
                return true;
            }
        }

        entityOut = null;
        return false;
    }
}