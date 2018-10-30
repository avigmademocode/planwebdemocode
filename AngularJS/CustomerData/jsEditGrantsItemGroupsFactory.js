HomeApp.factory('EditGrantsItemGroupsFactory', ['$http', function ($http) {
    var dataFactory = {};

    dataFactory.GetProdCategryData = function (val) {
        return $http.post('/AddProductCategory/GetProdCategryData?Custid='+ val);
    }

    dataFactory.SaveProductCategryData = function (ProdCatgrylist) {
        debugger;
        return $http.post('/AddProductCategory/SaveProductCatData', ProdCatgrylist);
    };

   
    return dataFactory;
}]);