using System.ComponentModel;
using System.Windows.Forms;
using Breakout3D.Framework;

namespace Breakout3D
{
    partial class GameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RenderControl = new OpenGL.GlControl();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.Label_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.FlexibleSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateTimeValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectiveViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ballViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.LivesLabel = new System.Windows.Forms.Label();
            this.ScoreValue = new System.Windows.Forms.Label();
            this.LivesValue = new System.Windows.Forms.Label();
            this.StatusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RenderControl
            // 
            this.RenderControl.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top |
                                                         System.Windows.Forms.AnchorStyles.Bottom) |
                                                        System.Windows.Forms.AnchorStyles.Left) |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.RenderControl.Animation = true;
            this.RenderControl.AutoSize = true;
            this.RenderControl.BackColor = System.Drawing.Color.DimGray;
            this.RenderControl.ColorBits = ((uint) (32u));
            this.RenderControl.DepthBits = ((uint) (24u));
            this.RenderControl.Location = new System.Drawing.Point(0, 27);
            this.RenderControl.MultisampleBits = ((uint) (4u));
            this.RenderControl.Name = "RenderControl";
            this.RenderControl.Size = new System.Drawing.Size(1106, 676);
            this.RenderControl.StencilBits = ((uint) (0u));
            this.RenderControl.TabIndex = 0;
            this.RenderControl.ContextCreated +=
                new System.EventHandler<OpenGL.GlControlEventArgs>(this.OnContextCreated);
            this.RenderControl.ContextDestroying +=
                new System.EventHandler<OpenGL.GlControlEventArgs>(this.OnContextDestroying);
            this.RenderControl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.OnRender);
            this.RenderControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.RenderControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.RenderControl.PreviewKeyDown +=
                new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.Label_Status, this.StatusText, this.FlexibleSpace, this.UpdateTimeLabel, this.UpdateTimeValue
            });
            this.StatusStrip.Location = new System.Drawing.Point(0, 703);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1106, 25);
            this.StatusStrip.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(45, 20);
            this.Label_Status.Text = "Status: ";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(12, 20);
            this.StatusText.Text = "-";
            // 
            // FlexibleSpace
            // 
            this.FlexibleSpace.Name = "FlexibleSpace";
            this.FlexibleSpace.Size = new System.Drawing.Size(906, 20);
            this.FlexibleSpace.Spring = true;
            // 
            // UpdateTimeLabel
            // 
            this.UpdateTimeLabel.Name = "UpdateTimeLabel";
            this.UpdateTimeLabel.Size = new System.Drawing.Size(78, 20);
            this.UpdateTimeLabel.Text = "Update time: ";
            // 
            // UpdateTimeValue
            // 
            this.UpdateTimeValue.AutoSize = false;
            this.UpdateTimeValue.Name = "UpdateTimeValue";
            this.UpdateTimeValue.Size = new System.Drawing.Size(50, 20);
            this.UpdateTimeValue.Text = "-";
            this.UpdateTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.gameMenuItem, this.viewMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameMenuItem
            // 
            this.gameMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.newGameMenuItem, this.pauseMenuItem, this.toolStripMenuItem2, this.exitMenuItem});
            this.gameMenuItem.Name = "gameMenuItem";
            this.gameMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameMenuItem.Text = "Game";
            // 
            // newGameMenuItem
            // 
            this.newGameMenuItem.Name = "newGameMenuItem";
            this.newGameMenuItem.ShortcutKeyDisplayString = "";
            this.newGameMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameMenuItem.Text = "New Game";
            this.newGameMenuItem.Click += new System.EventHandler(this.newGameMenuItem_Click);
            // 
            // pauseMenuItem
            // 
            this.pauseMenuItem.Name = "pauseMenuItem";
            this.pauseMenuItem.ShortcutKeyDisplayString = "";
            this.pauseMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pauseMenuItem.Text = "Pause / Play";
            this.pauseMenuItem.Click += new System.EventHandler(this.pauseMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeyDisplayString = "";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.perspectiveViewMenuItem, this.topViewMenuItem, this.ballViewMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "View";
            // 
            // perspectiveViewMenuItem
            // 
            this.perspectiveViewMenuItem.Name = "perspectiveViewMenuItem";
            this.perspectiveViewMenuItem.ShortcutKeyDisplayString = "";
            this.perspectiveViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this.perspectiveViewMenuItem.Text = "Perspective";
            this.perspectiveViewMenuItem.Click += new System.EventHandler(this.perspectiveViewMenuItem_Click);
            // 
            // topViewMenuItem
            // 
            this.topViewMenuItem.Name = "topViewMenuItem";
            this.topViewMenuItem.ShortcutKeyDisplayString = "";
            this.topViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this.topViewMenuItem.Text = "Top";
            this.topViewMenuItem.Click += new System.EventHandler(this.topViewMenuItem_Click);
            // 
            // ballViewMenuItem
            // 
            this.ballViewMenuItem.Name = "ballViewMenuItem";
            this.ballViewMenuItem.ShortcutKeyDisplayString = "";
            this.ballViewMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ballViewMenuItem.Text = "Ball";
            this.ballViewMenuItem.Click += new System.EventHandler(this.ballViewMenuItem_Click);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))),
                ((int) (((byte) (30)))));
            this.ScoreLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.ScoreLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ScoreLabel.Location = new System.Drawing.Point(911, 62);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(82, 24);
            this.ScoreLabel.TabIndex = 3;
            this.ScoreLabel.Text = "Score:";
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LivesLabel
            // 
            this.LivesLabel.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.LivesLabel.AutoSize = true;
            this.LivesLabel.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))),
                ((int) (((byte) (30)))));
            this.LivesLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.LivesLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LivesLabel.Location = new System.Drawing.Point(911, 98);
            this.LivesLabel.Name = "LivesLabel";
            this.LivesLabel.Size = new System.Drawing.Size(82, 24);
            this.LivesLabel.TabIndex = 4;
            this.LivesLabel.Text = "Lives:";
            this.LivesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScoreValue
            // 
            this.ScoreValue.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreValue.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))),
                ((int) (((byte) (30)))));
            this.ScoreValue.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.ScoreValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ScoreValue.Location = new System.Drawing.Point(1014, 62);
            this.ScoreValue.Name = "ScoreValue";
            this.ScoreValue.Size = new System.Drawing.Size(47, 28);
            this.ScoreValue.TabIndex = 5;
            this.ScoreValue.Text = "0";
            this.ScoreValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LivesValue
            // 
            this.LivesValue.Anchor =
                ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top |
                                                       System.Windows.Forms.AnchorStyles.Right)));
            this.LivesValue.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (25)))), ((int) (((byte) (25)))),
                ((int) (((byte) (30)))));
            this.LivesValue.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte) (238)));
            this.LivesValue.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LivesValue.Location = new System.Drawing.Point(1014, 98);
            this.LivesValue.Name = "LivesValue";
            this.LivesValue.Size = new System.Drawing.Size(47, 28);
            this.LivesValue.TabIndex = 6;
            this.LivesValue.Text = "0";
            this.LivesValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 728);
            this.Controls.Add(this.LivesValue);
            this.Controls.Add(this.ScoreValue);
            this.Controls.Add(this.LivesLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.RenderControl);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameWindow";
            this.Text = "Breakout 3D";
            this.Resize += new System.EventHandler(this.OnResize);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        
        private System.Windows.Forms.StatusStrip StatusStrip;
        private OpenGL.GlControl RenderControl;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripStatusLabel UpdateTimeValue;
        private System.Windows.Forms.ToolStripStatusLabel UpdateTimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel FlexibleSpace;
        private System.Windows.Forms.ToolStripStatusLabel Label_Status;
        private System.Windows.Forms.Label LivesValue;
        private System.Windows.Forms.Label ScoreValue;
        private System.Windows.Forms.Label LivesLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectiveViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topViewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ballViewMenuItem;
    }
}