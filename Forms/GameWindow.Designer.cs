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
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ballToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.RenderControl.Animation = true;
            this.RenderControl.AnimationTimer = false;
            this.RenderControl.BackColor = System.Drawing.Color.DimGray;
            this.RenderControl.ColorBits = ((uint) (32u));
            this.RenderControl.DepthBits = ((uint) (24u));
            this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderControl.Location = new System.Drawing.Point(0, 0);
            this.RenderControl.MultisampleBits = ((uint) (4u));
            this.RenderControl.Name = "RenderControl";
            this.RenderControl.Size = new System.Drawing.Size(1106, 728);
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
            this.StatusStrip.Location = new System.Drawing.Point(0, 699);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1106, 29);
            this.StatusStrip.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(45, 24);
            this.Label_Status.Text = "Status: ";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(12, 24);
            this.StatusText.Text = "-";
            // 
            // FlexibleSpace
            // 
            this.FlexibleSpace.Name = "FlexibleSpace";
            this.FlexibleSpace.Size = new System.Drawing.Size(906, 24);
            this.FlexibleSpace.Spring = true;
            // 
            // UpdateTimeLabel
            // 
            this.UpdateTimeLabel.Name = "UpdateTimeLabel";
            this.UpdateTimeLabel.Size = new System.Drawing.Size(78, 24);
            this.UpdateTimeLabel.Text = "Update time: ";
            // 
            // UpdateTimeValue
            // 
            this.UpdateTimeValue.AutoSize = false;
            this.UpdateTimeValue.Name = "UpdateTimeValue";
            this.UpdateTimeValue.Size = new System.Drawing.Size(50, 24);
            this.UpdateTimeValue.Text = "-";
            this.UpdateTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.gameToolStripMenuItem, this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.newGameToolStripMenuItem, this.pauseToolStripMenuItem, this.toolStripMenuItem2,
                this.exitToolStripMenuItem
            });
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.ShortcutKeyDisplayString = "N";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeyDisplayString = "P";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pauseToolStripMenuItem.Text = "Pause / Play";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.perspectiveToolStripMenuItem, this.topToolStripMenuItem, this.ballToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // perspectiveToolStripMenuItem
            // 
            this.perspectiveToolStripMenuItem.Name = "perspectiveToolStripMenuItem";
            this.perspectiveToolStripMenuItem.ShortcutKeyDisplayString = "num 1";
            this.perspectiveToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.perspectiveToolStripMenuItem.Text = "Perspective";
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.ShortcutKeyDisplayString = "num 2";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.topToolStripMenuItem.Text = "Top";
            // 
            // ballToolStripMenuItem
            // 
            this.ballToolStripMenuItem.Name = "ballToolStripMenuItem";
            this.ballToolStripMenuItem.ShortcutKeyDisplayString = "num 3";
            this.ballToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ballToolStripMenuItem.Text = "Ball";
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
            this.ScoreLabel.Location = new System.Drawing.Point(911, 38);
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
            this.LivesLabel.Location = new System.Drawing.Point(911, 74);
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
            this.ScoreValue.Location = new System.Drawing.Point(1014, 38);
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
            this.LivesValue.Location = new System.Drawing.Point(1014, 74);
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
        private System.Windows.Forms.ToolStripMenuItem ballToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}