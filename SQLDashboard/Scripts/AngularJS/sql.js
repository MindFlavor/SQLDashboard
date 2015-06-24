(function () {
    var app = angular.module('sql', ['ngResource', 'angularCharts', 'ui.bootstrap'])
          .filter('Filesize', function () {
              return function (size) {
                  if (isNaN(size))
                      size = 0;

                  if (size < 1024)
                      return size + ' Bytes';

                  size /= 1024;

                  if (size < 1024)
                      return size.toFixed(2) + ' Kb';

                  size /= 1024;

                  if (size < 1024)
                      return size.toFixed(2) + ' Mb';

                  size /= 1024;

                  if (size < 1024)
                      return size.toFixed(2) + ' Gb';

                  size /= 1024;

                  return size.toFixed(2) + ' Tb';
              };
          });

    app.controller('SQLServerController', function ($http) {

        this.selectedTab = 0;

        this.isSelected = function (tab) {
            return this.selectedTab === tab;
        };

        this.setTab = function (tab) {
            this.selectedTab = tab;

            if (this.selectedTab === 1)
                this.retrieveBufferPoolSnapshot(this);
            else if (this.selectedTab === 2)
                this.retrieveCachedPlanSnapshot(this);
            else if (this.selectedTab === 3)
                this.retrieveDatabaseList(this);
        };

        this.guid = undefined;
        this.connectionString = undefined;

        this.bufferPoolTotalSizeBytes = 0;
        this.cachedPlanTotalSizeBytes = 0;

        this.databaseList = undefined;

        this.setGuid = function (guidToSet) {
            this.guid = window.location.pathname.split('/')[2];
            console.log('guid == ' + this.guid);
            this.retrieveConnectionString(this);
        };

        /// graph buffer pool
        this.data_bufferPoolSnapshot = { series: undefined, data: undefined };
        this.config_bufferPoolSnapshot = {
            title: '',
            tooltips: true,
            labels: false,
            mouseover: function () { },
            mouseout: function () { },
            click: function () { },
            isAnimate: true,
            legend: {
                display: true,
                //could be 'left, right'
                position: 'right'
            }
        };

        /// graph cached plan snapshot
        this.data_cachedPlanSnapshot = { series: undefined, data: undefined };
        this.config_cachedPlanSnapshot = {
            title: '',
            tooltips: true,
            labels: false,
            mouseover: function () { },
            mouseout: function () { },
            click: function () { },
            legend: {
                display: true,
                //could be 'left, right'
                position: 'right'
            }
        };

        /// end

        this.retrieveConnectionString = function (ctrl) {
            $http.get('https://localhost:44300/api/SQLServers').
                success(function (data) {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].GUID == ctrl.guid) {
                            ctrl.connectionString = data[i];
                            return;
                        }
                    }
                }).
                error(function (data, status, headers, config) {
                    ctrl.connectionString = null;
                });
        };

        this.retrieveBufferPoolSnapshot = function (ctrl) {
            if (ctrl.data_bufferPoolSnapshot.data === undefined) {
                $http.get('https://localhost:44300/api/BufferPoolSnapshot/' + ctrl.guid).
                    success(function (data) {
                        console.log('received data ' + data);

                        tempdata = [];
                        tempseries = [];

                        ctrl.bufferPoolTotalSizeBytes = 0;

                        for (var i = 0; i < data.length; i++) {
                            tempdata.push({
                                x: data[i].Entity,
                                y: [data[i].Number],
                                tooltip: data[i].Entity + ': ' + data[i].Number + ' pages (' + ((data[i].Number * 8192) / (1024 * 1024)) + ' KB)'
                            });

                            ctrl.bufferPoolTotalSizeBytes += data[i].Number * 8192;
                            tempseries.push(data[i].Entity);
                        }

                        ctrl.data_bufferPoolSnapshot.data = tempdata;
                        ctrl.data_bufferPoolSnapshot.series = tempseries;
                    }).
                    error(function (data, status, headers, config) {
                        console.log('error receiving data ' + data);
                    });
            }
        };

        this.retrieveCachedPlanSnapshot = function (ctrl) {
            if (ctrl.data_cachedPlanSnapshot.data === undefined) {
                if (ctrl.databaseList === undefined) {
                    $http.get('https://localhost:44300/api/CachedPlanSnapshot/' + ctrl.guid).
                        success(function (data) {
                            console.log('received datra ' + data);

                            tempdata = [];
                            tempseries = [];
                            ctrl.cachedPlanTotalSizeBytes = 0;

                            for (var i = 0; i < data.length; i++) {
                                tempdata.push({
                                    x: data[i].cacheobjtype + ' - ' + data[i].objtype,
                                    y: [data[i].sizeInBytes],
                                    tooltip: data[i].cacheobjtype + ' - ' + data[i].objtype + ' (' + (data[i].sizeInBytes / (1024 * 1024)) + ' KB)'
                                });

                                tempseries.push(data[i].cacheobjtype + ' - ' + data[i].objtype);
                                ctrl.cachedPlanTotalSizeBytes += data[i].sizeInBytes;
                            }

                            ctrl.data_cachedPlanSnapshot.data = tempdata;
                            ctrl.data_cachedPlanSnapshot.series = tempseries;

                        }).
                        error(function (data, status, headers, config) {
                            console.log('error receiving data ' + data);
                        });
                }
            }
        };

        this.retrieveDatabaseList = function (ctrl) {
            $http.get('https://localhost:44300/api/Database/' + ctrl.guid)
            .success(function (data) {
                ctrl.databaseList = data;
                console.log('retrieved database data ' + data)
            })
            .error(function (data, status, headers, config) {
                console.log('error receiving data ' + data);
            });
        };
    });
})();