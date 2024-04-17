using Application.Contracts.Repos;
using Application.Features.Areas.Queries.GetAreaTypes;
using Domain.Entities;
using Moq;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Application.Tests.Areas.Queries
{
    [ExcludeFromCodeCoverage]
    public class GetAreaTypesTests
    {
        [Fact]
        public async Task Handle_ReturnsApiResponseWithMappedAreaTypes()
        {
            // Arrange
            var mockAreaRepo = new Mock<IAreaRepo>();
            var areaTypes = new List<AreaType>();
            mockAreaRepo.Setup(repo => repo.GetAreaTypesAsync()).ReturnsAsync(areaTypes);
            var handler = new GetAreaTypesQueryHandler(mockAreaRepo.Object, MapperConfig.GetMapper());
            var query = new GetAreaTypesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.Data.ShouldNotBeNull();
            result.Data.Count.ShouldBe(areaTypes.Count);
        }
    }
}
