using BP.CRUD.Domain.Queries.Client.Get;
using BP.CRUD.Repository.Interfaces;
using Moq;

namespace BP.Test.Unity.Client
{
    public class GetClientHandleTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnResult()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var getClientHandle = new GetClientHandle(clientRepositoryMock.Object);
            var getClientQuery = new GetClientQuery { Id = Guid.NewGuid() };

            var clients = new List<CRUD.Domain.Models.Client>
            {
                new CRUD.Domain.Models.Client { Id = getClientQuery.Id.Value, Name = "Usuário teste", Email = "usuario@mail.com" }
            };

            clientRepositoryMock.Setup(repo => repo.GetAsync(
                It.IsAny<Guid?>(),
                It.IsAny<string?>(),
                It.IsAny<string?>(),
                It.IsAny<bool?>(),
                It.IsAny<int?>(),
                It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(clients);

            // Act
            var result = await getClientHandle.Handle(getClientQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Clients);
            Assert.Single(result.Clients);
            Assert.Equal(getClientQuery.Id, result.Clients.First().Id);
        }

        [Fact]
        public async Task Handle_EmptyQuery_ShouldReturnEmptyResult()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var getClientHandle = new GetClientHandle(clientRepositoryMock.Object);
            var getClientQuery = new GetClientQuery();

            clientRepositoryMock.Setup(repo => repo.GetAsync(
                                        It.IsAny<Guid?>(),
                                        It.IsAny<string?>(),
                                        It.IsAny<string?>(),
                                        It.IsAny<bool?>(),
                                        It.IsAny<int?>(),
                                        It.IsAny<string?>(),
                                        It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(new List<CRUD.Domain.Models.Client>());

            // Act
            var result = await getClientHandle.Handle(getClientQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Clients);
            Assert.Empty(result.Clients);
        }
    }
}
