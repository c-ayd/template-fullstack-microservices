using AuthService.Persistence.Converters;

namespace AuthService.Test.Unit.Persistence.Converters
{
    public class ToLowerConverterTest
    {
        private readonly ToLowerConverter _converter = new ToLowerConverter();

        [Fact]
        public void ToLowerConverter_WhenGivenStringHasUpperCases_ShouldConvertUpperCasesToLowerCases()
        {
            // Arrange
            var str = "ABC";

            // Act
            var convertedStr = (string?)_converter.ConvertToProvider.Invoke(str);

            // Assert
            Assert.Equal(str.ToLower(), convertedStr);
        }

        [Fact]
        public void ToLowerConverter_WhenGivenStringIsNull_ShouldKeepNull()
        {
            // Act
            var convertedStr = (string?)_converter.ConvertToProvider.Invoke(null);

            // Assert
            Assert.Null(convertedStr);
        }

        [Fact]
        public void ToLowerConverter_WhenReceivedString_ShouldKeepSame()
        {
            // Arrange
            var str = "abc";

            // Act
            var convertedStr = (string?)_converter.ConvertFromProvider.Invoke(str);

            // Assert
            Assert.Equal(str, convertedStr);
        }
    }
}
