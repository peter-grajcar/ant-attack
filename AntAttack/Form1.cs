﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntAttack
{
    using Colour = Color;
    
    public partial class Form1 : Form
    {
        public enum State
        {
            MENU, GAME, STATS
        }

        private Graphics _graphics;
        public static Renderer Renderer;
        public static Map Map;
        
        public static bool Paused { get; set; }
        public static State CurrentState { get; set; }

        private Boy boy;
        private Girl girl;
        
        public Form1()
        {
            InitializeComponent();
            timer.Enabled = true;
            ConsoleUtil.init();
            
            
            Bitmap canvasBmp = new Bitmap(800, 600);
            canvas.Image = canvasBmp;
            
            _graphics = Graphics.FromImage(canvasBmp);
            Renderer = new Renderer(_graphics, 30);
            Map = new Map("AntAttack/Resources/map.txt");
            
            Renderer.Centre.Y += 100;
            
            // TODO: Load entities from map file
            boy = new Boy();
            boy.Position = new Vector3(39, 20, 0);
            boy.Direction = 2;
            boy.Controllable = true;
            Map.AddEntity(boy);
            Renderer.Centre = boy.Position;
            
            girl = new Girl();
            girl.Position = new Vector3(9, 25, 4);
            Map.AddEntity(girl);
            
            Ant ant1 = new Ant();
            ant1.Position = new Vector3(10, 20,0);
            ant1.Target = boy;
            Map.AddEntity(ant1);
            
            Ant ant2 = new Ant();
            ant2.Position = new Vector3(24, 10,0);
            ant2.Target = boy;
            Map.AddEntity(ant2);
            
            Ant ant3 = new Ant();
            ant3.Position = new Vector3(5, 13,0);
            ant3.Target = boy;
            Map.AddEntity(ant3);
            
            Renderer.SetMessage("Ah shit, here we go again.");
            CurrentState = State.MENU;
        }

        private bool _saved;
        private ulong _saveTime;
        public void OnTick(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.MENU:
                    Renderer.RenderMenu();
                    if (Keyboard.KeyPressed == Keys.G)
                    {
                     
                        CurrentState = State.GAME;
                        Time.T = 0;
                    }else if (Keyboard.KeyPressed == Keys.B)
                    {

                        CurrentState = State.GAME;
                        Time.T = 0;
                    }
                    break;
                case State.GAME:
                    Map.CreateAndDestroyEntities();
            
                    if (!Paused)
                    {
                        foreach (Entity entity in Map.Entities)
                        {
                            entity.Update();
                        }
                    }

                    if (!_saved && Map.IsSafe(girl) && Map.IsSafe(boy))
                    {
                        Renderer.SetMessage("Congratulations!");
                        _saved = true;
                        _saveTime = Time.T;
                    }
                    else if (_saved && Renderer.FinishedMessage())
                    {
                        CurrentState = State.STATS;
                    }
                    else if(_saved)
                    {
                        if (Time.T - _saveTime > 1200)
                            Renderer.Overlay = Colour.Lime;
                        else if (Time.T - _saveTime > 1000)
                            Renderer.Overlay = Colour.Red;
                        else if (Time.T - _saveTime > 800)
                            Renderer.Overlay = Colour.Blue;
                        else if (Time.T - _saveTime > 600)
                            Renderer.Overlay = Colour.Yellow;
                        else if (Time.T - _saveTime > 400)
                            Renderer.Overlay = Colour.Magenta;
                        else if (Time.T - _saveTime > 200)
                            Renderer.Overlay = Colour.Cyan;
                    }
                    
                    //TODO: not boy but controllable
                    if(boy.WasHit)
                        Renderer.Overlay = Colour.Red;
                    
                    Renderer.RenderMap(Map);
                    Renderer.RenderGui(boy, girl);
                    Renderer.RenderMessage();
                    break;
                case State.STATS:
                    Renderer.RenderStats();
                    break;
            }
            
            Keyboard.KeyPressed = Keys.None;
            Time.Tick();
            canvas.Refresh();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keyboard.KeyPressed = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
