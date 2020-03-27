using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public partial class GameWindow : Form
    {
        private Game m_Game;

        public GameWindow()
        {
            InitializeComponent();
        }
        
        private void OnContextCreated(object sender, GlControlEventArgs e)
        {
            m_Game = new Game(this);
        }
        
        private void OnRender(object sender, GlControlEventArgs e)
        {
            Time.RestartMeasure();
            UpdateTimeValue.Text = $"{Time.DeltaTime}ms";
            Input.HandleInput();
            m_Game.UpdateScene();
            m_Game.RenderScene();
        }

        private void OnContextDestroying(object sender, GlControlEventArgs e)
        {
            m_Game.Dispose();
        }
        
        private void OnResize(object sender, EventArgs e)
        {
            m_Game?.Resize();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Input.KeyDown(sender, e);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Input.KeyUp(sender, e);
        }

        private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Input.PreviewKeyDown(sender, e);
        }
        
        public void UpdateScore(int value)
        {
            ScoreValue.Text = $"{value}";
        }
        
        public void UpdateLives(int value)
        {
            LivesValue.Text = $"{value}";
        }
    }
}