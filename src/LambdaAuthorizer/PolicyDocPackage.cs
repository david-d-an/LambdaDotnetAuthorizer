namespace LambdaAuthorizer;

public record PolicyDocPackage {
    public string principalId { get; init; }
    public PolicyDocument policyDocument { get; init; }

    public PolicyDocPackage(string username, string resource, AuthType auth) {
        this.principalId = username;
        this.policyDocument = new PolicyDocument(resource, auth);            
    }
}

public record PolicyDocument {
    public string Version { get; init; }
    public Statement[] Statement { get; init; }

    public PolicyDocument(string resource, AuthType auth) {
        this.Version = "2012-10-17";
        this.Statement = new Statement[] {
            new Statement(resource, auth)
        };
    }
}

public record Statement {
    public string Action { get; init; }
    public string[] Resource { get; init; }
    public string Effect { get; init; }

    public Statement(string resource, AuthType auth) {
        this.Action = "execute-api:Invoke";
        this.Resource = new string[] { resource };
        this.Effect = auth.ToString();
    }            
}


// var policyDocPackage = new {
//     principalId = username,
//     policyDocument = new {
//         Version = "2012-10-17", 
//         Statement = new object[] {
//             new {
//                 Action = "execute-api:Invoke",
//                 Resource = new object[] {"arn:aws:execute-api:us-east-2:656601024875:x11x9w39f8/*/*"},
//                 Effect = auth == auth.ToString()
//             }
//         }
//     }
// };            
