HomeApp.factory('ViewRequestFactory', ['$http', '$q', function ($http, $q) {


    var dataFactory = {};

    dataFactory.viewRequestOrderDetails = function (OrderId) {
        return $http.post('/ViewRequest/ViewRequestOrderDetails?OrderID=' + OrderId);
    };

    dataFactory.ChangedStatus = function (Data) {
      
        return $http.post('/ViewRequest/ChangedStatus', Data);
    };

   

    dataFactory.UploadFile = function (data) {
        
        /*
        var formData = new FormData();
        formData.append('file', file);
        formData.append('description', description);
        var defer = $q.defer()*/
        return  $http.post("/ViewRequest/SaveFiles/", data, {
            withCredentials: true,
            processData: false,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
        //return $http.post('/ViewRequest/SaveFiles/', formData);
    }

    dataFactory.GetRequestedDocs = function (OrderId) {
        return $http.post('/ViewRequest/GetRequestedDocs?OrderID=' + OrderId);
    };

    //Bind Grant Code Details
    dataFactory.GrantCodeOrderDetails = function (OrderID) {

        return $http.post('/AddNewGrantCode/ViewRequestGrantCodeOrderDetails?OrderID=' + OrderID);
    };

    dataFactory.BudgetCodeOrderDetails = function (OrderID) {

        return $http.post('/RequestFinalize/BudgetCodeOrderDetails?OrderID=' + OrderID);
    };
    //sent Email
    dataFactory.SendEmailData = function (data) {
        //debugger
        return $http.post("/ViewRequest/SendEmailData", data);
    };

    //sent Email
    //dataFactory.PrintExcel = function (data) {
    //    debugger
    //    return $http.get("/ViewRequest/ExportToExcel");
    //};

    dataFactory.PrintExcel = function (data) {
        debugger;
        $http.get('/ViewRequest/ExportToExcel',data ,{ responseType: 'arraybuffer' }
        ).then(function (response) {
            debugger;
            var header = response.headers('Content-Disposition')
            var fileName ='test.xls';
            console.log(fileName);

            var blob = new Blob([response.data],
                { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8' });
            var objectUrl = (window.URL || window.webkitURL).createObjectURL(blob);
            var link = angular.element('<a/>');
            link.attr({
                href: objectUrl,
                download: fileName
            })[0].click();
        })
    };


    return dataFactory;
}]);


