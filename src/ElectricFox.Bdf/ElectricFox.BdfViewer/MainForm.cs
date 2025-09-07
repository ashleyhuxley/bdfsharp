using System.Drawing.Imaging;
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
                Title = "Open BDF Font",
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
                        MessageBox.Show(
                            $"Error loading font: {ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error loading font: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private async void LoadCharacters()
        {
            if (loadedFont is null)
            {
                return;
            }

            var items = await Task.Run(() =>
            {
                var imgList = new ImageList();
                var list = new List<ListViewItem>();
                int i = 0;

                foreach (var c in loadedFont.Chars.Values)
                {
                    imgList.Images.Add(GetCharacterImage(c));
                    list.Add(new ListViewItem { Text = c.Name, ImageIndex = i++ });
                }

                return (imgList, list);
            });

            characterListView.BeginUpdate();
            characterListView.Items.Clear();
            characterListView.LargeImageList = items.imgList;
            characterListView.Items.AddRange(items.list.ToArray());
            characterListView.EndUpdate();
        }

        private Bitmap GetCharacterImage(BdfChar c)
        {
            int width = Math.Max(c.BoundingBox.Width, 16);
            int height = Math.Max(c.BoundingBox.Height, 16);

            Bitmap bmp = new(width, height, PixelFormat.Format32bppArgb);

            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb
            );

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int stride = data.Stride;

                for (int y = 0; y < c.BoundingBox.Height; y++)
                {
                    byte* row = ptr + (y * stride);
                    for (int x = 0; x < c.BoundingBox.Width; x++)
                    {
                        bool on = c[x, y];
                        int offset = x * 4;

                        // BGRA layout
                        row[offset + 0] = on ? (byte)0 : (byte)255; // B
                        row[offset + 1] = on ? (byte)0 : (byte)255; // G
                        row[offset + 2] = on ? (byte)0 : (byte)255; // R
                        row[offset + 3] = 255; // A
                    }
                }
            }

            bmp.UnlockBits(data);
            return bmp;
        }

        private void characterListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (characterListView.SelectedItems.Count == 0 || loadedFont is null)
            {
                glyphBox.Image = null;
                return;
            }
            var selectedItem = characterListView.SelectedItems[0];
            var character = loadedFont.Chars.Values.FirstOrDefault(c =>
                c.Name == selectedItem.Text
            );
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
                    g.FillRectangle(
                        data[x, y] ? Brushes.Black : Brushes.White,
                        x + 20,
                        y + 20,
                        1,
                        1
                    );
                }
            }
        }
    }
}
