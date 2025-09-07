using System.Drawing;

namespace ElectricFox.BdfSharp.UnitTests
{
    public class BdfFontTests
    {
        private static BdfFont CreateTestFont(
            IReadOnlyList<BdfGlyph>? glyphs = null,
            BdfGeometry? geometry = null,
            BdfBoundingBox? bbox = null)
        {
            var defaultGlyph = new BdfGlyph(
                "A",
                65,
                new bool[2, 2] { { true, false }, { false, true } }
            )
            {
                Geometry = geometry ?? new BdfGeometry { DeviceSize = new Size(2, 2) },
                BoundingBox = bbox ?? new BdfBoundingBox { Width = 2, Height = 2, XOffset = 0, YOffset = 0 },
            };

            return new BdfFont
            {
                FontName = "TestFont",
                Version = "2.1",
                PointSize = 12,
                IntendedResolution = new Size(72, 72),
                FontBoundingBox = bbox ?? new BdfBoundingBox { Width = 2, Height = 2, XOffset = 0, YOffset = 0 },
                GlyphCount = glyphs?.Count ?? 1,
                Properties = new Dictionary<string, string> { { "FOUNDRY", "Test" } },
                Glyphs = glyphs ?? [defaultGlyph],
                Geometry = geometry ?? new BdfGeometry { DeviceSize = new Size(2, 2) }
            };
        }

        [Test]
        public void MeasureString_String_ReturnsExpectedRectangle()
        {
            var font = CreateTestFont();
            var rect = font.MeasureString("A");
            Assert.That(rect, Is.EqualTo(new Rectangle(0, -2, 2, 2)));
        }

        [Test]
        public void MeasureString_Enumerable_ReturnsExpectedRectangle()
        {
            var font = CreateTestFont();
            var rect = font.MeasureString([65]);
            Assert.That(rect, Is.EqualTo(new Rectangle(0, -2, 2, 2)));
        }

        [Test]
        public void MeasureString_ThrowsIfNoDeviceWidth()
        {
            var glyph = new BdfGlyph("A", 65, new bool[1, 1])
            {
                Geometry = new BdfGeometry { DeviceSize = null },
                BoundingBox = new BdfBoundingBox { Width = 1, Height = 1, XOffset = 0, YOffset = 0 },
            };

            var font = CreateTestFont(
                glyphs: [glyph],
                geometry: new BdfGeometry { DeviceSize = null }
            );

            Assert.Throws<InvalidOperationException>(() => font.MeasureString([65]));
        }

        [Test]
        public void RenderBitmap_Enumerable_ReturnsExpectedArray()
        {
            var font = CreateTestFont();
            var bitmap = font.RenderBitmap([65]);

            Assert.Multiple(() =>
            {
                Assert.That(bitmap[0, 0], Is.True);
                Assert.That(bitmap[0, 1], Is.False);
                Assert.That(bitmap[1, 0], Is.False);
                Assert.That(bitmap[1, 1], Is.True);
            });
        }

        [Test]
        public void RenderBitmap_ThrowsIfNoDeviceWidth()
        {
            var glyph = new BdfGlyph("A", 65, new bool[1, 1])
            {
                Geometry = new BdfGeometry { DeviceSize = null },
                BoundingBox = new BdfBoundingBox { Width = 1, Height = 1, XOffset = 0, YOffset = 0 }
            };

            var font = CreateTestFont(
                glyphs: [glyph],
                geometry: new BdfGeometry { DeviceSize = null }
            );

            Assert.Throws<InvalidOperationException>(() => font.RenderBitmap([65]));
        }

        [Test]
        public void RenderBitmap_String_ReturnsExpectedArray()
        {
            var font = CreateTestFont();
            var bitmap = font.RenderBitmap("A");
            Assert.Multiple(() =>
            {
                Assert.That(bitmap[0, 0], Is.True);
                Assert.That(bitmap[0, 1], Is.False);
                Assert.That(bitmap[1, 0], Is.False);
                Assert.That(bitmap[1, 1], Is.True);
            });
        }
    }
}
