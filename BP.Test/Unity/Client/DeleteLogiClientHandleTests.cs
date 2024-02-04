using BP.CRUD.Domain.Commands.Client.DeleteLogic;
using BP.CRUD.Repository.Interfaces;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Test.Unity.Client
{
    public class DeleteClientLogicHandleTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnTrue()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientLogicCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientLogicCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var deleteClientLogicHandle = new DeleteLogiClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var deleteClientLogicCommand = new DeleteClientLogicCommand { Id = clientId };

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CRUD.Domain.Models.Client { Id = clientId }); 

            // Act
            var result = await deleteClientLogicHandle.Handle(deleteClientLogicCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientLogicCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientLogicCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult { Errors = { new ValidationFailure("Id", "O Código é obrigatório.") } });

            var deleteClientLogicHandle = new DeleteLogiClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var deleteClientLogicCommand = new DeleteClientLogicCommand(); 

            // Act
            var result = await deleteClientLogicHandle.Handle(deleteClientLogicCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ClientNotFound_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<DeleteClientLogicCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<DeleteClientLogicCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var deleteClientLogicHandle = new DeleteLogiClientHandle(clientRepositoryMock.Object, validatorMock.Object);
            var clientId = Guid.NewGuid();
            var deleteClientLogicCommand = new DeleteClientLogicCommand { Id = clientId };

            clientRepositoryMock.Setup(repo => repo.GetByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((CRUD.Domain.Models.Client)null);

            // Act
            var result = await deleteClientLogicHandle.Handle(deleteClientLogicCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
