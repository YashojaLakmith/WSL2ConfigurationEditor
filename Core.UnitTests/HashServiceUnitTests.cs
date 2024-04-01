using System.Security.Cryptography;

using FluentAssertions;

using WSL2ConfigurationEditor.Core.Verifiers;

namespace WSL2ConfigurationEditor.Core.UnitTests
{
    [TestFixture]
    public class HashServiceUnitTests
    {
        [Test]
        public async Task HashDataAsync_WithSameInput_ShouldProduceTheSameHash()
        {
            var randomData = CreateRandomBytes();
            var instance = CreateInstance();

            var hash1 = await instance.HashDataAsync(randomData);
            var hash2 = await instance.HashDataAsync(randomData);
            var result = hash1.SequenceEqual(hash2);

            result.Should().BeTrue();
        }

        [Test]
        public async Task HashDataAsync_WithEmtyDataArray_ShouldThrowArgumentException()
        {
            byte[] emptyArray = [];
            var instance = CreateInstance();
            
            var act = () => instance.HashDataAsync(emptyArray);

            await act.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task HashDataAsync_WhenCancellationSignalled_ShouldThrowOperationCancelledException()
        {
            var randomBytes = CreateRandomBytes();
            var instance = CreateInstance();
            using var tokenSource = new CancellationTokenSource();
            await tokenSource.CancelAsync();

            var act = () => instance.HashDataAsync(randomBytes, tokenSource.Token);

            await act.Should().ThrowExactlyAsync<OperationCanceledException>();
        }

        [Test]
        public async Task CompareDataWithHashAsync_WithSameDataAndItsHash_ShouldReturnTrue()
        {
            var randomBytes = CreateRandomBytes();
            var instance = CreateInstance();
            var hash = await instance.HashDataAsync(randomBytes);

            var result = await instance.CompareDataWithHashAsync(randomBytes, hash);

            result.Should().BeTrue();
        }

        [Test]
        public async Task CompareDataWithHashAsync_WithValidDataAndEmptyHash_ShouldThrowArgumentException()
        {
            var randomBytes = CreateRandomBytes();
            var instance = CreateInstance();
            byte[] emptyHash = [];

            var act = () => instance.CompareDataWithHashAsync(randomBytes, emptyHash);

            await act.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task CompareDataWithHashAsync_WithEmptyDataAndValidHash_ShouldThrowArgumentException()
        {
            byte[] emptyData = [];
            var randomHash = RandomNumberGenerator.GetBytes(64);
            var instance = CreateInstance();

            var act = () => instance.CompareDataWithHashAsync(emptyData, randomHash);

            await act.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task CompareDataWithHashAsync_WithCancellationSignalled_ShouldThrowOperationCancelledException()
        {
            var randomBytes = CreateRandomBytes();
            var instance = CreateInstance();
            var randomHash = RandomNumberGenerator.GetBytes(64);
            using var tokenSource = new CancellationTokenSource();
            await tokenSource.CancelAsync();

            var act = () => instance.CompareDataWithHashAsync(randomBytes, randomHash, tokenSource.Token);

            await act.Should().ThrowExactlyAsync<OperationCanceledException>();
        }

        private static IHashService CreateInstance()
        {
            return new HashServiceImpl();
        }

        private static byte[] CreateRandomBytes()
        {
            return Guid.NewGuid().ToByteArray();
        }
    }
}
