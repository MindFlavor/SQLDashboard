(function () {
    var app = angular.module('app', ['ngResource', 'ui.bootstrap']);

    app.controller('SQLServerController', function ($http) {
        this.sqlServers = null;

        this.getSQLServers = function (ctrl) {
            $http.get('https://localhost:44300/api/SQLServers').
                success(function (data) {
                    ctrl.sqlServers = data;
                }).
                error(function (data, status, headers, config) {
                    ctrl.sqlServers = null;
                });
        };

        this.getSQLServers(this);
    });

    app.controller('SQLServerDetailController', function ($http) {
        this.GUID = null;
        this.summary = null;
        this.error = null;

        this.setID = function (sqlid) {
            this.GUID = sqlid;
            this.getSQLServerSummary(this);

            //$http.post('https://localhost:44300/api/SQLServerConnectionStrings', {
            //    Entity: 'Server=FRCOGNO81.europe.corp.microsoft.com\\V12;Trusted_Connection=True;',
            //    GUID: '84b84c25-ab4c-43de-a595-19ddfbc84faa'
            //});
        }

        this.getSQLServerSummary = function (ctrl) {
            url = 'https://localhost:44300/api/SQLServers/' + ctrl.GUID;
            $http.get('https://localhost:44300/api/SQLServers/' + ctrl.GUID).
                success(function (data) {
                    ctrl.summary = data;
                }).
                error(function (data, status, headers, config) {
                    ctrl.summary = null;
                    ctrl.error = data;
                });
        };
    });
})();