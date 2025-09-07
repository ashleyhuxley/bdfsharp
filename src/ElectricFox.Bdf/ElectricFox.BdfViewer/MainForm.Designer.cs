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
            sizeGroupBox = new GroupBox();
            size10Xradio = new RadioButton();
            size2Xradio = new RadioButton();
            size1Xradio = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)glyphBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            sizeGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // openButton
            // 
            openButton.Location = new Point(21, 358);
            openButton.Name = "openButton";
            openButton.Size = new Size(133, 23);
            openButton.TabIndex = 0;
            openButton.Text = "Open Font...";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += OpenButtonClick;
            // 
            // characterListView
            // 
            characterListView.Dock = DockStyle.Fill;
            characterListView.Location = new Point(0, 0);
            characterListView.Name = "characterListView";
            characterListView.Size = new Size(432, 922);
            characterListView.TabIndex = 1;
            characterListView.UseCompatibleStateImageBehavior = false;
            characterListView.SelectedIndexChanged += CharacterListViewSelectedIndexChanged;
            // 
            // glyphBox
            // 
            glyphBox.BackColor = Color.White;
            glyphBox.Location = new Point(3, 3);
            glyphBox.Name = "glyphBox";
            glyphBox.Size = new Size(759, 316);
            glyphBox.TabIndex = 2;
            glyphBox.TabStop = false;
            glyphBox.Paint += GlyphBoxPaint;
            // 
            // previewTextbox
            // 
            previewTextbox.Location = new Point(21, 436);
            previewTextbox.Name = "previewTextbox";
            previewTextbox.Size = new Size(393, 23);
            previewTextbox.TabIndex = 3;
            previewTextbox.TextChanged += PreviewTextboxTextChanged;
            // 
            // previewLabel
            // 
            previewLabel.AutoSize = true;
            previewLabel.Location = new Point(21, 408);
            previewLabel.Name = "previewLabel";
            previewLabel.Size = new Size(75, 15);
            previewLabel.TabIndex = 4;
            previewLabel.Text = "Preview Text:";
            // 
            // previewTextImage
            // 
            previewTextImage.BackColor = Color.White;
            previewTextImage.Dock = DockStyle.Bottom;
            previewTextImage.Location = new Point(0, 480);
            previewTextImage.Name = "previewTextImage";
            previewTextImage.Size = new Size(861, 442);
            previewTextImage.TabIndex = 5;
            previewTextImage.TabStop = false;
            previewTextImage.Paint += PreviewTextImagePaint;
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
            splitContainer1.Panel2.Controls.Add(sizeGroupBox);
            splitContainer1.Panel2.Controls.Add(previewTextImage);
            splitContainer1.Panel2.Controls.Add(openButton);
            splitContainer1.Panel2.Controls.Add(glyphBox);
            splitContainer1.Panel2.Controls.Add(previewLabel);
            splitContainer1.Panel2.Controls.Add(previewTextbox);
            splitContainer1.Size = new Size(1297, 922);
            splitContainer1.SplitterDistance = 432;
            splitContainer1.TabIndex = 6;
            // 
            // sizeGroupBox
            // 
            sizeGroupBox.Controls.Add(size10Xradio);
            sizeGroupBox.Controls.Add(size2Xradio);
            sizeGroupBox.Controls.Add(size1Xradio);
            sizeGroupBox.Location = new Point(437, 399);
            sizeGroupBox.Name = "sizeGroupBox";
            sizeGroupBox.Size = new Size(189, 60);
            sizeGroupBox.TabIndex = 6;
            sizeGroupBox.TabStop = false;
            sizeGroupBox.Text = "Preview Size";
            // 
            // size10Xradio
            // 
            size10Xradio.AutoSize = true;
            size10Xradio.Location = new Point(124, 28);
            size10Xradio.Name = "size10Xradio";
            size10Xradio.Size = new Size(44, 19);
            size10Xradio.TabIndex = 2;
            size10Xradio.Text = "10X";
            size10Xradio.UseVisualStyleBackColor = true;
            size10Xradio.CheckedChanged += SizeRadioCheckedChanged;
            // 
            // size2Xradio
            // 
            size2Xradio.AutoSize = true;
            size2Xradio.Location = new Point(64, 28);
            size2Xradio.Name = "size2Xradio";
            size2Xradio.Size = new Size(38, 19);
            size2Xradio.TabIndex = 1;
            size2Xradio.Text = "2X";
            size2Xradio.UseVisualStyleBackColor = true;
            size2Xradio.CheckedChanged += SizeRadioCheckedChanged;
            // 
            // size1Xradio
            // 
            size1Xradio.AutoSize = true;
            size1Xradio.Checked = true;
            size1Xradio.Location = new Point(6, 28);
            size1Xradio.Name = "size1Xradio";
            size1Xradio.Size = new Size(38, 19);
            size1Xradio.TabIndex = 0;
            size1Xradio.TabStop = true;
            size1Xradio.Text = "1X";
            size1Xradio.UseVisualStyleBackColor = true;
            size1Xradio.CheckedChanged += SizeRadioCheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 922);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "BDF Font Viewer";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)glyphBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            sizeGroupBox.ResumeLayout(false);
            sizeGroupBox.PerformLayout();
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
        private GroupBox sizeGroupBox;
        private RadioButton size2Xradio;
        private RadioButton size1Xradio;
        private RadioButton size10Xradio;
    }
}
