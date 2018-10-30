HomeApp.factory('AddFreightQuoteFactory', ['$http', function ($http) {


    var dataFactory = {};

    

    dataFactory.ViewFreightQuoteDetails = function (OrderId) {
        return $http.post('/AddFreightQuote/ViewFreightQuote?OrderID=' + OrderId);
    };

    dataFactory.AddFreightQuoteDetails = function (Freight) {
        //debugger;
        return $http.post('/AddFreightQuote/AddFreightQuote', Freight);
    };

    return dataFactory;
}]);


