HomeApp.factory('ItsetupFactory', ['$http', function ($http) {

    var dataFactory = {};

    dataFactory.ViewAddressDetails = function (OrderId) {
        return $http.post('/AddFreightQuote/ViewFreightQuote?OrderID=' + OrderId);
    };

    dataFactory.GetUserType = function () {
        //debugger;
        return $http.post('/Itsetup/GetCustomerUserType');
    };
    //get order by shipment info
    dataFactory.GetShipmentData = function (OrderId) {
        return $http.post('/Itsetup/GetITShipmentData?OrderID=' + OrderId);
    };
    //post data  IT
    dataFactory.SaveITSetUpData = function (data) {
        //debugger;
        return $http.post('/Itsetup/PostITShipmentData', data);
    };
    dataFactory.GetITSetUpData = function (OrderId) {
        return $http.post('/Itsetup/GetITSetUpData?OrderID=' + OrderId);
    };
    //post data  Soft
    dataFactory.SaveSoftwareSetUpData = function (data) {
        //debugger;
        return $http.post('/Itsetup/PostSoftwareSetUpData', data);
    };
    //get soft data 
    dataFactory.GetSoftwareSetUpData = function (OrderId) {
        return $http.post('/Itsetup/GetSoftSetUpData?OrderID=' + OrderId);
    };


    return dataFactory;
}]);


