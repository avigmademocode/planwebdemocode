HomeApp.factory('RequestShippingFactory', ['$http', function ($http) {

    var dataFactory = {};
    //get drop down carrier
    dataFactory.GetCarrierData = function () {
        //debugger;
        return $http.post('/RequestShipping/GetCarrier');
    };
    // add data all
    dataFactory.AddOrdershipmentinfo = function (Data) {
        //debugger;
        return $http.post('/RequestShipping/Ordershipmentinfo', Data);
    };

    //get order by details
    dataFactory.GetShipmentData = function (OrderId) {
        return $http.post('/RequestShipping/GetShipmentDataDtls?OrderID=' + OrderId);
    };


    //get shipment all 
    dataFactory.GetShipmentAll = function (ShipmentId) {
        return $http.post('/RequestShipping/GetShipmentAll?ShipmentId=' + ShipmentId);
    };


    //get order by details
    dataFactory.GetNewShipmentData = function (OrderId) {
        return $http.post('/RequestShipping/GetNewShipmentDataDtls?OrderID=' + OrderId);
    };



    return dataFactory;
}]);