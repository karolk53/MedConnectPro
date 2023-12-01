using API.Helpers;
using FluentAssertions;

namespace APITests
{
    public class ValidatorTests
    {
        [Theory]
        [InlineData("6885250")]
        [InlineData("3217401")] 
        [InlineData("2861447")] 
        public void ValidatePWZ_ForGivenPwz_ReturnTrue(string pwz)
        {
            //aset
            var result = Validators.ValidatePWZ(pwz);

            //assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("6885251")]
        [InlineData("3217431")] 
        [InlineData("2861347")]  
        public void ValidatePWZ_ForGivenPwz_ReturnFalse(string pwz)
        {
            //aset
            var result = Validators.ValidatePWZ(pwz);

            //assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("41013092291")]
        [InlineData("04222158639")]
        [InlineData("95111620076")]
        public void ValidatePesel_ForGivenPesel_RetrunTrue(string pesel)
        {
            //aset
            var result = Validators.ValidatePesel(pesel);

            //assert
            result.Should().BeTrue();
        }


        [Theory]
        [InlineData("41013492291")]
        [InlineData("04223158639")]
        [InlineData("95154620076")]
        public void ValidatePesel_ForGivenPesel_RetrunFalse(string pesel)
        {
            //aset
            var result = Validators.ValidatePesel(pesel);

            //assert
            result.Should().BeFalse();
        }

    }
}