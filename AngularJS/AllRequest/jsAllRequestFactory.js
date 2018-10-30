HomeApp.factory('AllRequestFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.SearchAllData = function (SearchDatalist) {
      
      //  return $http.get('/AllRequests/GetAllRequests', SearchDatalist);
        return $http.post('/AllRequests/GetAllRequests/', SearchDatalist);
    };

    dataFactory.GetAllOrderStatus = function () {
        
        return $http.get('/AllRequests/GetAllStatus');
    };
    return dataFactory;
}]);


