using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using LambdaAuthorizer;

namespace LambdaAuthorizer.Tests;

public class AuthorizerTest {
    [Fact]
    public async Task ShouldDenyInvalidAuthRequest() {
        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Authorizer();
        var context = new TestLambdaContext();

        var request = new ApiGatewayRequest {
            Type = "REQUEST",
            HttpMethod = "GET",
            Headers = new Headers {
                Username = "testuser",
                Password = "testpassword"
            }
        };

        var policyDocPackage = await function.RequestHandler(request, context);
        Assert.Contains("Deny", policyDocPackage);
    }
}
