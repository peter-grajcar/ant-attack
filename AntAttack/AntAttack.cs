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
            MENU, GAME, STATS, END
        }

        private Graphics _graphics;
        public static Renderer Renderer;
        public static Map Map;
        public static Levels Levels;
        
        public static bool Paused { get; set; }
        public static State CurrentState { get; set; }

        private Human _rescuer, _rescuee;
        
        public AntAttack()
        {
            InitializeComponent();
            timer.Enabled = true;
            ConsoleUtil.init();
            
            
            Bitmap canvasBmp = new Bitmap(800, 600);
            canvas.Image = canvasBmp;
            
            _graphics = Graphics.FromImage(canvasBmp);
            Renderer = new Renderer(_graphics, 30);
            Map = new Map("AntAttack/Resources/map.txt");
            Levels = new Levels("AntAttack/Resources/levels.txt");
            
            CurrentState = State.MENU;
        }

        private void initLevel(int i)
        {
            Level level = Levels.GetLevel(i);
            
            Map.RemoveAllEntities();
            Map.CreateAndDestroyEntities();

            _rescuee.Position = level.Rescuee;
            Map.AddEntity(_rescuee);
            _rescuer.Position = level.Rescuer;
            Map.AddEntity(_rescuer);

            foreach (Vector3 pos in level.Ants)
            {
                Ant ant = new Ant();
                ant.Position = pos;
                ant.Target = _rescuer;
                Map.AddEntity(ant);
            }

            Renderer.Centre = _rescuer.Position;
            Renderer.SetMessage("Ah shit, here we go again.");
        }

        private bool _saved;
        private ulong _saveTime;
        private int _currentLevel;
        public void OnTick(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.MENU:
                    Renderer.RenderMenu();
                    
                    bool startGame = false;
                    if (Keyboard.KeyPressed == Keys.G)
                    {
                        _rescuer = new Girl();
                        _rescuee = new Boy();
                        startGame = true;
                    }else if (Keyboard.KeyPressed == Keys.B)
                    {
                        _rescuer = new Boy();
                        _rescuee = new Girl();
                        startGame = true;
                    }

                    if (startGame)
                    {
                        _currentLevel = 0;
                        initLevel(_currentLevel);
                        Time.T = 0;
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
                        _currentLevel++;
                        _saveTime = Time.T;
                    }
                    else if (_saved && Renderer.FinishedMessage())
                    {
                        Renderer.Overlay = Colour.Empty;
                        if (_currentLevel < Levels.Count)
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
                    if (_rescuer.Health <= 0 || _rescuee.Health <= 0)
                    {
                        CurrentState = State.END;
                    }
                    
                    Renderer.RenderMap(Map);
                    Renderer.RenderGui(_rescuee, _rescuer);
                    Renderer.RenderMessage();
                    break;
                case State.STATS:
                    Renderer.RenderStats();

                    if (Keyboard.KeyPressed != Keys.None)
                    {
                        _saved = false;
                        _rescuee.Follow = null;
                        _rescuee.Health = 20;
                        CurrentState = State.GAME;
                        initLevel(_currentLevel);
                    }
                    break;
                case State.END:
                    Renderer.RenderEnd();
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
