HomeApp.factory('AddEditStatusFactory', ['$http', function ($http) {
    var dataFactory = {};

   

    dataFactory.GetStatusMasterData = function (StatusId) {
        return $http.post('/CustomerData/GetStatusData?StatusId=' + StatusId);
 
    }

    dataFactory.SaveStatus = function (data) {
        debugger
        return $http.post('/CustomerData/SaveStatus', data);
    }

    return dataFactory;
}]);