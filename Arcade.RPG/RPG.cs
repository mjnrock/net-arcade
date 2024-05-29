namespace Arcade.RPG;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcade.RPG.Worlds;
using Arcade.RPG.Configs;
using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using System.Collections.Generic;
using Arcade.RPG.Systems;
using Arcade.RPG.Lib.Utility;

public class RPG : Game {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public SpriteFont debugFont;
    public Random random = new Random();

    public RPGDebug Debug = new RPGDebug();

    public Config Config { get; set; }

    //TODO: Implement a SystemManager
    public TemporalDictionary<EnumSystemType, System> Systems { get; set; }

    public World World { get; set; }

    public RPG() {
        this.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content/bin/Windows";
        IsMouseVisible = true;

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        this.graphics.PreferredBackBufferWidth = screenWidth;
        this.graphics.PreferredBackBufferHeight = screenHeight;

        this.graphics.IsFullScreen = true;
        this.graphics.ApplyChanges();


        this.Systems = new TemporalDictionary<EnumSystemType, System> {
            { EnumSystemType.Input, new InputSystem(this) },
            { EnumSystemType.World, new WorldSystem(this) },
            { EnumSystemType.Physics, new PhysicsSystem(this) },
            { EnumSystemType.Entity, new EntitySystem(this) }
        };

        this.World = new AtlasWorld("demoCaveMap", this);

        this.Config = new Configs.Config();
        this.Config.Viewport.Subject = new Entity {
            components = new Dictionary<EnumComponentType, IComponent> {
                { EnumComponentType.Physics, new Physics(
                    model: new Lib.Geometry.Shapes.Circle(
                        origin: new Vector2(0, 2),
                        radius: 0.2f
                    ),
                    velocity: Vector2.Zero,
                    speed: 3.0f
                )},
                { EnumComponentType.Graphics, new Graphics(
                    graphicsDevice: this.GraphicsDevice,
                    color: Color.CornflowerBlue
                )}
            }
        };

        this.Route(EnumSystemType.World, new Message(
            type: WorldSystem.EnumAction.JoinWorld,
            payload: this.Config.Viewport.Subject
        ));
        this.Config.SyncWithSubject();

        //STUB: Temporary test code
        this.Route(EnumSystemType.World, new Message(
            type: WorldSystem.EnumAction.JoinWorld,
            payload: new Entity {
                components = new Dictionary<EnumComponentType, IComponent> {
                    { EnumComponentType.Physics, new Physics(
                        model: new Lib.Geometry.Shapes.Rectangle(
                            origin: new Vector2(0, 3),
                            width: 2,
                            height: 1
                        ),
                        velocity: Vector2.Zero,
                        speed: 4.0f
                    )},
                    { EnumComponentType.Graphics, new Graphics(
                        graphicsDevice: this.GraphicsDevice,
                        color: Color.Red
                    )}
                }
            }
        ));
    }

    public void Route(EnumSystemType to, Message message) {
        this.Systems[to].Receive(message);
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        this.debugFont = Content.Load<SpriteFont>("DebugFont");
        this.spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime) {
        if(this.Config.Settings.IsPaused) {
            /* Allow InputSystem to continue updating */
            this.Systems[EnumSystemType.Input].Update(this, gameTime);
            return;
        }

        /* Iterate over each System in registration order and Update */
        foreach(KeyValuePair<EnumSystemType, System> system in this.Systems) {
            system.Value.Update(this, gameTime);
        }

        /* Invoke super, just in cast it matters */
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        if(this.Config.Settings.IsPaused) {
            return;
        }

        /* Clear the screen to black */
        GraphicsDevice.Clear(Color.Black);

        /* Attempt to center drawing around the subject */
        if(this.Config.Viewport.Subject != null) {
            Physics subjectPhysics = this.Config.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
            Vector2 subjectPosition = subjectPhysics.Position;

            float viewportWidth = GraphicsDevice.Viewport.Width;
            float viewportHeight = GraphicsDevice.Viewport.Height;
            float zoom = this.Config.Viewport.Zoom.Current;

            Vector3 viewportCenter = new Vector3(viewportWidth / 2f, viewportHeight / 2f, 0);

            //FIXME: Something about this is not correct, it doesn't transform the viewport correctly
            //FIXME: Graphics may or not be correct, either; tbd after this offset culprit is found
            float pixelX = -subjectPosition.X * this.Config.Viewport.TileBaseWidth - subjectPhysics.model.Width * this.Config.Viewport.TileBaseWidth / 2;
            float pixelY = -subjectPosition.Y * this.Config.Viewport.TileBaseHeight - subjectPhysics.model.Height * this.Config.Viewport.TileBaseHeight / 2;

            Matrix translationMatrix = Matrix.CreateTranslation(
                new Vector3(
                    pixelX,
                    pixelY,
                    0
                )
            );
            Matrix transformationMatrix = translationMatrix * Matrix.CreateScale(zoom, zoom, 1f) * Matrix.CreateTranslation(viewportCenter);

            this.spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: transformationMatrix
            );
        } else {
            this.spriteBatch.Begin(
                samplerState: SamplerState.PointClamp
            );
        }

        /* Iterate over each System in registration order and Draw */
        foreach(KeyValuePair<EnumSystemType, System> system in this.Systems) {
            system.Value.Draw(this, GraphicsDevice, gameTime, this.spriteBatch);
        }

        //STUB: DEBUG, draw entity positions
        foreach(Entity entity in this.World.entityManager.cache) {
            if(entity is TerrainEntity) continue;

            Physics physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);
            Vector2 position = physicsComponent.Position;

            this.spriteBatch.DrawString(
                this.debugFont,
                $"({position.X}, {position.Y})",
                new Vector2(
                    position.X * this.Config.Viewport.TileBaseWidth,
                    position.Y * this.Config.Viewport.TileBaseHeight + this.Config.Viewport.TileBaseHeight
                ),
                Color.White
            );
        }

        this.spriteBatch.End();

        /* Invoke super, just in cast it matters */
        base.Draw(gameTime);
    }


}
