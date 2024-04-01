using FluentAssertions;

using Moq;

using WSL2ConfigurationEditor.Core.IO;
using WSL2ConfigurationEditor.Core.Parsers;
using WSL2ConfigurationEditor.Core.Validations;
using WSL2ConfigurationEditor.Core.Verifiers;

namespace WSL2ConfigurationEditor.Core.UnitTests
{
    [TestFixture]
    public class IntegrityHandlerUnitTests
    {
        private readonly Mock<IFileIO> _ioStub = new();
        private readonly Mock<IHashService> _hashServiceStub = new();
        private readonly Mock<IStringTransform> _stringTransformStub = new();
        private readonly Mock<IValidator> _validatorStub = new();

        [Test]
        public async Task CreateIntegrityTokenAsync_WhenCancellationSignalled_ShouldThrowOperationCancelledException()
        {
            using var tokenSource = new CancellationTokenSource();
            await tokenSource.CancelAsync();
            var instance = CreateInstance();

            var act = () => instance.CreateIntegrityTokenAsync(It.IsAny<IEnumerable<string>>(), tokenSource.Token);

            await act.Should().ThrowExactlyAsync<OperationCanceledException>();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldCallToValidateLineCollectionEmptyness()
        {
            var lines = CreateRandomStrings(10);
            _validatorStub.Setup(x => x.ThrowIfCollectionIsEmpty(lines))
                .Verifiable();
            var instance = CreateInstance();

            await instance.CreateIntegrityTokenAsync(lines);

            _validatorStub.Verify();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_WhenLineCollectionIsEmpty_ShouldLetTheExceptionBubbleUp()
        {
            ResetStubs();
            _validatorStub.Setup(x => x.ThrowIfCollectionIsEmpty(It.IsAny<IEnumerable<string>>()))
                .Throws<ArgumentException>();
            var instance = CreateInstance();

            var act = () => instance.CreateIntegrityTokenAsync(It.IsAny<IEnumerable<string>>());

            await act.Should().ThrowExactlyAsync<ArgumentException>();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldCallMethodToTransformLinesIntoByteArray()
        {
            ResetStubs();
            var lines = CreateRandomStrings(10);
            _stringTransformStub.Setup(x => x.CreateByteArrayFromStringCollection(lines))
                .Verifiable();
            var instance = CreateInstance();

            await instance.CreateIntegrityTokenAsync(lines);

            _stringTransformStub.Verify();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldReturnWhatByteArrayToHexConverterReturns()
        {
            ResetStubs();
            var strings = CreateRandomStrings(10);
            var randomStr = Guid.NewGuid().ToString();
            _stringTransformStub.Setup(x => x.CreateHexStringFromByteArray(It.IsAny<byte[]>()))
                .Returns(randomStr);
            var instance = CreateInstance();

            var token = await instance.CreateIntegrityTokenAsync(strings);

            token.Should().BeEquivalentTo(randomStr);
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldCallHashDataAsyncWith_UTF8ByteArrayRepresntationOfStrings()
        {
            ResetStubs();
            var bytes = CreateRandomByteArray();
            _stringTransformStub.Setup(x => x.CreateByteArrayFromStringCollection(It.IsAny<IEnumerable<string>>()))
                .Returns(bytes)
                .Verifiable();
            _hashServiceStub.Setup(x => x.HashDataAsync(bytes, It.IsAny<CancellationToken>()))
                .Verifiable();
            var instance = CreateInstance();

            var result = await instance.CreateIntegrityTokenAsync(It.IsAny<IEnumerable<string>>());

            _stringTransformStub.Verify();
            _hashServiceStub.Verify();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldCallByteToHexConvet_WithHashValueAsInput()
        {
            ResetStubs();
            var bytes = CreateRandomByteArray();
            _hashServiceStub.Setup(x => x.HashDataAsync(It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bytes)
                .Verifiable();
            _stringTransformStub.Setup(x => x.CreateHexStringFromByteArray(bytes))
                .Verifiable();
            var instance = CreateInstance();

            var token = await instance.CreateIntegrityTokenAsync(It.IsAny<IEnumerable<string>>());

            _hashServiceStub.Verify();
            _stringTransformStub.Verify();
        }

        [Test]
        public async Task CreateIntegrityTokenAsync_ShouldPassCancellationTokenToAsyncCalls()
        {
            ResetStubs();
            using var tokenSource = new CancellationTokenSource();
            _hashServiceStub.Setup(x => x.HashDataAsync(It.IsAny<byte[]>(), tokenSource.Token))
                .Verifiable();
            var instance = CreateInstance();

            var result = await instance.CreateIntegrityTokenAsync(It.IsAny<IEnumerable<string>>(), tokenSource.Token);

            _hashServiceStub.Verify();
        }

        [Test]
        public async Task ValidateFileIntegrityAsync_WhenCancellationSignalled_ShouldThrowOperationCancelledException()
        {
            ResetStubs();
            using var tokenSource = new CancellationTokenSource();
            await tokenSource.CancelAsync();
            var instance = CreateInstance();

            var act = () => instance.ValidateFileIntegrityAsync(tokenSource.Token);

            await act.Should().ThrowExactlyAsync<OperationCanceledException>();
        }

        [Test]
        public async Task ValidateFileIntegrityAsync_ShouldPassCancellationTokenToAsyncCalls()
        {
            ResetStubs();
            using var tokenSource = new CancellationTokenSource();
            _ioStub.Setup(x => x.ReadLinesFromFileAsync(tokenSource.Token))
                .Verifiable();
            _hashServiceStub.Setup(x => x.CompareDataWithHashAsync(It.IsAny<byte[]>(), It.IsAny<byte[]>(), tokenSource.Token))
                .Verifiable();
            var instance = CreateInstance();

            await instance.ValidateFileIntegrityAsync(tokenSource.Token);

            _ioStub.Verify();
            _hashServiceStub.Verify();
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task ValidateFileIntegrityAsync_ShouldReturnWhetherHashesMatchOrMismatch(bool isMatching)
        {
            ResetStubs();
            _hashServiceStub.Setup(x => x.CompareDataWithHashAsync(It.IsAny<byte[]>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(isMatching);
            var instance = CreateInstance();

            var result = await instance.ValidateFileIntegrityAsync();

            result.Should().Be(isMatching);
        }

        private FileIntegrityHandlerImpl CreateInstance()
        {
            return new(_ioStub.Object, _hashServiceStub.Object, _stringTransformStub.Object, _validatorStub.Object);
        }

        private void ResetStubs()
        {
            _ioStub.Reset();
            _hashServiceStub.Reset();
            _stringTransformStub.Reset();
            _validatorStub.Reset();
        }

        private static List<string> CreateRandomStrings(int count)
        {
            var result = new List<string>();

            for(var i = 0; i < count; i++)
            {
                var str = Guid.NewGuid().ToString();
                result.Add(str);
            }

            return result;
        }

        private static byte[] CreateRandomByteArray()
        {
            return Guid.NewGuid().ToByteArray();
        }
    }
}
