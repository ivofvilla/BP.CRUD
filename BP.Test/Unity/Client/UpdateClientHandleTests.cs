using BP.CRUD.Domain.Commands.Client.Update;
using BP.CRUD.Repository.Interfaces;
using FluentValidation.Results;
using FluentValidation;
using Moq;

namespace BP.Test.Unity.Client
{
    public class UpdateClientHandleTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnTrue()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<UpdateClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var updateClientHandle = new UpdateClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var updateClientCommand = new UpdateClientCommand
            {
                Name = "Usuário teste",
                Email = "usuario@teste.com",
                Phones = new List<UpdatePhoneCommand> { new UpdatePhoneCommand { Type = true, DDD = 12, Number = "987654321" } }
            };
            updateClientCommand.SetId(clientId);

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CRUD.Domain.Models.Client { Id = clientId }); 

            // Act
            var result = await updateClientHandle.Handle(updateClientCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<UpdateClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult { Errors = { new ValidationFailure("Id", "O Código é obrigatório.") } });

            var updateClientHandle = new UpdateClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var updateClientCommand = new UpdateClientCommand(); 

            // Act
            var result = await updateClientHandle.Handle(updateClientCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            clientRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<CRUD.Domain.Models.Client>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ClientNotFound_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<UpdateClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var updateClientHandle = new UpdateClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var updateClientCommand = new UpdateClientCommand
            {
                Name = "Usuário teste",
                Email = "usuario@teste.com",
                Phones = new List<UpdatePhoneCommand> { new UpdatePhoneCommand { Type = true, DDD = 12, Number = "987654321" } }
            };
            updateClientCommand.SetId(clientId);

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CRUD.Domain.Models.Client)null); 

            // Act
            var result = await updateClientHandle.Handle(updateClientCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
