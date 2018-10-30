HomeApp.factory('ProductFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.getCatList = function (Custid) {
        //alert('Factory', Custid)
        return $http.get('/Product/GetCategoryData/?Custid=' + Custid);
    };

    dataFactory.getProductList = function (Custid, ProdCatId) {
        //alert(Custid)
        //alert(ProdCatId)
        return $http.get('/Product/GetProductList/?Custid=' + Custid + '&ProdCatId=' + ProdCatId);
    };

    dataFactory.GetOrderDetailsList = function (data) {
        //alert(Custid)
        //alert(ProdCatId)
        debugger;
        return $http.post('/Product/GetOrderDetails', data);
    };

    return dataFactory;
}]);


