using AuthService.Persistence.Converters;

namespace AuthService.Test.Unit.Persistence.Converters
{
    public class EnumConverterTest
    {
        private enum ETestEnum
        {
            TestName1 = 1,
            TestName2 = 2
        }

        private readonly EnumConverter<ETestEnum> _converter = new EnumConverter<ETestEnum>();

        [Fact]
        public void EnumConverter_WhenGivenEnumValueIsWithinRange_ShouldConvertValueToString()
        {
            // Arrange
            var strValue1 = ETestEnum.TestName1.ToString();
            var strValue2 = ETestEnum.TestName2.ToString();

            // Act
            var convertedStr1 = (string)_converter.ConvertToProvider.Invoke(ETestEnum.TestName1)!;
            var convertedStr2 = (string)_converter.ConvertToProvider.Invoke(ETestEnum.TestName2)!;

            // Assert
            Assert.Equal(strValue1, convertedStr1);
            Assert.Equal(strValue2, convertedStr2);
        }

        [Fact]
        public void EnumConverter_WhenReceivedStringValueIsWithinRange_ShouldConvertValueToEnum()
        {
            // Arrange
            var strValue1 = ETestEnum.TestName1.ToString();
            var strValue2 = ETestEnum.TestName2.ToString();

            // Act
            var convertedEnum1 = (ETestEnum)_converter.ConvertFromProvider.Invoke(strValue1)!;
            var convertedEnum2 = (ETestEnum)_converter.ConvertFromProvider.Invoke(strValue2)!;

            // Assert
            Assert.Equal(ETestEnum.TestName1, convertedEnum1);
            Assert.Equal(ETestEnum.TestName2, convertedEnum2);
        }
    }
}