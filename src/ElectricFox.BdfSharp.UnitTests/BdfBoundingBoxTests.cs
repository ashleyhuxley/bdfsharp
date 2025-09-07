namespace ElectricFox.BdfSharp.UnitTests
{
    public class BdfBoundingBoxTests
    {
        [Test]
        public void Parse_ValidValues_CreatesValidBoundingBox()
        {
            // Arrange
            var x = "10";
            var y = "20";
            var width = "30";
            var height = "40";

            // Act
            var boundingBox = BdfBoundingBox.Parse(width, height, x, y);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(boundingBox.Width, Is.EqualTo(30));
                Assert.That(boundingBox.Height, Is.EqualTo(40));
                Assert.That(boundingBox.XOffset, Is.EqualTo(10));
                Assert.That(boundingBox.YOffset, Is.EqualTo(20));

            });
        }

        [Test]
        public void Parse_InvalidWidth_ThrowsBdfLoadException()
        {
            // Arrange
            var x = "10";
            var y = "20";
            var width = "A";
            var height = "40";

            // Act & Assert
            var ex = Assert.Throws<BdfLoadException>(() => BdfBoundingBox.Parse(width, height, x, y));

            Assert.That(ex.Message, Is.EqualTo("Invalid width in FONTBOUNDINGBOX specification"));
        }

        [Test]
        public void Parse_InvalidHeight_ThrowsBdfLoadException()
        {
            // Arrange
            var x = "10";
            var y = "20";
            var width = "30";
            var height = "B";

            // Act & Assert
            var ex = Assert.Throws<BdfLoadException>(() => BdfBoundingBox.Parse(width, height, x, y));
            
            Assert.That(ex.Message, Is.EqualTo("Invalid height in FONTBOUNDINGBOX specification"));
        }

        [Test]
        public void Parse_InvalidXOffset_ThrowsBdfLoadException()
        {
            // Arrange
            var x = "C";
            var y = "20";
            var width = "30";
            var height = "40";

            // Act & Assert
            var ex = Assert.Throws<BdfLoadException>(() => BdfBoundingBox.Parse(width, height, x, y));
            
            Assert.That(ex.Message, Is.EqualTo("Invalid X offset in FONTBOUNDINGBOX specification"));
        }

        [Test]
        public void Parse_InvalidYOffset_ThrowsBdfLoadException()
        {
            // Arrange
            var x = "10";
            var y = "D";
            var width = "30";
            var height = "40";

            // Act & Assert
            var ex = Assert.Throws<BdfLoadException>(() => BdfBoundingBox.Parse(width, height, x, y));
            
            Assert.That(ex.Message, Is.EqualTo("Invalid Y offset in FONTBOUNDINGBOX specification"));
        }
    }
}