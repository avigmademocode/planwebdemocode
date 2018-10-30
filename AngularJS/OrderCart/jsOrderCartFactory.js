HomeApp.factory('OrderCartFactory', ['$http', function ($http) {


    var dataFactory = {};

    //dataFactory.getProductList = function (Custid, ProdCatId) {
    //    return $http.get('/Product/GetProductList/?Custid=' + Custid + '&ProdCatId=' + ProdCatId);
    //};

    dataFactory.saveOrderDetails = function (OrderID, CustomerID, SubTotalAmount, data) {
        //debugger
        return $http.post('/OrderCart/SaveOrderDetails?OrderID=' + OrderID + '&CustomerID=' + CustomerID + '&SubTotal=' + SubTotalAmount, data)
    }

    return dataFactory;
}]);


