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
            ((System.ComponentModel.ISupportInitialize)glyphBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).BeginInit();
            SuspendLayout();
            // 
            // openButton
            // 
            openButton.Location = new Point(12, 12);
            openButton.Name = "openButton";
            openButton.Size = new Size(133, 23);
            openButton.TabIndex = 0;
            openButton.Text = "Open Font...";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += openButton_Click;
            // 
            // characterListView
            // 
            characterListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            characterListView.Location = new Point(12, 41);
            characterListView.Name = "characterListView";
            characterListView.Size = new Size(320, 520);
            characterListView.TabIndex = 1;
            characterListView.UseCompatibleStateImageBehavior = false;
            characterListView.SelectedIndexChanged += characterListView_SelectedIndexChanged;
            // 
            // glyphBox
            // 
            glyphBox.BackColor = Color.White;
            glyphBox.Location = new Point(352, 41);
            glyphBox.Name = "glyphBox";
            glyphBox.Size = new Size(241, 166);
            glyphBox.TabIndex = 2;
            glyphBox.TabStop = false;
            // 
            // previewTextbox
            // 
            previewTextbox.Location = new Point(352, 268);
            previewTextbox.Name = "previewTextbox";
            previewTextbox.Size = new Size(393, 23);
            previewTextbox.TabIndex = 3;
            previewTextbox.TextChanged += previewTextbox_TextChanged;
            // 
            // previewLabel
            // 
            previewLabel.AutoSize = true;
            previewLabel.Location = new Point(352, 241);
            previewLabel.Name = "previewLabel";
            previewLabel.Size = new Size(75, 15);
            previewLabel.TabIndex = 4;
            previewLabel.Text = "Preview Text:";
            // 
            // previewTextImage
            // 
            previewTextImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            previewTextImage.BackColor = Color.White;
            previewTextImage.Location = new Point(352, 297);
            previewTextImage.Name = "previewTextImage";
            previewTextImage.Size = new Size(652, 264);
            previewTextImage.TabIndex = 5;
            previewTextImage.TabStop = false;
            previewTextImage.Paint += previewTextImage_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1016, 573);
            Controls.Add(previewTextImage);
            Controls.Add(previewLabel);
            Controls.Add(previewTextbox);
            Controls.Add(glyphBox);
            Controls.Add(characterListView);
            Controls.Add(openButton);
            Name = "MainForm";
            Text = "BDF Font Viewer";
            ((System.ComponentModel.ISupportInitialize)glyphBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button openButton;
        private ListView characterListView;
        private PictureBox glyphBox;
        private TextBox previewTextbox;
        private Label previewLabel;
        private PictureBox previewTextImage;
    }
}
