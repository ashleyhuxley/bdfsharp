using ElectricFox.BdfFontLib;

namespace ElectricFox.BdfViewer
{
    public partial class MainForm : Form
    {
        private BdfFont? loadedFont;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void openButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "BDF Fonts (*.bdf)|*.bdf|All Files (*.*)|*.*",
                Title = "Open BDF Font"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    try
                    {
                        loadedFont = await BdfFont.LoadAsync(openDialog.FileName);
                        LoadCharacters();
                    }
                    catch (BdfLoadException ex)

                    {
                        MessageBox.Show($"Error loading font: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading font: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadCharacters()
        {
            if (loadedFont is null)
            {
                return;
            }

            characterListView.Items.Clear();
            int i = 0;
            ImageList images = new();
            characterListView.LargeImageList = images;

            foreach (var c in loadedFont.Chars.Values)
            {
                images.Images.Add(GetCharacterImage(c));
                var item = new ListViewItem
                {
                    Text = c.Name,
                    ImageIndex = i
                };

                characterListView.Items.Add(item);
                i += 1;
            }
        }

        private Image GetCharacterImage(BdfChar c)
        {
            var width = Math.Max(c.BoundingBox.Width, 16);
            var height = Math.Max(c.BoundingBox.Height, 16);

            var b = new Bitmap(width, height);

            for (int x = 0; x < c.BoundingBox.Width; x++)
            {
                for (int y = 0; y < c.BoundingBox.Height; y++)
                {
                    b.SetPixel(x, y, c[x, y] ? Color.Black : Color.White);
                }
            }

            return b;
        }

        private void characterListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (characterListView.SelectedItems.Count == 0 || loadedFont is null)
            {
                glyphBox.Image = null;
                return;
            }
            var selectedItem = characterListView.SelectedItems[0];
            var character = loadedFont.Chars.Values.FirstOrDefault(c => c.Name == selectedItem.Text);
            if (character != null)
            {
                glyphBox.Image = GetCharacterImage(character);
            }
            else
            {
                glyphBox.Image = null;
            }
        }

        private void previewTextbox_TextChanged(object sender, EventArgs e)
        {
            previewTextImage.Refresh();
        }

        private void previewTextImage_Paint(object sender, PaintEventArgs e)
        {
            if (loadedFont is null)
            {
                return;
            }

            var g = e.Graphics;

            var data = loadedFont.RenderBitmap(previewTextbox.Text);
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    g.FillRectangle(data[x, y] ? Brushes.Black : Brushes.White, x + 20, y + 20, 1, 1);
                }
            }
        }
    }
}
