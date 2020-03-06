using System.ComponentModel;
using System.Windows.Forms;

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
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UpdateTimeValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.FlexibleSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // RenderControl
            // 
            this.RenderControl.Animation = true;
            this.RenderControl.AnimationTimer = false;
            this.RenderControl.BackColor = System.Drawing.Color.DimGray;
            this.RenderControl.ColorBits = ((uint) (24u));
            this.RenderControl.DepthBits = ((uint) (0u));
            this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenderControl.Location = new System.Drawing.Point(0, 0);
            this.RenderControl.MultisampleBits = ((uint) (0u));
            this.RenderControl.Name = "RenderControl";
            this.RenderControl.Size = new System.Drawing.Size(853, 494);
            this.RenderControl.StencilBits = ((uint) (0u));
            this.RenderControl.TabIndex = 0;
            this.RenderControl.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.InitScene);
            this.RenderControl.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.DestroyScene);
            this.RenderControl.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderScene);
            this.RenderControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenderControl_KeyDown);
            this.RenderControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RenderControl_KeyUp);
            this.RenderControl.PreviewKeyDown +=
                new System.Windows.Forms.PreviewKeyDownEventHandler(this.RenderControl_PreviewKeyDown);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.StatusLabel, this.StatusText, this.FlexibleSpace, this.UpdateTimeLabel, this.UpdateTimeValue
            });
            this.StatusStrip.Location = new System.Drawing.Point(0, 472);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(853, 22);
            this.StatusStrip.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(45, 17);
            this.StatusLabel.Text = "Status: ";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(12, 17);
            this.StatusText.Text = "-";
            // 
            // UpdateTimeLabel
            // 
            this.UpdateTimeLabel.Name = "UpdateTimeLabel";
            this.UpdateTimeLabel.Size = new System.Drawing.Size(78, 17);
            this.UpdateTimeLabel.Text = "Update time: ";
            // 
            // UpdateTimeValue
            // 
            this.UpdateTimeValue.AutoSize = false;
            this.UpdateTimeValue.Name = "UpdateTimeValue";
            this.UpdateTimeValue.Size = new System.Drawing.Size(50, 17);
            this.UpdateTimeValue.Text = "-";
            // 
            // FlexibleSpace
            // 
            this.FlexibleSpace.Name = "FlexibleSpace";
            this.FlexibleSpace.Size = new System.Drawing.Size(622, 17);
            this.FlexibleSpace.Spring = true;
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 494);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.RenderControl);
            this.Name = "GameWindow";
            this.Text = "Breakout 3D";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        
        private System.Windows.Forms.StatusStrip StatusStrip;
        private OpenGL.GlControl RenderControl;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripStatusLabel UpdateTimeValue;
        private System.Windows.Forms.ToolStripStatusLabel UpdateTimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel FlexibleSpace;
    }
}