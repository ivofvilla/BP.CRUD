using BP.CRUD.Domain.Queries.Client.Gets;
using BP.CRUD.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Test.Unity.Client
{
    public class GetClientsHandleTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnResult()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var getClientsHandle = new GetClientsHandle(clientRepositoryMock.Object);
            var getClientsQuery = new GetClientsQuery();

            var clients = new List<CRUD.Domain.Models.Client>
        {
            new CRUD.Domain.Models.Client { Id = Guid.NewGuid(), Name = "Usuário teste", Email = "usuario@email.com" },
            new CRUD.Domain.Models.Client { Id = Guid.NewGuid(), Name = "Testando Usuario", Email = "teste@gmail.com" }
        };

            clientRepositoryMock.Setup(repo => repo.GetsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(clients);

            // Act
            var result = await getClientsHandle.Handle(getClientsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Clients);
            Assert.Equal(clients.Count, result.Clients.Count());
        }

        [Fact]
        public async Task Handle_EmptyQuery_ShouldReturnEmptyResult()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var getClientsHandle = new GetClientsHandle(clientRepositoryMock.Object);
            var getClientsQuery = new GetClientsQuery();

            clientRepositoryMock.Setup(repo => repo.GetsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<CRUD.Domain.Models.Client>());

            // Act
            var result = await getClientsHandle.Handle(getClientsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Clients);
            Assert.Empty(result.Clients);
        }
    }
}
