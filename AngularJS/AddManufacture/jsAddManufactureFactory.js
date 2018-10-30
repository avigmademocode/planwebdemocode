HomeApp.factory('AddManufactureFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.GetManufactureData = function () {
        debugger;
        return $http.post('/AddManufacture/GetManufactureData');
    };

    dataFactory.SaveManufactureData = function (Manulist) {
        debugger;
        return $http.post('/AddManufacture/SaveManufactureData', Manulist);
    };
   return dataFactory;
}]);