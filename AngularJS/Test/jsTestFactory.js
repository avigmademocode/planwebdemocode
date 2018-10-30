HomeApp.factory('TestFactory', ['$http', function ($http) {


var dataFactory = {};

dataFactory.getCatList = function (Custid) {
    //alert('Factory', Custid)
    return $http.get('/Product/GetCategoryData/?Custid=' + Custid);
    };

    return dataFactory;
}]);