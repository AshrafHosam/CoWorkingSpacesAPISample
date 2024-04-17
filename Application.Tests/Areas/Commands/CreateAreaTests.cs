using Application.Contracts.Repos;
using Application.Features.Areas.Commands.CreateArea;
using Domain.Entities;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Application.Tests.Areas.Commands
{
    [ExcludeFromCodeCoverage]
    public class CreateAreaTests
    {
        [Fact]
        public async Task Handle_ValidInput_CreatesAreaSuccessfully()
        {
            // Arrange
            var request = new CreateAreaCommand { /* valid input data */ };
            var mockAreaTypeRepo = new Mock<IAreaTypeRepo>();
            var mockBranchRepo = new Mock<IBranchRepo>();
            var mockAreaRepo = new Mock<IAreaRepo>();
            mockAreaTypeRepo.Setup(repo => repo.AnyAsync(request.AreaTypeId)).ReturnsAsync(true);
            mockBranchRepo.Setup(repo => repo.AnyAsync(request.BranchId)).ReturnsAsync(true);
            mockAreaRepo.Setup(repo => repo.AddAsync(It.IsAny<Area>()))
                .ReturnsAsync(new Area
                {
                    Id = Guid.NewGuid()
                });

            var handler = new CreateAreaCommandHandler(mockAreaRepo.Object, mockBranchRepo.Object, mockAreaTypeRepo.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.StatusCode.ShouldBe(200);
            result.Data.ShouldNotBeNull();
        }

        [Fact]
        public async Task Handle_AreaTypeDoesNotExist_ReturnsNotFoundApiResponse()
        {
            // Arrange
            var request = new CreateAreaCommand { /* area type that does not exist */ };
            var mockAreaTypeRepo = new Mock<IAreaTypeRepo>();
            mockAreaTypeRepo.Setup(repo => repo.AnyAsync(request.AreaTypeId)).ReturnsAsync(false);
            var handler = new CreateAreaCommandHandler(null, null, mockAreaTypeRepo.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccessStatusCode.ShouldBeFalse();
            result.Errors.First().ShouldBe("Area Type Not Found");
        }
        [Fact]
        public async Task Handle_BranchDoesNotExist_ReturnsNotFoundApiResponse()
        {
            // Arrange
            var request = new CreateAreaCommand { /* branch that does not exist */ };
            var mockAreaTypeRepo = new Mock<IAreaTypeRepo>();
            var mockBranchRepo = new Mock<IBranchRepo>();
            mockBranchRepo.Setup(repo => repo.AnyAsync(request.BranchId)).ReturnsAsync(false);
            mockAreaTypeRepo.Setup(repo => repo.AnyAsync(request.AreaTypeId)).ReturnsAsync(true);
            var handler = new CreateAreaCommandHandler(null, mockBranchRepo.Object, mockAreaTypeRepo.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccessStatusCode.ShouldBeFalse();
            result.Errors.First().ShouldBe("Branch Not Found");
        }

        [Fact]
        public async Task Handle_AreaCreationFailure_ReturnsBadRequestApiResponse()
        {
            // Arrange
            var request = new CreateAreaCommand { /* valid input data */ };
            var mockAreaTypeRepo = new Mock<IAreaTypeRepo>();
            var mockBranchRepo = new Mock<IBranchRepo>();
            var mockAreaRepo = new Mock<IAreaRepo>();
            mockAreaTypeRepo.Setup(repo => repo.AnyAsync(request.AreaTypeId)).ReturnsAsync(true);
            mockBranchRepo.Setup(repo => repo.AnyAsync(request.BranchId)).ReturnsAsync(true);
            mockAreaRepo.Setup(repo => repo.AddAsync(It.IsAny<Area>())).ReturnsAsync(null as Area);

            var handler = new CreateAreaCommandHandler(mockAreaRepo.Object, mockBranchRepo.Object, mockAreaTypeRepo.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccessStatusCode.ShouldBeFalse();
            result.Errors.First().ShouldBe("Area Couldn't be Created");
        }
    }
}
