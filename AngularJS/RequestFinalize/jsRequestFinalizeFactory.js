HomeApp.factory('RequestFinalizeFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.ViewOrderFinalizeDetails = function (OrderId) {
        return $http.post('/RequestFinalize/ViewOrderFinalizeDetails?OrderID=' + OrderId);
    };

    dataFactory.GetCustBudgetMastr = function (OrderId) {
        return $http.post('/RequestFinalize/GetCustBudgetMastr?OrderID=' + OrderId);
    };

    dataFactory.AddBudgetDetails = function (BudgetDet) {
        //debugger;
        return $http.post('/RequestFinalize/AddBudgetDetails', BudgetDet);
    };
    //get order by details
    dataFactory.GetBudgetOrderDetails = function (OrderId) {
        //debugger;
        return $http.post('/RequestFinalize/GetBudgetOrderDetails?OrderID=' + OrderId);
    };

    return dataFactory;
}]);


