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
            propertyGrid = new PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)glyphBox).BeginInit();
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
            // propertyGrid1
            // 
            propertyGrid.Location = new Point(352, 213);
            propertyGrid.Name = "propertyGrid1";
            propertyGrid.Size = new Size(241, 348);
            propertyGrid.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 573);
            Controls.Add(propertyGrid);
            Controls.Add(glyphBox);
            Controls.Add(characterListView);
            Controls.Add(openButton);
            Name = "MainForm";
            Text = "BDF Font Viewer";
            ((System.ComponentModel.ISupportInitialize)glyphBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button openButton;
        private ListView characterListView;
        private PictureBox glyphBox;
        private PropertyGrid propertyGrid;
    }
}
