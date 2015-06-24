function generateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
};

(function () {
    var app = angular.module('connections', ['ngResource', 'ui.bootstrap']);

    app.controller('ConnectionController', function ($http) {
        this.connectionStrings = null;
        //this.connectionStringToAdd = '';

        this.connectionStrings = function (ctrl) {
            $http.get('https://localhost:44300/api/SQLServerConnectionStrings').
                success(function (data) {
                    ctrl.connectionStrings = data;

                    for (var i = 0; i < ctrl.connectionStrings.length; i++) {
                        if (ctrl.connectionStrings[i].GUID == '84b84c25-ab4c-43de-a595-19ddfbc84faa')
                            var found = true;
                    }

                }).
                error(function (data, status, headers, config) {
                    ctrl.connectionStrings = null;
                });
        };

        this.connectionStrings(this);

        this.deleteConnectionString = function (ctrl, guid) {
            $http.delete('https://localhost:44300/api/SQLServerConnectionStrings/' + guid).
                 success(function (data) {
                     for (var i = 0; i < ctrl.connectionStrings.length; i++) {
                         if (ctrl.connectionStrings[i].GUID == guid) {
                             ctrl.connectionStrings.splice(i, 1);
                             console.log('found and removed ' + guid);
                             return;
                         }
                     }
                 }).
                 error(function (data, status, headers, config) {
                     console.log('error');
                 });
        };

        this.addConnectionString = function (ctrl, connectionStringToAdd) {
            var uid = generateUUID();
            $http.post('https://localhost:44300/api/SQLServerConnectionStrings', {
                Entity: connectionStringToAdd,
                GUID: uid
            }).success(function (data) {
                ctrl.connectionStrings.push({
                    Entity: connectionStringToAdd,
                    GUID: uid
                });
                console.log('Adding ' + connectionStringToAdd + ' - ' + uid);
            }).error(function (data, status, headers, config) {
                console.log('error ' + data);
            });
        }
    });
})();