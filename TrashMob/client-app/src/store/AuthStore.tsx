import * as msal from "@azure/msal-browser";

export function GetMsalClient() {

    var host = window.location.host;
    var protocol = window.location.protocol;

    var uri = protocol + "//" + host;

    var clientId = 'e46d67ba-fe46-40f4-b222-2f982b2bb112';
    var authority = 'TrashMobDev.b2clogin.com';
    var fullAuthority = 'https://TrashMobDev.b2clogin.com/TrashMobDev.onmicrosoft.com/B2C_1A_TM_SIGNUP_SIGNIN';

    if (host.startsWith("www.trashmob.eco") || host.startsWith("trashmob.eco")) {
        clientId = "0a1647a4-c758-4964-904f-a9b66958c071";
        authority = 'TrashMob.b2clogin.com';
        fullAuthority = 'https://TrashMob.b2clogin.com/TrashMob.onmicrosoft.com/B2C_1A_TM_SIGNUP_SIGNIN'
    }

    var msalC = new msal.PublicClientApplication({
        auth:
        {
            clientId: clientId,
            authority: fullAuthority,
            postLogoutRedirectUri: "/",
            navigateToLoginRequestUrl: true,
            knownAuthorities: [authority],
            redirectUri: uri            
        },
        cache: { cacheLocation: "sessionStorage", storeAuthStateInCookie: false },
        system: {
            loggerOptions: {
                loggerCallback: (level, message, containsPii) => {
                    if (containsPii) {
                        console.info("Note: Logging message contained PII");
                        return;
                    }
                    switch (level) {
                        case msal.LogLevel.Error:
                            console.error(message);
                            return;
                        case msal.LogLevel.Info:
                            console.info(message);
                            return;
                        case msal.LogLevel.Verbose:
                            console.debug(message);
                            return;
                        case msal.LogLevel.Warning:
                            console.warn(message);
                            return;
                    }
                }
            }
        }
    });

    return msalC;
}

export const msalClient: msal.PublicClientApplication = GetMsalClient();

export function getApiConfig() {
    var host = window.location.host;

    if (host.startsWith("www.trashmob.eco") || host.startsWith("trashmob.eco")) {
        return apiConfigProd;
    }
    else {
        return apiConfigDev;
    }
}

const apiConfigProd = {
    b2cScopes: ["https://TrashMob.onmicrosoft.com/api/TrashMob.Read", "https://TrashMob.onmicrosoft.com/api/TrashMob.Writes", "email"],
};

const apiConfigDev = {
    b2cScopes: ["https://TrashMobDev.onmicrosoft.com/api/TrashMob.Read", "https://TrashMobDev.onmicrosoft.com/api/TrashMob.Writes", "email"],
};

export const tokenRequest = {
    scopes: apiConfigProd.b2cScopes
}

export function getDefaultHeaders(method: string): Headers {
    var headers = new Headers();
    headers.append('Allow', method);
    headers.append('Accept', 'application/json, text/plain');
    headers.append('Content-Type', 'application/json');
    return headers;
}