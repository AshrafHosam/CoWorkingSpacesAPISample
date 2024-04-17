using Application.Contracts.Repos;
using Application.Features.Areas.Queries.GetArea;
using Bogus;
using Domain.Entities;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Application.Tests.Areas.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetAreaTests
    {
        [Fact]
        public async Task Handle_AreaFound_ReturnsSuccessApiResponse()
        {
            // Arrange
            var mockAreaRepo = new Mock<IAreaRepo>();

            var area = new Area
            {
                Id = Guid.NewGuid(),
                Name = "Area 1"
            };

            mockAreaRepo.Setup(m => m.GetAreaPricingPlansIncluded(It.IsAny<Guid>())).ReturnsAsync(area);
            var handler = new GetAreaQueryHandler(mockAreaRepo.Object, MapperConfig.GetMapper());

            var request = new GetAreaQuery
            {
                AreaId = area.Id
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.ShouldBe(200);
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
        }

        [Fact]
        public async Task Handle_AreaNotFound_ReturnsNotFoundApiResponse()
        {
            // Arrange
            var mockAreaRepo = new Mock<IAreaRepo>();

            mockAreaRepo.Setup(m => m.GetAreaPricingPlansIncluded(It.IsAny<Guid>())).ReturnsAsync(null as Area);

            var handler = new GetAreaQueryHandler(mockAreaRepo.Object, MapperConfig.GetMapper());

            var request = new GetAreaQuery
            {
                AreaId = Guid.NewGuid()
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.ShouldBe(404);
            result.IsSuccessStatusCode.ShouldBeFalse();
            result.Data.ShouldBeNull();
            result.Errors.Count.ShouldBeGreaterThanOrEqualTo(1);
        }
    }
}
