using NUnit.Framework;
using NUnitRetry.ReqnrollPlugin;
using Reqnroll;

namespace NUnitRetry.Tests.Steps
{
    [Binding]
    public class RetryTestSteps
    {
        private readonly RetryConfiguration _configuration;
        private readonly IReqnrollOutputHelper _outputHelper;

        public static int RetryCount { get; set; }

        public RetryTestSteps(RetryConfiguration configuration, IReqnrollOutputHelper outputHelper)
        {
            _configuration = configuration;
            _outputHelper = outputHelper;
        }

        [When(@"I increment the default retry count")]
        public void WhenIIncrementTheDefaultRetryCount()
        {
            RetryCount++;
            _outputHelper.WriteLine($"[Retry Count]: {RetryCount}");
        }

        [When(@"I increment the retry count")]
        public void WhenIIncrementTheRetryCount()
        {
            RetryCount++;
            _outputHelper.WriteLine($"[Retry Count]: {RetryCount}");
        }

        [Then("the retry result should be {int}")]
        public void ThenTheRetryResultShouldBe(int expected)
        {
            Assert.That(RetryCount, Is.EqualTo(expected));
        }

        [Then(@"the retry result should be equal to config")]
        public void ThenTheRetryResultShouldBeEqualToConfig()
        {
            Assert.That(RetryCount, Is.EqualTo(_configuration.MaxRetries));
        }

        [Then(@"the retry result should be equal to 1 or to config value")]
        public void ThenTheRetryResultShouldBeEqualToOneOr()
        {
            if (_configuration.ApplyGlobally)
                Assert.That(RetryCount, Is.EqualTo(_configuration.MaxRetries));
            else
                Assert.That(1, Is.EqualTo(RetryCount));
        }

        [When(@"I increment the no retry count")]
        public void WhenIIncrementTheNoRetryCount()
        {

        }

    }
}
