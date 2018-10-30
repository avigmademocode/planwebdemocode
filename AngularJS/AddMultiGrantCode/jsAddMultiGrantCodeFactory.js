HomeApp.factory('AddMultiGrantCodeFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.GetNewGrantCode = function (OrderID) {

        return $http.post('/AddNewGrantCode/GetNewGrantCodeDtls?OrderID=' + OrderID);
    };

    dataFactory.AddNewGrantCode = function (obj) {

        return $http.post('/AddNewGrantCode/AddNewGrantCode', obj);
    };

    dataFactory.GrantCodeOrderDetails = function (OrderID) {

        return $http.post('/AddNewGrantCode/GrantCodeOrderDetails?OrderID=' + OrderID);
    };

    dataFactory.SaveGrantData = function (obj) {
        debugger;
        return $http.post('/AddNewGrantCode/SaveGrantData',obj);
    };

    dataFactory.ViewGrantCodeOrderDetails = function (OrderID) {

        return $http.post('/AddNewGrantCode/ViewGrantCodeOrderDetails?OrderID=' + OrderID);
    };

    return dataFactory;
}]);


