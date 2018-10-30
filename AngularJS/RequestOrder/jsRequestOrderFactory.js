HomeApp.factory('RequestOrderFactory', ['$http', function ($http) {


    var dataFactory = {};



    dataFactory.ViewRequestOrderDetails = function (orderID) {
       
        return $http.post('/RequestOrder/ViewRequestOrderDetails ', orderID);
    };

     
    dataFactory.ChangedStatus = function (Data) {
        debugger;
        return $http.post('/RequestOrder/ChangedStatus', Data);
    };

    dataFactory.PrintExcel = function () {
        debugger;
        return $http.post('/RequestOrder/PrintExcel');
    };

    //sent Email
    dataFactory.SendEmailData = function (data) {
        //debugger
        return $http.post("/RequestOrder/SendEmailData", data);
    };
    return dataFactory;
}]);


