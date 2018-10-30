HomeApp.factory('OrderSummaryFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.getOrderSummary = function (orderID, CustId, UserId,Type) {
        return $http.get('/OrderSummary/GetOrderSummary/?orderID=' + orderID + '&CustId=' + CustId + '&UserId=' + UserId + '&Type=' + Type);
    };

    dataFactory.SaveOrderSummary = function (Order) {
        //debugger;
        return $http.post('/OrderSummary/SaveOrderSummary', Order);
    };
    return dataFactory;
}]);


