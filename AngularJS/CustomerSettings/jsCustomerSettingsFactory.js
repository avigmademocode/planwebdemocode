HomeApp.factory('CustomerSettingsFactory', ['$http', function ($http) {


    var dataFactory = {};


    dataFactory.savePostData = function (data) {
        return $http.post('/CustomerSettings/SaveNewRequest', data);
    };


    dataFactory.GetCustData = function (CustID) {
        return $http.post('/CustomerSettings/GetCustomerSettings/?CustID=' + CustID);
    };


    return dataFactory;
}]);


