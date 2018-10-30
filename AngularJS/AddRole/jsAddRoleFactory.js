HomeApp.factory('AddRoleFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.GetRoleData = function () {
        //debugger;
        return $http.post('/AddRole/GetRoleData');
    };

    dataFactory.SaveRoleData = function (Manulist) {
        debugger;
        return $http.post('/AddRole/SaveRoleData', Manulist);
    };
    return dataFactory;
}]);