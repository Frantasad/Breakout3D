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
            this.StatusStrip.SuspendLayout();
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
            this.RenderControl.Size = new System.Drawing.Size(1193, 698);
            this.RenderControl.StencilBits = ((uint) (0u));
            this.RenderControl.TabIndex = 0;
            this.RenderControl.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.OnContextCreated);
            this.RenderControl.ContextDestroying += new System.EventHandler<OpenGL.GlControlEventArgs>(this.OnContextDestroying);
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
            this.StatusStrip.Location = new System.Drawing.Point(0, 660);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1193, 38);
            this.StatusStrip.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(45, 33);
            this.Label_Status.Text = "Status: ";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(12, 33);
            this.StatusText.Text = "-";
            // 
            // FlexibleSpace
            // 
            this.FlexibleSpace.Name = "FlexibleSpace";
            this.FlexibleSpace.Size = new System.Drawing.Size(993, 33);
            this.FlexibleSpace.Spring = true;
            // 
            // UpdateTimeLabel
            // 
            this.UpdateTimeLabel.Name = "UpdateTimeLabel";
            this.UpdateTimeLabel.Size = new System.Drawing.Size(78, 33);
            this.UpdateTimeLabel.Text = "Update time: ";
            // 
            // UpdateTimeValue
            // 
            this.UpdateTimeValue.AutoSize = false;
            this.UpdateTimeValue.Name = "UpdateTimeValue";
            this.UpdateTimeValue.Size = new System.Drawing.Size(50, 33);
            this.UpdateTimeValue.Text = "-";
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 698);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.RenderControl);
            this.Name = "GameWindow";
            this.Text = "Breakout 3D";
            this.Resize += new System.EventHandler(this.OnResize);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
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
    }
}