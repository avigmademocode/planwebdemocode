HomeApp.controller('ItsetupController', ['$scope', '$window', 'ItsetupFactory', function ($scope, $window, ItsetupFactory) {

    $scope.OrderID = $scope.OrderID ? $scope.OrderID.split('?')[1] : window.location.search.slice(1);
    $scope.RequestDetails;

    $scope.showFlag = false;
    $scope.btnHide = true;
    $scope.btnSoftSaveAll = false;
    $scope.btnITSaveAll = false;
    //dropdown hide n show
    $scope.getproduct = function (item) {
        //alert(JSON.stringify(item));
        $scope.quantity = item.Quantity;
        $scope.ITFlag = item.ItFormRequired;
        $scope.softflag = item.SoftwareFormRequired;
        $scope.ProductID = item.ProdID;
        $scope.productName = item.PartNo;

        $scope.getITSetupDataByOrderId();
        $scope.getSoft();
        $scope.getTotalQuantity();

    }

    $scope.ViewRequestOrderDetails = function () {
        //debugger;
        ItsetupFactory.ViewAddressDetails($scope.OrderID)
            .then(function (response) {

                if (response.data[0][0].length != 0) {
                    $scope.RequestDetails = response.data[0][0];
                }
                if (response.data[0][1].length != 0) {
                    $scope.ApprovarDetails = response.data[0][1];
                }
                debugger
                if (response.data[0][2].length != 0) {
                    $scope.CartItems = response.data[0][2];
                    //$scope.FormTotalLength = $scope.CartItems.length;  /// all data length

                    //var Quantity = $scope.CartItems.find(v => v.ProdID == 1316).Quantity;
                    //var ItFormRequired = $scope.CartItems.find(v => v.ProdID == 1316).ItFormRequired;
                    //var SoftwareFormRequired = $scope.CartItems.find(v => v.ProdID == 1316).SoftwareFormRequired;
                    //var PartNo = $scope.CartItems.find(v => v.ProdID == 1316).PartNo;
                }
                if (response.data[0][3].length != 0) {
                    $scope.SAdd1 = response.data[0][3][0];
                    $scope.SAdd2 = response.data[0][3][1];
                    $scope.SAdd3 = response.data[0][3][2];
                    $scope.SCity = response.data[0][3][3];
                    $scope.SState = response.data[0][3][4];
                    $scope.SZip = response.data[0][3][5];
                    $scope.SCountry = response.data[0][3][6];
                }
                if (response.data[0][4].length != 0) {
                    $scope.BAdd1 = response.data[0][4][0];
                    $scope.BAdd2 = response.data[0][4][1];
                    $scope.BAdd3 = response.data[0][4][2];
                    $scope.BCity = response.data[0][4][3];
                    $scope.BState = response.data[0][4][4];
                    $scope.BZip = response.data[0][4][5];
                    $scope.BCountry = response.data[0][4][6];
                }
                if (response.data[0][5].length != 0) {
                    $scope.Prodcount = response.data[0][5][0].ProdCount;
                }
                if (response.data[1][0].length != 0) {
                    $scope.Freight = response.data[1][0];
                }

                $scope.getTotalQuantity();
            });
    }  // top address code here
    $scope.ViewRequestOrderDetails();




    $scope.getTotalQuantity = function () {
        var qty = 0;
        var temp = 0;
        for (var i = 0; i < $scope.CartItems.length; i++) {

            temp = $scope.CartItems[i].Quantity;
            qty = qty + temp;
        }
        $scope.Grandqty = qty;
    }
   




    //form meka dropdown
    $scope.GetDropdowunALL = function () {
        //debugger;
        ItsetupFactory.GetUserType()
            .then(function (response) {
                
                $scope.UserTypeVal = response.data[0];
                $scope.WorkLocationVal = response.data[1];
            });
    }
    $scope.GetDropdowunALL();

    //get shipment data by order Id
    $scope.getShipmentDataByOrderId = function () {

        ItsetupFactory.GetShipmentData($scope.OrderID)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    $scope.shipData = response.data[0];

                    for (var j = 0; j < $scope.shipData.length; j++) {
                        if ($scope.shipData[j].DeliveryDate != null) {
                            var dateOut = new Date($scope.shipData[j].DeliveryDate);
                            $scope.shipData[j].DeliveryDate = dateOut;
                        } else {
                            $scope.shipData[j].DeliveryDate = null;
                        }
                        if ($scope.shipData[j].ShipmentDate != null) {
                            var dateOutx = new Date($scope.shipData[j].ShipmentDate);
                            $scope.shipData[j].ShipmentDate = dateOutx;
                        } else {
                            $scope.shipData[j].ShipmentDate = null;
                        }
                    }
                }

            });

    }
    $scope.getShipmentDataByOrderId();


    //get Order ITSetup data by order Id
    $scope.getITSetupDataByOrderId = function () {
        $scope.ITSetupDataFilter = [];
        ItsetupFactory.GetITSetUpData($scope.OrderID)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    $scope.ITSetupData = response.data[0];
                    //actual length IT setup
                    $scope.AcutalITLength = $scope.ITSetupData.length;

                   // filter by ProduID
                    for (var i = 0; i < $scope.ITSetupData.length; i++) {
                        if ($scope.ITSetupData[i].ProductId == $scope.ProductID) {

                            var obj = {};
                            obj.ITSetUpId = $scope.ITSetupData[i].ITSetUpId;
                            obj.Applications = $scope.ITSetupData[i].Applications;
                            obj.DeliveryLocation = $scope.ITSetupData[i].DeliveryLocation;
                            obj.Department = $scope.ITSetupData[i].Department;
                           
                            obj.MigrateInfo = $scope.ITSetupData[i].MigrateInfo;
                            obj.OrderId = $scope.ITSetupData[i].OrderId;
                            obj.PartNo = $scope.ITSetupData[i].PartNo;
                            obj.ProductId = $scope.ITSetupData[i].ProductId;
                            obj.Serial = $scope.ITSetupData[i].Serial;
                            obj.SpecialInstructions = $scope.ITSetupData[i].SpecialInstructions;
                            obj.UserName = $scope.ITSetupData[i].UserName;

                            obj.UserType = $scope.ITSetupData[i].UserType;
                            obj.UserTypeId = $scope.ITSetupData[i].UserTypeId;
                            obj.WorkLocation = $scope.ITSetupData[i].WorkLocation;
                            obj.WorkLocationId = $scope.ITSetupData[i].WorkLocationId;

                            $scope.ITSetupDataFilter.push(obj);

                        }
                    }
                    // length of 
                    $scope.TotalLength = $scope.ITSetupDataFilter.length;

                }

            });

    }
  


    $scope.Back = function () {
        debugger;

        $window.location.href = '/OrderSummary?' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    ///// migrateinfo
    $scope.migrateinfo = [
        { id: 2, name: "Unknown" },
        { id: 1, name: "Yes" },
        { id: 0, name: "No" }
    ];
   
    
    // get edit IT
    $scope.GetEdit = function (item) {
        //alert(JSON.stringify(item));
        $scope.form.EditNow = item;
       
        $scope.form.EditNow.ITSetUpId = item.ITSetUpId
        $scope.form.EditNow.EditName = item.UserName;
        $scope.form.EditNow.EditType = item.UserTypeId;
        $scope.form.EditNow.EditWorkLocation = item.WorkLocationId;
        $scope.form.EditNow.EditDeliveryLocation = item.DeliveryLocation;
        $scope.form.EditNow.EditDepartment = item.Department;
        $scope.form.EditNow.EditApplications = item.Applications;
        $scope.form.EditNow.EditSpecial = item.SpecialInstructions;
        $scope.form.EditNow.Editmigrate = item.MigrateInfo;

        if ($scope.quantity == $scope.TotalLength) {
            $scope.btnHide = false;
            $scope.btnITSaveAll = true;
        }

    }

    $scope.submitForm = function () {
        //debugger
        var TypeVAL = 1;
        var ITSetUpIdVAL = 0;
        if ($scope.form.EditNow.ITSetUpId != undefined) { // checking for new or not
            ITSetUpIdVAL = $scope.form.EditNow.ITSetUpId;
            TypeVAL = 2;
        }
        var data = {
            ITSetUpId: ITSetUpIdVAL,
            strOrderID: $scope.OrderID,
            ProductId: $scope.ProductID,
            Type: TypeVAL,
            Serial: 1,

            UserName: $scope.form.EditNow.EditName,
            UserTypeId: $scope.form.EditNow.EditType,
            WorkLocationId: $scope.form.EditNow.EditWorkLocation,
            DeliveryLocation: $scope.form.EditNow.EditDeliveryLocation,
            Department: $scope.form.EditNow.EditDepartment,
            Applications: $scope.form.EditNow.EditApplications,
            SpecialInstructions: $scope.form.EditNow.EditSpecial,
            MigrateInfo: $scope.form.EditNow.Editmigrate,

            isActive:1

        }
        
        //alert(JSON.stringify(data));
        //debugger;
        ItsetupFactory.SaveITSetUpData(data)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    alert('Request has been Updated successfully.');

                    $scope.form.EditNow = [];
                    $scope.btnHide = true;
                    $scope.btnITSaveAll = false;
                    $scope.getITSetupDataByOrderId();
                    $scope.event = {
                        type: {
                            checked: false
                        }
                    };// for check box uncheck..
                }
            });
    }

   
   

    $scope.copydata = function (val) {
        //debugger
        if (val) {
            $scope.form.EditNow = $scope.ITSetupDataFilter; // some time this is error
            $scope.form.EditNow.EditName = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].UserName;
            $scope.form.EditNow.EditType = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].UserTypeId;
            $scope.form.EditNow.EditWorkLocation = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].WorkLocationId;
            $scope.form.EditNow.EditDeliveryLocation = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].DeliveryLocation;
            $scope.form.EditNow.EditDepartment = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].Department;
            $scope.form.EditNow.EditApplications = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].Applications;
            $scope.form.EditNow.EditSpecial = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].SpecialInstructions;
            $scope.form.EditNow.Editmigrate = $scope.ITSetupDataFilter[$scope.ITSetupDataFilter.length - 1].MigrateInfo;
            //alert("copy...");
        } else {
            $scope.form.EditNow.EditName = '';
            $scope.form.EditNow.EditType = null;
            $scope.form.EditNow.EditWorkLocation = null;
            $scope.form.EditNow.EditDeliveryLocation = '';
            $scope.form.EditNow.EditDepartment = '';
            $scope.form.EditNow.EditApplications = '';
            $scope.form.EditNow.EditSpecial = '';
            $scope.form.EditNow.Editmigrate = null;
            //alert("Clear...");
        }
        
    }
    // click save all btn
    $scope.saveAllData = function () {
        alert("Save all " + $scope.productName + ", please check out dropdown other product");
    }



    //software form here//////////////////////////////////////////////////////
    $scope.submitForm2 = function () {
        //debugger
        var TypeVAL = 1;
        var ITSetUpIdVAL = 0;
        if ($scope.form.EditSoft.SoftwareSetupId != undefined) { // checking for new or not
            ITSetUpIdVAL = $scope.form.EditSoft.SoftwareSetupId;
            TypeVAL = 2;
        }
        var data = {
            SoftwareSetupId: ITSetUpIdVAL,
            strOrderID: $scope.OrderID,
            ProductId: $scope.ProductID,
            Serial: 1,
            UserName: $scope.form.EditSoft.UserName,
            UserEmail: $scope.form.EditSoft.UserEmail,
            Type: TypeVAL,
            isActive: 1
        }
        //alert(JSON.stringify(data));
        //debugger;
        ItsetupFactory.SaveSoftwareSetUpData(data)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    alert('Request has been Updated successfully...');
                    $scope.form.EditSoft = [];
                    $scope.btnHide = true;
                    $scope.btnSoftSaveAll = false;
                    $scope.getSoft();  
                    $scope.event = {
                        type: {
                            checked: false
                        }
                    };// for check box uncheck..
                }

            });


    }

    $scope.BackToPrevious = function () {
        debugger;

        $window.location.href = '/ViewRequest?OrderId=' + encodeURIComponent(decodeURIComponent($scope.OrderID));
    }

    //get Order softwareSetup data by order Id
    $scope.getSoft = function () {
        $scope.SoftwareSetupDataFilter = [];
        ItsetupFactory.GetSoftwareSetUpData($scope.OrderID)
            .then(function (response) {
                if (response.data.length != 0) {
                    //debugger
                    $scope.SoftwareSetupData = response.data[0];
                    //Actual soft length 
                    $scope.AcutalSoftLength = $scope.SoftwareSetupData.length;
                    // filter by ProduID
                    for (var i = 0; i < $scope.SoftwareSetupData.length; i++) {
                        if ($scope.SoftwareSetupData[i].ProductId == $scope.ProductID) {

                            var obj = {};
                            obj.SoftwareSetupId = $scope.SoftwareSetupData[i].SoftwareSetupId;
                            obj.UserEmail = $scope.SoftwareSetupData[i].UserEmail;
                            obj.Serial = $scope.SoftwareSetupData[i].Serial;
                            obj.UserName = $scope.SoftwareSetupData[i].UserName;
                            obj.ProductId = $scope.SoftwareSetupData[i].ProductId;
                            obj.PartNo = $scope.SoftwareSetupData[i].PartNo;

                            $scope.SoftwareSetupDataFilter.push(obj);

                        }
                    }
                    // length of Soft
                    $scope.TotalSoftLength = $scope.SoftwareSetupDataFilter.length;

                }

            });

    }
    //$scope.getSoft();
    $scope.GetSoftEdit = function (item) {
        //alert(JSON.stringify(item));
        //debugger
        $scope.form.EditSoft = item;
        $scope.form.EditSoft.SoftwareSetupId = item.SoftwareSetupId;
        $scope.form.EditSoft.UserName = item.UserName;
        $scope.form.EditSoft.UserEmail = item.UserEmail;

        //btn hide n show
        if ($scope.quantity == $scope.TotalSoftLength) {
            $scope.btnHide = false;
            $scope.btnSoftSaveAll = true;
        }

    }

    $scope.copySoftdata = function (val) {
        //debugger
        if (val) {
            $scope.form.EditSoft = $scope.SoftwareSetupDataFilter; // some time this is error
            $scope.form.EditSoft.UserName = $scope.SoftwareSetupDataFilter[$scope.SoftwareSetupDataFilter.length - 1].UserName;
            $scope.form.EditSoft.UserEmail = $scope.SoftwareSetupDataFilter[$scope.SoftwareSetupDataFilter.length - 1].UserEmail;
            //alert("copy...");
        } else {
            $scope.form.EditSoft.UserName = '';
            $scope.form.EditSoft.UserEmail = '';
            //alert("Clear...");
        }
    }

}]);