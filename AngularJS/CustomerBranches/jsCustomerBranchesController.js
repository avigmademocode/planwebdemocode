HomeApp.controller('CustomerBranchesController', ['$scope', '$window', 'NewRequestFactory', 'CustomerBranchesFactory', function ($scope, $window, NewRequestFactory, CustomerBranchesFactory) {

    var CustomerID = $window.localStorage.getItem('MCustID');
    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;

    $scope.IstxtHide = false;
    $scope.IsDdlHide = true;
    $scope.IsAdd = false;
    //
    //alert(CustomerID);

    $scope.GetCustomersList = function () {
        NewRequestFactory.GetCustomers(1)
            .then(function (response) {
                if (response.data.length != 0) {
                    debugger;
                    $scope.custList = response.data[0];
                    $scope.SCity = response.data[1];
                    $scope.City = response.data[1];
                    $scope.SCountry = response.data[2];
                    $scope.Country = response.data[2];

                    $scope.BCity = response.data[1];
                    $scope.BCountry = response.data[2];
                }
                else {
                    alert('Failled!!!');
                }
            });
    }
    $scope.GetCustomersList();
    $scope.Back = function () {
        $window.location.href = '/ManageCustomer';
    }


    //copy address
    $scope.CoppyAddress = function () {
        //debugger;
        if ($scope.chkCoppy == true) {
            //      alert('Address Coppied.');
            $scope.form.BAdd1 = $scope.form.SAdd1;
            $scope.form.BAdd2 = $scope.form.SAdd2;
            $scope.form.BAdd3 = $scope.form.SAdd3;

            $scope.form.BState = $scope.form.SState;
            $scope.form.BZip = $scope.form.SZip;

            $scope.form.Bname = $scope.form.Sname;
            $scope.form.Bphone = $scope.form.Sphone;
            $scope.form.Bemail = $scope.form.Semail;

            if ($scope.selectedCityTo != 'undefined' && $scope.selectedCityTo != '') {
                var cityid = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
                var cityname = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName : '';


                $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: cityname, CityID: cityid });

            }
            if ($scope.selectedCountryTo != 'undefined' && $scope.selectedCountryTo != '') {

                var countryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
                var countryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';

                $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: countryname, CountryId: countryid });
            }


        }
        else {
            $scope.form.BAdd1 = '';
            $scope.form.BAdd2 = '';
            $scope.form.BAdd3 = '';
            $scope.form.BState = '';
            $scope.form.BZip = '';
            $scope.form.Bname = '';
            $scope.form.Bphone = '';
            $scope.form.Bemail = '';

            //$scope.form = $scope.City;
            // $scope.initialBCountryValue = { CountryName: '', CountryId: '' };
            alert('un ckt.');

            $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: '', CountryId: 0 });
            $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: '', CityID: 0 });
        }
    }


    $scope.NeedWarningFun = function () {
        //debugger;
        if ($scope.form.NeedWarning != true) {
            $scope.form.FeeWarning = '';
            alert('clear');
        }
    }



    $scope.GetBranchData = function (custID) {
        debugger;
        // ResetData();
        CustomerBranchesFactory.GetCustData(custID)
            .then(function (response) {
                debugger;
                $scope.form.CustomerId = parseInt(custID);
                if (response.data.length != 0) {

                    $scope.Branchlst = response.data[0];
                    $scope.ddlBranchlst = response.data[0];
                }

            });
    }

    $scope.GetCustomerData = function (BranchID) {


        debugger;

        $scope.form.DisplayName = $scope.Branchlst.find(v => v.BranchId == BranchID).DisplayName;
        // $scope.form.BranchName = response.data[0][0].BranchName;

        $scope.form.HeadOffice = $scope.Branchlst.find(v => v.BranchId == BranchID).HeadOffice;
        $scope.form.PreSet = $scope.Branchlst.find(v => v.BranchId == BranchID).PreSetAddress;
        $scope.form.SAdd1 = $scope.Branchlst.find(v => v.BranchId == BranchID).BrAdd1;
        $scope.form.SAdd2 = $scope.Branchlst.find(v => v.BranchId == BranchID).BrAdd2;
        $scope.form.SAdd3 = $scope.Branchlst.find(v => v.BranchId == BranchID).BrAdd3;
        $scope.form.SState = $scope.Branchlst.find(v => v.BranchId == BranchID).BrState;

        $scope.form.SZip = $scope.Branchlst.find(v => v.BranchId == BranchID).Brpin;
        $scope.form.Sname = $scope.Branchlst.find(v => v.BranchId == BranchID).BrConName;
        $scope.form.Sphone = $scope.Branchlst.find(v => v.BranchId == BranchID).BrContact;

        $scope.form.Semail = $scope.Branchlst.find(v => v.BranchId == BranchID).BrEmail;
        $scope.form.SFullAdd = $scope.Branchlst.find(v => v.BranchId == BranchID).BrFullAddress;



        $scope.form.BAdd1 = $scope.Branchlst.find(v => v.BranchId == BranchID).BIAdd1;
        $scope.form.BAdd2 = $scope.Branchlst.find(v => v.BranchId == BranchID).BIAdd2;
        $scope.form.BAdd3 = $scope.Branchlst.find(v => v.BranchId == BranchID).BIAdd3;
        $scope.form.BState = $scope.Branchlst.find(v => v.BranchId == BranchID).BlState;
        $scope.form.BZip = $scope.Branchlst.find(v => v.BranchId == BranchID).BIpin;


        $scope.form.Bemail = $scope.Branchlst.find(v => v.BranchId == BranchID).BIEmail;
        $scope.form.Bphone = $scope.Branchlst.find(v => v.BranchId == BranchID).BIContact;
        $scope.form.Bname = $scope.Branchlst.find(v => v.BranchId == BranchID).BIConName;
        $scope.form.NeedDelivery = $scope.Branchlst.find(v => v.BranchId == BranchID).NeedDelivery;
        $scope.form.NeedWarning = $scope.Branchlst.find(v => v.BranchId == BranchID).NeedWarning;
        $scope.form.FeeWarning = $scope.Branchlst.find(v => v.BranchId == BranchID).FeeWarning;

        //document.getElementById("ex1_value").value = response.data[0][0].BrCityName;
        //document.getElementById("ex2_value").value = response.data[0][0].BrCountryName;
        //document.getElementById("ex3_value").value = response.data[0][0].BICityName;
        //document.getElementById("ex4_value").value = response.data[0][0].BillCountryName;

        $scope.$broadcast('angucomplete-alt:changeInput', 'ex1', { CityName: $scope.Branchlst.find(v => v.BranchId == BranchID).BrCityName, CityID: $scope.Branchlst.find(v => v.BranchId == BranchID).intBrCity });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex2', { CountryName: $scope.Branchlst.find(v => v.BranchId == BranchID).BrCountryName, CountryId: $scope.Branchlst.find(v => v.BranchId == BranchID).intBrCountry });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: $scope.Branchlst.find(v => v.BranchId == BranchID).BICityName, CityID: $scope.Branchlst.find(v => v.BranchId == BranchID).intBlCity });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: $scope.Branchlst.find(v => v.BranchId == BranchID).BillCountryName, CountryId: $scope.Branchlst.find(v => v.BranchId == BranchID).intBlCountry });


        $scope.BrrCityName = $scope.Branchlst.find(v => v.BranchId == BranchID).BrCityName;
        $scope.BrrCountryName = $scope.Branchlst.find(v => v.BranchId == BranchID).BrCountryName;

        $scope.BIICityName = $scope.Branchlst.find(v => v.BranchId == BranchID).BICityName;
        $scope.BIICountryName = $scope.Branchlst.find(v => v.BranchId == BranchID).BillCountryName;


    }
    $scope.GetBranchData(CustomerID);

    $scope.GetFullAddress = function () {
        debugger;
        //$scope.form.SState = response.data[0][0].BrState;

        //$scope.form.SZip = response.data[0][0].Brpin;
        var ShipcityID, ShipCityName, Shipcountryid, Shipcountryname;

        if ($scope.selectedCityTo != '' && $scope.selectedCityTo != undefined) {
            ShipcityID = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
            ShipCityName = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedCountryTo != '' && $scope.selectedCountryTo != undefined) {
            Shipcountryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
            Shipcountryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';
        }

        if ($scope.form.SAdd2 == null)
        {
            $scope.form.SAdd2 = '';
        }
        if ($scope.form.SAdd3 == null)
        {
            $scope.form.SAdd3 = '';
        }



        $scope.form.SFullAdd = $scope.form.SAdd1 + " " + $scope.form.SAdd2 + "  " + $scope.form.SAdd3 + " " + ShipCityName + " " + $scope.form.SState + " " + $scope.form.SZip + " " + Shipcountryname;
    }


    $scope.submitForm = function () {
        debugger;
        formData = $scope.form;
        var txtBranchId = 0;
        var txtBranchname = '';
        var txttype  = 0;
        //alert(JSON.stringify(formData));
        var ShipcityID, ShipCityName, BillcityID, BillCityName, Shipcountryid, Shipcountryname, Billcountryid, Billcountryname;

        if ($scope.selectedCityTo != '' && $scope.selectedCityTo != undefined) {
            ShipcityID = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
            ShipCityName = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedCountryTo != '' && $scope.selectedCountryTo != undefined) {
            Shipcountryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
            Shipcountryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';
        }
        if ($scope.selectedBCityTo != '' && $scope.selectedBCityTo != undefined) {
            BillcityID = $scope.selectedBCityTo.originalObject.CityID ? $scope.selectedBCityTo.originalObject.CityID : '';
            BillCityName = $scope.selectedBCityTo.originalObject.CityName ? $scope.selectedBCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedBCountryTo != '' && $scope.selectedBCountryTo != undefined) {
            Billcountryid = $scope.selectedBCountryTo.originalObject.CountryId ? $scope.selectedBCountryTo.originalObject.CountryId : '';
            Billcountryname = $scope.selectedBCountryTo.originalObject.CountryName ? $scope.selectedBCountryTo.originalObject.CountryName : '';
        }
        if ($scope.IsAdd)
        {
            txtBranchId = 0;
            txtBranchname = $scope.form.addbranchname;
            txttype = 1;
        }
        else
        {
            txtBranchId = $scope.form.BranchName;
            txtBranchname = $scope.Branchlst.find(v => v.BranchId == txtBranchId).BranchName;
            txttype = 2;
        }
        var Data = {
            CustomerId: $scope.form.CustomerId,
            BranchId: txtBranchId,
            //BranchName: $scope.form.BranchName,
            BranchName: txtBranchname,
            DisplayName: $scope.form.DisplayName,
            HeadOffice: $scope.form.HeadOffice,
            PreSetAddress: $scope.form.PreSet,
            HideBillingAddress: $scope.form.Hide,
            BrAdd1: $scope.form.SAdd1,
            BrAdd2: $scope.form.SAdd2,
            BrAdd3: $scope.form.SAdd3,
            BrCity: ShipcityID,
            BrCityName: ShipCityName,
            BrCountry: Shipcountryid,
            BrCountryName: Shipcountryname,

            Brstate: $scope.form.SState,
            Brpin: $scope.form.SZip,
            BrConName: $scope.form.Sname,
            BrContact: $scope.form.Sphone,
            BrEmail: $scope.form.Semail,
            BrFullAddress: $scope.form.SAdd1 + " " + $scope.form.SAdd2 + " " + $scope.form.SAdd3,

            BIAdd1: $scope.form.BAdd1,
            BIAdd2: $scope.form.BAdd2,
            BIAdd3: $scope.form.BAdd3,

            BICity: BillcityID,
            BICityName: BillCityName,
            BlState: $scope.form.BState,
            BlCountry: Billcountryid,
            BillCountryName: Billcountryname,

            BIpin: $scope.form.BZip,
            BIConName: $scope.form.Bname,
            BIContact: $scope.form.Bphone,
            BIEmail: $scope.form.Bemail,
            NeedDelivery: $scope.form.NeedDelivery,
            NeedWarning: $scope.form.NeedWarning,
            FeeWarning: $scope.form.FeeWarning,
            Type: txttype

        };
        //alert(JSON.stringify(Data));
        //  console.log(Data);

        CustomerBranchesFactory.savePostData(Data)
            .then(function (response) {
                // alert(response.data.length);
                if (response.data.length != 0) {
                    alert('Request has been saved successfully.');

                    $scope.IsAdd = false;
                    $scope.IstxtHide = false;
                    $scope.IsDdlHide = true;
                    ResetData();
                }
            });
    };

    $scope.AddNew = function()
    {

        debugger;
        $scope.IsAdd = true;
        $scope.IstxtHide = true;
        $scope.IsDdlHide = false;
        ResetData();
    }
    function ResetData() {
        //$scope.form.CustomerId = 0;
        $scope.form.DisplayName = '';
        //$scope.form.BranchName = '';

        $scope.form.HeadOffice = '';
        $scope.form.PreSet = '';
        $scope.form.SAdd1 = '';
        $scope.form.SAdd2 = '';
        $scope.form.SAdd3 = '';
        $scope.form.SState = '';

        $scope.form.SZip = '';
        $scope.form.Sname = '';
        $scope.form.Sphone = '';

        $scope.form.Semail = '';
        $scope.form.SFullAdd = '';

        if ($scope.IsAdd) {
            $scope.form.addbranchname = '';
        } else
        {
            // $scope.Branchlst = $scope.ddlBranchlst;
            $scope.form.BranchName = 0;
        }
      
        $scope.form.BAdd1 = '';
        $scope.form.BAdd2 = '';
        $scope.form.BAdd3 = '';
        $scope.form.BState = '';
        $scope.form.BZip = '';


        $scope.form.Bemail = '';
        $scope.form.Bphone = '';
        $scope.form.Bname = '';
        $scope.form.NeedDelivery = '';
        $scope.form.NeedWarning = '';
        $scope.form.FeeWarning = '';

        //document.getElementById("ex1_value").value = response.data[0][0].BrCityName;
        //document.getElementById("ex2_value").value = response.data[0][0].BrCountryName;
        //document.getElementById("ex3_value").value = response.data[0][0].BICityName;
        //document.getElementById("ex4_value").value = response.data[0][0].BillCountryName;

        $scope.$broadcast('angucomplete-alt:changeInput', 'ex1', { CityName: '', CityID: 0 });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex2', { CountryName: '', CountryId: 0 });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: '', CityID: 0 });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: '', CountryId: 0 });


        $scope.BrrCityName = '';
        $scope.BrrCountryName = '';

        $scope.BIICityName = '';
        $scope.BIICountryName = '';
    }

    

}]);