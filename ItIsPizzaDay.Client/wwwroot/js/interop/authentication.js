(function(self) {
    var userAgentApplication = null;

    function initMSAL(applicationConfig) {
        if (userAgentApplication === null) {
            userAgentApplication = new Msal.UserAgentApplication(applicationConfig.clientId,
                null,
                _ => {},
                applicationConfig);
        }
    }
    
    self.authentication = {
        logout: function(applicationConfig) {
            initMSAL(applicationConfig);
            userAgentApplication.logout();
            return "";
        },
        getTokenAsync: function(applicationConfig) {
            initMSAL(applicationConfig);
            return new Promise(function (resolve, reject) {

                userAgentApplication.acquireTokenSilent(applicationConfig.scopes)
                    .then(function (token) {
                        var user = userAgentApplication.getUser();
                        var expires = new Date(user.idToken.exp * 1000);

                        const now = new Date();

                        if (now <= expires) {
                            resolve({
                                idToken: token,
                                expires: expires,
                                userName: user.name
                            });
                        } else {
                            resolve(null);
                        }
                    }, function (error) {
                        if (error) {
                            if(error === 'user_login_error|User login is required'){
                                resolve(null);
                            }else {
                                reject(error);
                            }
                        }
                    });
            });
        },
        loginRedirect: function(applicationConfig) {

            initMSAL(applicationConfig);
            userAgentApplication.loginRedirect(applicationConfig.scopes);
            return "";
        }
    }
    
})(window.interop || (window.interop = {}));

