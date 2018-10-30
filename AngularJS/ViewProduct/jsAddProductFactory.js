HomeApp.factory('AddProductFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.GetProductsDataList = function () {
        return $http.get('/EditProduct/GetProductsDataList');
    };


    dataFactory.UploadFile = function (data) {
        //debugger
        return $http.post("/EditProduct/SaveFiles/", data, {
            withCredentials: true,
            processData: false,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    }

    dataFactory.getDataByProductID = function (ProductID) {
        //debugger
        return $http.post('/EditProduct/GetDataByProductID', ProductID );
    };
    // add Tier
    dataFactory.AddTierData = function (data) {
        //debugger
        return $http.post('/EditProduct/AddTierData', data);
    };

    return dataFactory;
}]);


