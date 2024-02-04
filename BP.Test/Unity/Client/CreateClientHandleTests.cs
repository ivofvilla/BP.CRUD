using BP.CRUD.Domain.Commands.Client.Create;
using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace BP.Test.Unity.Client
{
    public class CreateClientHandleTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnTrue()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<CreateClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var createClientHandle = new CreateClientHandle(clientRepositoryMock.Object, validatorMock.Object);

            // Act
            var result = await createClientHandle.Handle(new CreateClientCommand
            {
                Name = "Usuário teste",
                Email = "usuario@email.com",
                Phones = new List<PhoneCommand>{
                            new PhoneCommand { Type = true, DDD = 123, Number = "123456789" },
                            }
            }, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnFalse()
        {
            // Arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var validatorMock = new Mock<IValidator<CreateClientCommand>>();
            validatorMock.Setup(validator => validator.ValidateAsync(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("PropertyName", "ErrorMessage") }));

            var createClientHandle = new CreateClientHandle(clientRepositoryMock.Object, validatorMock.Object);

            // Act
            var result = await createClientHandle.Handle(new CreateClientCommand(), CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
