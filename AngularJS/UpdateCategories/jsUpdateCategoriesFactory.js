HomeApp.factory('UpdateCategoriesFactory', ['$http', '$q', function ($http, $q) {


    var dataFactory = {};

    dataFactory.UploadFile = function (data) {
        return $http.post("/AddProductCategory/UpdateCategories/", data, {
            withCredentials: true,
            processData: false,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    }

    return dataFactory;
}]);


