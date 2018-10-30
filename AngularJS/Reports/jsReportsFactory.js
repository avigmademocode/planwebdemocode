HomeApp.factory('ReportsFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.GetReports = function (data) {
        //debugger;
        return $http.post('/Reports/GetReports', data);
    };


    dataFactory.GetCustomerStatus = function () {
        //debugger;
        return $http.post('/Reports/GetCustomerStatus');
    };

    //dataFactory.getCatList = function (Custid) {
    //    return $http.get('/Product/GetCategoryData/?Custid=' + Custid);
    //};


   

    return dataFactory;
}]);


