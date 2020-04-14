using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using System.IO;



namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Mine> enemies;
        List<Tripod> enemies2;
        SpriteFont Arial;
        static ExcelWorksheet sheet;
        static ExcelPackage package;
        static FileInfo file;
        int raknare;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 1500;
            graphics.ApplyChanges();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            file = new FileInfo(@"C:\Users\Joel\Desktop\UtanArvMedkrockar.xlsx");

            package = new ExcelPackage(file);

            sheet = package.Workbook.Worksheets.Add("blad1");

            raknare = 1;

            sheet.Cells[$"A{raknare}"].Value = "Update";
            sheet.Cells[$"B{raknare++}"].Value = "Draw";

            

           


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies2 = new List<Tripod>();
            enemies = new List<Mine>();
            Random random = new Random();
            Texture2D tmpSprite =
                Content.Load<Texture2D>("mine");
            int posX = 0;
            int posY = 0;
            for (int i = 0; i < 100; i++)
            {
                if (posX < 1350)
                {
                    posX += 150;
                }
                else
                {
                    posX = 75;
                    posY += 150;
                }
                Mine temp = new Mine(tmpSprite, posX, posY, 6f, 0.3f);
                enemies.Add(temp);
                
            }
            tmpSprite = Content.Load<Texture2D>("tripod");
            int posX1 = 75;
            int posY1 = 75;
            for (int i = 0; i < 100; i++)
            {


                if (posX1 < 1350)
                {
                    posX1 += 150;
                }
                else
                {
                    posX1 = 75;
                    posY1 += 150;
                }

                Tripod temp = new Tripod(tmpSprite, posX1, posY1, 0f, 3f);

                // Lägg till i listan
                enemies2.Add(temp);
            }
            Arial = Content.Load<SpriteFont>("Fonts/Arial");


            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // minor krockar med tripods

            

            //minor krockar med varandra 
            foreach (Mine m in enemies.ToList())
            {
                Rectangle mRect = new Rectangle(Convert.ToInt32(m.X), Convert.ToInt32(m.Y),
                    Convert.ToInt32(m.Height), Convert.ToInt32(m.Width));
                foreach (Mine m2 in enemies.ToList())
                {
                    Rectangle m2Rect = new Rectangle(Convert.ToInt32(m2.X), Convert.ToInt32(m2.Y),
                        Convert.ToInt32(m2.Width), Convert.ToInt32(m2.Height));
                    if (!(m == m2))
                    {
                        if (CheckCollision(mRect, m2Rect))
                        {
                            m.Changedirection();
                        }
                    }
                        
                }
                foreach(Tripod t in enemies2.ToList())
                {
                    Rectangle tRect = new Rectangle(Convert.ToInt32(t.X), Convert.ToInt32(t.Y),
                        Convert.ToInt32(t.Width), Convert.ToInt32(t.Height));
                    if (CheckCollision(mRect, tRect))
                    {
                        m.Changedirection();
                    }
                }
                m.Update(Window);

            }

            //tripods krockar med varandra 
            foreach (Tripod t in enemies2.ToList())
            {
                foreach (Tripod t2 in enemies2.ToList())
                {
                    if(!(t == t2))
                    {
                        if (t.CheckCollision2(t2))
                        {
                            t.Changedirection2();
                        }
                    }
                }

                t.Update(Window);
            }



            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
   
 
            //Kontrollera ifall rymdskeppet har åkt ut från kanten, om det //
            //har det, så återställ dess position.
            //Har det åkt ut till vänster:



            base.Update(gameTime);
            watch.Stop();
            var elapsedMS = watch.ElapsedTicks;
            if (raknare <= 1200)
            {
                sheet.Cells[$"A{raknare}"].Value = elapsedMS;
            }
            else
            {
                package.Save();
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Mine m in enemies)
                m.Draw(spriteBatch);
            foreach (Tripod t in enemies2)
                t.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(Arial, "antal fiender" + enemies.Count + enemies2.Count, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            watch.Stop();
            var elapsedMS = watch.ElapsedTicks;
            if (raknare <= 1200)
            {
                sheet.Cells[$"B{raknare++}"].Value = elapsedMS;
            }
        }

        public bool CheckCollision(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Intersects(rect2));
        }
    }
}



