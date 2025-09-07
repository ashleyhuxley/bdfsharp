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
            characterListView = new ListView();
            splitContainer1 = new SplitContainer();
            previewTextImage = new PictureBox();
            groupBox3 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            glyphEncodingTextbox = new TextBox();
            glyphNameTextbox = new TextBox();
            sizeGroupBox = new GroupBox();
            size10Xradio = new RadioButton();
            size2Xradio = new RadioButton();
            size1Xradio = new RadioButton();
            groupBox2 = new GroupBox();
            glyphsTextbox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            fontNameTextbox = new TextBox();
            versionTextbox = new TextBox();
            openButton = new Button();
            groupBox1 = new GroupBox();
            previewTextbox = new TextBox();
            glyphBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewTextImage).BeginInit();
            groupBox3.SuspendLayout();
            sizeGroupBox.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)glyphBox).BeginInit();
            SuspendLayout();
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
            splitContainer1.Panel2.Controls.Add(groupBox3);
            splitContainer1.Panel2.Controls.Add(sizeGroupBox);
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Controls.Add(glyphBox);
            splitContainer1.Size = new Size(1297, 922);
            splitContainer1.SplitterDistance = 432;
            splitContainer1.TabIndex = 6;
            // 
            // previewTextImage
            // 
            previewTextImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            previewTextImage.BackColor = Color.White;
            previewTextImage.Location = new Point(14, 424);
            previewTextImage.Name = "previewTextImage";
            previewTextImage.Size = new Size(829, 486);
            previewTextImage.TabIndex = 12;
            previewTextImage.TabStop = false;
            previewTextImage.Paint += PreviewTextImagePaint;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(glyphEncodingTextbox);
            groupBox3.Controls.Add(glyphNameTextbox);
            groupBox3.Location = new Point(14, 220);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(502, 116);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Glyph Information";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 67);
            label6.Name = "label6";
            label6.Size = new Size(57, 15);
            label6.TabIndex = 5;
            label6.Text = "Encoding";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 38);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 4;
            label5.Text = "Name";
            // 
            // glyphEncodingTextbox
            // 
            glyphEncodingTextbox.Location = new Point(102, 64);
            glyphEncodingTextbox.Name = "glyphEncodingTextbox";
            glyphEncodingTextbox.ReadOnly = true;
            glyphEncodingTextbox.Size = new Size(100, 23);
            glyphEncodingTextbox.TabIndex = 2;
            // 
            // glyphNameTextbox
            // 
            glyphNameTextbox.Location = new Point(102, 35);
            glyphNameTextbox.Name = "glyphNameTextbox";
            glyphNameTextbox.ReadOnly = true;
            glyphNameTextbox.Size = new Size(384, 23);
            glyphNameTextbox.TabIndex = 1;
            // 
            // sizeGroupBox
            // 
            sizeGroupBox.Controls.Add(size10Xradio);
            sizeGroupBox.Controls.Add(size2Xradio);
            sizeGroupBox.Controls.Add(size1Xradio);
            sizeGroupBox.Location = new Point(537, 356);
            sizeGroupBox.Name = "sizeGroupBox";
            sizeGroupBox.Size = new Size(306, 62);
            sizeGroupBox.TabIndex = 13;
            sizeGroupBox.TabStop = false;
            sizeGroupBox.Text = "Preview Size";
            // 
            // size10Xradio
            // 
            size10Xradio.AutoSize = true;
            size10Xradio.Location = new Point(157, 26);
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
            size2Xradio.Location = new Point(97, 26);
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
            size1Xradio.Location = new Point(39, 26);
            size1Xradio.Name = "size1Xradio";
            size1Xradio.Size = new Size(38, 19);
            size1Xradio.TabIndex = 0;
            size1Xradio.TabStop = true;
            size1Xradio.Text = "1X";
            size1Xradio.UseVisualStyleBackColor = true;
            size1Xradio.CheckedChanged += SizeRadioCheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(glyphsTextbox);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(fontNameTextbox);
            groupBox2.Controls.Add(versionTextbox);
            groupBox2.Controls.Add(openButton);
            groupBox2.Location = new Point(14, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(502, 190);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Font Information";
            // 
            // glyphsTextbox
            // 
            glyphsTextbox.Location = new Point(102, 146);
            glyphsTextbox.Name = "glyphsTextbox";
            glyphsTextbox.ReadOnly = true;
            glyphsTextbox.Size = new Size(121, 23);
            glyphsTextbox.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 149);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 4;
            label3.Text = "Glyphs";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 120);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 3;
            label2.Text = "Font Name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 91);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 2;
            label1.Text = "Version";
            // 
            // fontNameTextbox
            // 
            fontNameTextbox.Location = new Point(102, 117);
            fontNameTextbox.Name = "fontNameTextbox";
            fontNameTextbox.ReadOnly = true;
            fontNameTextbox.Size = new Size(384, 23);
            fontNameTextbox.TabIndex = 1;
            // 
            // versionTextbox
            // 
            versionTextbox.Location = new Point(102, 88);
            versionTextbox.Name = "versionTextbox";
            versionTextbox.ReadOnly = true;
            versionTextbox.Size = new Size(121, 23);
            versionTextbox.TabIndex = 0;
            // 
            // openButton
            // 
            openButton.Location = new Point(102, 35);
            openButton.Name = "openButton";
            openButton.Size = new Size(168, 34);
            openButton.TabIndex = 8;
            openButton.Text = "Open Font...";
            openButton.UseVisualStyleBackColor = true;
            openButton.Click += OpenButtonClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(previewTextbox);
            groupBox1.Location = new Point(14, 356);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(502, 62);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Preview Text";
            // 
            // previewTextbox
            // 
            previewTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            previewTextbox.Location = new Point(6, 22);
            previewTextbox.Name = "previewTextbox";
            previewTextbox.Size = new Size(480, 23);
            previewTextbox.TabIndex = 3;
            previewTextbox.TextChanged += PreviewTextboxTextChanged;
            // 
            // glyphBox
            // 
            glyphBox.BackColor = Color.White;
            glyphBox.Location = new Point(537, 12);
            glyphBox.Name = "glyphBox";
            glyphBox.Size = new Size(306, 324);
            glyphBox.TabIndex = 9;
            glyphBox.TabStop = false;
            glyphBox.Paint += GlyphBoxPaint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1297, 922);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            Text = "BDF Font Viewer";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewTextImage).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            sizeGroupBox.ResumeLayout(false);
            sizeGroupBox.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)glyphBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ListView characterListView;
        private SplitContainer splitContainer1;
        private PictureBox previewTextImage;
        private GroupBox groupBox3;
        private Label label6;
        private Label label5;
        private TextBox glyphEncodingTextbox;
        private TextBox glyphNameTextbox;
        private GroupBox sizeGroupBox;
        private RadioButton size10Xradio;
        private RadioButton size2Xradio;
        private RadioButton size1Xradio;
        private GroupBox groupBox2;
        private TextBox glyphsTextbox;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox fontNameTextbox;
        private TextBox versionTextbox;
        private GroupBox groupBox1;
        private TextBox previewTextbox;
        private PictureBox glyphBox;
        private Button openButton;
    }
}
