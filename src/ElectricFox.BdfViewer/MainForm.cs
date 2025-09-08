using System.ComponentModel.Design;
using System.Drawing.Imaging;
using ElectricFox.BdfSharp;

namespace ElectricFox.BdfViewer
{
    public partial class MainForm : Form
    {
        private BdfFont? loadedFont;

        public MainForm()
        {
            InitializeComponent();
        }

        private async Task LoadFont(string filename)
        {
            try
            {
                try
                {
                    loadedFont = await BdfFont.LoadAsync(filename);
                    LoadGlyphs();
                    LoadInformation();
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

        private void LoadInformation()
        {
            fontNameTextbox.Text = loadedFont?.FontName ?? string.Empty;
            versionTextbox.Text = loadedFont?.Version ?? string.Empty;
            glyphsTextbox.Text = loadedFont?.GlyphCount.ToString() ?? string.Empty;
            intendedReolutionTextbox.Text = $"{loadedFont?.IntendedResolution.Width}, {loadedFont?.IntendedResolution.Height}";
            pointSizeTextbox.Text = loadedFont?.PointSize.ToString() ?? string.Empty;
        }

        private void LoadGlyphInformation()
        {
            if (characterListView.SelectedItems.Count > 0)
            {
                var selectedItem = characterListView.SelectedItems[0];
                var glyph = (BdfGlyph?)selectedItem.Tag;

                if (glyph is not null)
                {
                    glyphNameTextbox.Text = glyph.Name;
                    glyphEncodingTextbox.Text = glyph.Encoding.ToString();
                    return;
                }
            }

            glyphNameTextbox.Text = string.Empty;
            glyphEncodingTextbox.Text = string.Empty;
            return;
        }

        private async void LoadGlyphs()
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

                foreach (var c in loadedFont.Glyphs)
                {
                    imgList.Images.Add(GetGlyphImage(c));
                    list.Add(
                        new ListViewItem
                        {
                            Text = c.Name,
                            ImageIndex = i++,
                            Tag = c,
                        }
                    );
                }

                return (imgList, list);
            });

            characterListView.BeginUpdate();
            characterListView.Items.Clear();
            characterListView.LargeImageList = items.imgList;
            characterListView.Items.AddRange(items.list.ToArray());
            characterListView.EndUpdate();
        }

        private Bitmap GetGlyphImage(BdfGlyph c)
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

        private void CharacterListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGlyphInformation();

            if (characterListView.SelectedItems.Count == 0 || loadedFont is null)
            {
                glyphBox.Image = null;
                return;
            }

            glyphBox.Refresh();
        }

        private void PreviewTextboxTextChanged(object sender, EventArgs e)
        {
            previewTextImage.Refresh();
        }

        private void PreviewTextImagePaint(object sender, PaintEventArgs e)
        {
            if (loadedFont is null)
            {
                return;
            }

            var g = e.Graphics;

            var data = loadedFont.RenderBitmap(previewTextbox.Text);

            var size = 1;
            if (size2Xradio.Checked)
            {
                size = 2;
            }
            if (size10Xradio.Checked)
            {
                size = 10;
            }

            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    g.FillRectangle(
                        data[x, y] ? Brushes.Black : Brushes.White,
                        (size * x) + 20,
                        (size * y) + 20,
                        size,
                        size
                    );
                }
            }

            if (size10Xradio.Checked)
            {
                var rect = loadedFont.MeasureString(previewTextbox.Text);

                for (int x = 0; x <= data.GetLength(0); x++)
                {
                    g.DrawLine(new Pen(Color.LightGray), (size * x) + 20, 20, (size * x) + 20, (size * data.GetLength(1)) + 20);
                }
                for (int y = 0; y <= data.GetLength(1); y++)
                {
                    g.DrawLine(new Pen(Color.LightGray), 20, (size * y) + 20, (size * data.GetLength(0)) + 20, (size * y) + 20);
                }

                var origin = new Point(20 + (0 - (rect.X * size)), 20 + (0 - (rect.Y * size)));

                g.DrawLine(new Pen(Color.Red), origin.X - 5, origin.Y, origin.X + 5, origin.Y);
                g.DrawLine(new Pen(Color.Red), origin.X, origin.Y - 5, origin.X, origin.Y + 5);
            }

        }

        private void GlyphBoxPaint(object sender, PaintEventArgs e)
        {
            if (characterListView.SelectedItems.Count == 0 || loadedFont is null)
            {
                return;
            }

            var selectedItem = characterListView.SelectedItems[0];
            var glyph = (BdfGlyph?)selectedItem.Tag;

            if (glyph is null)
            {
                return;
            }

            var g = e.Graphics;

            Point offset = new(20, 20);

            // Glyph scaled 10x
            for (int x = 0; x < glyph.BoundingBox.Width; x++)
            {
                for (int y = 0; y < glyph.BoundingBox.Height; y++)
                {
                    g.FillRectangle(
                        glyph[x, y] ? Brushes.Black : Brushes.White,
                        offset.X + (x * 10),
                        offset.Y + (y * 10),
                        10,
                        10
                    );
                }
            }

            // Pixel Grid
            for (int x = 0; x < glyph.BoundingBox.Width + 1; x++)
            {
                g.DrawLine(new Pen(Color.LightGray), offset.X + (x * 10), offset.Y, offset.X + (x * 10), offset.Y + (glyph.BoundingBox.Height * 10));
            }
            for (int y = 0; y < glyph.BoundingBox.Height + 1; y++)
            {
                g.DrawLine(new Pen(Color.LightGray), offset.X, offset.Y + (y * 10), offset.X + (glyph.BoundingBox.Width * 10), offset.Y + (y * 10));
            }

            // Origin point
            var glyphOrigin = new Point(offset.X - (glyph.BoundingBox.XOffset * 10), offset.Y + (glyph.BoundingBox.Height * 10) + (glyph.BoundingBox.YOffset * 10));
            g.DrawLine(new Pen(Color.Red), glyphOrigin.X - 5, glyphOrigin.Y, glyphOrigin.X + 5, glyphOrigin.Y);
            g.DrawLine(new Pen(Color.Red), glyphOrigin.X, glyphOrigin.Y - 5, glyphOrigin.X, glyphOrigin.Y + 5);
        }

        private void SizeRadioCheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                previewTextImage.Refresh();
            }
        }

        private async void OpenButtonClick(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "BDF Fonts (*.bdf)|*.bdf|All Files (*.*)|*.*",
                Title = "Open BDF Font",
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                await LoadFont(openDialog.FileName);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
