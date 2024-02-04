using BP.CRUD.Domain.Commands.Client.Delete;
using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;


namespace BP.Test.Unity.Client
{
    public class DeleteClientHandleTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnTrue()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var deleteClientHandle = new DeleteClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var deleteClientCommand = new DeleteClientCommand { Id = clientId };

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CRUD.Domain.Models.Client { Id = clientId });

            // Act
            var result = await deleteClientHandle.Handle(deleteClientCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult { Errors = { new ValidationFailure("Id", "O Código é obrigatório.") } });

            var deleteClientHandle = new DeleteClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var deleteClientCommand = new DeleteClientCommand();

            // Act
            var result = await deleteClientHandle.Handle(deleteClientCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ClientNotFound_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var deleteClientHandle = new DeleteClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var deleteClientCommand = new DeleteClientCommand { Id = clientId };

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CRUD.Domain.Models.Client)null);

            // Act
            var result = await deleteClientHandle.Handle(deleteClientCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
