HomeApp.factory('ViewProductFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.SearchAllData = function (SearchDatalist) {
        return $http.post('/ViewProduct/GetProducts/', SearchDatalist);
    };

    dataFactory.GetProductsData = function () {
        return $http.get('/ViewProduct/GetProductsData');
    };

    dataFactory.GetCatByCustomerID = function (CustomerID) {
        return $http.get('/ViewProduct/GetCatDataByCustID?CustID=' + CustomerID);
    };

    return dataFactory;
}]);


