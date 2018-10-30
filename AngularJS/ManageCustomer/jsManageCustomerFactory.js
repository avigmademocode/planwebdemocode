HomeApp.factory('ManageCustomerFactory', ['$http', function ($http) {


    var dataFactory = {};


    dataFactory.GetCustomerData = function (Custid) {
        return $http.post('/ManageCustomer/GetCustomerData?Custid=' + Custid);
    };


    dataFactory.SaveCustomerData = function (data) {
        return $http.post('/ManageCustomer/SaveCustomerData', data);
    };
    return dataFactory;
}]);