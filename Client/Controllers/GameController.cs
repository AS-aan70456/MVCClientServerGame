﻿using Client.Interfeices;
using Client.Views;
using Client.Models;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;
using Client.Services;
using Client.Models.Dungeons;

namespace Client.Controllers{
    class GameController : IDrawController{
        //Button
        bool[] Button;

        private GameView View;

        private ReyCastService reyCast;

        private List<Enemy> enemy;
        private Player player;

        private DungeonsGenerator generator;
        private Level level;

        private Vector2i MousePosition;

        public void Activation(RenderWindow window){
            window.SetMouseCursorVisible(false);

            generator = new DungeonsGenerator(new Vector2i(48, 48), 8, 16);
            level = generator.GenerateDungeon(DateTime.Now.Second);
            enemy = new List<Enemy>();
            player = new Player(level);

            reyCast = new ReyCastService(level);

            View = new GameView(window, reyCast, player, enemy, level);

            Button = new bool[6];

            Vector2i newMousePos = View.window.Position + (Vector2i)(View.window.Size / 2);
            Mouse.SetPosition(newMousePos);
            MousePosition = newMousePos - View.window.Position;

            //add event
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(MouseMoved);
            window.KeyPressed += new EventHandler<KeyEventArgs>(KeyPressed);
            window.KeyReleased += new EventHandler<KeyEventArgs>(KeyReleased);
        }

        private void KeyPressed(object sender, KeyEventArgs @event) {
            if (@event.Code == Keyboard.Key.W)
                Button[0] = true;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = true;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = true;
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = true;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = true;
            else if (@event.Code == Keyboard.Key.E)
                Button[5] = true;
            else if (@event.Code == Keyboard.Key.Escape){
                View.window.SetMouseCursorVisible(true);
                Router.Init().graphicsControllers.SetController(new MenuController());
            }

        }

        private void KeyReleased(object sender, KeyEventArgs @event){
            if (@event.Code == Keyboard.Key.W)
                Button[0] = false;
            else if (@event.Code == Keyboard.Key.S)
                Button[1] = false;
            else if (@event.Code == Keyboard.Key.A)
                Button[2] = false;
            else if (@event.Code == Keyboard.Key.D)
                Button[3] = false;
            else if (@event.Code == Keyboard.Key.Q)
                Button[4] = false;
            else if (@event.Code == Keyboard.Key.E)
                Button[5] = false;
        }

        public void Draw(){
            View.Render();
        }
    
        public void Updata(){
            Router router = Router.Init();
            if (Button[0] == true)
                player.Move(new Vector2f(0.1f, 0));
            if (Button[1] == true)
                player.Move(new Vector2f(-0.1f, 0));
            if (Button[2] == true)
                player.Move(new Vector2f(0, 0.1f));
            if (Button[3] == true)
                player.Move(new Vector2f(0, -0.1f));
            if (Button[4] == true)
                player.Rotate(1.2f);
            if (Button[5] == true)
                player.Rotate(-1.2f);
            
        }

        private void MouseMoved(object sender, MouseMoveEventArgs @event) {
            player.Rotate(( MousePosition.X - @event.X) * 0.2f);

            RenderWindow render = (RenderWindow)sender;
            MousePosition = new Vector2i(@event.X, @event.Y);
            if (@event.X < 500 || @event.X > View.window.Size.X - 500) {
                Vector2i newMousePos = View.window.Position + (Vector2i)(View.window.Size / 2);
                Mouse.SetPosition(newMousePos);
                MousePosition = newMousePos - View.window.Position;
            }

        }

        public void DizActivation() {
            View.window.MouseMoved -= new EventHandler<MouseMoveEventArgs>(MouseMoved);
            View.window.KeyPressed -= new EventHandler<KeyEventArgs>(KeyPressed);
            View.window.KeyReleased -= new EventHandler<KeyEventArgs>(KeyReleased);
        }
    }
}
