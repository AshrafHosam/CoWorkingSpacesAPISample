using Application.Response;
using FluentValidation.Results;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Application.Tests.Responses
{
    [ExcludeFromCodeCoverage]
    public class ApiResponseTests
    {
        [Fact]
        public void SuccessApiResponse()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetSuccessApiResponse(1);

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeTrue();
            apiResponse.StatusCode.ShouldBe(200);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
            apiResponse.Data.ShouldBe(1);
            apiResponse.Data.GetType().ShouldBe(typeof(int));
        }

        [Fact]
        public void BadRequestApiResponse()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetBadRequestApiResponse(error: "error");

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(400);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
            apiResponse.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public void NotFoundApiResponse()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetNotFoundApiResponse(error: "error");

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(404);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
            apiResponse.Errors.Count.ShouldBe(1);
        }

        [Fact]
        public void BadRequestApiResponse_MultipleMessages()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetBadRequestApiResponse(new List<string> { "error1", "error2" });

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(400);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
            apiResponse.Errors.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public void NotFoundApiResponse_MultipleMessages()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetNotFoundApiResponse(new List<string> { "error1", "error2" });

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(404);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
            apiResponse.Errors.Count.ShouldBeGreaterThan(1);
        }

        [Fact]
        public void NoContentApiResponse()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetNoContentApiResponse();

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeTrue();
            apiResponse.StatusCode.ShouldBe(204);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
            apiResponse.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void ConflictApiResponse()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetConflictApiResponse(1);

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(409);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.Conflict);
            apiResponse.Data.GetType().ShouldBe(typeof(int));
            apiResponse.Data.ShouldBe(1);
        }

        [Fact]
        public void BadRequestResponseFromValidationResult()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<int>.GetApiResponseFromInvalidValidationResult(new ValidationResult
            {
                Errors = new List<ValidationFailure> {
                    new ValidationFailure
                    {
                        ErrorMessage = "error1"
                    },
                    new ValidationFailure
                    {
                        ErrorMessage = "error2"
                    }
                }
            });

            // Assert
            apiResponse.ShouldNotBeNull();
            apiResponse.IsSuccessStatusCode.ShouldBeFalse();
            apiResponse.StatusCode.ShouldBe(400);
            apiResponse.HttpStatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
            apiResponse.Errors.Count.ShouldBeGreaterThan(1);
        }
    }
}
