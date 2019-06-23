using System;
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
    
    public partial class AntAttack : Form
    {
        public enum State
        {
            START, GAME, STATS, END
        }

        private Graphics _graphics;
        public static Renderer Renderer;
        public static Map Map;
        public static Levels Levels;
        
        public static bool Paused { get; set; }
        public static State CurrentState { get; set; }
        public static ulong TimeLeft { get; set; }
        public static int CurrentLevel { get; set; }

        private Human _rescuer, _rescuee;
        
        public AntAttack()
        {
            InitializeComponent();
            timer.Enabled = true;
            
            Bitmap canvasBmp = new Bitmap(800, 600);
            canvas.Image = canvasBmp;
            
            _graphics = Graphics.FromImage(canvasBmp);
            Renderer = new Renderer(_graphics, 30);
            Map = new Map();
            Levels = new Levels();
            
            CurrentState = State.START;
        }

        private bool _saved;
        private ulong _saveTime;
        public void OnTick(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.START:
                    Renderer.RenderStart();
                    Map.RemoveAllEntities();
                    Map.CreateAndDestroyEntities();
                    
                    bool startGame = false;
                    if (Keyboard.KeyPressed == Keys.G)
                    {
                        _rescuer = new Girl();
                        _rescuee = new Boy();
                        startGame = true;
                        
                        Keyboard.KeyPressed = Keys.None;
                    }else if (Keyboard.KeyPressed == Keys.B)
                    {
                        _rescuer = new Boy();
                        _rescuee = new Girl();
                        startGame = true;
                        
                        Keyboard.KeyPressed = Keys.None;
                    }

                    if (startGame)
                    {
                        CurrentLevel = 0;
                        InitLevel(CurrentLevel);
                        TimeLeft = 1_000_000;
                        CurrentState = State.GAME;
                        _rescuer.Controllable = true;
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

                    if (!_saved && Map.IsSafe(_rescuer) && Map.IsSafe(_rescuee))
                    {
                        Renderer.SetMessage("Congratulations!");
                        _saved = true;
                        CurrentLevel++;
                        _saveTime = Time.T;
                    }
                    else if (_saved && Renderer.FinishedMessage())
                    {
                        Renderer.Overlay = Colour.Empty;
                        if (CurrentLevel < Levels.Count)
                            CurrentState = State.STATS;
                        else
                            CurrentState = State.END;
                    }
                    else if(_saved)
                    {
                        if (Time.T - _saveTime > 2400)
                            Renderer.Overlay = Colour.Lime;
                        else if (Time.T - _saveTime > 2000)
                            Renderer.Overlay = Colour.Red;
                        else if (Time.T - _saveTime > 1600)
                            Renderer.Overlay = Colour.Blue;
                        else if (Time.T - _saveTime > 1200)
                            Renderer.Overlay = Colour.Yellow;
                        else if (Time.T - _saveTime > 800)
                            Renderer.Overlay = Colour.Magenta;
                        else if (Time.T - _saveTime > 400)
                            Renderer.Overlay = Colour.Cyan;
                    }
                    
                    if(_rescuer.WasHit)
                        Renderer.Overlay = Colour.Red;
                    if (_rescuer.Health <= 0 || _rescuee.Health <= 0 || TimeLeft <= 0)
                    {
                        CurrentState = State.END;
                    }
                    
                    Renderer.RenderMap(Map);
                    Renderer.RenderGui(_rescuee, _rescuer);
                    Renderer.RenderMessage();
                    TimeLeft -= Time.DeltaT;
                    break;
                case State.STATS:
                    Renderer.RenderStats();

                    if (Keyboard.KeyPressed != Keys.None)
                    {
                        _saved = false;
                        _rescuee.Follow = null;
                        _rescuee.Health = 20;
                        CurrentState = State.GAME;
                        InitLevel(CurrentLevel);

                        Keyboard.KeyPressed = Keys.None;
                    }
                    break;
                case State.END:
                    Renderer.RenderEnd(_rescuer.Health <= 0 || _rescuee.Health <= 0 || TimeLeft <= 0);
                    
                    if (Keyboard.KeyPressed != Keys.None)
                    {
                        _saved = false;
                        CurrentState = State.START;
                        
                        Keyboard.KeyPressed = Keys.None;
                    }
                    break;
            }
            Time.Tick();
            canvas.Refresh();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keyboard.KeyPressed = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void InitLevel(int i)
        {
            Level level = Levels.GetLevel(i);
            
            Map.RemoveAllEntities();
            Map.CreateAndDestroyEntities();

            _rescuee.Position = level.Rescuee.copy();
            Map.AddEntity(_rescuee);
            _rescuer.Position = level.Rescuer.copy();
            Map.AddEntity(_rescuer);

            foreach (Vector3 pos in level.Ants)
            {
                Ant ant = new Ant();
                ant.Position = pos.copy();
                Map.AddEntity(ant);
            }

            Renderer.Centre = _rescuer.Position;
            //Renderer.SetMessage("Ah shit, here we go again.");
        }
    }
}
