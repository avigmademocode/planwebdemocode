HomeApp.factory('CustomerApproverFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.CustomerApproverData = function (CustId) {
        return $http.post('/CustomerApprover/GetCustomerApproverData?CustID=' + CustId);
    };

    dataFactory.SaveCustomerApproverData = function (ApproverList) {
        return $http.post('/CustomerApprover/SaveCustomerApproverData', ApproverList);
    };

    return dataFactory;
}]);