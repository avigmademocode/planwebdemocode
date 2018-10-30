HomeApp.controller('NewRequestController', ['$scope', '$window', 'NewRequestFactory', function ($scope, $window, NewRequestFactory) {
    // debugger;
    $scope.formData = {};
    $scope.custList = [];
    $scope.City = [];
    $scope.Country = [];
    $scope.CustomerId;
    $scope.level = [];
    $scope.desg = '';
    $scope.orderID = '';
    $scope.RefNoAuto = [];
    $scope.EditOrderID = 0;
    $scope.isDisabled;
    $scope.Type;
    $scope.pagename = '';

    function GetCityIDByName(CityName)
    {
        debugger;
        var CityID = $scope.City.find(v => v.CityName == CityName).CityID
        return CityID;
    }

    function GetCountryIDByName(CountryName)
    {
        debugger;
        var CountryID = $scope.Country.find(v => v.CountryName == CountryName).CountryId
        return CountryID;
    }

    function GetShipToAndDilvery(CustID,Shipto,DeliveryTo) {



        NewRequestFactory.getDeleveryAndTerms(CustID)
            .then(function (response) {
                // alert(response.data.length);
                if (response.data.length != 0) {
               //     debugger;
                    $scope.ShipTo = response.data[0];
                    $scope.Delivery = response.data[1];
                    $scope.desg = response.data[2][0].Desg;  // If this True show The box with Title else not 
                    $scope.level = response.data[2][0].level; // Based on level need to poulate dynamically
                    $scope.RefNoAuto = response.data[2][0].RefNoAuto;
                    $scope.isDisabled = $scope.RefNoAuto;

                    if (Shipto != 0)
                    {
                        var index = $scope.ShipTo.findIndex(obj => obj.BranchID == Shipto);
                        $scope.formData.BranchID = $scope.ShipTo[index].BranchID;
                       // $scope.formData.BranchID = 1;
                      
                    }
                    if (DeliveryTo != 0 ) {
                        var index = $scope.Delivery.findIndex(obj => obj.IncoTermID == DeliveryTo);
                        $scope.formData.DeliveryTo = $scope.Delivery[1].IncoTermID;
                    }
                    /*
                    if ($scope.level > 0) {
                        $scope.ApprovarArray = []
                        for (var i = 0; i < $scope.level; i++) {
                            $scope.ApprovarArray[i] = i;
                        }
                    }
                    else { $scope.ApprovarArray[i] = 1; }
                   
*/

                    //debugger;
                    $scope.ApprovarArray = []
                    if ($scope.level > 0) {
                        for (var i = 0; i < $scope.level; i++) {
                            if ($scope.desg) {
                                var Aprrover = {};
                                Aprrover.LabelName = response.data[3][i].ApproverNameDisplay;
                                if ($scope.ApprovarList != null || $scope.ApprovarList != undefined)
                                {
                                    Aprrover.AprName = $scope.AprName = $scope.ApprovarList[i].ApproverName;
                                    Aprrover.AprTitle = $scope.AprTitle = $scope.ApprovarList[i].ApproverTitle;
                                    Aprrover.AprEmail = $scope.AprEmail =  $scope.ApprovarList[i].ApproverEmail;

                                }
                                else
                                {
                                    Aprrover.AprName = $scope.AprName;
                                    Aprrover.AprTitle = $scope.AprTitle;
                                    Aprrover.AprEmail = $scope.AprEmail;

                                }
                                
                                $scope.ApprovarArray.push(Aprrover);
                            }
                            else
                            {
                                var Aprrover = {};
                                Aprrover.LabelName = response.data[3][i].ApproverNameDisplay;
                                if ($scope.ApprovarList != null || $scope.ApprovarList != undefined) {
                                    Aprrover.AprName = $scope.AprName =  $scope.ApprovarList[i].ApproverName;

                                    Aprrover.AprEmail = $scope.AprEmail = $scope.ApprovarList[i].ApproverEmail;
                                }
                                else
                                {
                                    Aprrover.AprName = $scope.AprName;

                                    Aprrover.AprEmail = $scope.AprEmail;
                                }
                                $scope.ApprovarArray.push(Aprrover);
                            }

                        }
                    }
                    else {
                        if ($scope.desg)
                        {
                            var Aprrover = {};
                            if ($scope.ApprovarList != null || $scope.ApprovarList != undefined) {
                                Aprrover.AprName = $scope.AprName = $scope.ApprovarList[i].ApproverName;
                                Aprrover.AprTitle = $scope.AprTitle = $scope.ApprovarList[i].ApproverTitle;
                                Aprrover.AprEmail = $scope.AprEmail = $scope.ApprovarList[i].ApproverEmail;

                            }
                            else
                            {
                                Aprrover.AprName = $scope.AprName;
                                Aprrover.AprTitle = $scope.AprTitle;
                                Aprrover.AprEmail = $scope.AprEmail;

                            }

                            $scope.ApprovarArray.push(Aprrover);
                        }
                        else {
                            var Aprrover = {};
                            if ($scope.ApprovarList != null || $scope.ApprovarList != undefined) {
                                Aprrover.AprName = $scope.AprName = $scope.ApprovarList[i].ApproverName;
                                
                                Aprrover.AprEmail = $scope.AprEmail = $scope.ApprovarList[i].ApproverEmail;
                            }
                            else
                            {
                                Aprrover.AprName = $scope.AprName;

                                Aprrover.AprEmail = $scope.AprEmail;
                            }
                            $scope.ApprovarArray.push(Aprrover);
                        }
                    }


                }
            });
    }
    $scope.GetCustomersList = function () {
       //debugger;
        Init();

        $scope.pagename = window.location.search.slice(1);
        if ($scope.pagename == 'Prod')
        {
            if ($window.localStorage.getItem('pageID') != null && $window.localStorage.getItem('pageID') != undefined && $window.localStorage.getItem('pageID') != '') {
                var pageID = JSON.parse($window.localStorage.getItem('pageID'));
                if (pageID == '10') {
                    if ($window.localStorage.getItem('orderID') != null && $window.localStorage.getItem('orderID') != undefined && $window.localStorage.getItem('orderID') != '') {
                        $scope.EditOrderID = JSON.parse($window.localStorage.getItem('orderID'));
                        $scope.Type = 1;
                    }
                    else {
                        $scope.EditOrderID = $scope.EditOrderID ? $scope.EditOrderID.split('?')[1] : window.location.search.slice(1);
                        $scope.Type = 2;
                    }
                }
                else {
                    $window.localStorage.removeItem('orderID');
                    $scope.EditOrderID = $scope.EditOrderID ? $scope.EditOrderID.split('?')[1] : window.location.search.slice(1);
                    $scope.Type = 2;
                }
            }
            else {
                $window.localStorage.removeItem('orderID');
                $scope.EditOrderID = $scope.EditOrderID ? $scope.EditOrderID.split('?')[1] : window.location.search.slice(1);
                $scope.Type = 2;
            }
        }
        else
        {
            $window.localStorage.removeItem('orderID');
            $window.localStorage.removeItem('pageID');
            $scope.buttonsPEnabled = true;
            $scope.buttonsEnabled = true;

            $scope.EditOrderID = $scope.EditOrderID ? $scope.EditOrderID.split('?')[1] : window.location.search.slice(1);
            if ($scope.EditOrderID != '' && $scope.EditOrderID != null) {
                $scope.Type = 2;
            }
            else
            {
                $scope.Type = 1;
            }
        }
        
      
        
       
        NewRequestFactory.GetCustomers(1)
            .then(function (response) {
                //debugger;
                if (response.data.length != 0) {
                    //$scope.custList = [{ "CustId": 1, "CustName": "International Rescue Committee", "Acronym": "IRC", "NoofBranches": 0, "LevelofAuthority": 2, "Code": "Rescue", "Ticker": "Ticker", "InDemo": false, "TieredPricing": true, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }, { "CustId": 3, "CustName": "Save the Children", "Acronym": "SCI", "NoofBranches": 0, "LevelofAuthority": 3, "Code": "SCI-IPU", "Ticker": "", "InDemo": false, "TieredPricing": true, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }, { "CustId": 5, "CustName": "Mercy Corps", "Acronym": "MC", "NoofBranches": 0, "LevelofAuthority": 3, "Code": "MC123", "Ticker": "", "InDemo": false, "TieredPricing": false, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }, { "CustId": 7, "CustName": "Catholic Relief Services", "Acronym": "CRS", "NoofBranches": 0, "LevelofAuthority": 2, "Code": "CRS2018", "Ticker": "", "InDemo": false, "TieredPricing": false, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }, { "CustId": 8, "CustName": "Demo NGO", "Acronym": "NGO", "NoofBranches": 0, "LevelofAuthority": 0, "Code": "NGO", "Ticker": "", "InDemo": false, "TieredPricing": false, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }, { "CustId": 9, "CustName": "TestCustomer", "Acronym": "Test", "NoofBranches": 0, "LevelofAuthority": null, "Code": "Ticker", "Ticker": "Ticker", "InDemo": false, "TieredPricing": true, "HOAdd1": null, "HOAdd2": null, "HOAdd3": null, "HOCITY": null, "HOState": null, "HOCountry": null, "HOPin": null, "HOFullAddress": null, "HOEmailId": null, "HOContactPerson": null }];
                    $scope.custList = response.data[0];
                    $scope.SCity = response.data[1];
                    $scope.City = response.data[1];
                    $scope.SCountry = response.data[2];
                    $scope.Country = response.data[2];

                    $scope.BCity = response.data[1];
                    $scope.BCountry = response.data[2];

                    if (response.data[0].length == 1) {
                        $scope.CustomerId = response.data[0][0].CustId;
                        $window.localStorage.setItem('CustomerId', $scope.CustomerId);
                        GetShipToAndDilvery($scope.CustomerId, 0, 0);
                       

                        // $scope.selectedCustomer = { CustName: response.data[0][0].CustName, CustId: response.data[0][0].CustId };
                        $scope.divEnabled = true;
                    }
                    else {
                        $scope.divEnabled = false;
                    }

                }
                else {
                    alert('Failled!!!');
                }
            });
        //debugger
        if ($scope.EditOrderID != 0) {
            NewRequestFactory.GetCustomersData($scope.EditOrderID, $scope.Type)
                .then(function (response) {
                    debugger;
                    if (response.data.length != 0) {

                        if (response.data[3][0].length != 0)
                        {
                            $scope.ApprovarList = response.data[3][0];


                        }


                        $scope.CustomerId = response.data[0][0].CustId;
                        $window.localStorage.setItem('CustomerId', $scope.CustomerId);
                        $scope.formData.Refernce = response.data[0][0].ReferenceNo;
                        $scope.formData.Department = response.data[0][0].Department;
                        $scope.formData.Sname = response.data[0][0].ContactName;
                        $scope.formData.Sphone = response.data[0][0].ContactNum;
                        $scope.formData.Semail = response.data[0][0].ContactEmail;
                        $scope.formData.SAdd1 = response.data[1][0];
                        $scope.formData.SAdd2 = response.data[1][1];
                        $scope.formData.SAdd3 = response.data[1][2];
                        var SCityName = response.data[1][3];
                        var SCityID = response.data[0][0].CityId;
                        $scope.formData.SState = response.data[1][4];
                        $scope.formData.SZip = response.data[1][5];
                        var SCountryName = response.data[1][6];
                        var SCountryID = response.data[0][0].CountryId;

       
                        $scope.formData.BAdd1 = response.data[2][0];
                        $scope.formData.BAdd2 = response.data[2][1];
                        $scope.formData.BAdd3 = response.data[2][2];
                        var BCityName = response.data[2][3];
                        $scope.formData.BState = response.data[2][4];

                       

                        $scope.formData.BZip = response.data[2][5];
                        var BCountryName = response.data[2][6];
                        var index = $scope.custList.findIndex(obj => obj.CustId == response.data[0][0].CustId);
                        $scope.formData.selectedCustomer = $scope.custList[index].CustId;

                        $scope.$broadcast('angucomplete-alt:changeInput', 'ex1', { CityName: SCityName, CityID: SCityID });
                        $scope.$broadcast('angucomplete-alt:changeInput', 'ex2', { CountryName: SCountryName, CountryId: SCountryID });

                        var BCityID = GetCityIDByName(BCityName);
                        var BCountryID = GetCountryIDByName(BCountryName);

                        $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: BCityName, CityID: BCityID });
                        $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: BCountryName, CountryId: BCountryID });

                        GetShipToAndDilvery($scope.formData.selectedCustomer, parseInt(response.data[0][0].BranchId), parseInt(response.data[0][0].IncoTermId));

                      //  $scope.formData.ShipTo = parseInt(response.data[0][0].BranchId);
                      //  $scope.formData.DeliveryTo = parseInt(response.data[0][0].IncoTermId);

                        if ($scope.pagename == 'Prod')
                        {
                            $scope.buttonsPEnabled = true;
                            $scope.buttonsEnabled = false;
                        }
                        else
                        {
                            $scope.buttonsPEnabled = false;
                            $scope.buttonsEnabled = false;
                        }
                     

                    }
                    else
                    {
                        $window.localStorage.clear();
                    }
                });
        }
        else
        {
            $scope.buttonsEnabled = true;
            window.localStorage.removeItem("CustomerId");
            window.localStorage.removeItem("productlist");
        }
    }
    
    $scope.GetCustomersList();
    $scope.ApprovarArray = [];

   
    $scope.GetDeleveryAndTerms = function () {
        ResetData();
        //$scope.ApprovarArray = [];
        $scope.CustomerId = $scope.formData.selectedCustomer;
        $window.localStorage.setItem('CustomerId', $scope.CustomerId);
        GetShipToAndDilvery($scope.formData.selectedCustomer,0,0);

    }



    //$scope.selectedShipTo = function (selected) {
    //    alert(selected);
    //};

    //$scope.GetCustDetails = function () {
    //    NewRequestFactory.GetCustDetails($scope.selectedCustomer, $scope.selectedShipTo)
    //        .then(function (response) {
    //            alert(response.data.length);
    //            if (response.data.length != 0) {


    //            }
    //        });
    $scope.BranchID = '';
    $scope.selectedShipTo = function (selected) {
        //$scope.BranchID = selected.originalObject.BranchID ? selected.originalObject.BranchID : '';
        $scope.BranchID = selected;
        ResetData();
        NewRequestFactory.GetCustDetails($scope.CustomerId, $scope.BranchID)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger;
                    $scope.formData.SAdd1 = response.data[0].ShipAddress1;
                    $scope.formData.SAdd2 = response.data[0].ShipAddress2;
                    $scope.formData.SAdd3 = response.data[0].ShipAddress3;
                    // $scope.formData.SCity = response.data[0].ShipCity;
                    
                    //$scope.initialSCityValue = { CityName: response.data[0].ShipCityName, CityID: response.data[0].ShipCity };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'ex1', { CityName: response.data[0].ShipCityName, CityID: response.data[0].ShipCity });

                    $scope.formData.SState = response.data[0].ShipState;
                    $scope.formData.SZip = response.data[0].ShipPin;
                    //$scope.formData.SCountry = response.data[0].ShipCountry;

                    //$scope.initialSCountryValue = { CountryName: response.data[0].ShipCountryName, CountryId: response.data[0].ShipCountry };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'ex2', { CountryName: response.data[0].ShipCountryName, CountryId: response.data[0].ShipCountry });

                    $scope.formData.Sname = response.data[0].ContactPerson;
                    $scope.formData.Sphone = response.data[0].ShipPhoneNo;
                    $scope.formData.Semail = response.data[0].ShipEmail;


                    $scope.formData.BAdd1 = response.data[0].BillAddress1;
                    $scope.formData.BAdd2 = response.data[0].BillAddress2;
                    $scope.formData.BAdd3 = response.data[0].BillAddress3;
                    //$scope.formData.BCity = response.data[0].BillCity;

                    //$scope.initialBCityValue = { CityName: response.data[0].ShipCityName, CityID: response.data[0].ShipCity };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: response.data[0].ShipCityName, CityID: response.data[0].ShipCity });

                    $scope.formData.BState = response.data[0].BillState;
                    $scope.formData.BZip = response.data[0].BillPin;
                    // $scope.formData.BCountry = response.data[0].BillCountry;

                    //$scope.initialBCountryValue = { CountryName: response.data[0].ShipCountryName, CountryId: response.data[0].ShipCountry };
                    $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: response.data[0].ShipCountryName, CountryId: response.data[0].ShipCountry });

                    FullAddress = response.data[0].FullAddress;
                }
            });
    }



    function ResetData() {
        //debugger;
       // $scope.$broadcast('angucomplete-alt:clearInput');
        //$scope.Delivery = '';
        $scope.formData.SAdd1 = '';
        $scope.formData.SAdd2 = '';
        $scope.formData.SAdd3 = '';
        $scope.formData.SCity = $scope.City;
        $scope.formData.SState = '';
        $scope.formData.SZip = '';
        $scope.formData.SCountry = $scope.Country;
        $scope.formData.Sname = '';
        $scope.formData.Sphone = '';
        $scope.formData.Semail = '';


        $scope.formData.BAdd1 = '';
        $scope.formData.BAdd2 = '';
        $scope.formData.BAdd3 = '';
        $scope.formData.BCity = $scope.City;
        $scope.formData.BState = '';
        $scope.formData.BZip = '';
        $scope.formData.BCountry = $scope.Country;

        $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: '', CountryId: 0 });
        $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: '', CityID: 0 });

        
    } 


    $scope.BackToPrevious = function () {
        debugger;
       
        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.EditOrderID)); 
    }


    $scope.Next = function () {
        debugger;
        $window.localStorage.setItem('pageID', JSON.stringify('1'));
        if ($scope.pagename == 'Prod') {
            $window.location.href = '/Product';
        }
        else
        {
            $window.location.href = '/Product?' + encodeURIComponent(decodeURIComponent($scope.EditOrderID));
        }

    }

    



    $scope.CoppyAddress = function () {
        debugger;
        if ($scope.chkCoppy == true) {
            // alert('Address Coppied.');
            $scope.formData.BAdd1 = $scope.formData.SAdd1;
            $scope.formData.BAdd2 = $scope.formData.SAdd2;
            $scope.formData.BAdd3 = $scope.formData.SAdd3;
            
        

            
            if ($scope.selectedCityTo != 'undefined' && $scope.selectedCityTo != '' && $scope.selectedCityTo != undefined )
            {
                var cityid = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
                var cityname = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName:'';
                //$scope.initialBCityValue = { CityName: cityname, CityID: cityid };
                $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: cityname, CityID: cityid });
                
            }
            if ($scope.selectedCountryTo != 'undefined' && $scope.selectedCountryTo != '' && $scope.selectedCountryTo != undefined ) {
               
                var countryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
                var countryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';
                //$scope.initialBCountryValue = { CountryName: countryname, CountryId: countryid };
                $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: countryname, CountryId: countryid });
            }
             


       
            $scope.formData.BState = $scope.formData.SState;
            $scope.formData.BZip = $scope.formData.SZip;
         

        }
        else {
            $scope.formData.BAdd1 = '';
            $scope.formData.BAdd2 = '';
            $scope.formData.BAdd3 = '';
            $scope.formData.BState = '';
            $scope.formData.BZip = '';
            $scope.BCity = $scope.City;
            $scope.$broadcast('angucomplete-alt:changeInput', 'ex4', { CountryName: '', CountryId: 0 });
            $scope.$broadcast('angucomplete-alt:changeInput', 'ex3', { CityName: '', CityID: 0 });
            // $scope.initialBCountryValue = { CountryName: '', CountryId: '' };
        }
    }
   
    function Init()
    {
        //  debugger;

     

        $scope.formData.Refernce = '';
        $scope.formData.Department = '';

        $scope.selectedShipTo = '';
        $scope.selectedDeliveryTo = '';

        $scope.selectedCityTo = '';
        $scope.selectedCountryTo = '';

        $scope.selectedBCityTo = '';
        $scope.selectedBCountryTo = '';

        $scope.chkCoppy = false;
    }
  
    $scope.SaveNewRequest = function (ApprovarArray) {
        debugger;
        
        //if ($scope.selectedDeliveryTo != '')
        //{
        //    $scope.formData.DeliveryTo = $scope.selectedDeliveryTo.originalObject.IncoTermID ? $scope.selectedDeliveryTo.originalObject.IncoTermID : '';
        //}
        //if ($scope.selectedShipTo != '')
        //{
        //    $scope.formData.BranchID = $scope.BranchID;
        //}
        var ShipcityID, ShipCityName, BillcityID, BillCityName, Shipcountryid, Shipcountryname, Billcountryid,Billcountryname;

        if ($scope.selectedCityTo != '')
        {
            ShipcityID = $scope.selectedCityTo.originalObject.CityID ? $scope.selectedCityTo.originalObject.CityID : '';
            ShipCityName = $scope.selectedCityTo.originalObject.CityName ? $scope.selectedCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedCountryTo != '') {
            Shipcountryid = $scope.selectedCountryTo.originalObject.CountryId ? $scope.selectedCountryTo.originalObject.CountryId : '';
            Shipcountryname = $scope.selectedCountryTo.originalObject.CountryName ? $scope.selectedCountryTo.originalObject.CountryName : '';
        }
        if ($scope.selectedBCityTo != '' ) {
            BillcityID = $scope.selectedBCityTo.originalObject.CityID ? $scope.selectedBCityTo.originalObject.CityID : '';
            BillCityName = $scope.selectedBCityTo.originalObject.CityName ? $scope.selectedBCityTo.originalObject.CityName : '';
        }
        if ($scope.selectedBCountryTo != '' && $scope.selectedBCountryTo != undefined && $scope.selectedBCountryTo != 'undefined') {
            Billcountryid = $scope.selectedBCountryTo.originalObject.CountryId ? $scope.selectedBCountryTo.originalObject.CountryId : '';
            Billcountryname = $scope.selectedBCountryTo.originalObject.CountryName ? $scope.selectedBCountryTo.originalObject.CountryName : '';
        }
        else
        {
            Billcountryid = 0;
            Billcountryname = "";
        }
        var bool = validation();
        if (bool) {
            var approverlist = ApprovarArray;
            //debugger;
            var orderID = $scope.EditOrderID;
            //  var inData = { 'ApproverDetails': JSON.stringify(approverlist) };
            var Data =
                {
                    

                    selectedCustomer: $scope.CustomerId,
                    Refernce: $scope.formData.Refernce,
                    Department: $scope.formData.Department,
                    // Office: $Scope.formData.Office,
                    BranchID: $scope.formData.BranchID,
                    DeliveryTo: $scope.formData.DeliveryTo,
                    SAdd1: $scope.formData.SAdd1,
                    SAdd2: $scope.formData.SAdd2,
                    SAdd3: $scope.formData.SAdd3,
                    SCity: ShipcityID,
                    SCityName: ShipCityName,
                    SState: $scope.formData.SState,
                    SZip: $scope.formData.SZip,
                    SCountry: Shipcountryid,
                    SCountryName: Shipcountryname,
                    Sname: $scope.formData.Sname,
                    Sphone: $scope.formData.Sphone,
                    Semail: $scope.formData.Semail,
                    BAdd1: $scope.formData.BAdd1,
                    BAdd2: $scope.formData.BAdd2,
                    BAdd3: $scope.formData.BAdd3,
                    BCity: BillcityID,
                    BCityName: BillCityName,
                    BState: $scope.formData.BState,
                    BZip: $scope.formData.BZip,
                    BCountry: Billcountryid,
                    BCountryName: Billcountryname,
                    ApproverDetails: JSON.stringify(approverlist),
                    OrderID: orderID,
                    Type: $scope.Type
                }

            NewRequestFactory.saveNewRequest(Data)
           // NewRequestFactory.saveNewRequest($scope.formData)
                .then(function (response) {
                    // alert(response.data.length);
                    if (response.data.length != 0) {
                        //debugger;
                        $scope.orderID = response.data[0].TempOrderID;
                       
                        if (response.data[0].RetVal == "99")
                        {
                            alert('Reference Number Already Exist.');
                        }
                        if (response.data[1] == "1") {
                            debugger;

                            if ($scope.orderID != 0) {
                                $window.localStorage.setItem('orderID', $scope.orderID);
                                alert('Request has been saved successfully.');
                                $window.localStorage.removeItem('pageID');
                                $window.localStorage.setItem('pageID', JSON.stringify('9'));
                                $window.location.href = '/Product';
                            }
                        }
                        else if (response.data[1] == "2") {
                            alert('Request has been saved successfully.');
                        }
                        
                    }
                });
        }
    }
    var bool = true;
   
    function validation()
    { 
        //debugger;
        if ($scope.RefNoAuto == false) {


            if ($scope.formData.Refernce == '' || $scope.formData.Refernce == undefined || $scope.formData.Refernce == 'undefined') {
                alert('Please Enter Reference No');
                bool = false
                return bool;
            }
        }
        if ($scope.formData.Department == '' || $scope.formData.Department == undefined || $scope.formData.Department == 'undefined')
        {
            alert('Please Enter Department');
            bool = false
            return bool;
        }
        //if ($scope.selectedShipTo == '' || $scope.selectedShipTo == 'undefined' || $scope.BranchID == '') {
        //    alert('Please Enter Ship To');
        //    bool = false
        //    return bool;
        //}
        if ($scope.formData.BranchID == '' || $scope.formData.BranchID == 'undefined' || $scope.formData.BranchID == '') {
            alert('Please Enter Ship To');
            bool = false
            return bool;
        }

        //if ($scope.selectedDeliveryTo == '' || $scope.selectedDeliveryTo == 'undefined') {
        //    alert('Please Enter Delivery Terms');
        //    bool = false
        //    return bool;
        //}

        if ($scope.formData.DeliveryTo == '' || $scope.formData.DeliveryTo == 'undefined' || $scope.formData.DeliveryTo == undefined) {
            alert('Please Enter Delivery Terms');
            bool = false
            return bool;
        }
    


        if ($scope.formData.SAdd1 == '' || $scope.formData.SAdd1 == undefined || $scope.formData.SAdd1 == 'undefined') {
            alert('Please Enter Shipping Address');
            bool = false
            return bool;
        }


        if ($scope.formData.Sname == null ||$scope.formData.Sname == '' || $scope.formData.Sname == undefined || $scope.formData.Sname == 'undefined') {
            alert('Please Enter Delivery Contact Name');
            bool = false
            return bool;
        }

        if ($scope.formData.Sphone == null || $scope.formData.Sphone == '' || $scope.formData.Sphone == undefined || $scope.formData.Sphone == 'undefined') {
            alert('Please Enter Delivery Contact Phone Number');
            bool = false
            return bool;
        }

        if ($scope.formData.Semail == null || $scope.formData.Semail == '' || $scope.formData.Semail == undefined || $scope.formData.Semail == 'undefined') {
            alert('Please Enter Delivery Contact Email');
            bool = false
            return bool;
        }
        bool = true;
        return bool;
    }
    //$scope.SaveDummyData = function () {
    //    debugger
    //    alert('Passed')
    //    $scope.formData = {};
    //    $scope.formData.CustName = 'Ramesh Verma';
    //    NewRequestFactory.saveNewRequest($scope.formData)
    //        .then(function (response) {
    //            alert(response.data.length);
    //            if (response.data.length != 0) {
    //                alert('Request has been saved successfuully.');
    //            }
    //        });
    //}


}]);