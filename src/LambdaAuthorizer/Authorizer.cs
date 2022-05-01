using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace LambdaAuthorizer;

public class Authorizer {
    private HttpClient client = new HttpClient();

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="auth"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> RequestHandler(ApiGatewayRequest request, ILambdaContext context) {
        try {
            var headers = request.Headers;
            context.Logger.LogLine($"Username: {headers.Username}, Password: ********");
            return await GeneratePolicyDocPackage(headers.Username, headers.Password);
        } catch(Exception ex) {
            context.Logger.LogLine($"Exception: {ex.Message}");
            string username = request.Headers?.Username ?? "Unknown";
            var policyDocPackage = GetPolicyDocPackage(username, AuthType.Deny);
            return JsonConvert.SerializeObject(policyDocPackage);
        }
    }

    private async Task<HttpResponseMessage> LoginToParse(string username, string password) {
        var body = new FormUrlEncodedContent(new Dictionary<string, string> {
            {"username", username},
            {"password", password},
            {"accountId", "Z2oIUVW3Vy"},
            {"origConfig", "NewDemoTest"},
            {"_ApplicationId", "zquCagFDNXC8ipCFPjRjV8xw7y4Jik"}
        });

        var url = "https://parse-virbela-intern.herokuapp.com/parse/functions/guiLogIn";
        return await client.PostAsync(url, body);
    }

    private async Task<string> GeneratePolicyDocPackage(string username, string password) {
        var response = await LoginToParse(username, password);
        var auth = AuthType.Deny;
        if (response.StatusCode == HttpStatusCode.OK) {
            auth = AuthType.Allow;
        }

        var policyDocPackage = GetPolicyDocPackage(username, auth);
        return JsonConvert.SerializeObject(policyDocPackage);
    }

    private PolicyDocPackage GetPolicyDocPackage(string username, AuthType auth) {
        var resource = "arn:aws:execute-api:us-east-2:656601024875:x11x9w39f8/*/*";
        return new PolicyDocPackage(username, resource, auth);
    }
}
