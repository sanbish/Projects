// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace QuickstartIdentityServer
{
    using System.Security.Claims;

    public class Config
    {
        
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin","manager", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.manager", "dataEventRecords.user" } ),
                new IdentityResource("securedfilesscope",new []{ "role", "admin", "manager", "user", "securedFiles", "securedFiles.admin", "securedFiles.manager", "securedFiles.user"} )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1")
                {
                    ApiSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "api1",
                            DisplayName = "Scope for the dataEventRecords ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "manager", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.manager", "dataEventRecords.user" }
                },
                new ApiResource("securedFiles")
                {
                    ApiSecrets =
                    {
                        new Secret("securedFilesSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "securedfilesscope",
                            DisplayName = "Scope for the securedFiles ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "manager", "user", "securedFiles", "securedFiles.admin", "securedFiles.manager", "securedFiles.user" }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            string HOST_URL = "http://localhost:4200";

            string HOST_URL1 = "https://localhost:44311";
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientName = "js",
                    ClientId = "js",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        HOST_URL + "/oidc-callback.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        HOST_URL + "/index.html"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        HOST_URL
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "email",
                        "profile",
                        "api1",
                        "api1",
                        "securedFiles",
                        "securedfilesscope",
                    }
                },
                new Client
                {
                    ClientName = "angular2client",
                    ClientId = "angular2client",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                         HOST_URL1 + "/authorized"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        HOST_URL + "/Unauthorized.html" 
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        HOST_URL1,
                        "http://localhost:44311"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "securedFiles",
                        "securedfilesscope",
                        "role"
                    }
                }
            };
        }
    }
}