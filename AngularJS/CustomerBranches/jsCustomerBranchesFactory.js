HomeApp.factory('CustomerBranchesFactory', ['$http', function ($http) {


    var dataFactory = {};


    dataFactory.savePostData = function (data) {
        return $http.post('/CustomerBranches/SaveNewRequest', data);
    };


    dataFactory.GetCustData = function (CustID) {
        return $http.post('/CustomerBranches/GetCustomerBranches/?CustID=' + CustID);
    };
    return dataFactory;
}]);