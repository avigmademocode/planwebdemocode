HomeApp.factory('AddEditCustomerStatusFactory', ['$http', function ($http) {
    var dataFactory = {};

    dataFactory.SaveStatusData = function (StatusData) {

        return $http.post('/CustomerData/SaveStatusData', StatusData);
    };

    dataFactory.GetCustStatusData = function (CustId) {
        debugger;
        return $http.get('/CustomerData/GetCustStatusData?CustID=' + CustId);

    };

     

    return dataFactory;
}]);