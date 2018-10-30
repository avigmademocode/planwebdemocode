HomeApp.factory('NewRequestFactory', ['$http', function ($http) {


    var dataFactory = {};

    dataFactory.GetCustomers = function (type) {
        //debugger;
        return $http.post('/NewRequest/GetCustomer/?type='+ type)
    }

    dataFactory.getDeleveryAndTerms = function (Custid) {
        //alert(Custid)
        return $http.get('/NewRequest/GetDeleveryAndTerms/?Custid=' + Custid);
    };


    dataFactory.GetCustDetails = function (Custid, BranchID) {
       // alert(BranchID)
        return $http.get('/NewRequest/GetCustDetails/?Custid=' + Custid + '&BranchID=' + BranchID);
    };

    dataFactory.saveNewRequest = function (data) {
        return $http.post('/NewRequest/SaveNewRequest', data);
    };

    dataFactory.GetCustomersData = function (OrderID,Type) {
        debugger;

        return $http.get('/NewRequest/GetCustomersData/?OrderID=' + OrderID + '&Type=' + Type);
    }
    //dataFactory.saveNewRequest = function (CustData) {
    //    alert('Factory Called.');
    //    debugger;
    //    var payload = new FormData();
    //    payload.append('CustName', CustData.CustName);
    //    return $http({
    //        url: '/NewRequest/SaveNewRequest',
    //        method: 'POST',
    //        data: payload,
    //        headers: { 'Content-Type': undefined },
    //        transformRequest: angular.identity
    //    });
    //}


    //dataFactory.saveNewRequest = function (data) {
    //    alert('Factory Called.');
    //    debugger;
    //    var fd = new FormData();
    //    fd.append("AprName", data.AprName);
    //    fd.append("AprTitle", data.AprTitle);
    //    fd.append("BAdd1", data.BAdd1);
    //    fd.append("BAdd2", data.BAdd2);
    //    fd.append("BAdd3", data.BAdd3);
    //    fd.append("BCity", data.BCity);
    //    fd.append("BCountry", data.BCountry);
    //    fd.append("AprEmail", data.AprEmail);
    //    fd.append("BState", data.BState);
    //    fd.append("BZip", data.BZip);
    //    fd.append("BranchID", data.BranchID);
    //    fd.append("DeliveryTo", data.DeliveryTo);
    //    fd.append("Department", data.Department);
    //    fd.append("Office", data.Office);
    //    fd.append("Refernce", data.Refernce);
    //    fd.append("SAdd1", data.SAdd1);
    //    fd.append("SAdd2", data.SAdd2);
    //    fd.append("SAdd3", data.SAdd3);
    //    fd.append("SCity", data.SCity);
    //    fd.append("SCountry", data.SCountry);
    //    fd.append("SState", data.SState);
    //    fd.append("SZip", data.SZip);
    //    fd.append("Semail", data.Semail);
    //    fd.append("Sname", data.Sname);
    //    fd.append("Sphone", data.Sphone);
    //    fd.append("selectedCustomer", data.selectedCustomer);
    //    return $http.post('/NewRequest/SaveNewRequest', fd, {
    //        transformRequest: angular.identity,
    //        headers: { 'Content-Type': undefined }
    //    });
    //};

    return dataFactory;
}]);


