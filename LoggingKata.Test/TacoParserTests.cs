using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            // TODO: Complete Something, if anything

            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var test = new TacoParser();

            //Act
            var rest = test.Parse(line);
            var actual = rest.Location.Longitude;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var test = new TacoParser();

            //Act
            var rest = test.Parse(line);
            var actual = rest.Location.Latitude;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", "Taco Bell Acwort...")]
        public void ShouldParseString(string line, string expected)
        {
            //Arrange
            var test = new TacoParser();

            //Act
            var rest = test.Parse(line);
            var actual = rest.Name.Trim();

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
