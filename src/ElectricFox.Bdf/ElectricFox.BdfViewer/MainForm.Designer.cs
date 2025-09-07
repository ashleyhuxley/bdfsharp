namespace ElectricFox.BdfViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openButton = new Button();
            characterListView = new ListView();
            glyphBox = new PictureBox();
            previewTextbox = new TextBox();
            previewLabel = new Label();
            previewTextImage = new PictureBox();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)glyphBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // openButton
            // 
            openButton.Location = new Point(582, 12);
            openButton.Name = "openButton";
            openButton.Size = new Size(133, 23);
            openButton.TabIndex = 0;
            openButton.Text = "Open Font...";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // characterListView
            // 
            characterListView.Dock = DockStyle.Fill;
            characterListView.Location = new Point(0, 0);
            characterListView.Name = "characterListView";
            characterListView.Size = new Size(432, 922);
            characterListView.TabIndex = 1;
            characterListView.UseCompatibleStateImageBehavior = false;
            characterListView.SelectedIndexChanged += characterListView_SelectedIndexChanged;
            // 
            // glyphBox
            // 
            glyphBox.BackColor = Color.White;
            glyphBox.Location = new Point(3, 3);
            glyphBox.Name = "glyphBox";
            glyphBox.Size = new Size(563, 173);
            glyphBox.TabIndex = 2;
            glyphBox.TabStop = false;
            // 
            // previewTextbox
            // 
            previewTextbox.Location = new Point(13, 244);
            previewTextbox.Name = "previewTextbox";
            previewTextbox.Size = new Size(393, 23);
            previewTextbox.TabIndex = 3;
            previewTextbox.TextChanged += previewTextbox_TextChanged;
            // 
            // previewLabel
            // 
            previewLabel.AutoSize = true;
            previewLabel.Location = new Point(13, 216);
            previewLabel.Name = "previewLabel";
            previewLabel.Size = new Size(75, 15);
            previewLabel.TabIndex = 4;
            previewLabel.Text = "Preview Text:";
            previewLabel.Click += previewLabel_Click;
            // 
            // previewTextImage
            // 
            previewTextImage.BackColor = Color.White;
            previewTextImage.Dock = DockStyle.Bottom;
            previewTextImage.Location = new Point(0, 289);
            previewTextImage.Name = "previewTextImage";
            previewTextImage.Size = new Size(861, 633);
            previewTextImage.TabIndex = 5;
            previewTextImage.TabStop = false;
            previewTextImage.Paint += previewTextImage_Paint;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(characterListView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(previewTextImage);
            splitContainer1.Panel2.Controls.Add(openButton);
            splitContainer1.Panel2.Controls.Add(glyphBox);
            splitContainer1.Panel2.Controls.Add(previewLabel);
            splitContainer1.Panel2.Controls.Add(previewTextbox);
            splitContainer1.Size = new Size(1297, 922);
            splitContainer1.SplitterDistance = 432;
            splitContainer1.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 922);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "BDF Font Viewer";
            ((System.ComponentModel.ISupportInitialize)glyphBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button openButton;
        private ListView characterListView;
        private PictureBox glyphBox;
        private TextBox previewTextbox;
        private Label previewLabel;
        private PictureBox previewTextImage;
        private SplitContainer splitContainer1;
    }
}
