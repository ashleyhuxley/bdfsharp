namespace ElectricFox.BdfSharp.UnitTests
{
    public class BdfGlyphLoaderTests
    {
        private string _name = "test";

        [Test]
        public void GetGlyph_NoBBX_ThrowsException()
        {
            var loader = new BdfGlyphLoader(_name);
            var ex = Assert.Throws<BdfLoadException>(() => loader.GetGlyph());
            Assert.That(ex.Message, Is.EqualTo("Glyph 'test' missing BBX"));
        }

        [Test]
        public void ProcessLine_InvalidData_ThrowsException()
        {
            // Arrange
            var loader = new BdfGlyphLoader(_name);
            var bdfFile = File.ReadAllLines("Assets/test.bdf");
            for (int i = 10; i < 16; i++)
            {
                loader.ProcessLine(bdfFile[i]);
            }

            // Act & Assert
            var ex = Assert.Throws<BdfLoadException>(() => loader.ProcessLine("$*()not hex data!"));
            Assert.That(ex.Message, Is.EqualTo("Invalid bitmap data"));
        }

        [Test]
        public void GetGlyph_ValidData_ReturnsGlyph()
        {
            // Arrange
            var loader = new BdfGlyphLoader(_name);
            var bdfFile = File.ReadAllLines("Assets/test.bdf");
            for (int i = 10; i < 39; i++)
            {
                loader.ProcessLine(bdfFile[i]);
            }

            // Act
            var glyph = loader.GetGlyph();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(glyph.Name, Is.EqualTo(_name));
                Assert.That(glyph.Encoding, Is.EqualTo(106));
                Assert.That(glyph.Width, Is.EqualTo(9));
                Assert.That(glyph.Height, Is.EqualTo(22));

                Assert.That(glyph.BoundingBox.XOffset, Is.EqualTo(-2));
                Assert.That(glyph.BoundingBox.YOffset, Is.EqualTo(-6));
                Assert.That(glyph.BoundingBox.Width, Is.EqualTo(9));
                Assert.That(glyph.BoundingBox.Height, Is.EqualTo(22));
                
                Assert.That(glyph.Geometry.DeviceSize, Is.Not.Null);
                Assert.That(glyph.Geometry.ScalableSize, Is.Not.Null);

                Assert.That(glyph.Geometry.ScalableSize.Value.Width, Is.EqualTo(355));
                Assert.That(glyph.Geometry.ScalableSize.Value.Height, Is.EqualTo(0));

                Assert.That(glyph.Geometry.DeviceSize.Value.Width, Is.EqualTo(8));
                Assert.That(glyph.Geometry.DeviceSize.Value.Height, Is.EqualTo(0));
            });
        }
    }
}
